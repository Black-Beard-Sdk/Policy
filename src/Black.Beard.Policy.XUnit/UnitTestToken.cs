


using Bb.Policies.Asts;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Black.Beard.Policies.XUnit
{


    public class UnitTestToken
    {

    
        [Fact]
        public void TestPolicy13()
        {

            string policyPayload = @"
alias role : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role""
policy p1 : role has [admin]
";
            PolicyContainer p = Policy.Evaluate(policyPayload);



            var jwt =new JwtHelper()
                .WithRsa(2048)
                .WithAudience("myappli")
                .WithIssuer("Black.Beard")
                .WithClaimMail("gaelgael5@gmail.com")
                .WithClaimJti()
                .WithRole("Admin")
                ;
            var token = jwt.Generate();
            var principal = jwt.Validate(token, out SecurityToken validatedToken);





            //if (principal is ClaimsPrincipal p)
            //{

            //    p.IsInRole("");


            //    foreach (Claim item in p.Claims)
            //    {
                    
            //    }

            //    foreach (var item in p.Identities)
            //    {

            //    }

            //}
         

        }


    }


}