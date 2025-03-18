using Bb.Policies.Asts;

namespace Black.Beard.Policies.XUnit
{


    public class UnitTestParser
    {

        [Fact]
        public void TestAlias()
        {

            string txt = @"
alias role : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role""
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("alias role : \"http://schemas.microsoft.com/ws/2008/06/identity/claims/role\"", o);

        }

        [Fact]
        public void TestPolicy1()
        {

            string txt = @"
policy p1 : role=admin
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : role = admin", o);

        }

        [Fact]
        public void TestPolicy2()
        {

            string txt = @"
policy p1 : role = admin & role = guest
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : role = admin & role = guest", o);

        }

        [Fact]
        public void TestPolicy2bis()
        {

            string txt = @"
policy p1' : role = admin & role = guest
";

            var p = Policy.EvaluateTextForIntellisense(txt);
            var items = p.Select("policy_id").ToList();
            Assert.True(items.Any());
        }

        [Fact]
        public void TestPolicy3()
        {

            string txt = @"
policy p1 : role = admin & role != guest
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : role = admin & role != guest", o);

        }

        [Fact]
        public void TestPolicy4()
        {

            string txt = @"
policy p1 : (role = admin & role != guest)
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : (role = admin & role != guest)", o);

        }

        [Fact]
        public void TestPolicy5()
        {

            string txt = @"
policy p1 : !(role = admin & role != guest)
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : !(role = admin & role != guest)", o);

        }

        [Fact]
        public void TestPolicy6()
        {

            string txt = @"
policy p1 : role in [admin, guest]
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : role in [admin, guest]", o);

        }

        [Fact]
        public void TestPolicy7()
        {

            string txt = @"
policy p1 : role !in [admin, guest]
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : role !in [admin, guest]", o);

        }

        [Fact]
        public void TestPolicy8()
        {

            string txt = @"
policy p1 : role !has [admin, guest]
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : role !has [admin, guest]", o);

        }

        [Fact]
        public void TestPolicy9()
        {

            string txt = @"
policy p1 : role has [admin, guest]
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : role has [admin, guest]", o);

        }                      

        [Fact]
        public void TestPolicy10()
        {

            string txt = @"
policy p1 : source.name = test
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : source.name = test", o);

        }

        [Fact]
        public void TestPolicy13()
        {
            string txt = @"
policy p1 : role=Admin
policy p1 inherit p1 : source.name = test
";
            var p = Policy.ParseText(txt);
            Assert.True(p.Diagnostics.InError);
        }

        [Fact]
        public void TestPolicy14()
        {

            string txt = @"policy p1 : Identity.IsAuthenticated = true";
            var p = Policy.ParseText(txt);
            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : Identity.IsAuthenticated = true", o);

            txt = @"policy p1 : Identity.IsAuthenticated = false";
            p = Policy.ParseText(txt);
            o = p.ToString().Trim();
            Assert.Equal("policy p1 : Identity.IsAuthenticated = false", o);

        }

        [Fact]
        public void TestPolicy15()
        {
            string txt = @"policy p1 inherit p1 : Identity.IsAuthenticated = true";
            var p = Policy.ParseText(txt);
            Assert.True(p.Diagnostics.InError);
        }

        [Fact]
        public void TestPolicy16()
        {
            string txt = @"policy p1 : carr+";
            var p = Policy.ParseText(txt);
            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : carr+", o);

        }

        [Fact]
        public void TestPolicy17()
        {
            string txt = @"policy p1 : carr+ | ope+ | pkt+";
            var p = Policy.ParseText(txt);
            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : carr+ | ope+ | pkt+", o);

        }

    }


}