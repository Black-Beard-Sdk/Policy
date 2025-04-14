
using System.Diagnostics;

namespace Bb.Http
{

    /// <summary>
    /// Encapsulates request, response, and other details associated with an HTTP call. Useful for diagnostics and available in
    /// global event handlers and UrlHttpException.Call.
    /// </summary>
    public class UrlCall
	{

        public UrlCall()
        {
			this._time = new Stopwatch();
        }

        /// <summary>
        /// The IUrlRequest associated with this call.
        /// </summary>
        public IUrlRequest Request { get; set; }

		/// <summary>
		/// The raw HttpRequestMessage associated with this call.
		/// </summary>
		public HttpRequestMessage HttpRequestMessage { get; set; }

		/// <summary>
		/// Captured request body. Available ONLY if HttpRequestMessage.Content is a Url.Http.Content.CapturedStringContent.
		/// </summary>
		public string? RequestBody => (HttpRequestMessage.Content as StringContent)?.ToString(); // todo : Fix this

		/// <summary>
		/// The IUrlResponse associated with this call if the call completed, otherwise null.
		/// </summary>
		public IUrlResponse Response { get; set; }

		/// <summary>
		/// If this call has a 3xx response and Location header, contains information about how to handle the redirect.
		/// Otherwise null.
		/// </summary>
		public UrlRedirect Redirect { get; set; }

		/// <summary>
		/// The raw HttpResponseMessage associated with the call if the call completed, otherwise null.
		/// </summary>
		public HttpResponseMessage HttpResponseMessage { get; set; }

		/// <summary>
		/// Exception that occurred while sending the HttpRequestMessage.
		/// </summary>
		public Exception Exception { get; set; }
	
		/// <summary>
		/// User code should set this to true inside global event handlers (OnError, etc) to indicate
		/// that the exception was handled and should not be propagated further.
		/// </summary>
		public bool ExceptionHandled { get; set; }

		/// <summary>
		/// DateTime the moment the request was sent.
		/// </summary>
		public DateTime StartedUtc { get; private set; }

		/// <summary>
		/// Elapsed time
		/// </summary>
        public TimeSpan Time => _time.Elapsed;

        /// <summary>
        /// True if a response was received, regardless of whether it is an error status.
        /// </summary>
        public bool Completed => HttpResponseMessage != null;

		/// <summary>
		/// True if response was received with any success status or a match with AllowedHttpStatusRange setting.
		/// </summary>
		public bool Succeeded =>
			HttpResponseMessage == null ? false :
			(int)HttpResponseMessage.StatusCode < 400 ? true :
			string.IsNullOrEmpty(Request?.Settings?.AllowedHttpStatusRange) ? false :
			HttpStatusRangeParser.IsMatch(Request.Settings.AllowedHttpStatusRange, HttpResponseMessage.StatusCode);


        /// <summary>
        /// Returns the verb and absolute URI associated with this call.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
			return $"{HttpRequestMessage.Method:U} {Request.Url}";
		}



        /// <summary>
        /// Total duration of the call if it completed, otherwise null.
        /// </summary>
        private Stopwatch _time;


        internal void Start()
        {
            StartedUtc = DateTime.UtcNow;
			_time.Start();
        }

        internal void Stop()
        {
			if (_time.IsRunning)
	            _time.Stop();
        }


    }
}
