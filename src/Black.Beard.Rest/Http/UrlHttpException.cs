
namespace Bb.Http
{
    /// <summary>
    /// An exception that is thrown when an HTTP call made by Url.Http fails, including when the response
    /// indicates an unsuccessful HTTP status code.
    /// </summary>
    public class UrlHttpException : Exception
	{
		/// <summary>
		/// An object containing details about the failed HTTP call
		/// </summary>
		public UrlCall Call { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="UrlHttpException"/> class.
		/// </summary>
		/// <param name="call">The call.</param>
		/// <param name="message">The message.</param>
		/// <param name="inner">The inner.</param>
		public UrlHttpException(UrlCall call, string message, Exception? inner) : base(message, inner) {
			Call = call;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UrlHttpException"/> class.
		/// </summary>
		/// <param name="call">The call.</param>
		/// <param name="inner">The inner.</param>
		public UrlHttpException(UrlCall call, Exception? inner) : this(call, BuildMessage(call, inner), inner) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="UrlHttpException"/> class.
		/// </summary>
		/// <param name="call">The call.</param>
		public UrlHttpException(UrlCall call) : this(call, BuildMessage(call, null), null) { }

		private static string BuildMessage(UrlCall call, Exception? inner) {
			if (call?.Response != null && !call.Succeeded)
				return $"Call failed with status code {call.Response.StatusCode} ({call.HttpResponseMessage.ReasonPhrase}): {call}";

			var msg = "Call failed";
			if (inner != null) 
				msg += ". " + inner.Message.TrimEnd('.');

			return msg + ((call == null) ? "." : $": {call}");

		}

		/// <summary>
		/// Gets the HTTP status code of the response if a response was received, otherwise null.
		/// </summary>
		public int? StatusCode => Call?.Response?.StatusCode;

		/// <summary>
		/// Gets the response body of the failed call.
		/// </summary>
		/// <returns>A task whose result is the string contents of the response body.</returns>
		public Task<string> GetResponseStringAsync() => Call?.Response?.GetStringAsync() ?? Task.FromResult((string)null);

		/// <summary>
		/// Deserializes the JSON response body to an object of the given type.
		/// </summary>
		/// <typeparam name="T">A type whose structure matches the expected JSON response.</typeparam>
		/// <returns>A task whose result is an object containing data in the response body.</returns>
		public Task<T> GetResponseJsonAsync<T>() => Call?.Response?.GetObjectAsync<T>() ?? Task.FromResult(default(T));
	}

	/// <summary>
	/// An exception that is thrown when an HTTP call made by Url.Http times out.
	/// </summary>
	public class UrlHttpTimeoutException : UrlHttpException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UrlHttpTimeoutException"/> class.
		/// </summary>
		/// <param name="call">Details of the HTTP call that caused the exception.</param>
		/// <param name="inner">The inner exception.</param>
		public UrlHttpTimeoutException(UrlCall call, Exception inner) : base(call, BuildMessage(call), inner) { }

		private static string BuildMessage(UrlCall call) =>
			(call == null) ? "Call timed out." :  $"Call timed out: {call}";
	}
}