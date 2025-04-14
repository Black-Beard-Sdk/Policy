using Bb.Http;
using Bb.Http.Configuration;
using System.Net;

namespace Bb.Extensions
{
    public static partial class IUrlExtensions
    {



        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRequest"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns><see cref="UrlRequest"/></returns>
        public static UrlRequest Request(this Url url)
        {
            return new UrlRequest(url);
        }



        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous HEAD request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> HeadAsync(this Url url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).HeadAsync(completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous DELETE request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> DeleteAsync(this Url url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).DeleteAsync(completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous OPTIONS request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> OptionsAsync(this Url url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).OptionsAsync(completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a new UrlRequest and asynchronously downloads a file.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="localFolderPath">Path of local folder where file is to be downloaded.</param>
        /// <param name="localFileName">Name of local file. If not specified, the source filename (last segment of the URL) is used.</param>
        /// <param name="bufferSize">Buffer size in bytes. Default is 4096.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the local path of the downloaded file.</returns>
        public static Task<string> DownloadFileAsync(this Url url, string localFolderPath, string localFileName = null, int bufferSize = 4096, HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).DownloadFileAsync(localFolderPath, localFileName, bufferSize, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous multipart/form-data POST request.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="buildContent">A delegate for building the content parts.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostMultipartAsync(this Url url, Action<MultipartFormDataContent> buildContent, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PostMultipartAsync(buildContent, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets request headers based on property names/values of the provided object, or keys/values if object is a dictionary, to be sent.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="headers">Names/values of HTTP headers to set. Typically an anonymous object or IDictionary.</param>
        /// <param name="replaceUnderscoreWithHyphen">If true, underscores in property names will be replaced by hyphens. Default is true.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithHeaders(this Url url, object headers, bool replaceUnderscoreWithHyphen = true)
        {
            return new UrlRequest(url).WithHeaders(headers, replaceUnderscoreWithHyphen);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets request headers based on property names/values of the provided object, or keys/values if object is a dictionary, to be sent.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="contentType">Content type value</param>        
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithContentType(this Url url, ContentType contentType)
        {
            return new UrlRequest(url).WithHeader(ContentType.Key, contentType);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets request headers based on property names/values of the provided object, or keys/values if object is a dictionary, to be sent.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="contentType">Content type value</param>        
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithContentType(this Url url, string contentType)
        {
            return new UrlRequest(url).WithHeader(ContentType.Key, contentType);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets the Authorization header according to Basic Authentication protocol.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="userName">User name of authenticating user.</param>
        /// <param name="password">Password of authenticating user.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithBasicAuth(this Url url, string userName, string password)
        {
            return new UrlRequest(url).WithBasicAuth(userName, password);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets the Authorization header with a bearer token according to OAuth 2.0 specification.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="token">The acquired oAuth bearer token.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithOAuthBearerToken(this Url url, string token)
        {
            return new UrlRequest(url).WithOAuthBearerToken(token);
        }

        /// <summary>
        /// Creates a new UrlRequest and adds a name-value pair to its Cookie header. To automatically maintain a cookie "session", consider using a CookieJar or CookieSession instead.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="name">The cookie name.</param>
        /// <param name="value">The cookie value.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithCookie(this Url url, string name, object value)
        {
            return new UrlRequest(url).WithCookie(name, value);
        }

        /// <summary>
        /// Creates a new UrlRequest and adds name-value pairs to its Cookie header based on property names/values of the provided object, or keys/values if object is a dictionary. To automatically maintain a cookie "session", consider using a CookieJar or CookieSession instead.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="values">Names/values of HTTP cookies to set. Typically an anonymous object or IDictionary.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithCookies(this Url url, object values)
        {
            return new UrlRequest(url).WithCookies(values);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets the CookieJar associated with this request, which will be updated with any Set-Cookie headers present in the response and is suitable for reuse in subsequent requests.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="cookieJar">The CookieJar.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithCookies(this Url url, CookieJar cookieJar)
        {
            return new UrlRequest(url).WithCookies(cookieJar);
        }

        /// <summary>
        /// Creates a new UrlRequest and associates it with a new CookieJar, which will be updated with any Set-Cookie headers present in the response and is suitable for reuse in subsequent requests.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="cookieJar">The created CookieJar, which can be reused in subsequent requests.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithCookies(this Url url, out CookieJar cookieJar)
        {
            return new UrlRequest(url).WithCookies(out cookieJar);
        }

        /// <summary>
        /// Creates a new UrlRequest and allows changing its Settings inline.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="action">A delegate defining the Settings changes.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest ConfigureRequest(this Url url, Action<IUrlRequest> action)
        {
            return new UrlRequest(url).ConfigureRequest(action);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets the request timeout.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="timespan">Time to wait before the request times out.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithTimeout(this Url url, TimeSpan timespan)
        {
            return new UrlRequest(url).WithTimeout(timespan);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets the request timeout.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="seconds">Seconds to wait before the request times out.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithTimeout(this Url url, int seconds)
        {
            return new UrlRequest(url).WithTimeout(seconds);
        }

        /// <summary>
        /// Creates a new UrlRequest and adds a pattern representing an HTTP status code or range of codes which (in addition to 2xx) will NOT result in a UrlHttpException being thrown.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="pattern">Examples: "3xx", "100,300,600", "100-299,6xx"</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest AllowHttpStatus(this Url url, string pattern)
        {
            return new UrlRequest(url).AllowHttpStatus(pattern);
        }

        /// <summary>
        /// Creates a new UrlRequest and adds an HttpStatusCode which (in addition to 2xx) will NOT result in a UrlHttpException being thrown.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="statusCodes">The HttpStatusCode(s) to allow.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest AllowHttpStatus(this Url url, params HttpStatusCode[] statusCodes)
        {
            return new UrlRequest(url).AllowHttpStatus(statusCodes);
        }

        /// <summary>
        /// Creates a new UrlRequest and configures it to allow any returned HTTP status without throwing a UrlHttpException.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest AllowAnyHttpStatus(this Url url)
        {
            return new UrlRequest(url).AllowAnyHttpStatus();
        }

        /// <summary>
        /// Creates a new UrlRequest and configures whether redirects are automatically followed.
        /// </summary>
        /// <param name="url">This Url.</param>
        /// <param name="enabled">true if url should automatically send a new request to the redirect URL, false if it should not.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithAutoRedirect(this Url url, bool enabled)
        {
            return new UrlRequest(url).WithAutoRedirect(enabled);
        }





        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="verb">The HTTP verb used to make the request.</param>
        /// <param name="content">The request body content.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> SendAsync(this string url, HttpMethod verb, HttpContent content = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).SendAsync(verb, content, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="verb">The HTTP verb used to make the request.</param>
        /// <param name="body">An object representing the request body, which will be serialized to JSON.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> SendJsonAsync(this string url, HttpMethod verb, object body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).SendObjectAsync(verb, body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="verb">The HTTP verb used to make the request.</param>
        /// <param name="body">The request body.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> SendStringAsync(this string url, HttpMethod verb, string body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).SendStringAsync(verb, body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="verb">The HTTP verb used to make the request.</param>
        /// <param name="body">An object representing the request body, which will be serialized to a URL-encoded string.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> SendUrlEncodedAsync(this string url, HttpMethod verb, object body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).SendUrlEncodedAsync(verb, body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous GET request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> GetAsync(this string url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).GetAsync(completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous GET request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the JSON response body deserialized to an object of type T.</returns>
        public static Task<T?> GetObjectAsync<T>(this string url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).GetObjectAsync<T>(completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous GET request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the response body as a string.</returns>
        public static Task<string?> GetStringAsync(this string url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).GetStringAsync(completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous GET request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the response body as a Stream.</returns>
        public static Task<Stream?> GetStreamAsync(this string url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).GetStreamAsync(completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous GET request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the response body as a byte array.</returns>
        public static Task<byte[]?> GetBytesAsync(this string url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).GetBytesAsync(completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous POST request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="content">The request body content.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostAsync(this string url, HttpContent content = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PostAsync(content, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous POST request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="body">An object representing the request body, which will be serialized to JSON.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostJsonAsync(this string url, object body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PostAsync(body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous POST request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="body">The request body.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostStringAsync(this string url, string body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PostAsync(body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous POST request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="body">An object representing the request body, which will be serialized to a URL-encoded string.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostUrlEncodedAsync(this string url, object body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PostUrlEncodedAsync(body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous HEAD request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> HeadAsync(this string url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).HeadAsync(completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous PUT request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="content">The request body content.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PutAsync(this string url, HttpContent content = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PutAsync(content, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous PUT request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="body">An object representing the request body, which will be serialized to JSON.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PutJsonAsync(this string url, object body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PutObjectAsync(body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous PUT request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="body">The request body.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PutStringAsync(this string url, string body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PutStringAsync(body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous DELETE request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> DeleteAsync(this string url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).DeleteAsync(completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous PATCH request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="content">The request body content.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PatchAsync(this string url, HttpContent content = null, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PatchAsync(content, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous PATCH request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="body">An object representing the request body, which will be serialized to JSON.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PatchJsonAsync(this string url, object body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PatchObjectAsync(body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous PATCH request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="body">The request body.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PatchStringAsync(this string url, string body, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PatchStringAsync(body, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous OPTIONS request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> OptionsAsync(this string url, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).OptionsAsync(completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a new UrlRequest and asynchronously downloads a file.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="localFolderPath">Path of local folder where file is to be downloaded.</param>
        /// <param name="localFileName">Name of local file. If not specified, the source filename (last segment of the URL) is used.</param>
        /// <param name="bufferSize">Buffer size in bytes. Default is 4096.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the local path of the downloaded file.</returns>
        public static Task<string> DownloadFileAsync(this string url, string localFolderPath, string localFileName = null, int bufferSize = 4096, HttpCompletionOption completionOption = HttpCompletionOption.ResponseHeadersRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).DownloadFileAsync(localFolderPath, localFileName, bufferSize, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a UrlRequest and sends an asynchronous multipart/form-data POST request.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="buildContent">A delegate for building the content parts.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostMultipartAsync(this string url, Action<MultipartFormDataContent> buildContent, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            return new UrlRequest(url).PostMultipartAsync(buildContent, completionOption, cancellationToken);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets a request header.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="name">The header name.</param>
        /// <param name="value">The header value.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithHeader(this string url, string name, object value)
        {
            return new UrlRequest(url).WithHeader(name, value);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets request headers based on property names/values of the provided object, or keys/values if object is a dictionary, to be sent.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="headers">Names/values of HTTP headers to set. Typically an anonymous object or IDictionary.</param>
        /// <param name="replaceUnderscoreWithHyphen">If true, underscores in property names will be replaced by hyphens. Default is true.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithHeaders(this string url, object headers, bool replaceUnderscoreWithHyphen = true)
        {
            return new UrlRequest(url).WithHeaders(headers, replaceUnderscoreWithHyphen);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets the Authorization header according to Basic Authentication protocol.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="userName">User name of authenticating user.</param>
        /// <param name="password">Password of authenticating user.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithBasicAuth(this string url, string userName, string password)
        {
            return new UrlRequest(url).WithBasicAuth(userName, password);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets the Authorization header with a bearer token according to OAuth 2.0 specification.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="token">The acquired oAuth bearer token.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithOAuthBearerToken(this string url, string token)
        {
            return new UrlRequest(url).WithOAuthBearerToken(token);
        }

        /// <summary>
        /// Creates a new UrlRequest and adds a name-value pair to its Cookie header. To automatically maintain a cookie "session", consider using a CookieJar or CookieSession instead.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="name">The cookie name.</param>
        /// <param name="value">The cookie value.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithCookie(this string url, string name, object value)
        {
            return new UrlRequest(url).WithCookie(name, value);
        }

        /// <summary>
        /// Creates a new UrlRequest and adds name-value pairs to its Cookie header based on property names/values of the provided object, or keys/values if object is a dictionary. To automatically maintain a cookie "session", consider using a CookieJar or CookieSession instead.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="values">Names/values of HTTP cookies to set. Typically an anonymous object or IDictionary.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithCookies(this string url, object values)
        {
            return new UrlRequest(url).WithCookies(values);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets the CookieJar associated with this request, which will be updated with any Set-Cookie headers present in the response and is suitable for reuse in subsequent requests.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="cookieJar">The CookieJar.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithCookies(this string url, CookieJar cookieJar)
        {
            return new UrlRequest(url).WithCookies(cookieJar);
        }

        /// <summary>
        /// Creates a new UrlRequest and associates it with a new CookieJar, which will be updated with any Set-Cookie headers present in the response and is suitable for reuse in subsequent requests.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="cookieJar">The created CookieJar, which can be reused in subsequent requests.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithCookies(this string url, out CookieJar cookieJar)
        {
            return new UrlRequest(url).WithCookies(out cookieJar);
        }

        /// <summary>
        /// Creates a new UrlRequest and allows changing its Settings inline.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="action">A delegate defining the Settings changes.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest ConfigureRequest(this string url, Action<IUrlRequest> action)
        {
            return new UrlRequest(url).ConfigureRequest(action);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets the request timeout.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="timespan">Time to wait before the request times out.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithTimeout(this string url, TimeSpan timespan)
        {
            return new UrlRequest(url).WithTimeout(timespan);
        }

        /// <summary>
        /// Creates a new UrlRequest and sets the request timeout.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="seconds">Seconds to wait before the request times out.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithTimeout(this string url, int seconds)
        {
            return new UrlRequest(url).WithTimeout(seconds);
        }

        /// <summary>
        /// Creates a new UrlRequest and adds a pattern representing an HTTP status code or range of codes which (in addition to 2xx) will NOT result in a UrlHttpException being thrown.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="pattern">Examples: "3xx", "100,300,600", "100-299,6xx"</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest AllowHttpStatus(this string url, string pattern)
        {
            return new UrlRequest(url).AllowHttpStatus(pattern);
        }

        /// <summary>
        /// Creates a new UrlRequest and adds an HttpStatusCode which (in addition to 2xx) will NOT result in a UrlHttpException being thrown.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="statusCodes">The HttpStatusCode(s) to allow.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest AllowHttpStatus(this string url, params HttpStatusCode[] statusCodes)
        {
            return new UrlRequest(url).AllowHttpStatus(statusCodes);
        }

        /// <summary>
        /// Creates a new UrlRequest and configures it to allow any returned HTTP status without throwing a UrlHttpException.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest AllowAnyHttpStatus(this string url)
        {
            return new UrlRequest(url).AllowAnyHttpStatus();
        }

        /// <summary>
        /// Creates a new UrlRequest and configures whether redirects are automatically followed.
        /// </summary>
        /// <param name="url">This URL.</param>
        /// <param name="enabled">true if url should automatically send a new request to the redirect URL, false if it should not.</param>
        /// <returns>A new IUrlRequest.</returns>
        public static IUrlRequest WithAutoRedirect(this string url, bool enabled)
        {
            return new UrlRequest(url).WithAutoRedirect(enabled);
        }


    }

}
