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

    /// <summary>
    /// Base class for parsers that process script files or strings using ANTLR.
    /// </summary>
    /// <typeparam name="TParser">The type of parser to create.</typeparam>
    /// <typeparam name="T">The type of parser rule context to use.</typeparam>
    /// <remarks>
    /// ScriptParserBase provides a foundation for creating parsers that can process script content
    /// from various sources like strings, files, or StringBuilders. It handles the setup of
    /// ANTLR parsers and provides access to the resulting parse tree.
    /// </remarks>
    public class ScriptParserBase<TParser, T> where TParser : Antlr4.Runtime.Parser where T : ParserRuleContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptParserBase{TParser, T}"/> class.
        /// </summary>
        /// <param name="output">The text writer for outputting parser messages. If null, Console.Out is used.</param>
        /// <param name="outputError">The text writer for outputting parser error messages. If null, Console.Error is used.</param>
        /// <param name="content">The content to be parsed as a StringBuilder. Must not be null.</param>
        /// <param name="creator">Function to create a parser instance from a character stream. Must not be null.</param>
        /// <param name="getRoot">Function to get the root context from a parser. Must not be null.</param>
        /// <remarks>
        /// This constructor initializes a parser with the specified output streams and content,
        /// and calculates a CRC32 checksum for the content for change detection.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when content, creator, or getRoot is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var content = new StringBuilder("policy IsAdult : Identity.age >= 18");
        /// var output = new StringWriter();
        /// var errorOutput = new StringWriter();
        /// 
        /// var parser = new ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;(
        ///     output, 
        ///     errorOutput, 
        ///     content,
        ///     (script, stream) => {
        ///         var lexer = new PolicyLexer(stream);
        ///         var tokens = new CommonTokenStream(lexer);
        ///         return new PolicyParser(tokens);
        ///     },
        ///     parser => parser.script()
        /// );
        /// </code>
        /// </example>
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

        /// <summary>
        /// Parses a string containing script content.
        /// </summary>
        /// <param name="creator">Function to create a parser instance from a character stream. Must not be null.</param>
        /// <param name="funcGetContext">Function to get the root context from a parser. Must not be null.</param>
        /// <param name="source">The script content as a string. Must not be null.</param>
        /// <returns>A ScriptParserBase instance with the parsed content.</returns>
        /// <remarks>
        /// This method creates a parser for the specified script content string and parses it.
        /// It uses default output streams (Console.Out and Console.Error) for parser messages.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when any parameter is null.</exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException">Thrown when the parser encounters a syntax error.</exception>
        /// <example>
        /// <code lang="C#">
        /// string policyText = "policy IsAdult : Identity.age >= 18";
        /// 
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     (script, stream) => {
        ///         var lexer = new PolicyLexer(stream);
        ///         var tokens = new CommonTokenStream(lexer);
        ///         return new PolicyParser(tokens);
        ///     },
        ///     parser => parser.script(),
        ///     policyText
        /// );
        /// 
        /// // Access the parse tree
        /// var tree = parser.Tree;
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="ScriptParserBase{TParser, T}"/> instance with the parsed content.
        /// </returns>
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

        /// <summary>
        /// Parses a StringBuilder containing script content.
        /// </summary>
        /// <param name="creator">Function to create a parser instance from a character stream. Must not be null.</param>
        /// <param name="funcGetContext">Function to get the root context from a parser. Must not be null.</param>
        /// <param name="source">The script content as a StringBuilder. Must not be null.</param>
        /// <param name="sourceFile">Optional name of the source file for error reporting. If null, an empty string is used.</param>
        /// <param name="output">Optional TextWriter for outputting parser messages. If null, messages are discarded.</param>
        /// <param name="outputError">Optional TextWriter for outputting parser error messages. If null, error messages are discarded.</param>
        /// <returns>A ScriptParserBase instance with the parsed content.</returns>
        /// <remarks>
        /// This method creates a parser for the specified script content in a StringBuilder and parses it.
        /// It allows specifying output streams for parser messages and a source file name for error reporting.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when creator, funcGetContext, or source is null.</exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException">Thrown when the parser encounters a syntax error.</exception>
        /// <example>
        /// <code lang="C#">
        /// var sb = new StringBuilder();
        /// sb.AppendLine("policy IsAdult : Identity.age >= 18");
        /// sb.AppendLine("policy HasPermission : Identity.roles has 'admin'");
        /// 
        /// var output = new StringWriter();
        /// var errorOutput = new StringWriter();
        /// 
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     (script, stream) => {
        ///         var lexer = new PolicyLexer(stream);
        ///         var tokens = new CommonTokenStream(lexer);
        ///         return new PolicyParser(tokens);
        ///     },
        ///     parser => parser.script(),
        ///     sb,
        ///     "policies.txt",
        ///     output,
        ///     errorOutput
        /// );
        /// 
        /// // Check for parser messages
        /// string messages = output.ToString();
        /// string errors = errorOutput.ToString();
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="ScriptParserBase{TParser, T}"/> instance with the parsed content.
        /// </returns>
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

        /// <summary>
        /// Parses a script file from a specified path.
        /// </summary>
        /// <param name="creator">Function to create a parser instance from a character stream. Must not be null.</param>
        /// <param name="funcGetContext">Function to get the root context from a parser. Must not be null.</param>
        /// <param name="source">The path to the script file to parse. Must not be null or empty.</param>
        /// <param name="output">Optional TextWriter for outputting parser messages. If null, messages are discarded.</param>
        /// <param name="outputError">Optional TextWriter for outputting parser error messages. If null, error messages are discarded.</param>
        /// <returns>A ScriptParserBase instance with the parsed content.</returns>
        /// <remarks>
        /// This method loads the script from the specified file path and parses it.
        /// It allows specifying output streams for parser messages.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when creator, funcGetContext, or source is null.</exception>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when the specified file cannot be found.</exception>
        /// <exception cref="System.IO.IOException">Thrown when an I/O error occurs while reading the file.</exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException">Thrown when the parser encounters a syntax error.</exception>
        /// <example>
        /// <code lang="C#">
        /// string filePath = @"C:\Policies\access_policy.txt";
        /// var output = new StringWriter();
        /// var errorOutput = new StringWriter();
        /// 
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParsePath(
        ///     (script, stream) => {
        ///         var lexer = new PolicyLexer(stream);
        ///         var tokens = new CommonTokenStream(lexer);
        ///         return new PolicyParser(tokens);
        ///     },
        ///     parser => parser.script(),
        ///     filePath,
        ///     output,
        ///     errorOutput
        /// );
        /// 
        /// // Check for parser messages
        /// string messages = output.ToString();
        /// string errors = errorOutput.ToString();
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="ScriptParserBase{TParser, T}"/> instance with the parsed content.
        /// </returns>
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

        /// <summary>
        /// Initializes the parser with a character stream.
        /// </summary>
        /// <param name="stream">The character stream to parse. Must not be null.</param>
        /// <remarks>
        /// This method creates a parser for the specified character stream and parses it,
        /// storing the resulting parse tree in the Tree property.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when stream is null.</exception>
        /// <exception cref="Antlr4.Runtime.RecognitionException">Thrown when the parser encounters a syntax error.</exception>
        /// <example>
        /// <code lang="C#">
        /// var parser = new ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;(
        ///     output, errorOutput, content, creator, getRoot);
        ///     
        /// ICharStream stream = CharStreams.fromString("policy IsAdult : Identity.age >= 18");
        /// parser.Initialize(stream);
        /// 
        /// // Access the parse tree
        /// var tree = parser.Tree;
        /// </code>
        /// </example>
        public void Initialize(ICharStream stream)
        {
            _parser = Create(stream);
            _context = _getRoot(_parser);
        }

        /// <summary>
        /// Creates a parser for the specified character stream.
        /// </summary>
        /// <param name="stream">The character stream to create a parser for. Must not be null.</param>
        /// <returns>A new parser instance.</returns>
        /// <remarks>
        /// This protected method is used internally to create a parser instance for a character stream
        /// using the creator function specified in the constructor.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when stream is null or when the creator function returns null.</exception>
        /// <returns>
        /// A <typeparamref name="TParser"/> instance for parsing the character stream.
        /// </returns>
        protected TParser Create(ICharStream stream) => _creator(this, stream);

        /// <summary>
        /// Gets or sets a value indicating whether parser tracing is enabled.
        /// </summary>
        /// <remarks>
        /// When set to true, parsers created by this class will generate trace output for debugging purposes.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// // Enable tracing for debugging
        /// ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.Trace = true;
        /// 
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     creator, funcGetContext, "policy IsAdult : Identity.age >= 18");
        /// </code>
        /// </example>
        public static bool Trace { get; set; }

        /// <summary>
        /// Gets the collection of files included by the parsed script.
        /// </summary>
        /// <remarks>
        /// This property provides access to the set of file paths that were included
        /// or imported by the parsed script.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParsePath(
        ///     creator, funcGetContext, "main_policy.txt");
        /// 
        /// // List all included files
        /// foreach (string includedFile in parser.Includes)
        /// {
        ///     Console.WriteLine($"Included: {includedFile}");
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of strings representing the included file paths.
        /// </returns>
        public IEnumerable<string> Includes { get => _includes; }

        /// <summary>
        /// Gets or sets the file path of the parsed script.
        /// </summary>
        /// <remarks>
        /// This property stores the file path of the script being parsed, which is useful
        /// for error reporting and for resolving relative include paths.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     creator, funcGetContext, "policy IsAdult : Identity.age >= 18");
        /// 
        /// parser.File = "C:\\Policies\\age_policy.txt";
        /// Console.WriteLine($"Parsing file: {parser.File}");
        /// </code>
        /// </example>
        public string File { get; set; }

        /// <summary>
        /// Gets the content being parsed.
        /// </summary>
        /// <remarks>
        /// This property provides access to the original content passed to the parser.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     creator, funcGetContext, "policy IsAdult : Identity.age >= 18");
        /// 
        /// // Get the original content
        /// string content = parser.Content.ToString();
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="StringBuilder"/> containing the original content.
        /// </returns>
        public StringBuilder Content { get; }

        /// <summary>
        /// Gets the text writer for outputting parser messages.
        /// </summary>
        /// <remarks>
        /// This property provides access to the text writer that receives parser messages,
        /// which can be used for logging or diagnostics.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var output = new StringWriter();
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     creator, funcGetContext, "policy IsAdult : Identity.age >= 18", "", output, null);
        /// 
        /// // Write a message to the output
        /// parser.Output.WriteLine("Parsing completed successfully.");
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="TextWriter"/> for outputting parser messages.
        /// </returns>
        public TextWriter Output { get; }

        /// <summary>
        /// Gets the text writer for outputting parser error messages.
        /// </summary>
        /// <remarks>
        /// This property provides access to the text writer that receives parser error messages,
        /// which can be used for error logging or diagnostics.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var errorOutput = new StringWriter();
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     creator, funcGetContext, "policy IsAdult : Identity.age >= 18", "", null, errorOutput);
        /// 
        /// // Write an error message
        /// parser.OutputError.WriteLine("Warning: No rules defined in policy.");
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="TextWriter"/> for outputting parser error messages.
        /// </returns>
        public TextWriter OutputError { get; }

        /// <summary>
        /// Gets the root parse tree context.
        /// </summary>
        /// <remarks>
        /// This property provides access to the root of the parse tree created by the parser.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     creator, funcGetContext, "policy IsAdult : Identity.age >= 18");
        /// 
        /// // Access the parse tree
        /// var tree = parser.Tree;
        /// 
        /// // Use a visitor to process the tree
        /// var visitor = new MyCustomVisitor();
        /// visitor.Visit(tree);
        /// </code>
        /// </example>
        /// <returns>
        /// A <typeparamref name="T"/> representing the root of the parse tree.
        /// </returns>
        public T Tree { get { return _context; } }

        /// <summary>
        /// Gets the CRC32 checksum of the parsed content.
        /// </summary>
        /// <remarks>
        /// This property provides a checksum that can be used to detect changes in the content.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var parser1 = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     creator, funcGetContext, "policy IsAdult : Identity.age >= 18");
        ///     
        /// var parser2 = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     creator, funcGetContext, "policy IsAdult : Identity.age > 18");
        ///     
        /// // Compare checksums to detect changes
        /// if (parser1.Crc != parser2.Crc)
        /// {
        ///     Console.WriteLine("Content has changed!");
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="System.UInt32"/> representing the CRC32 checksum of the content.
        /// </returns>
        public uint Crc { get; }

        /// <summary>
        /// Gets the parser instance.
        /// </summary>
        /// <remarks>
        /// This property provides access to the underlying ANTLR parser instance,
        /// which can be used for advanced parser operations.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var parserBase = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     creator, funcGetContext, "policy IsAdult : Identity.age >= 18");
        ///     
        /// // Access the underlying parser
        /// var parser = parserBase.Parser;
        /// 
        /// // Check the token stream
        /// var tokens = parser.TokenStream;
        /// foreach (var token in tokens.GetTokens())
        /// {
        ///     Console.WriteLine($"Token: {token.Text}, Type: {token.Type}");
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// A <typeparamref name="TParser"/> instance used for parsing.
        /// </returns>
        public TParser Parser { get => _parser; }

        /// <summary>
        /// Gets a value indicating whether the parser encountered errors.
        /// </summary>
        /// <remarks>
        /// This property returns true if the parser's error listeners detected any syntax errors.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     creator, funcGetContext, "policy IsAdult : Identity.age >== 18"); // Invalid syntax
        ///     
        /// if (parser.InError)
        /// {
        ///     Console.WriteLine("Parser encountered syntax errors!");
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// <c>true</c> if the parser encountered errors; otherwise, <c>false</c>.
        /// </returns>
        public bool InError { get => _parser.ErrorListeners.Count > 0; }

        /// <summary>
        /// Visits the parse tree with the specified visitor.
        /// </summary>
        /// <typeparam name="Result">The return type of the visitor's methods.</typeparam>
        /// <param name="visitor">The visitor to use for traversing the parse tree. Must not be null.</param>
        /// <returns>The result of the visitor's processing of the parse tree.</returns>
        /// <remarks>
        /// This method applies the visitor pattern to traverse the parse tree and return a result.
        /// If a debugger is attached and a file path is specified, it outputs the file path to the trace.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var parser = ScriptParserBase&lt;PolicyParser, PolicyParser.ScriptContext&gt;.ParseString(
        ///     creator, funcGetContext, "policy IsAdult : Identity.age >= 18");
        ///     
        /// // Create a visitor to evaluate the parse tree
        /// var visitor = new PolicyAstBuilderVisitor();
        /// 
        /// // Visit the parse tree to build an AST
        /// var ast = parser.Visit(visitor);
        /// </code>
        /// </example>
        /// <returns>
        /// An object of type <typeparamref name="Result"/> or null, depending on the visitor's implementation.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object? Visit<Result>(IParseTreeVisitor<Result> visitor)
        {

            if (System.Diagnostics.Debugger.IsAttached && !string.IsNullOrEmpty(File))
                System.Diagnostics.Trace.TraceInformation(File);

            var context = _context;
            return visitor.Visit(context);

        }

        /// <summary>
        /// The underlying ANTLR parser instance.
        /// </summary>
        /// <remarks>
        /// This field stores the ANTLR parser that was created for parsing the content.
        /// </remarks>
        protected TParser _parser;

        /// <summary>
        /// Set of file paths included by the parsed script.
        /// </summary>
        /// <remarks>
        /// This field stores the paths of files included or imported by the parsed script.
        /// </remarks>
        private readonly HashSet<string> _includes;

        /// <summary>
        /// Function to create a parser instance from a character stream.
        /// </summary>
        /// <remarks>
        /// This field stores the factory function that creates a parser for a given character stream.
        /// </remarks>
        protected readonly Func<ScriptParserBase<TParser, T>, ICharStream, TParser> _creator;

        /// <summary>
        /// Function to get the root context from a parser.
        /// </summary>
        /// <remarks>
        /// This field stores the function that extracts the root parse tree context from a parser.
        /// </remarks>
        private readonly Func<TParser, T> _getRoot;

        /// <summary>
        /// The root parse tree context.
        /// </summary>
        /// <remarks>
        /// This field stores the root of the parse tree created by the parser.
        /// </remarks>
        protected T _context;

    }

}
