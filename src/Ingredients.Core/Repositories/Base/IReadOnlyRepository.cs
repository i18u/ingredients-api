using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Ingredients.Core.Repositories.Base
{
	/// <summary>
	/// Interface for a read-only data repository.
	/// </summary>
	/// <typeparam name="TData">Type of data stored in this repository.</typeparam>
	/// <typeparam name="TIdentifier">Type of unique identifier for objects.</typeparam>
	public interface IReadOnlyRepository<TData, TIdentifier> where TData : IUnique<TIdentifier>
	{
		/// <summary>
		/// Get a single <typeparamref name="TData"/> object matching the specified unique identifier.
		/// </summary>
		/// <param name="id">Unique identifier to match.</param>
		/// <returns>Single <typeparamref name="TData"/> object.</returns>
		TData Get(TIdentifier id);

		/// <summary>
		/// Get one or more <typeparamref name="TData"/> objects matching the specified filter.
		/// </summary>
		/// <param name="filterExpression">Filter to match.</param>
		/// <returns>Collection of <typeparamref name="TData"/> objects.</returns>
		IEnumerable<TData> Get(Expression<Func<TData, bool>> filterExpression);
	}
}