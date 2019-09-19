using Ingredients.Web.Models.Database;
using MongoDB.Driver;

namespace Ingredients.Web.Repositories
{
	/// <summary>
	/// Repository for retrieval of <see cref="Ingredient"/> objects.
	/// </summary>
	public class IngredientRepository : MongoRepository<Ingredient>
	{
		/// <summary>
		/// Repository MongoDB database key.
		/// </summary>
		private const string DatabaseKey = "cookbook";

		/// <summary>
		/// Repository MongoDB collection key.
		/// </summary>
		private const string CollectionKey = "ingredients";

		/// <summary>
		/// Create a new <see cref="IngredientRepository"/> with the specified <see cref="IMongoClient"/>.
		/// </summary>
		/// <param name="client"><see cref="IMongoClient"/> implementation to use.</param>
		public IngredientRepository(IMongoClient client)
			: base(client.GetDatabase(DatabaseKey).GetCollection<Ingredient>(CollectionKey))
		{
		}
	}
}
