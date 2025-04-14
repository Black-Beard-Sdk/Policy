
using Bb.Extensions;
using Bb.Http.Configuration;
using Bb.Util;
using System.Net;

namespace Bb.Http
{

    /// <inheritdoc />
    public class UrlRequest : IUrlRequest
    {

        

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRequest"/> class.
        /// </summary>
        /// <param name="url">The URL to call with this UrlRequest instance.</param>
        public UrlRequest(Url url = null)
        {
            Url = url;
            Version = HttpVersion.Version20;

        }

        /// <summary>
        /// Used internally by UrlClient.Request
        /// </summary>
        internal UrlRequest(IUrlClient client, params object[] urlSegments) : this(client?.BaseUrl, urlSegments)
        {
            Client = client;
        }

        /// <summary>
        /// Used internally by UrlClient.Request and CookieSession.Request
        /// </summary>
        internal UrlRequest(string baseUrl, params object[] urlSegments)
        {
            var parts = new List<string>(urlSegments.Select(s => s.ToInvariantString()));
            if (!Url.IsValid(parts.FirstOrDefault()) && !string.IsNullOrEmpty(baseUrl))
                parts.Insert(0, baseUrl);

            if (parts.Any())
                Url = Url.Combine(parts.ToArray());
        }

        /// <inheritdoc />
        public UrlHttpSettings Settings { get; } = new UrlHttpSettings();

        /// <inheritdoc />
        public IUrlClient Client
        {
            get => _client;
            set
            {
                _client = value;
                Settings.Defaults = _client?.Settings;
            }
        }

        /// <inheritdoc />
        public HttpMethod Verb { get; set; }

        /// <inheritdoc />
        public Url Url { get; set; }

        /// <inheritdoc />
        public HttpContent Content { get; set; }

        public Version Version { get; set; }

        /// <inheritdoc />
        public UrlCall RedirectedFrom { get; set; }

        /// <inheritdoc />
        public INameValueList<string> Headers { get; } = new NameValueList<string>(false); // header names are case-insensitive https://stackoverflow.com/a/5259004/62600

        /// <inheritdoc />
        public IEnumerable<(string Name, string Value)> Cookies =>
            CookieCutter.ParseRequestHeader(Headers.FirstOrDefault("Cookie"));

        /// <inheritdoc />
        public CookieJar CookieJar
        {
            get => _jar;
            set => ApplyCookieJar(value);
        }

        /// <summary>
        /// If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>
        /// </summary>
        public bool EnsureSuccessStatusCode { get; set; }

        public Task<IUrlResponse> SendAsync(HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            Client ??= UrlHttp.GlobalSettings.UrlClientFactory.Get(Url);
            return Client.SendAsync(this, completionOption, cancellationToken);
        }
        
        /// <inheritdoc />
        public Task<IUrlResponse> SendAsync(HttpMethod verb, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            Verb = verb;
            Client ??= UrlHttp.GlobalSettings.UrlClientFactory.Get(Url);
            return Client.SendAsync(this, completionOption, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IUrlResponse> SendAsync(HttpMethod verb, HttpContent content = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            Verb = verb;
            Content = content;
            Client ??= UrlHttp.GlobalSettings.UrlClientFactory.Get(Url);
            return Client.SendAsync(this, completionOption, cancellationToken);
        }


        private void ApplyCookieJar(CookieJar jar)
        {
            _jar = jar;
            if (jar == null)
                return;

            this.WithCookies(
                from c in CookieJar
                where c.ShouldSendTo(this.Url, out _)
                // sort by longest path, then earliest creation time, per #2: https://tools.ietf.org/html/rfc6265#section-5.4
                orderby (c.Path ?? c.OriginUrl.Path).Length descending, c.DateReceived
                select (c.Name, c.Value));
        }

        private IUrlClient _client;
        private CookieJar _jar;

    }
}