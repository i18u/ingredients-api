using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ingredients.Core.Models.Results;
using Ingredients.Core.Repositories.Base;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Ingredients.Core.Repositories.Impl
{
	/// <summary>
	/// <see cref="IRepository{TData,ObjectId}"/> implementation for MongoDB.
	/// </summary>
	/// <typeparam name="TData">Type of data stored in this repository.</typeparam>
	public class MongoRepository<TData> : IRepository<TData, ObjectId>
		where TData : IUnique<ObjectId>
	{
		/// <summary>
		/// The backing <see cref="IMongoCollection{TData}"/> implementation for this repository.
		/// </summary>
		private readonly IMongoCollection<TData> _backingCollection;

		/// <summary>
		/// Create a new <see cref="MongoRepository{TData}"/> instance with the specified backing collection.
		/// </summary>
		/// <param name="backingCollection">Backing <see cref="IMongoCollection{TData}"/> implementation.</param>
		public MongoRepository(IMongoCollection<TData> backingCollection)
		{
			_backingCollection = backingCollection;
		}

		/// <inheritdoc />
		public TData Get(ObjectId id)
		{
			var filter = Builders<TData>.Filter.Eq(data => data.Id, id);

			// .FirstOrDefault() will implicitly ask for the cursor's next result.
			return _backingCollection.Find(filter).FirstOrDefault();
		}

		/// <inheritdoc />
		public IEnumerable<TData> Get(Expression<Func<TData, bool>> filterExpression)
		{
			var filter = Builders<TData>.Filter.Where(filterExpression);

			return _backingCollection.Find(filter).ToEnumerable();
		}

		/// <inheritdoc />
		public IOperationResult Create(TData data)
		{
			try
			{
				_backingCollection.InsertOne(data);
				return new SuccessResult($"Created '{typeof(TData)}' id '{data.Id}'.");
			}
			catch (MongoException ex)
			{
				return new FailureResult($"Could not create '{typeof(TData)}' - {ex}");
			}
		}
	}
}