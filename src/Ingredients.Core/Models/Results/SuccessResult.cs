namespace Ingredients.Core.Models.Results
{
	/// <summary>
	/// Successful operation result.
	/// </summary>
	public class SuccessResult : IOperationResult
	{
		/// <inheritdoc />
		public bool Success => true;

		/// <inheritdoc />
		public string Message { get; }

		/// <summary>
		/// Create a new <see cref="SuccessResult"/> with the specified message.
		/// </summary>
		/// <param name="message">Result message.</param>
		public SuccessResult(string message)
		{
			Message = message;
		}
	}
}