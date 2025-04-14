using System.Net.Http.Json;
using System.Text.Json;

namespace Bb.Curls
{
    /// <summary>
    /// Specialized curl command parser
    /// </summary>
    public static partial class CurlParserExtension
    {


        /// <summary>
        /// Results to string.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        /// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        /// <returns></returns>
        public static string ResultToString(this Task<HttpResponseMessage> self, bool ensureSuccessStatusCode = false, CancellationToken cancellationToken = default)
        {
            var e = self.ResultToStringAsync(ensureSuccessStatusCode, cancellationToken);
            e.Wait();
            return e.Result;
        }

        /// <summary>
        /// Results to string asynchronous.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        /// <returns></returns>
        public static async Task<string> ResultToStringAsync(this Task<HttpResponseMessage> self, bool ensureSuccessStatusCode = false, CancellationToken cancellationToken = default)
        {
            self.Wait();
            var response = self.Result;
            if (ensureSuccessStatusCode && !response.IsSuccessStatusCode)
                response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            return responseBody;
        }

        /// <summary>
        /// Results to json asynchronous.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        /// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        /// <returns></returns>
        public static object? ResultToJson<T>(this Task<HttpResponseMessage> self, bool ensureSuccessStatusCode = false, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            var e = self.ResultToJsonAsync<T>(ensureSuccessStatusCode, options, cancellationToken);
            e.Wait();
            return e.Result;
        }

        /// <summary>
        /// Results to json asynchronous.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        /// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        /// <returns></returns>
        public static async Task<object?> ResultToJsonAsync<T>(this Task<HttpResponseMessage> self, bool ensureSuccessStatusCode = false, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            self.Wait();
            var response = self.Result;
            if (ensureSuccessStatusCode && !response.IsSuccessStatusCode)
                response.EnsureSuccessStatusCode();
            object? responseBody = await response.Content.ReadFromJsonAsync<T>(options, cancellationToken);
            return responseBody;
        }

        /// <summary>
        /// Results to json asynchronous.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="type"><see cref="Type"/> </param>
        /// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        /// <param name="options"><see cref="JsonSerializerOptions"/> </param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        /// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        /// <returns></returns>
        public static object? ResultToJson(this Task<HttpResponseMessage> self, Type type, bool ensureSuccessStatusCode = false, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            var e = self.ResultToJsonAsync(type, ensureSuccessStatusCode, options, cancellationToken);
            e.Wait();
            return e.Result;
        }

        /// <summary>
        /// Results to json asynchronous.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="type"><see cref="Type"/> </param>
        /// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        /// <param name="options"><see cref="JsonSerializerOptions"/> </param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        /// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        /// <returns></returns>
        public static async Task<object?> ResultToJsonAsync(this Task<HttpResponseMessage> self, Type type, bool ensureSuccessStatusCode = false, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            self.Wait();
            var response = self.Result;
            if (ensureSuccessStatusCode && !response.IsSuccessStatusCode)
                response.EnsureSuccessStatusCode();
            object? responseBody = await response.Content.ReadFromJsonAsync(type, options, cancellationToken);
            return responseBody;
        }

        /// <summary>
        /// Results to byte array.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        /// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        /// <returns></returns>
        public static byte[] CallToByteArray(this Task<HttpResponseMessage> self, bool ensureSuccessStatusCode = false, CancellationToken cancellationToken = default)
        {
            var e = self.ResultToByteArrayAsync(ensureSuccessStatusCode, cancellationToken);
            e.Wait();
            return e.Result;
        }

        /// <summary>
        /// Results to byte array asynchronous.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        /// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        /// <returns></returns>
        public static async Task<byte[]> ResultToByteArrayAsync(this Task<HttpResponseMessage> self, bool ensureSuccessStatusCode = false, CancellationToken cancellationToken = default)
        {
            self.Wait();
            var response = self.Result;
            if (ensureSuccessStatusCode && !response.IsSuccessStatusCode)
                response.EnsureSuccessStatusCode();
            byte[] responseBody = await response.Content.ReadAsByteArrayAsync(cancellationToken);
            return responseBody;
        }

        /// <summary>
        /// Results to stream.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        /// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        /// <returns></returns>
        public static Stream CallToStream(this Task<HttpResponseMessage> self, bool ensureSuccessStatusCode = false, CancellationToken cancellationToken = default)
        {
            var e = self.ResultToStreamAsync(ensureSuccessStatusCode, cancellationToken);
            e.Wait();
            return e.Result;
        }

        /// <summary>
        /// Results to stream asynchronous.
        /// </summary>
        /// <param name="self">The self.</param>
        /// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        /// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        /// <returns></returns>
        public static async Task<Stream> ResultToStreamAsync(this Task<HttpResponseMessage> self, bool ensureSuccessStatusCode = false, CancellationToken cancellationToken = default)
        {
            self.Wait();
            var response = self.Result;
            if (ensureSuccessStatusCode && !response.IsSuccessStatusCode)
                response.EnsureSuccessStatusCode();
            Stream responseBody = await response.Content.ReadAsStreamAsync(cancellationToken);
            return responseBody;
        }


    }



}
