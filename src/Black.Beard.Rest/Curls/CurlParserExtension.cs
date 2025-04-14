
using Bb.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;


namespace Bb.Curls
{

    /// <summary>
    /// Specialized curl command parser
    /// </summary>
    public static partial class CurlParserExtension
    {


        /// <summary>
        /// Initializes the <see cref="CurlParserExtension"/> class.
        /// </summary>
        static CurlParserExtension()
        {
            _regIsUrl = new Regex(pattern, options);
        }

        /// <summary>
        /// Parses the curl line.
        /// </summary>
        /// <param name="lineArg">The line argument.</param>
        /// <returns></returns>
        public static string[] ParseCurlLine(this string lineArg)
        {

            var _lexer = new CurlLexer(lineArg);

            List<string> result = new List<string>();
            while (_lexer.Next())
            {

                var c = _lexer.Current;

                if (!string.IsNullOrEmpty(c))
                {
                    if ((c.StartsWith("-") && c.Length == 1) || (c.StartsWith("--") && c.Length == 2))
                        throw new ArgumentException($"Invalid argument {c} at position {_lexer.CurrentPosition}");

                    result.Add(c);

                }
            }

            return result.ToArray();

        }


        /// <summary>
        /// Pre compiles the specified line argument and build CurlInterpreter.
        /// </summary>
        /// <param name="lineArg"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CurlInterpreter Precompile(this string lineArg)
        {
            var interpreter = new CurlInterpreter(lineArg.ParseCurlLine());
            interpreter.BuildActions();
            return interpreter;
        }

        ///// <summary>
        ///// makes asynchronous curl Calls.
        ///// </summary>
        ///// <param name="lineArg">The line argument.</param>
        ///// <param name="cancellationTokenSource">The cancellation token to cancel operation.<see cref="CancellationTokenSource"/></param>
        ///// <returns><see cref="T:Task<HttpResponseMessage>"/>The task object representing the asynchronous operation.</returns>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static async Task<IUrlResponse?> CallAsync(this string lineArg, CancellationTokenSource cancellationTokenSource = null, bool ensureSuccessStatusCode = false)
        //{
        //    var interpreter = Precompile(lineArg);
        //    return await interpreter.CallAsync(ensureSuccessStatusCode, cancellationTokenSource);
        //}


        ///// <summary>
        ///// makes asynchronous curl Calls
        ///// </summary>
        ///// <param name="arguments">The list of argument.</param>
        ///// <param name="cancellationTokenSource">The cancellation token to cancel operation.<see cref="CancellationTokenSource"/></param>
        ///// <param name="ensureSuccessStatusCode">thrown an exception if the reesponse is not 2xx</param>
        ///// <returns><see cref="T:Task<HttpResponseMessage>"/>The task object representing the asynchronous operation.</returns>
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static async Task<IUrlResponse?> CallAsync(this string[] arguments, CancellationTokenSource cancellationTokenSource = null, bool ensureSuccessStatusCode = false)
        //{
        //    var interpreter = new CurlInterpreter(arguments);
        //    return await interpreter.CallAsync(ensureSuccessStatusCode, cancellationTokenSource);
        //}


        /// <summary>
        /// Determines whether this instance is an URL.
        /// </summary>
        /// <param name="self">The self text to test.</param>
        /// <returns>
        ///   <c>true</c> if the specified self is URL; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUrl(this string self)
        {
            return _regIsUrl.IsMatch(self);
        }


        ///// <summary>
        ///// Results to string.
        ///// </summary>
        ///// <param name="self">The self.</param>
        ///// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        ///// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        ///// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        ///// <returns></returns>
        //public static string? ResultToString(this CurlInterpreter self, bool ensureSuccessStatusCode = false, CancellationToken cancellationToken = default)
        //{
        //    var e = self.CallToStringAsync(ensureSuccessStatusCode, cancellationToken);
        //    e.Wait();

        //    return e.Result;
        //}

        ///// <summary>
        ///// Results to Json.
        ///// </summary>
        ///// <param name="self">The self.</param>
        ///// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        ///// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        ///// <returns></returns>
        //public static JsonElement? ResultToJson(this CurlInterpreter self, CancellationToken cancellationToken = default)
        //{
        //    var e = self.ResultToJson(false, cancellationToken, new JsonDocumentOptions());
        //    return e;
        //}

        ///// <summary>
        ///// Results to Json.
        ///// </summary>
        ///// <param name="self">The self.</param>
        ///// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        ///// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        ///// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        ///// <returns></returns>
        //public static JsonElement? ResultToJson(this CurlInterpreter self, bool ensureSuccessStatusCode, CancellationToken cancellationToken = default)
        //{
        //    var e = self.ResultToJson(ensureSuccessStatusCode, cancellationToken, new JsonDocumentOptions());
        //    return e;
        //}

        ///// <summary>
        ///// Results to Json.
        ///// </summary>
        ///// <param name="self">The self.</param>
        ///// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        ///// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        ///// <param name="options"><see cref="CJsonDocumentOptions"/> </param>
        ///// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        ///// <returns></returns>
        //public static JsonElement? ResultToJson(this CurlInterpreter self, bool ensureSuccessStatusCode, CancellationToken cancellationToken, JsonDocumentOptions options)
        //{
            
