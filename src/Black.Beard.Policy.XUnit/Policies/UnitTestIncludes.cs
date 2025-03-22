namespace Black.Beard.Policy.XUnit.Policies
{

    using Bb;
    using Bb.Policies.Asts;

    public class UnitTestIncludes
    {


        [Fact]
        public void TestPolicy0()
        {

            var folder = Path.GetTempPath()
                .Combine(Path.GetFileNameWithoutExtension(Path.GetTempFileName()));
            var dir = folder.AsDirectory();
            dir.Create();

            dir.WriteFile("base.policies.txt", @"
alias role2 : ""http://schemas.microsoft.com/ws/2008/06/identity/claims/role2""
");

            var file = dir.WriteFile("rules.policies.txt", @"
include ""base.policies.txt""
policy p1 : role2 has [Admin]
");

            PolicyContainer policies = Policy.ParsePath(file.FullName);

            dir.DeleteFolderIfExists(true);


            if (!policies.Diagnostics.Success)
                throw new Exception("Failed to evaluate policies");

            var alias = policies.ResolveVariable("role2", out string value);



            Assert.True(alias);

        }


    }



}