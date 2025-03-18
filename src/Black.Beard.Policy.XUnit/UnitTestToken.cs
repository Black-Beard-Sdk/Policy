


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
        public void TestPolicy0()
        {

            string policyPayload = @"
alias role : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role""
policy p1 : role has [Admin]
";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "Admin"));

            var result = e.Evaluate("p1", principal, out var ctx);
            Assert.True(result);

        }

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

        [Fact]
        public void TestPolicy3()
        {

            string policyPayload = @"
alias role : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role""
policy p1 : role = admin & role = guest
";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "admin"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.False(result);

        }

        [Fact]
        public void TestPolicy4()
        {

            string policyPayload = @"policy p1 : role = admin & role != guest";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "admin"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.True(result);

        }

        [Fact]
        public void TestPolicy5()
        {

            string policyPayload = @"policy p1 : (role = admin & role != guest)";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "admin"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.True(result);

        }

        [Fact]
        public void TestPolicy6()
        {

            string policyPayload = @"policy p1 : !(role = admin & role != guest)";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "admin"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.False(result);

        }

        [Fact]
        public void TestPolicy71()
        {

            string policyPayload = @"policy p1 : role in [admin, guest]";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "admin"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.True(result);

        }

        [Fact]
        public void TestPolicy72()
        {

            string policyPayload = @"policy p1 : role in [admin, guest]";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "guest"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.True(result);

        }

        [Fact]
        public void TestPolicy73()
        {

            string policyPayload = @"policy p1 : role in [admin, guest]";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "guest1"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.False(result);

        }

        [Fact]
        public void TestPolicy74()
        {

            string policyPayload = @"policy p1 : role !in [admin, guest]";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "guest1"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.True(result);

        }

        [Fact]
        public void TestPolicy75()
        {

            string policyPayload = @"policy p1 : role !in [admin, guest]";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "guest"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.False(result);

        }

        [Fact]
        public void TestPolicy81()
        {

            string policyPayload = @"policy p1 : role !has [admin, guest]";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "guest"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.False(result);

        }

        [Fact]
        public void TestPolicy82()
        {

            string policyPayload = @"policy p1 : role !has [admin, guest]";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "guest1"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.True(result);

        }

        [Fact]
        public void TestPolicy91()
        {

            string policyPayload = @"policy p1 : role has [admin, guest]";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "guest"), new Claim("role", "admin"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.True(result);

        }

        [Fact]
        public void TestPolicy92()
        {

            string policyPayload = @"policy p1 : role has [admin, guest]";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "guest"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.False(result);

        }

        [Fact]
        public void TestPolicy101()
        {

            string policyPayload = @"policy p1 : Source.Name = test";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "guest"));

            var result = e.Evaluate("p1", new { Principal = principal, Source = new { Name = "test" } }, out var ctx);
            Assert.True(result);

        }

        [Fact]
        public void TestPolicy102()
        {

            string policyPayload = @"policy p1 : Source.Name = test";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "guest"));

            var result = e.Evaluate("p1", new { Principal = principal, Source = new { Name = "test1" } }, out var ctx);
            Assert.False(result);

        }

        [Fact]
        public void TestPolicy103()
        {

            string policyPayload = @"policy p1 : Source.Name = test";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "guest"));

            var result = e.Evaluate("p1", new { Principal = principal }, out var ctx);
            Assert.False(result);

        }

        [Fact]
        public void TestPolicy130()
        {

            string policyPayload = @"
policy p1 : role=Admin
policy p2 inherit p1 : Source.Name = test
";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "Admin"));

            var result = e.Evaluate("p2", new { Principal = principal, Source = new { Name = "test" } }, out var ctx);
            Assert.True(result);

        }

        [Fact]
        public void TestPolicy131()
        {

            string policyPayload = @"
policy p1 : role=Admin
policy p2 inherit p1 : Source.Name = test
";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "admin"));

            var result = e.Evaluate("p2", new { Principal = principal, Source = new { Name = "test" } }, out var ctx);
            Assert.False(result);

        }

        [Fact]
        public void TestPolicy132()
        {

            string policyPayload = @"
policy p1 : role=Admin
policy p2 inherit p1 : Source.Name = test
";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("role", "Admin"));

            var result = e.Evaluate("p2", new { Principal = principal, Source = new { Name = "test1" } }, out var ctx);
            Assert.False(result);

        }

        [Fact]
        public void TestPolicy160()
        {
            string policyPayload = @"policy p1 : carr+ | ope+ | pkt+";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal(new Claim("pkt", "cc1"));
            var result = e.Evaluate("p1", principal, out var ctx);
            Assert.True(result);
        }

        [Fact]
        public void TestPolicy140()
        {

            string policyPayload = @"policy p1 inherit p1 : Identity.IsAuthenticated = true";
            var e = GetEvaluator(policyPayload);
            var principal = GetPrincipal();

            var result = e.Evaluate("p1", principal, out var ctx);
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