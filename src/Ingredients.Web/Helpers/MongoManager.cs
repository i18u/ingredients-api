using System;
using MongoDB.Driver;

namespace Ingredients.Web.Helpers
{
	public class MongoManager
	{
		private static readonly Lazy<IMongoClient> _connection = new Lazy<IMongoClient>(CreateClient);

		public static IMongoClient Connection => _connection.Value;

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
