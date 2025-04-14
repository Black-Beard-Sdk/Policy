using Bb.Http;
using RestSharp;

namespace Bb.Helpers
{

    public static class RestRequestExtension
    {


        public static RestRequest WithBearer(this RestRequest self, TokenResponse token)
        {
            self.AddHeader("Authorization", $"Bearer {token.AccessToken}");
            return self;
        }

        public static RestRequest WithContentType(this RestRequest self, ContentType contentType)
        {
            self.AddHeader("Content-Type", contentType);
            return self;
        }

        public static RestRequest NewRequest(this Method method, string path, DataFormat? format = null)
        {
            var request = new RestRequest(path, method) { RequestFormat = format.HasValue ? format.Value : DataFormat.Json };
            return request;
        }



    }

}
