
using Bb.Extensions;

namespace Bb.Http
{
    /// <summary>
    /// A context where multiple requests use a common CookieJar.
    /// </summary>
    public class CookieSession : IDisposable
	{
		private readonly string _baseUrl;
		private readonly IUrlClient _client;

		/// <summary>
		/// Creates a new CookieSession where all requests are made off the same base URL.
		/// </summary>
		public CookieSession(string baseUrl = null) {
			_baseUrl = baseUrl;
		}

		/// <summary>
		/// Creates a new CookieSession where all requests are made using the provided IUrlClient
		/// </summary>
		public CookieSession(IUrlClient client) {
			_client = client;
		}

		/// <summary>
		/// The CookieJar used by all requests sent with this CookieSession.
		/// </summary>
		public CookieJar Cookies { get; } = new CookieJar();

		/// <summary>
		/// Creates a new IUrlRequest with this session's CookieJar that can be further built and sent fluently.
		/// </summary>
		/// <param name="urlSegments">The URL or URL segments for the request.</param>
		public IUrlRequest Request(params object[] urlSegments) => (_client == null) ?
			new UrlRequest(_baseUrl, urlSegments).WithCookies(Cookies) :
			new UrlRequest(_client, urlSegments).WithCookies(Cookies);

		/// <summary>
		/// Not necessary to call. IDisposable is implemented mainly for the syntactic sugar of using statements.
		/// </summary>
		public void Dispose() { }
	}
}
