namespace Bb.Http
{
    /// <summary>
    /// Represents an HTTP request. Can be created explicitly via new UrlRequest(), fluently via Url.Request(),
    /// or implicitly when a call is made via methods like Url.GetAsync().
    /// </summary>
    public interface IUrlRequest : IHttpSettingsContainer
	{
		/// <summary>
		/// Gets or sets the IUrlClient to use when sending the request.
		/// </summary>
		IUrlClient Client { get; set; }

		/// <summary>
		/// Gets or sets the HTTP method of the request. Normally you don't need to set this explicitly; it will be set
		/// when you call the sending method, such as GetAsync, PostAsync, etc.
		/// </summary>
		HttpMethod Verb { get; set; }

		/// <summary>
		/// Gets or sets the URL to be called.
		/// </summary>
		Url Url { get; set; }

		/// <summary>
		/// The body content of this request.
		/// </summary>
		HttpContent Content { get; set; }

        /// <summary>
        /// If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>
        /// </summary>
        bool EnsureSuccessStatusCode { get; set; }


        Version Version { get; set; }

		/// <summary>
		/// Gets Name/Value pairs parsed from the Cookie request header.
		/// </summary>
		IEnumerable<(string Name, string Value)> Cookies { get; }

		/// <summary>
		/// Gets or sets the collection of HTTP cookies that can be shared between multiple requests. When set, values that
		/// should be sent with this request (based on Domain, Path, and other rules) are immediately copied to the Cookie
		/// request header, and any Set-Cookie headers received in the response will be written to the CookieJar.
		/// </summary>
		CookieJar CookieJar { get; set; }

		/// <summary>
		/// The UrlCall that received a 3xx response and automatically triggered this request.
		/// </summary>
		UrlCall RedirectedFrom { get; set; }


        /// <summary>
        /// Asynchronously sends the HTTP request. Mainly used to implement higher-level extension methods (GetJsonAsync, etc).
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        Task<IUrlResponse> SendAsync(HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously sends the HTTP request. Mainly used to implement higher-level extension methods (GetJsonAsync, etc).
        /// </summary>
        /// <param name="verb">The HTTP method used to make the request.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        Task<IUrlResponse> SendAsync(HttpMethod verb, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously sends the HTTP request. Mainly used to implement higher-level extension methods (GetJsonAsync, etc).
        /// </summary>
        /// <param name="verb">The HTTP method used to make the request.</param>
        /// <param name="content">Contents of the request body.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        Task<IUrlResponse> SendAsync(HttpMethod verb, HttpContent? content = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default);

    }

}