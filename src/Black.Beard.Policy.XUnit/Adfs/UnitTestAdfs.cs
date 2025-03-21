


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


            var apiKey = ApiKeyGenerator.GenerateApiKey(100)
                            .GenerateIdentifiers(25, 35, "mysalt");

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


            using (var adfs = new AdfsConnection("myserver", "mydomain", "myuser"))
            {

                if (adfs.Connect("mypassword"))
                {

                    var user = adfs.CreateApiKey(salt, firstname, lastname, email, organisation, "group1");

                }
                else
                {
                    Assert.True(false, "Connection failed");
                }

            }

        }


    }


}