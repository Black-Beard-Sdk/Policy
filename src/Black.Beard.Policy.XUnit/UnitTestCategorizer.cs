


using Bb.Policies;
using Bb.Policies.Asts;

namespace Black.Beard.Policies.XUnit
{

    public class UnitTestCategorizer
    {

        [Fact]
        public void TestRolePolicy1()
        {

            string txt = @"
policy p1 : role=admin
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));

            Assert.True(categ.ContainsRole);
            Assert.False(categ.ContainsClaim);
            Assert.True(categ.ContainsOnlyRole);
            Assert.False(categ.ContainsOnlyClaim);

            var items = PoliciesResolveItems.Get(p.GetRule("p1"));

            Assert.True(items.Roles.Any());
            Assert.True(items.Roles.Any(c => c == "admin"));

            Assert.False(items.Claims.Any());

        }

        [Fact]
        public void TestRolePolicy2()
        {

            string txt = @"
policy p1 : role = admin & role = guest
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.True(categ.ContainsRole);
            Assert.False(categ.ContainsClaim);
            Assert.True(categ.ContainsOnlyRole);
            Assert.False(categ.ContainsOnlyClaim);
        }

        [Fact]
        public void TestRolePolicy3()
        {

            string txt = @"
policy p1 : role = admin & role != guest
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.True(categ.ContainsRole);
            Assert.False(categ.ContainsClaim);
            Assert.True(categ.ContainsOnlyRole);
            Assert.False(categ.ContainsOnlyClaim);
        }

        [Fact]
        public void TestRolePolicy4()
        {

            string txt = @"
policy p1 : (role = admin & role != guest)
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.True(categ.ContainsRole);
            Assert.False(categ.ContainsClaim);
            Assert.True(categ.ContainsOnlyRole);
            Assert.False(categ.ContainsOnlyClaim);
        }

        [Fact]
        public void TestRolePolicy5()
        {

            string txt = @"
policy p1 : !(role = admin & role != guest)
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.True(categ.ContainsRole);
            Assert.False(categ.ContainsClaim);
            Assert.True(categ.ContainsOnlyRole);
            Assert.False(categ.ContainsOnlyClaim);
        }

        [Fact]
        public void TestRolePolicy6()
        {

            string txt = @"
policy p1 : role in [admin, guest]
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.False(categ.ContainsRole);
            Assert.False(categ.ContainsClaim);
            Assert.False(categ.ContainsOnlyRole);
            Assert.False(categ.ContainsOnlyClaim);
        }

        [Fact]
        public void TestRolePolicy7()
        {

            string txt = @"
policy p1 : role !in [admin, guest]
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.False(categ.ContainsRole);
            Assert.False(categ.ContainsClaim);
            Assert.False(categ.ContainsOnlyRole);
            Assert.False(categ.ContainsOnlyClaim);
        }

        [Fact]
        public void TestRolePolicy8()
        {

            string txt = @"
policy p1 : role !has [admin, guest]
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.False(categ.ContainsRole);
            Assert.False(categ.ContainsClaim);
            Assert.False(categ.ContainsOnlyRole);
            Assert.False(categ.ContainsOnlyClaim);
        }

        [Fact]
        public void TestRolePolicy9()
        {

            string txt = @"
policy p1 : role has [admin, guest]
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.True(categ.ContainsRole);
            Assert.False(categ.ContainsClaim);
            Assert.True(categ.ContainsOnlyRole);
            Assert.False(categ.ContainsOnlyClaim);
        }

        [Fact]
        public void TestClaimPolicy1()
        {

            string txt = @"
policy p1 : car=admin
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.True(categ.ContainsClaim);
            Assert.False(categ.ContainsRole);
            Assert.True(categ.ContainsOnlyClaim);
            Assert.False(categ.ContainsOnlyRole);
        }

        [Fact]
        public void TestClaimPolicy2()
        {

            string txt = @"
policy p1 : ope = admin & role = guest
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.True(categ.ContainsClaim);
            Assert.True(categ.ContainsRole);
            Assert.False(categ.ContainsOnlyClaim);
            Assert.False(categ.ContainsOnlyRole);
        }

        [Fact]
        public void TestClaimPolicy3()
        {

            string txt = @"
policy p1 : car = admin & rule != guest
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.True(categ.ContainsClaim);
            Assert.False(categ.ContainsRole);
            Assert.True(categ.ContainsOnlyClaim);
            Assert.False(categ.ContainsOnlyRole);
        }

        [Fact]
        public void TestClaimPolicy4()
        {

            string txt = @"
policy p1 : (ope = admin & car != guest)
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.True(categ.ContainsClaim);
            Assert.False(categ.ContainsRole);
            Assert.True(categ.ContainsOnlyClaim);
            Assert.False(categ.ContainsOnlyRole);
        }

        [Fact]
        public void TestClaimPolicy5()
        {

            string txt = @"
policy p1 : !(car = admin & role != guest)
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.True(categ.ContainsClaim);
            Assert.False(categ.ContainsRole);
            Assert.False(categ.ContainsOnlyClaim);
            Assert.False(categ.ContainsOnlyRole);
        }

        [Fact]
        public void TestClaimPolicy6()
        {

            string txt = @"
policy p1 : rule in [admin, guest]
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.False(categ.ContainsClaim);
            Assert.False(categ.ContainsRole);
            Assert.False(categ.ContainsOnlyClaim);
            Assert.False(categ.ContainsOnlyRole);
        }

        [Fact]
        public void TestClaimPolicy7()
        {

            string txt = @"
policy p1 : clm !in [admin, guest]
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.False(categ.ContainsClaim);
            Assert.False(categ.ContainsRole);
            Assert.False(categ.ContainsOnlyClaim);
            Assert.False(categ.ContainsOnlyRole);
        }

        [Fact]
        public void TestClaimPolicy8()
        {

            string txt = @"
policy p1 : rul !has [admin, guest]
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.False(categ.ContainsClaim);
            Assert.False(categ.ContainsRole);
            Assert.False(categ.ContainsOnlyClaim);
            Assert.False(categ.ContainsOnlyRole);
        }

        [Fact]
        public void TestClaimPolicy9()
        {

            string txt = @"
policy p1 : usr has [admin, guest]
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p1"));
            Assert.True(categ.ContainsClaim);
            Assert.False(categ.ContainsRole);
            Assert.True(categ.ContainsOnlyClaim);
            Assert.False(categ.ContainsOnlyRole);
        }

        [Fact]
        public void TestClaimPolicy10()
        {

            string txt = @"
policy p1 : usr has [admin, guest]
policy p2 inherit p1 : usr has [admin, guest]
";
            var p = Policy.ParseText(txt);
            var categ = PoliciesCategorizer.Get(p.GetRule("p2"));
            Assert.True(categ.ContainsClaim);
            Assert.False(categ.ContainsRole);
            Assert.False(categ.ContainsOnlyClaim);
            Assert.False(categ.ContainsOnlyRole);
        }


    }


}