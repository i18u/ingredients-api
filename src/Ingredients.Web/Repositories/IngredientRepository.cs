using Ingredients.Web.Models.Database;
using MongoDB.Driver;

namespace Ingredients.Web.Repositories
{
	public class IngredientRepository : MongoRepository<Ingredient>
	{
		private const string DatabaseKey = "cookbook";

		private const string CollectionKey = "ingredients";

		public IngredientRepository(IMongoClient client)
			: base(client.GetDatabase(DatabaseKey).GetCollection<Ingredient>(CollectionKey))
		{
		}
	}
}
