namespace Black.Beard.Policy.XUnit.Policies
{

    using Bb.Policies.Asts;

    public class UnitTestParser
    {

        [Fact]
        public void TestAlias()
        {

            string txt = @"
alias role2 : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role2""
";
            var p = Policy.ParseText(txt);

            var o = p.ToString().Trim();
            Assert.Equal("alias role2 : \"http://schemas.microsoft.com/ws/2008/06/identity/claims/role2\"", o);

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
policy p1 : p1 & source.name = test
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
            string txt = @"
policy isAuthenticated : Identity.IsAuthenticated
policy p1 : isAuthenticated & Identity.IsAuthenticated = true
";
            var p = Policy.ParseText(txt);

            var r1 = p.GetRule("isAuthenticated");
            var txt1 = r1.ToString().Trim();
            Assert.Equal("policy isAuthenticated : Identity.IsAuthenticated", txt1);

            var r2 = p.GetRule("p1");
            var txt2 = r2.ToString().Trim();
            Assert.Equal("policy p1 : isAuthenticated & Identity.IsAuthenticated = true", txt2);

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

        [Fact]
        public void TestPolicy180()
        {
            string txt = @"policy p1 : Source.Age = 18";
            var p = Policy.ParseText(txt);
            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : Source.Age = 18", o);

        }

        [Fact]
        public void TestPolicy181()
        {
            string txt = @"policy p1 : Source.Age > 18";
            var p = Policy.ParseText(txt);
            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : Source.Age > 18", o);

        }

        [Fact]
        public void TestPolicy182()
        {
            string txt = @"policy p1 : Source.Age >= 18";
            var p = Policy.ParseText(txt);
            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : Source.Age >= 18", o);

        }

        [Fact]
        public void TestPolicy183()
        {
            string txt = @"policy p1 : Source.Age < 18";
            var p = Policy.ParseText(txt);
            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : Source.Age < 18", o);

        }

        [Fact]
        public void TestPolicy184()
        {
            string txt = @"policy p1 : Source.Age <= 18";
            var p = Policy.ParseText(txt);
            var o = p.ToString().Trim();
            Assert.Equal("policy p1 : Source.Age <= 18", o);
        }

    }


}