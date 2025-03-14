using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Bb.Policies.Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bb.Policies
{


    public class ScriptParserBase<TParser, T> where TParser : Antlr4.Runtime.Parser where T : ParserRuleContext
    {

        public ScriptParserBase(TextWriter output, TextWriter outputError, StringBuilder content, Func<ScriptParserBase<TParser, T>, ICharStream, TParser> creator, Func<TParser, T> getRoot)
        {

            Output = output ?? Console.Out;
            OutputError = outputError ?? Console.Error;
            _includes = new HashSet<string>();
            Content = content;
            Crc = content.CalculateCrc32();
            _creator = creator;
            _getRoot = getRoot;
        }

        public static ScriptParserBase<TParser, T> ParseString(
            Func<ScriptParserBase<TParser, T>, ICharStream, TParser> creator,
            Func<TParser, T> funcGetContext,
            string source)
        {

            ICharStream stream = CharStreams.fromString(source);
            var parser = new ScriptParserBase<TParser, T>(null, null, new StringBuilder(source), creator, funcGetContext)
            {
                File = string.Empty,
            };

            parser.Initialize(stream);
            return parser;

        }

        public static ScriptParserBase<TParser, T> ParseString(
            Func<ScriptParserBase<TParser, T>, ICharStream, TParser> creator,
            Func<TParser, T> funcGetContext, 
            StringBuilder source, string sourceFile = "", TextWriter output = null, TextWriter outputError = null)
        {

            ICharStream stream = CharStreams.fromString(source.ToString());
            var parser = new ScriptParserBase<TParser, T>(output, outputError, source, creator, funcGetContext)
            {
                File = sourceFile ?? string.Empty,
            };
            parser.Initialize(stream);
            return parser;

        }

        public static ScriptParserBase<TParser, T> ParsePath(
            Func<ScriptParserBase<TParser, T>, ICharStream, TParser> creator,
            Func<TParser, T> funcGetContext, 
            string source, TextWriter output = null, TextWriter outputError = null)
        {

            var payload = source.LoadFromFile();
            ICharStream stream = CharStreams.fromString(payload.ToString());
            var parser = new ScriptParserBase<TParser, T>(output, outputError, new StringBuilder(source), creator, funcGetContext)
            {
                File = source,
            };

            parser.Initialize(stream);

            return parser;

        }

        public void Initialize(ICharStream stream)
        {
            _parser = Create(stream);
            _context = _getRoot(_parser);
        }

        protected TParser Create(ICharStream stream) => _creator(this, stream);

        public static bool Trace { get; set; }

        public IEnumerable<string> Includes { get => _includes; }

        public string File { get; set; }

        public StringBuilder Content { get; }

        public TextWriter Output { get; }

        public TextWriter OutputError { get; }

        public T Tree { get { return _context; } }

        public uint Crc { get; }

        public TParser Parser { get => _parser; }

        public bool InError { get => _parser.ErrorListeners.Count > 0; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object? Visit<Result>(IParseTreeVisitor<Result> visitor)
        {

            if (System.Diagnostics.Debugger.IsAttached && !string.IsNullOrEmpty(File))
                System.Diagnostics.Trace.WriteLine(File);

            var context = _context;
            return visitor.Visit(context);

        }

        protected TParser _parser;
        private readonly HashSet<string> _includes;
        protected readonly Func<ScriptParserBase<TParser, T>, ICharStream, TParser> _creator;
        private readonly Func<TParser, T> _getRoot;
        protected T _context;


    }

}
