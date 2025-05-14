//using Microsoft.Extensions.Logging;
//using System;
//using System.Net;
//using System.Threading.Tasks;
//using Keycloak.AuthServices.Authentication;
//using Bb.Keycloak;
//using System.Xml.Schema;
//using Testcontainers.Keycloak;

//namespace Policy.Tests
//{
//    public class KeycloakTests
//    {



//        //  http://192.168.10.3:8080
//        //  /realms/{realm}/protocol/openid-connect/token

//        private const string realm = "dev1";
//        private const string KeycloakUrl = "http://192.168.10.3/auth/realms/Black.Beard.Dev/protocol/openid-connect/token";
//        private const string ClientId = "unit_tests";
//        private const string ClientSecret = "lWpRvc0IRssa3s1ldH3C12j3cCpJ85KF"; // Replace with your client secret

//        [Fact]
//        public async Task TestKeycloakConnection()
//        {

//            string username = "root";
//            string password = "Password123";


//            var container = new KeycloakBuilder().Build();

//            var config = new KeycloakConfiguration(username, password);
//            var container1 = new KeycloakContainer(config);
//            var server = container
//                .StartAsync()
//                .ConfigureAwait(false)
//            ;

//            //server.Wait(s => !s.IsStarted());


//            string url = "http://192.168.10.3:8080";


//            using (var cnx = await new Connector(url)
//                .Connect(username, password))
//                if (await cnx.RealmExists(realm) || await cnx.CreateRealmAsync(realm))
//                {

                    
//                }

//        }


//    }

//    public static class TaskExtensions
//    {

//        public static bool IsStarted(this Task self)
//        {

//            if (self == null)
//                throw new ArgumentNullException(nameof(self));
//            return self.Status == TaskStatus.RanToCompletion || self.Status == TaskStatus.Faulted || self.Status == TaskStatus.Canceled;

//        }

//        public static T Wait<T>(this T self, Func<T, bool> test, TimeSpan? waitMax = null)
//        {

//            if (test == null)
//                throw new ArgumentNullException(nameof(test));

//            if (!waitMax.HasValue)
//                waitMax = TimeSpan.FromSeconds(30);

//            var m = DateTime.Now.Add(waitMax.Value);

//            while (!test(self) && m > DateTime.Now)
//            {
//                Task.Yield();
//                Thread.Sleep(100);
//            }

//            return self;

//        }

//    }


//}


