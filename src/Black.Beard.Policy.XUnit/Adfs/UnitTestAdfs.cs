
using Bb.Adfs;
using Microsoft.Extensions.Logging;

namespace Black.Beard.Policy.XUnit.Adfs
{

    public class UnitTestAdfs
    {


        //[Fact]
        //public void TestGenerateApiKey()
        //{
        //    var apiKey = ApiKeyGenerator.GenerateApiKey(100)
        //                    .GenerateIdentifiers(25, 35, "mysalt");

        //    Assert.NotNull(apiKey);
        //}

        [Fact]
        public void TestCreateApiOnAdfs()
        {

            string salt = "my_salt";
            string firstname = "firstname";
            string lastname = "lastname";
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
                    var user = adfs.CreateApiKey(salt, firstname, lastname, email, organisation, "group1");
                }
                else
                    Assert.True(false, "Connection failed");

            }

        }

    }


}