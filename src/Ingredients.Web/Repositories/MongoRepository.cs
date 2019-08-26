using System;
using Ingredients.Web.Models.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Ingredients.Web.Repositories
{
	/// <summary>
	/// Abstract repository pattern for MongoDB for retrieval and modification of <typeparamref name="TModel"/> objects.
	/// </summary>
	/// <typeparam name="TModel">Type of object in this repository.</typeparam>
	public class MongoRepository<TModel>
		: ReadOnlyMongoRepository<TModel>, IRepository<TModel> where TModel : IMongoModel
	{
		/// <summary>
		/// Create a new <see cref="MongoRepository{TModel}"/> backed
		/// with the specified <see cref="IMongoCollection{TModel}"/>.
		/// </summary>
		/// <param name="collection">Backing <see cref="IMongoCollection{TModel}"/> to use.</param>
		protected MongoRepository(IMongoCollection<TModel> collection) : base(collection)
		{
		}

		/// <inheritdoc />
		public ObjectId Upsert(TModel model)
		{
			if (model == null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			var filter = Builders<TModel>.Filter.Eq(document => document.Id, model.Id);

			var upsertResult = Collection.ReplaceOne(filter, model, new UpdateOptions
			{
				IsUpsert = true
			});

			return upsertResult.UpsertedId?.AsObjectId ?? model.Id;
		}
	}
}
