namespace Black.Beard.Policy.XUnit.Policies
{

    using Bb;
    using System.Text;

    public static class DirectoryIntension
    {


        public static FileInfo WriteFile(this DirectoryInfo self, string filename, string content, Encoding encoding = null)
        {
            var file = self.Combine(filename).AsFile();
            file.WriteFile(content, encoding);
            return file;

        }


        public static FileInfo WriteFile(this FileInfo self,string content, Encoding encoding = null)
        {
            File.WriteAllText(self.FullName, content, encoding ?? Encoding.UTF8);
            return self;
        }


    }



}