namespace Black.Beard.Policy.XUnit.Policies
{

    using Bb;

    public static class DirectoryIntension
    {

        public static DirectoryInfo WriteFile(this DirectoryInfo self, string filename, string  content)
        {
            var file = self.Combine(filename).AsFile();
            file.WriteAllText(content);
            return self;

        }



    }



}