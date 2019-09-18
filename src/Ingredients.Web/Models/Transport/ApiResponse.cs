using System.Net;
using System.Runtime.Serialization;

namespace Ingredients.Web.Models.Transport
{
	[DataContract]
	public class ApiResponse<T>
	{
		[DataMember(Name = "status")]
		public int Status { get; private set; }

		[DataMember(Name = "data", EmitDefaultValue = false)]
		public T Data { get; private set; }

		[DataMember(Name = "message", EmitDefaultValue = false)]
		public string Message { get; private set; }

		public static ApiResponse<T> WithStatus(int status)
		{
			var apiResponse = new ApiResponse<T>();
			apiResponse.Status = status;

			return apiResponse;
		}

		public static ApiResponse<T> WithStatus(HttpStatusCode status)
		{
			return WithStatus((int)status);
		}

		public ApiResponse<T> WithMessage(string message)
		{
			Message = message;

			return this;
		}

		public ApiResponse<T> WithData(T data)
		{
			Data = data;

			return this;
		}
	}
}
