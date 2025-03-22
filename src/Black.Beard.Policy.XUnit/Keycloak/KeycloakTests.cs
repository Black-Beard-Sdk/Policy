using Bb.Adfs;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using Keycloak.AuthServices.Authentication;

namespace Policy.Tests
{
    public class KeycloakTests
    {
        private const string realm = "Black.Beard.Dev";
        private const string KeycloakUrl = "http://192.168.10.3/auth/realms/Black.Beard.Dev/protocol/openid-connect/token";
        private const string url = "http://192.168.10.3:8080";
        private const string Username = "gaelgael5";
        private const string Password = "password123";
        private const string ClientId = "unit_tests";
        private const string ClientSecret = "lWpRvc0IRssa3s1ldH3C12j3cCpJ85KF"; // Replace with your client secret

        [Fact]
        public async Task TestKeycloakConnection()
        {


            //ILogger logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<AdfsConnection>();
            //var keycloak = new KeycloakConnection(new HttpClient(), logger);
            //var result = await keycloak.ConnectAsync(url, realm, ClientId, ClientSecret);





        }
    }

}
