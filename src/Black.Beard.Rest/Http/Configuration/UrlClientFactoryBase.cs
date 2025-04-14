using RestSharp;
using System.Collections.Concurrent;
using System.Net;

namespace Bb.Http.Configuration
{
    /// <summary>
    /// Encapsulates a creation/caching strategy for IUrlClient instances. Custom factories looking to extend
    /// Url's behavior should inherit from this class, rather than implementing IUrlClientFactory directly.
    /// </summary>
    public abstract class UrlClientFactoryBase : IUrlClientFactory
    {
        private readonly ConcurrentDictionary<string, IUrlClient> _clients = new ConcurrentDictionary<string, IUrlClient>();

        /// <summary>
        /// By default, uses a caching strategy of one UrlClient per host. This maximizes reuse of
        /// underlying RestClient/Handler while allowing things like cookies to be host-specific.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The UrlClient instance.</returns>
        public virtual IUrlClient Get(Url url)
        {

            if (url == null)
                throw new ArgumentNullException(nameof(url));

            return _clients.AddOrUpdate(
                GetCacheKey(url),
                u => Create(u),
                (u, client) => client.IsDisposed ? Create(u) : client);
        }

        /// <summary>
        /// Defines a strategy for getting a cache key based on a Url. Default implementation
        /// returns the host part (i.e www.api.com) so that all calls to the same host use the
        /// same UrlClient (and RestClient/HttpMessageHandler) instance.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The cache key</returns>
        protected abstract string GetCacheKey(Url url);

        /// <summary>
        /// Creates a new UrlClient
        /// </summary>
        /// <param name="url">The URL (not used)</param>
        /// <returns></returns>
        protected virtual IUrlClient Create(Url url) => new UrlClient();

        /// <summary>
        /// Disposes all cached IUrlClient instances and clears the cache.
        /// </summary>
        public void Dispose()
        {
            foreach (var kv in _clients)
            {
                if (!kv.Value.IsDisposed)
                    kv.Value.Dispose();
            }
            _clients.Clear();
        }

        /// <summary>
        /// Override in custom factory to customize the creation of RestClient used in all Url HTTP calls
        /// (except when one is passed explicitly to the UrlClient constructor). In order not to lose
        /// Url.Http functionality, it is recommended to call base.CreateClient and customize the result.
        /// </summary>
        public virtual RestClient CreateRestClient(HttpMessageHandler handler)
        {
            return new RestClient(handler)
            {
                // Timeouts handled per request via UrlHttpSettings.Timeout
                //Timeout = System.Threading.Timeout.InfiniteTimeSpan
            };
        }

        /// <summary>
        /// Override in custom factory to customize the creation of the top-level HttpMessageHandler used in all
        /// Url HTTP calls (except when an RestClient is passed explicitly to the UrlClient constructor).
        /// In order not to lose Url.Http functionality, it is recommended to call base.CreateMessageHandler, and
        /// either customize the returned RestClientHandler, or set it as the InnerHandler of a DelegatingHandler.
        /// </summary>
        public virtual HttpMessageHandler CreateMessageHandler()
        {
            var RestClientHandler = new HttpClientHandler();

            // Url has its own mechanisms for managing cookies and redirects

            try { RestClientHandler.UseCookies = false; }
            catch (PlatformNotSupportedException) { } // look out for WASM platforms (#543)

            if (RestClientHandler.SupportsRedirectConfiguration)
                RestClientHandler.AllowAutoRedirect = false;

            if (RestClientHandler.SupportsAutomaticDecompression)
            {
                // #266
                // deflate not working? see #474
                RestClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }
            return RestClientHandler;
        }
    }
}
