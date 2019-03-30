using System;
using System.Runtime.Caching;

namespace Ingredients.Cache.Framework.Layers.Basic
{
	/// <inheritdoc />
	/// <summary>
	/// Represents a caching layer which stores objects in memory
	/// </summary>
	/// <typeparam name="T">The object type which this cache layer handles</typeparam>
	public class MemoryCacheLayer<T> : IBasicCacheLayer<T>
	{
		private readonly MemoryCache _cache = MemoryCache.Default;

		/// <inheritdoc cref="IBasicCacheLayer{T}"/>
		public virtual bool TryGet(string key, out T value)
		{
			var cachedObject = _cache.Get(key);
			if (cachedObject is T typedObject)
			{
				value = typedObject;
				return true;
			}

			value = default(T);
			return false;
		} 

		/// <inheritdoc cref="IBasicCacheLayer{T}"/>
		public virtual bool TrySet(string key, T value, TimeSpan? expiry = null)
		{
			// Set a sliding expiration, if requested
			CacheItemPolicy policy = null;
			if (expiry.HasValue)
				policy = new CacheItemPolicy { SlidingExpiration = expiry.Value };

			_cache.Set(key, value, policy);
			return true;
		}

		/// <inheritdoc cref="IBasicCacheLayer{T}"/>
		public virtual bool TryRemove(string key)
		{
			_cache.Remove(key);
			return true;
		}
	}
}