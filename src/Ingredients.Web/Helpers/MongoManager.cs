using System;
using MongoDB.Driver;

namespace Ingredients.Web.Helpers
{
	/// <summary>
	/// Helper class for MongoDB integration
	/// </summary>
	public static class MongoManager
	{
		/// <summary>
		/// Lazy singleton
		/// </summary>
		private static readonly Lazy<IMongoClient> _connection = new Lazy<IMongoClient>(CreateClient);

		/// <summary>
		/// Lazy singleton instance
		/// </summary>
		public static IMongoClient Connection => _connection.Value;

		/// <summary>
		/// Create a new <see cref="IMongoClient"/> instance
		/// </summary>
		/// <returns>New <see cref="IMongoClient"/> instance</returns>
		private static IMongoClient CreateClient()
		{
			var mongoAddress = Environment.GetEnvironmentVariable("COOKBOOK_MONGO_ADDRESS") ?? "127.0.0.1:27017";

			return new MongoClient(new MongoClientSettings
			{
				Server = MongoServerAddress.Parse(mongoAddress)
			});
		}
	}
}
