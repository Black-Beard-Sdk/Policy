using Bb.Http;
using Bb.Util;
using System.Net.Http.Headers;

namespace Bb.Extensions
{
    /// <summary>
    /// Fluent extension methods for sending multipart/form-data requests.
    /// </summary>
    public static class MultipartExtensions
    {

        /// <summary>
        /// Sends an asynchronous multipart/form-data POST request.
        /// </summary>
        /// <param name="buildContent">A delegate for building the content parts.</param>
        /// <param name="request">The IUrlRequest.</param>
        /// <param name="completionOption">The HttpCompletionOption used in the request. Optional.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A Task whose result is the received IUrlResponse.</returns>
        public static Task<IUrlResponse> PostMultipartAsync(this IUrlRequest request, Action<MultipartFormDataContent> buildContent, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead, CancellationToken cancellationToken = default)
        {
            var cmc = new MultipartFormDataContent();
            buildContent(cmc);
            return request.SendAsync(HttpMethod.Post, cmc, completionOption, cancellationToken);
        }

        /// <summary>
        /// Add string parts to the MultipartFormDataContent.
        /// </summary>
        /// <param name="self">The content <see cref="MultipartFormDataContent"/></param>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static MultipartFormDataContent AddStringParts(this MultipartFormDataContent self, string name, object data, string? contentType = null)
        {
            foreach (var kv in data.ToKeyValuePairs())
            {
                if (kv.Value == null)
                    continue;
                self.AddString(kv.Key, kv.Value.ToInvariantString(), contentType);
            }
            return self;
        }

        /// <summary>
        /// Add json to the MultipartFormDataContent.
        /// </summary>
        /// <param name="self">The content <see cref="MultipartFormDataContent"/></param>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static MultipartFormDataContent AddJson(this MultipartFormDataContent self, string name, string data)
        {

            var content = new StringContent(data);
            content.Headers.TryAddWithoutValidation("Content-Type", ContentType.ApplicationJson.WithCharsetUtf8());

            self.Add(content, name);
            return self;
        }

        /// <summary>
        /// Add string to the MultipartFormDataContent.
        /// </summary>
        /// <param name="self">The content <see cref="MultipartFormDataContent"/></param>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static MultipartFormDataContent AddString(this MultipartFormDataContent self, string name, string data, string? contentType = null)
        {
            var content = new StringContent(data);

            if (!string.IsNullOrEmpty(contentType))
                content.Headers.TryAddWithoutValidation("Content-Type", contentType);

            self.Add(content, name);
            return self;
        }

        /// <summary>
        /// Add datas url encoded to the MultipartFormDataContent.
        /// </summary>
        /// <param name="self">The content <see cref="MultipartFormDataContent"/></param>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static MultipartFormDataContent AddUrlEncoded(this MultipartFormDataContent self, string name, List<KeyValuePair<string, string>> data)
        {
            var item = new FormUrlEncodedContent(data);
            self.Add(item, name);
            return self;
        }


        /// <summary>
        /// Add file to the MultipartFormDataContent.
        /// </summary>
        /// <param name="self">The content <see cref="MultipartFormDataContent"/></param>
        /// <param name="name"></param>
        /// <param name="filename"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static MultipartFormDataContent AddFile(this MultipartFormDataContent self, string name, string filename, string? contentType = null)
        {

            var filePath = filename;

            if (filePath.StartsWith("@"))
                filePath = filePath.Substring(1);

            HttpContent content = new StreamContent(new FileStream(filePath, FileMode.Open));

            if (!string.IsNullOrEmpty(contentType))
                content.Headers.TryAddWithoutValidation("Content-Type", contentType);

            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = name,
                FileName = Path.GetFileName(filePath)
            };
            self.Add(new StringContent(name), name);
            self.Add(content);

            return self;

        }

        /// <summary>
        /// Append a content type to the HttpContent headers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">The content <see cref="HttpContent"/></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static T WithContentType<T>(this T self, ContentType contentType)
            where T : HttpContent
        {
            self.Headers.TryAddWithoutValidation(ContentType.Key, contentType.Constant);
            return self;
        }

        public static ContentType WithCharsetUtf8(this ContentType self)
        {
            return new ContentType(self + ContentType.CharsetUtf8);
        }


    }

}
