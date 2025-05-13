// Ignore Spelling: Asts

namespace Bb.Policies.Asts
{
    public class PolicyInclude : PolicyNamed
    {

        public PolicyInclude(string name) 
            : base(name)
        {
            FullPath = string.Empty;
        }

        public override T? Accept<T>(IPolicyVisitor<T> visitor)
            where T : default
        {
            return default(T);
        }

        public override bool ToString(Writer writer)
        {

            return false;

        }


        public string ResolveLocation(string folder)
        {

            var file = this.Name;
            if (!file.DirectoryPathIsAbsolute())
                file = folder.Combine(file);

            var f = file.AsFile();

            this.FileExists = f.Exists;

            return this.FullPath = f.FullName;

        }


        public bool IsLoaded { get; internal set; }

        public string FullPath { get; private set; }

        public bool FileExists { get; private set; }

    }

}
