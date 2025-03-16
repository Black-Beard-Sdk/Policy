


using Bb.Policies;
using Bb.Policies.Asts;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Black.Beard.Policies.XUnit
{


    public class UnitTestToken
    {


        [Fact]
        public void TestPolicy1()
        {

            string policyPayload = @"
alias role : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role""
policy p1 : role has [Admin]
";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "Admin"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.True(result);

        }


        [Fact]
        public void TestPolicy2()
        {

            string policyPayload = @"
alias role : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role""
policy p1 : role = admin & role = guest
";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "admin"), new Claim("role", "guest"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.True(result);

        }




        public static PolicyEvaluator GetEvaluator(string policyPayload)
        {

            PolicyContainer policies = Policy.ParseText(policyPayload);
            if (!policies.Diagnostics.Success)
                throw new Exception("Failed to evaluate policies");

            var e = new PolicyEvaluator(policies);

            return e;

        }

        public static IPrincipal GetPrincipal(params Claim[] claims)
        {

            var jwt = new JwtHelper()
              .WithRsa(2048)
              .WithAudience("myappli")
              .WithIssuer("Black.Beard")
              .WithClaimMail("gaelgael5@gmail.com")
              .WithClaimJti()
              ;

            jwt.WithClaims(claims);

            var token = jwt.Generate();
            var principal = jwt.Validate(token, out SecurityToken validatedToken);

            return principal;

        }

    }


}