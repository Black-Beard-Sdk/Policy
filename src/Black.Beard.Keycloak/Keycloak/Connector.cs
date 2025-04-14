using Bb.Helpers;
using Bb.Http;
using Bb.Util;
using Keycloak.AuthServices.Sdk.Kiota.Admin;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;

namespace Bb.Keycloak
{
   

    public class Connector : IDisposable
    {

        public Connector() :
            this("http://localhost:8080")
        {
         
        }

        public Connector(Url keycloalUrl)
        {

            _options = new RestClientOptions(keycloalUrl.ToUri())
            .WithLog()
            ;

            _client = new RestClient(_options);

        }


        public Url KeycloalUrl { get; }


        public string ClientId { get; set; } = "admin-cli";


        public bool IsConnected => _token != null;


        public async Task<Connector> Connect(string username, string password)
        {

            Url url = tokenPath
                .Map("realm", "master");

            _token = await _client.GetTokenAsync(url, ClientId, username, password);
            // _options.WithOAuth2(_token.AccessToken);

            return this;
        }


        public async Task<bool> RealmExists(string realmName)
        {

            if (string.IsNullOrEmpty(realmName))
                throw new ArgumentNullException(nameof(realmName));

            EnsureIsConnected();

            var url = realmExistsPath.Map("realm", "master");

            var response = await _client.ExecuteAsync
            (
                Method.Post.NewRequest(url)
                    .WithBearer(_token)
            );

            // Vérifier si la création a réussi (code 201 Created)
             return response.IsSuccessful;

        }

        public async Task<bool> CreateRealmAsync(string realmName)
        {

            if (string.IsNullOrEmpty(realmName))
                throw new ArgumentNullException(nameof(realmName));

            EnsureIsConnected();

            // Créer le corps de la requête
            var realmData = new
            {
                id = realmName,
                realm = realmName,
                enabled = true
            };

            // Exécuter la requête
            var response = await _client.ExecuteAsync
            (
                Method.Post.NewRequest(adminRealmsPath)
                    .WithContentType(ContentTypes.ApplicationJson)
                    .WithBearer(_token)
                    .AddJsonBody(realmData)
            );

            // Vérifier si la création a réussi (code 201 Created)
            return response.IsSuccessful;

        }

        private void EnsureIsConnected()
        {
            if (!IsConnected)
                throw new InvalidOperationException("You must to be connected");
        }


        public void Dispose()
        {
            _client?.Dispose();
            _token = null;
            GC.SuppressFinalize(this);
        }

        private readonly RestClientOptions _options;
        readonly RestClient _client;
        private TokenResponse? _token;
        private const string tokenPath = "/realms/{realm}/protocol/openid-connect/token";
        private const string realmExistsPath = "/admin/realms/{realm}";
        private const string adminRealmsPath = "/admin/realms";


    }
}
