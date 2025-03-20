


using Bb.Policies;
using Bb.Policies.Asts;
using Black.Beard.Adfs;
using Black.Beard.Policies.XUnit;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Principal;

namespace Black.Beard.Policy.XUnit.Adfs
{

    public class UnitTestAdfs
    {


        [Fact]
        public void TestGenerateApiKey()
        {


            var apiKey = ApiKeyGenerator.GenerateApiKey()
                            .GenerateIdentifiers("mysalt");

            Assert.NotNull(apiKey);

        }

        [Fact]
        public void TestCreateUser()
        {

            string salt = "salt";
            string firstname = "firstname";
            string lastname = "lastname";
            string email = "email";
            string organisation = "organisation";


            using (var adfs = new AdfsConnection("myserver", "mydomain", "myuser", "mypassword"))
            {

                adfs.Connect();

                var user = adfs.CreateApikey(salt, firstname, lastname, email, organisation, "group1");


            }


        }


    }


}