using System;
using System.Threading.Tasks;
using CachingFramework.Redis;
using Ingredients.Web.Repositories;
using StackExchange.Redis;

namespace Ingredients.Web.Cache
{
	public abstract class Cache<TCacheModel, TDatabaseModel, TId> 
		: ICache<TCacheModel, TDatabaseModel, TId>, IAsyncCache<TCacheModel, TDatabaseModel, TId>
		where TCacheModel : ICacheModel<TDatabaseModel>, new()
	{
		private readonly RedisContext _cache;
		protected readonly IRepository<TDatabaseModel, TId> Repository;
		private readonly string _cacheKey;

		protected Cache(IRepository<TDatabaseModel, TId> repository, string cacheKey)
		{
			Repository = repository;
			_cacheKey = cacheKey;
			_cache = new RedisContext();
		}

		public TCacheModel GetOne(TId key, Func<TDatabaseModel> cacheMissFunc)
		{
			return GetOneAsync(key, () => Task.FromResult(cacheMissFunc())).GetAwaiter().GetResult();
		}

		public Task<TCacheModel> GetOneAsync(TId key, Func<Task<TDatabaseModel>> cacheMissFunc)
		{
			var stringKey = GetCacheKey(key);
			
			return _cache.Cache.FetchObjectAsync(stringKey, async () =>
			{
				var databaseModel = await cacheMissFunc();
				return TransformDatabaseToCacheModel(databaseModel);
			});
		}

		public void SetOne(TId key, TDatabaseModel repoModel, TimeSpan? expiry = null)
		{
			SetOneAsync(key, repoModel, expiry).GetAwaiter().GetResult();
		}

		public Task SetOneAsync(TId key, TDatabaseModel repoModel, TimeSpan? expiry = null)
		{
			var stringKey = GetCacheKey(key);
			var cacheModel = TransformDatabaseToCacheModel(repoModel);
			
			return _cache.Cache.SetObjectAsync(stringKey, cacheModel, expiry);
		}

		public void AddTags(TId key, string[] tags)
		{
			AddTagsAsync(key, tags).GetAwaiter().GetResult();
		}

		public Task AddTagsAsync(TId key, string[] tags)
		{
			var stringKey = GetCacheKey(key);
			
			return _cache.Cache.AddTagsToKeyAsync(stringKey, tags);
		}

		public void RemoveTags(TId key, string[] tags)
		{			
			RemoveTagsAsync(key, tags).GetAwaiter().GetResult();
		}

		public Task RemoveTagsAsync(TId key, string[] tags)
		{
			var stringKey = GetCacheKey(key);
			
			return _cache.Cache.RemoveTagsFromKeyAsync(stringKey, tags);
		}

		private string GetCacheKey(TId key)
		{
			return $"{_cacheKey}_{key}";
		}

		private static TCacheModel TransformDatabaseToCacheModel(TDatabaseModel repoModel)
		{
			var cacheModel = new TCacheModel();
			cacheModel.SetFieldsFromDatabaseModel(repoModel);
			return cacheModel;
		}
	}
}