using System;

namespace Ingredients.Cache.Framework.Layers.Basic 
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a simple key-value layer of cache
    /// </summary>
    /// <typeparam name="T">The object type which this cache layer handles</typeparam>
    public interface IBasicCacheLayer<T> : ICacheLayer<T>
    {
        /// <summary>
        /// Retrieves a value from cache at the given key
        /// </summary>
        /// <param name="key">The key to retrieve from</param>
        /// <param name="value">The object stored at the given key, if one exists, else default for the <see cref="T"/> type</param>
        /// <returns>True if the get operation yielded a successful object from cache; false otherwise</returns>
        bool TryGet(string key, out T value);

        /// <summary>
        /// Upserts a value into cache in the given key, with an optional expiry
        /// </summary>
        /// <param name="key">The key to upsert into</param>
        /// <param name="value">The value to upsert</param>
        /// <param name="expiry">The optional expiry to apply to the cache item</param>
        /// <returns>True if the set operation succeeded; false otherwise</returns>
        bool TrySet(string key, T value, TimeSpan? expiry = null);

        /// <summary>
        /// Removes any cached value at the given key 
        /// </summary>
        /// <param name="key">The key to remove</param>
        /// <returns>True if the remove operation succeeded; false otherwise</returns>
        bool TryRemove(string key);
    }
}