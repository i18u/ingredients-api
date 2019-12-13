namespace Ingredients.Core.Repositories.Base
{
	/// <summary>
	/// Interface to represent an object that can be uniquely identified by a <typeparamref name="TIdentifier"/> value.
	/// </summary>
	public interface IUnique<TIdentifier>
	{
		/// <summary>
		/// Unique object identifier.
		/// </summary>
		TIdentifier Id { get; }
	}
}