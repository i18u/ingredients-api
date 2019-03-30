using System;
using System.Collections.Generic;
using System.Linq;
using Ingredients.Cache.Framework.Layers.Basic;

namespace Ingredients.Cache.Framework.Managers
{
	/// <inheritdoc />
	/// <summary>
	/// Provides a basic key-value cache for objects
	/// </summary>
	/// <typeparam name="T">The object type which this cache handles</typeparam>
	public class BasicCacheManager<T> : CacheManager<IBasicCacheLayer<T>, T>
	{
		private readonly Func<string, T> _dbGetFunc;
		public BasicCacheManager(Func<string, T> dbGetFunc)
		{
			_dbGetFunc = dbGetFunc;
		}
		
		/// <summary>
		/// Retrieves a value from cache at the given key
		/// </summary>
		/// <param name="key">The key to retrieve from</param>
		/// <returns>The object which existed in cache, if there was one; the default of <see cref="T"/> type, if not</returns>
		public T Get(string key)
		{
			// Iterate the caching layers to find the value, if it exists
			var layerIdx = GetValueFromLayers(Layers, key, out var objectModel);

			// If we didn't find the object in the cache layers, query the data source
			if (layerIdx == -1)
			{
				objectModel = _dbGetFunc(key);
			}
			
			// Back-fill layers we missed the object in
			var layersToFill = Layers.Take(layerIdx).ToArray();
			SetValuesInLayers(layersToFill, key, objectModel, DefaultExpiry);

			return objectModel;
		}

		/// <summary>
		/// Upserts a value into cache in the given key, with an optional expiry
		/// </summary>
		/// <param name="key">The key to upsert into</param>
		/// <param name="value">The value to upsert</param>
		/// <param name="expiry">The optional expiry to apply to the cache item</param>
		public void Set(string key, T value, TimeSpan? expiry = null)
		{
			// Set the value in all of the cache layers
			SetValuesInLayers(Layers, key, value, DefaultExpiry);
		}
		
		/// <summary>
		/// Removes any cached value at the given key 
		/// </summary>
		/// <param name="key">The key to remove</param>
		/// <returns>True if the remove operation succeeded; false otherwise</returns>
		public void Remove(string key)
		{
			// Remove the value from all of the cache layers
			RemoveValuesFromLayers(Layers, key);
		}

		private static int GetValueFromLayers(IEnumerable<IBasicCacheLayer<T>> layers, string key, out T objectModel)
		{
			var layerIdx = -1;
			foreach (var layer in layers)
			{
				layerIdx++;
				
				// We have not found the object in the cache layer, try the next
				if (!layer.TryGet(key, out objectModel))
				{
					// TODO: Log this
					continue;
				}

				// We have found the object in a cache layer
				// Return the index of the cache layer we found it in
				return layerIdx;
			}

			objectModel = default(T);
			return -1;
		}

		private static void SetValuesInLayers(IEnumerable<IBasicCacheLayer<T>> layers, string key, T value, TimeSpan? expiry = null)
		{
			foreach (var layer in layers)
			{
				if (!layer.TrySet(key, value, expiry))
				{
					// TODO: Log this
				}
			}
		}

		private static void RemoveValuesFromLayers(IEnumerable<IBasicCacheLayer<T>> layers, string key)
		{
			foreach (var layer in layers)
			{
				if (!layer.TryRemove(key))
				{
					// TODO: Log this
				}
			}
		}
	}
}