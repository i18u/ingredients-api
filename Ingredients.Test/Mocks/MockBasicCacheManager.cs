using System;
using System.Collections.Generic;
using Ingredients.Cache.Framework;
using Ingredients.Cache.Framework.Managers;

namespace Ingredients.Test.Mocks
{
	internal class MockBasicCacheManager<T>
	{
		private readonly Dictionary<string, T> _dbValues;
		private BasicCacheManager<T> _cache;
		
		internal EventHandler<EventArgs> LayerGetCalled;
		internal EventHandler<EventArgs> LayerSetCalled;
		internal EventHandler<EventArgs> LayerRemoveCalled;
		internal EventHandler<EventArgs> DbGetCalled;
			
		internal MockBasicCacheManager(Dictionary<string, T> dbValues)
		{
			_dbValues = dbValues;

			// Create the mock layer and bind all events to this cache manager (bubble them up)
			var layer = new MockMemoryCacheLayer<T>();
			layer.LayerGetCalled += (sender, args) => LayerGetCalled?.Invoke(sender, args);
			layer.LayerSetCalled += (sender, args) => LayerSetCalled?.Invoke(sender, args);
			layer.LayerRemoveCalled += (sender, args) => LayerRemoveCalled?.Invoke(sender, args);
			
			_cache = new BasicCacheManager<T>(MockDbGet)
				.WithLayer(layer);
		}

		internal T Get(string key)
		{
			return _cache.Get(key);
		}

		internal void Set(string key, T value, TimeSpan? expiry = null)
		{
			_cache.Set(key, value, expiry);
		}

		internal void Remove(string key)
		{
			_cache.Remove(key);
		}
		
		private T MockDbGet(string key)
		{
			DbGetCalled?.Invoke(this, EventArgs.Empty);

			return _dbValues.TryGetValue(key, out var value)
				? value
				: default(T);
		}
	}
}