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
    /// <summary>
    /// Provides methods for parsing policy scripts from various sources.
    /// </summary>
    /// <remarks>
    /// ScriptParser offers static methods to parse policy scripts from strings or files,
    /// and create abstract syntax trees or parser contexts for further processing.
    /// </remarks>
    public class ScriptParser
    {
        /// <summary>
        /// Parses a policy script string into an intellisense-compatible abstract syntax tree.
        /// </summary>
        /// <param name="text">The policy script text to parse. Must not be null.</param>
        /// <returns>An abstract syntax tree that can be used for intellisense features.</returns>
        /// <remarks>
        /// This method parses the provided script text and generates an abstract syntax tree
        /// optimized for intellisense features such as code completion and syntax highlighting.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when text is null.</exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException">Thrown when the parser encounters a syntax error.</exception>
        /// <example>
        /// <code lang="C#">
        /// string policyText = "policy IsAdult : user.age >= 18";
        /// IntellisenseAst ast = ScriptParser.EvaluateString(policyText);
        /// 
        /// // Use the AST for intellisense features
        /// var completions = ast.GetCompletionsAtPosition(10);
        /// foreach (var item in completions)
        /// {
        ///     Console.WriteLine($"Completion: {item.Label}");
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// An <see cref="IntellisenseAst"/> object representing the parsed policy script.
        /// </returns>
        public static IntellisenseAst EvaluateString(string text)
        {
            var _errors = new ScriptDiagnostics();
            var parser = ParseString(text);
            var ctx = new IntellisenseContext(parser.Parser, _errors, string.Empty);
            var tree = new IntellisenseAst(ctx, parser.Tree);
            return tree;
        }

        /// <summary>
        /// Parses a policy script file into an intellisense-compatible abstract syntax tree.
        /// </summary>
        /// <param name="text">The path to the policy script file to parse. Must not be null or empty.</param>
        /// <returns>An abstract syntax tree that can be used for intellisense features.</returns>
        /// <remarks>
        /// This method loads the policy script from the specified file path and generates
        /// an abstract syntax tree optimized for intellisense features.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when text is null.</exception>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when the specified file cannot be found.</exception>
        /// <exception cref="System.IO.IOException">Thrown when an I/O error occurs while reading the file.</exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException">Thrown when the parser encounters a syntax error.</exception>
        /// <example>
        /// <code lang="C#">
        /// string filePath = @"C:\Policies\access_policy.txt";
        /// IntellisenseAst ast = ScriptParser.EvaluatePath(filePath);
        /// 
        /// // Use the AST for intellisense features
        /// var diagnostics = ast.Context.Diagnostics;
        /// foreach (var diagnostic in diagnostics)
        /// {
        ///     Console.WriteLine($"{diagnostic.Message} at {diagnostic.Location}");
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// An <see cref="IntellisenseAst"/> object representing the parsed policy script.
        /// </returns>
        public static IntellisenseAst EvaluatePath(string text)
        {
            var _errors = new ScriptDiagnostics();
            var parser = ParsePath(text);
            var ctx = new IntellisenseContext(parser.Parser, _errors, string.Empty);
            var tree = new IntellisenseAst(ctx, parser.Tree);
            return tree;
        }

        /// <summary>
        /// Parses a policy script string into a parser context.
        /// </summary>
        /// <param name="source">The policy script text to parse. Must not be null.</param>
        /// <returns>A script parser base containing the parser and parse tree.</returns>
        /// <remarks>
        /// This method parses the provided script text and generates a parser context
        /// that can be used for further processing, such as building an AST or evaluating the script.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when source is null.</exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException">Thrown when the parser encounters a syntax error.</exception>
        /// <example>
        /// <code lang="C#">
        /// string policyText = "policy IsAdult : user.age >= 18";
        /// var parserContext = ScriptParser.ParseString(policyText);
        /// 
        /// // Access the parse tree
        /// var tree = parserContext.Tree;
        /// 
        /// // Use the parser for further processing
        /// var visitor = new ScriptBuilderVisitor(parserContext.Parser, new ScriptDiagnostics(), new PolicyContainer(), null);
        /// visitor.Visit(tree);
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="ScriptParserBase{TParser, TContext}"/> object containing the parser and parse tree.
        /// </returns>
        public static ScriptParserBase<PolicyParser, PolicyParser.ScriptContext> ParseString(string source)
        {
            return ScriptParserBase<PolicyParser, PolicyParser.ScriptContext>.ParseString(creator, _func, source);
        }

        /// <summary>
        /// Parses a policy script from a StringBuilder into a parser context.
        /// </summary>
        /// <param name="source">The StringBuilder containing the policy script text to parse. Must not be null.</param>
        /// <param name="sourceFile">Optional name of the source file for error reporting.</param>
        /// <param name="output">Optional TextWriter for outputting parser messages. If null, messages are discarded.</param>
        /// <param name="outputError">Optional TextWriter for outputting parser error messages. If null, error messages are discarded.</param>
        /// <returns>A script parser base containing the parser and parse tree.</returns>
        /// <remarks>
        /// This method parses the provided script text from a StringBuilder and generates a parser context
        /// that can be used for further processing. It allows specifying output streams for parser messages.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when source is null.</exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException">Thrown when the parser encounters a syntax error.</exception>
        /// <example>
        /// <code lang="C#">
        /// var sb = new StringBuilder();
        /// sb.AppendLine("policy IsAdult : user.age >= 18");
        /// sb.AppendLine("policy HasPermission : user.roles has 'admin'");
        /// 
        /// var output = new StringWriter();
        /// var errorOutput = new StringWriter();
        /// var parserContext = ScriptParser.ParseString(sb, "policies.txt", output, errorOutput);
        /// 
        /// // Check for parser messages
        /// string messages = output.ToString();
        /// string errors = errorOutput.ToString();
        /// 
        /// // Process the parse tree
        /// var visitor = new ScriptBuilderVisitor(parserContext.Parser, new ScriptDiagnostics(), new PolicyContainer(), null);
        /// visitor.Visit(parserContext.Tree);
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="ScriptParserBase{TParser, TContext}"/> object containing the parser and parse tree.
        /// </returns>
        public static ScriptParserBase<PolicyParser, PolicyParser.ScriptContext> ParseString(StringBuilder source, string sourceFile = "", TextWriter output = null, TextWriter outputError = null)
        {
            return ScriptParserBase<PolicyParser, PolicyParser.ScriptContext>.ParseString(creator, _func, source, sourceFile, output, outputError);
        }

        /// <summary>
        /// Parses a policy script from a file into a parser context.
        /// </summary>
        /// <param name="source">The path to the policy script file to parse. Must not be null or empty.</param>
        /// <param name="output">Optional TextWriter for outputting parser messages. If null, messages are discarded.</param>
        /// <param name="outputError">Optional TextWriter for outputting parser error messages. If null, error messages are discarded.</param>
        /// <returns>A script parser base containing the parser and parse tree.</returns>
        /// <remarks>
        /// This method loads the policy script from the specified file path and generates a parser context
        /// that can be used for further processing. It allows specifying output streams for parser messages.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when source is null.</exception>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when the specified file cannot be found.</exception>
        /// <exception cref="System.IO.IOException">Thrown when an I/O error occurs while reading the file.</exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException">Thrown when the parser encounters a syntax error.</exception>
        /// <example>
        /// <code lang="C#">
        /// string filePath = @"C:\Policies\access_policy.txt";
        /// var output = new StringWriter();
        /// var errorOutput = new StringWriter();
        /// 
        /// var parserContext = ScriptParser.ParsePath(filePath, output, errorOutput);
        /// 
        /// // Check for parser messages
        /// string messages = output.ToString();
        /// string errors = errorOutput.ToString();
        /// 
        /// // Process the parse tree
        /// var container = new PolicyContainer();
        /// var visitor = new ScriptBuilderVisitor(parserContext.Parser, new ScriptDiagnostics(), container, null, filePath);
        /// visitor.Visit(parserContext.Tree);
        /// 
        /// // Evaluate a rule from the parsed policy
        /// var evaluator = new PolicyEvaluator(container);
        /// var user = new { age = 25, roles = new[] { "user", "editor" } };
        /// bool result = evaluator.Evaluate("IsAdult", user, out var evalContext);
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="ScriptParserBase{TParser, TContext}"/> object containing the parser and parse tree.
        /// </returns>
        public static ScriptParserBase<PolicyParser, PolicyParser.ScriptContext> ParsePath(string source, TextWriter output = null, TextWriter outputError = null)
        {
            return ScriptParserBase<PolicyParser, PolicyParser.ScriptContext>.ParsePath(creator, _func, source, output, outputError);
        }

        /// <summary>
        /// Factory function that creates a PolicyParser for a given character stream.
        /// </summary>
        /// <remarks>
        /// This delegate is used by the parsing methods to create a PolicyParser from a character stream.
        /// It sets up the lexer and token stream, and configures the parser with appropriate options.
        /// </remarks>
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

        /// <summary>
        /// Function that gets the root context from a PolicyParser.
        /// </summary>
        /// <remarks>
        /// This delegate is used by the parsing methods to extract the script context from a PolicyParser.
        /// </remarks>
        private static Func<PolicyParser, PolicyParser.ScriptContext> _func = (parser) => parser.script();

    }

}
