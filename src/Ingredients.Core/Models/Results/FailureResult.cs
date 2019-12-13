namespace Ingredients.Core.Models.Results
{
	/// <summary>
	/// Failed operation result.
	/// </summary>
	public class FailureResult : IOperationResult
	{
		/// <inheritdoc />
		public bool Success => false;

		/// <inheritdoc />
		public string Message { get; }

		/// <summary>
		/// Create a new <see cref="FailureResult"/> with the specified message.
		/// </summary>
		/// <param name="message">Result message.</param>
		public FailureResult(string message)
		{
			Message = message;
		}
	}
}