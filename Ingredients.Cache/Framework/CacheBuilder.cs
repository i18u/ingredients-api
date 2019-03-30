using Ingredients.Cache.Framework.Layers.Basic;
using Ingredients.Cache.Framework.Managers;

namespace Ingredients.Cache.Framework
{
	/// <summary>
	/// Provides a fluent API in which to construct a series of Cache providers with appropriate layers
	/// </summary>
	public static class CacheBuilder
	{
		/// <summary>
		/// Adds the provided layer of caching to the <see cref="BasicCacheManager{T}"/> provided
		/// </summary>
		/// <param name="cacheManager">An existing cache</param>
		/// <param name="layer">The layer to add to the existing cache</param>
		/// <typeparam name="T">The object type which this cache handles</typeparam>
		/// <returns>The existing cache, with the layer added</returns>
		public static BasicCacheManager<T> WithLayer<T>(this BasicCacheManager<T> cacheManager, IBasicCacheLayer<T> layer)
		{
			cacheManager.AddLayer(layer);
			return cacheManager;
		}
	}
}