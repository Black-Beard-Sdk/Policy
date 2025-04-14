using RestSharp;

namespace Bb.Http
{
    /// <summary>
    /// Interface defining UrlClient's contract (useful for mocking and DI)
    /// </summary>
    public interface IUrlClient : IHttpSettingsContainer, IDisposable
    {
        /// <summary>
        /// Gets the RestClient to be used in subsequent HTTP calls. Creation (when necessary) is delegated
        /// to UrlHttp.UrlClientFactory. Reused for the life of the UrlClient.
        /// </summary>
        RestClient Client { get; }

        /// <summary>
        /// Gets or sets the base URL used for all calls made with this client.
        /// </summary>
        string BaseUrl { get; set; }

        /// <summary>
        /// Creates a new IUrlRequest that can be further built and sent fluently.
        /// </summary>
        /// <param name="urlSegments">The URL or URL segments for the request. If BaseUrl is defined, it is assumed that these are path segments off that base.</param>
        /// <returns>A new IUrlRequest</returns>
        IUrlRequest Request(params object[] urlSegments);

        /// <summary>
        /// Gets a value indicating whether this instance (and its underlying RestClient) has been disposed.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Asynchronously sends an HTTP request.
        /// </summary>
        /// <param name="request">The IUrlRequest to send.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        Task<IUrlResponse> SendAsync(IUrlRequest request, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default);

       
    }
}