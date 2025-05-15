

// Ignore Spelling: Adfs Api

using Bb.Adfs;
using Microsoft.Extensions.Logging;
using System.Runtime.Versioning;

namespace Black.Beard.Policy.XUnit.Adfs
{

    [SupportedOSPlatform("windows")]
    public class UnitTestAdfs
    {

        [Fact]
        public void TestCreateApiOnAdfs()
        {

            string salt = "my_salt";
            string firstname = "firstname";
            string lastname = "lastname";
            string username = "username";
            string email = "gaelgael5@gmail.com";
            string organisation = "black.beard";

            string adfsServer = "adfs.company.com";
            string adfsDomain = "DC=company,DC=com";
            string adfsUser = "administrator";
            string adfsPassword = "password123";


            ILogger logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<AdfsConnection>();
            using (var adfs = new AdfsConnection(logger))
            {
                if (adfs.Connect(adfsServer, adfsDomain, adfsUser, adfsPassword))
                {
                    adfs.CreateApiKey(salt, username, firstname, lastname, email, organisation, "group1");
                }
                else
                    Assert.Fail("Connection failed");
            }

        }

    }


}