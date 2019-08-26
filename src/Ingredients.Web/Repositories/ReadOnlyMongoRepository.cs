using System.Collections.Generic;
using Ingredients.Web.Models.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Ingredients.Web.Repositories
{
	/// <summary>
	/// Abstract repository pattern for MongoDB for retrieval of <typeparamref name="TModel"/> objects.
	/// </summary>
	/// <typeparam name="TModel">Type of object in this repository.</typeparam>
	public class ReadOnlyMongoRepository<TModel> : IReadOnlyRepository<TModel> where TModel : IMongoModel
	{
		/// <summary>
		/// The backing <see cref="IMongoCollection{TModel}"/> for this <see cref="MongoRepository{TModel}"/>.
		/// </summary>
		protected IMongoCollection<TModel> Collection { get; }

		/// <summary>
		/// Create a new <see cref="ReadOnlyMongoRepository{TModel}"/> backed
		/// with the specified <see cref="IMongoCollection{TModel}"/>.
		/// </summary>
		/// <param name="collection">Backing <see cref="IMongoCollection{TModel}"/> to use.</param>
		protected ReadOnlyMongoRepository(IMongoCollection<TModel> collection)
		{
			Collection = collection;
		}

		/// <inheritdoc />
		public TModel Get(ObjectId id)
		{
			var filter = Builders<TModel>.Filter.Eq(document => document.Id, id);

			return Collection.Find(filter).FirstOrDefault();
		}

		/// <inheritdoc />
		public IEnumerable<TModel> Get(int page, int limit)
		{
			if (page < 1 || limit < 1 || limit > 250)
			{
				return new List<TModel>();
			}

			var cursor = Collection
				.Find(FilterDefinition<TModel>.Empty)
				.Limit(limit);

			if (page > 1)
			{
				var totalSkipped = page * limit;

				cursor = cursor.Skip(totalSkipped);
			}

			return cursor.ToEnumerable();
		}
	}
}
