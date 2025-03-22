namespace Bb.Policies.Asts
{
    public class PolicyInclude : PolicyNamed
    {

        public PolicyInclude(string name) 
            : base(name)
        {

        }

        public override T Accept<T>(IPolicyVisitor<T> visitor)
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

            return this.Fullpath = f.FullName;

        }


        public bool IsLoaded { get; internal set; }

        public string Fullpath { get; private set; }

        public bool FileExists { get; private set; }

    }

}
