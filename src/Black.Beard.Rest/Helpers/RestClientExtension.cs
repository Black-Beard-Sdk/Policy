using Bb.Http;
using RestSharp;

namespace Bb.Helpers
{

    public static class RestClientExtension
    {

        public static async Task<TokenResponse?> GetTokenAsync(this RestClient self, string path, string client_id, string username, string password)
        {

            var request = Method.Post.NewRequest(path, DataFormat.Json)
                   .AddParameter("client_id", client_id)    // Ajouter les paramètres du formulaire
                   .AddParameter("username", username)
                   .AddParameter("password", password)
                   .AddParameter("grant_type", "password");

            var response = await self.ExecuteAsync<TokenResponse>(request);

            if (!response.IsSuccessful)
                throw new Exception($"Échec de l'obtention du token: {response.ErrorMessage ?? response.StatusDescription}");

            return response.Data;

        }

    }

}
