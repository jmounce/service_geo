namespace Geo.Service.Model.Exceptions
{
	using System;
	using System.Net;

	/// <summary>
	/// Standard exception structure used to communicate to the client
	/// </summary>
	public class GeoServiceException
	{
		public GeoServiceException(HttpStatusCode statusCode, string message, Uri url, string stackTrace)
		{
			Url = url;
			StatusCode = statusCode;
			StackTrace = stackTrace;
			Message = message;
		}

		public Uri Url { get; }
		public HttpStatusCode StatusCode { get; }
		public string Message { get; }
		public string StackTrace { get; }
	}
}