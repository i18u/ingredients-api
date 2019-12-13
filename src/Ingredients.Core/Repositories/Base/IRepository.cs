using System.Runtime.InteropServices.ComTypes;
using Ingredients.Core.Models.Results;

namespace Ingredients.Core.Repositories.Base
{
	/// <summary>
	/// Interface to represent a repository supporting both read and write operations.
	/// </summary>
	/// <typeparam name="TData">Type of data stored in this repository.</typeparam>
	/// <typeparam name="TIdentifier">Type of unique identifier for objects.</typeparam>
	public interface IRepository<TData, TIdentifier> : IReadOnlyRepository<TData, TIdentifier>
		where TData : IUnique<TIdentifier>
	{
		/// <summary>
		/// Create a <see cref="TData"/> object in this repository.
		/// </summary>
		/// <param name="data">Data object to store.</param>
		/// <returns>Create <see cref="IOperationResult"/> object.</returns>
		IOperationResult Create(TData data);
	}
}