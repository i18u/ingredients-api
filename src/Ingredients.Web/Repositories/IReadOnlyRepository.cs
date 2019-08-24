using System;
using System.Collections.Generic;
using Ingredients.Web.Models.Database;
using MongoDB.Bson;

namespace Ingredients.Web.Repositories
{
	/// <summary>
	/// Interface to define a repository storing <typeparamref name="TModel"/> objects.
	/// </summary>
	/// <typeparam name="TModel">Type of object stored in this repository.</typeparam>
	public interface IReadOnlyRepository<TModel> where TModel : IMongoModel
	{
		/// <summary>
		/// Retrieve a single <typeparamref name="TModel"/> instance from
		/// the database using a unique <see cref="ObjectId"/>.
		/// </summary>
		/// <returns>Single <typeparamref name="TModel"/> instance.</returns>
		TModel Get(ObjectId id);

		/// <summary>
		/// Retrieve a set of <typeparamref name="TModel"/> instances from the database
		/// using pagination parameters <paramref name="page"/> and <paramref name="limit"/>.
		/// </summary>
		/// <param name="page">Page number.</param>
		/// <param name="limit">Number of results to return.</param>
		/// <returns>Set of <typeparamref name="TModel"/> objects for the specified page.</returns>
		IEnumerable<TModel> Get(int page, int limit);
	}
}