        //    var e = self.CallToStringAsync(ensureSuccessStatusCode, cancellationToken);
        //    e.Wait();

        //    string payload = e.Result;

        //    if (!string.IsNullOrEmpty(payload))
        //    {
        //        var doc = JsonDocument.Parse(payload, options);
        //        return doc.RootElement;
        //    }

        //    return null;

        //}

        ///// <summary>
        ///// Results to typed object asynchronous.
        ///// </summary>
        ///// <param name="self">The self.</param>
        ///// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        ///// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        ///// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        ///// <returns></returns>
        //public static object? ResultToObject<T>(this CurlInterpreter self, bool ensureSuccessStatusCode = false, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        //{
        //    var e = self.CallToObjectAsync<T>(ensureSuccessStatusCode, options, cancellationToken);
        //    e.Wait();
        //    return e.Result;
        //}

        ///// <summary>
        ///// Results to stream.
        ///// </summary>
        ///// <param name="self">The self.</param>
        ///// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        ///// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        ///// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        ///// <returns></returns>
        //public static Stream? ResultToStream(this CurlInterpreter self, bool ensureSuccessStatusCode = false, CancellationToken cancellationToken = default)
        //{
        //    var e = self.CallToStreamAsync(ensureSuccessStatusCode, cancellationToken);
        //    e.Wait();
        //    return e.Result;
        //}





        ///// <summary>
        ///// Results to typed object asynchronous.
        ///// </summary>
        ///// <param name="self">The self.</param>
        ///// <param name="type"><see cref="Type"/> </param>
        ///// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        ///// <param name="options"><see cref="JsonSerializerOptions"/> </param>
        ///// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        ///// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        ///// <returns></returns>
        //public static async Task<object?> CallToObjectAsync(this CurlInterpreter self, Type type, bool ensureSuccessStatusCode = false, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        //{
        //    var response = await self.CallAsync(ensureSuccessStatusCode);
        //    if (response != null)
        //        return await response.ResponseMessage.Content.ReadFromJsonAsync(type, options, cancellationToken);

        //    return null;
        //}

        ///// <summary>
        ///// Results to string asynchronous.
        ///// </summary>
        ///// <param name="self">The self.</param>
        ///// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        ///// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        ///// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        ///// <returns></returns>
        //public static async Task<string?> CallToStringAsync(this CurlInterpreter self, bool ensureSuccessStatusCode = false, CancellationToken cancellationToken = default)
        //{
        //    var response = await self.CallAsync(ensureSuccessStatusCode);
        //    if (response != null)
        //        return await response.ResponseMessage.Content.ReadAsStringAsync(cancellationToken);

        //    return null;
        //}

        ///// <summary>
        ///// Results to typed object asynchronous.
        ///// </summary>
        ///// <param name="self">The self.</param>
        ///// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        ///// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        ///// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        ///// <returns></returns>
        //public static async Task<object?> CallToObjectAsync<T>(this CurlInterpreter self, bool ensureSuccessStatusCode = false, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        //{
        //    var response = await self.CallAsync(ensureSuccessStatusCode);
        //    if (response != null)
        //        return await response.ResponseMessage.Content.ReadFromJsonAsync<T>(options, cancellationToken);

        //    return null;
        //}


        ///// <summary>
        ///// Results to byte array asynchronous.
        ///// </summary>
        ///// <param name="self">The self.</param>
        ///// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        ///// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        ///// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        ///// <returns></returns>
        //public static async Task<byte[]?> CallToByteArrayAsync(this CurlInterpreter self, bool ensureSuccessStatusCode = false, CancellationToken cancellationToken = default)
        //{
        //    var response = await self.CallAsync(ensureSuccessStatusCode);
        //    if (response != null)
        //        return await response.ResponseMessage.Content.ReadAsByteArrayAsync(cancellationToken);

        //    return null;
        //}

        ///// <summary>
        ///// Results to stream asynchronous.
        ///// </summary>
        ///// <param name="self">The self.</param>
        ///// <param name="ensureSuccessStatusCode">If true and the http result code is not between 200 and 299, throw <see cref="HttpRequestException"/>.</param>
        ///// <param name="cancellationToken"><see cref="CancellationToken"/> </param>
        ///// <exception cref="HttpRequestException">if the result is not between 200 and 299</exception>
        ///// <returns></returns>
        //public static async Task<Stream?> CallToStreamAsync(this CurlInterpreter self, bool ensureSuccessStatusCode = false, CancellationToken cancellationToken = default)
        //{
        //    var response = await self.CallAsync(ensureSuccessStatusCode);
        //    if (response != null)
        //        return await response.ResponseMessage.Content.ReadAsStreamAsync(cancellationToken);

        //    return null;
        //}






        private const string pattern = @"(https?|ftp|ssh|mailto):\/\/([a-z]+[a-z0-9.-]+|(\d{1,3}\.){3,3}\d{1,3})(:\d{0,5})?(\/[a-z]+[a-z0-9.-]+)*(\?([a-z]+[a-z0-9%&=+#]*)+)?";
        private const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        //[GeneratedRegex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline | RegexOptions.ExplicitCapture)]
        //private static partial Regex _regIsUrl();
        private static readonly Regex _regIsUrl;


    }

}
