namespace Ingredients.Core.Models.Results
{
	/// <summary>
	/// The result of some operation.
	/// </summary>
	public interface IOperationResult
	{
		/// <summary>
		/// Whether or not the operation was successful.
		/// </summary>
		bool Success { get; }

		/// <summary>
		/// Message for this result.
		/// </summary>
		string Message { get; }
	}
}