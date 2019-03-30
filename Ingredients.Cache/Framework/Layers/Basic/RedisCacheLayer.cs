using System;
using Ingredients.Cache.Framework.Serialisation;
using StackExchange.Redis;

namespace Ingredients.Cache.Framework.Layers.Basic
{
	/// <inheritdoc />
	/// <summary>
	/// Represents a cache layer which stores objects in Redis
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal class RedisCacheLayer<T> : IBasicCacheLayer<T>
	{
		private readonly IConnectionMultiplexer _connection;
		private readonly int _database;
		private readonly ISerialiser<T> _serializer;

		internal RedisCacheLayer(IConnectionMultiplexer connection, int database = -1, ISerialiser<T> serializer = null)
		{
			_connection = connection;
			_database = database;
			_serializer = serializer;
		}
		
		/// <inheritdoc cref="IBasicCacheLayer{T}"/>
		public bool TryGet(string key, out T value)
		{
			var cachedObject = Database.StringGet(key);
			if (!cachedObject.HasValue)
			{
				value = default(T);
				return false;
			}

			value = _serializer.Deserialise(cachedObject);
			return true;
		}

		/// <inheritdoc cref="IBasicCacheLayer{T}"/>
		public bool TrySet(string key, T value, TimeSpan? expiry = null)
		{
			var objectBytes = _serializer.Serialise(value);

			return Database.StringSet(key, objectBytes, expiry);
		}

		/// <inheritdoc cref="IBasicCacheLayer{T}"/>
		public bool TryRemove(string key)
		{
			return Database.KeyDelete(key);
		}

		private IDatabase Database => _connection.GetDatabase(_database);
	}
}