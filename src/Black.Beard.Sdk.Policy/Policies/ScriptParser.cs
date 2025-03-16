using Antlr4.Runtime;
using Bb.Analysis.DiagTraces;
using Bb.Policies.Asts;
using Bb.Policies.Parser;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Bb.Policies
{

    public class ScriptParser
    {


        /// <summary>
        /// Evaluate text value
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IntellisenseAst EvaluateString(string text)
        {
            var _errors = new ScriptDiagnostics();
            var parser = ParseString(text);
            var ctx = new IntellisenseContext(parser.Parser, _errors, string.Empty);
            var tree = new IntellisenseAst(ctx, parser.Tree);
            return tree;
        }

        /// <summary>
        /// Evaluate text value
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IntellisenseAst EvaluatePath(string text)
        {
            var _errors = new ScriptDiagnostics();
            var parser = ParsePath(text);
            var ctx = new IntellisenseContext(parser.Parser, _errors, string.Empty);
            var tree = new IntellisenseAst(ctx, parser.Tree);
            return tree;
        }


        public static ScriptParserBase<PolicyParser, PolicyParser.ScriptContext> ParseString(string source)
        {
            return ScriptParserBase<PolicyParser, PolicyParser.ScriptContext>.ParseString(creator, _func, source);
        }

        public static ScriptParserBase<PolicyParser, PolicyParser.ScriptContext> ParseString(StringBuilder source, string sourceFile = "", TextWriter output = null, TextWriter outputError = null)
        {
            return ScriptParserBase<PolicyParser, PolicyParser.ScriptContext>.ParseString(creator, _func, source, sourceFile, output, outputError);
        }


        /// <summary>
        /// Load specified document in a dedicated parser
        /// </summary>
        /// <param name="source"></param>
        /// <param name="output"></param>
        /// <param name="outputError"></param>
        /// <returns></returns>
        public static ScriptParserBase<PolicyParser, PolicyParser.ScriptContext> ParsePath(string source, TextWriter output = null, TextWriter outputError = null)
        {
            return ScriptParserBase<PolicyParser, PolicyParser.ScriptContext>.ParsePath(creator, _func, source, output, outputError);
        }


        private static Func<ScriptParserBase<PolicyParser, PolicyParser.ScriptContext>, ICharStream, PolicyParser> creator = (script, stream) =>
        {

            var lexer = new PolicyLexer(stream, script.Output, script.OutputError);
            var token = new CommonTokenStream(lexer);
            var parser = new PolicyParser(token)
            {
                BuildParseTree = true,
                //Trace = ScriptParser.Trace, // Ca plante sur un null, pourquoi ?
            };

            return parser;
        };
        private static Func<PolicyParser, PolicyParser.ScriptContext> _func = (parser) => parser.script();

    }

}
