using System.Net;
using System.Runtime.Serialization;

namespace Ingredients.Web.Models.Transport
{
	/// <summary>
	/// Object ot represent a response from the API.
	/// </summary>
	/// <typeparam name="TData">Type of data in this response.</typeparam>
	[DataContract]
	public class ApiResponse<TData>
	{
		/// <summary>
		/// The status for this response.
		/// </summary>
		[DataMember(Name = "status")]
		public int Status { get; private set; }

		/// <summary>
		/// The data for this response.
		/// </summary>
		[DataMember(Name = "data", EmitDefaultValue = false)]
		public TData Data { get; private set; }

		/// <summary>
		/// Any informational message for this response.
		/// </summary>
		[DataMember(Name = "message", EmitDefaultValue = false)]
		public string Message { get; private set; }

		/// <summary>
		/// Specify a status for this response as an integer.
		/// </summary>
		/// <param name="status">Status to use.</param>
		/// <returns>This <see cref="ApiResponse{TData}"/> object.</returns>
		public static ApiResponse<TData> WithStatus(int status)
		{
			var apiResponse = new ApiResponse<TData>();
			apiResponse.Status = status;

			return apiResponse;
		}

		/// <summary>
		/// Specify a status for this response as a <see cref="HttpStatusCode"/>.
		/// </summary>
		/// <param name="status">Status to use.</param>
		/// <returns>This <see cref="ApiResponse{TData}"/> object.</returns>
		public static ApiResponse<TData> WithStatus(HttpStatusCode status)
		{
			return WithStatus((int)status);
		}

		/// <summary>
		/// Specify a message for this response.
		/// </summary>
		/// <param name="message">Message to use.</param>
		/// <returns>This <see cref="ApiResponse{TData}"/> object.</returns>
		public ApiResponse<TData> WithMessage(string message)
		{
			Message = message;

			return this;
		}

		/// <summary>
		/// Specify data for this response.
		/// </summary>
		/// <param name="data">Data of type <typeparamref name="TData"/> to use.</param>
		/// <returns>This <see cref="ApiResponse{TData}"/> object.</returns>
		public ApiResponse<TData> WithData(TData data)
		{
			Data = data;

			return this;
		}
	}
}
