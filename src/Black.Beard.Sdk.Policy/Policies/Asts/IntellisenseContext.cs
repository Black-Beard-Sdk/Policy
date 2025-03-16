using Bb.Analysis.DiagTraces;

namespace Bb.Policies.Asts
{
    public struct IntellisenseContext
    {

        public static readonly IntellisenseContext Default = new IntellisenseContext();

        public IntellisenseContext()
        {
            Parser = default;
            Diagnostics = default;
            ScriptPath = default;
            NotNull = false;
        }

        public IntellisenseContext(Antlr4.Runtime.Parser parser, ScriptDiagnostics diagnostics, string path)
        {
            Parser = parser;
            Diagnostics = diagnostics;
            ScriptPath = path;
            NotNull = true;
        }

        public Antlr4.Runtime.Parser Parser { get; }

        public ScriptDiagnostics Diagnostics { get; }

        public string ScriptPath { get; }

        public bool NotNull { get; }

    }

}
