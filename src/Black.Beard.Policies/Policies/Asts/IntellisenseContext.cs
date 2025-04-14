using Bb.Analysis.DiagTraces;

namespace Bb.Policies.Asts
{
    /// <summary>
    /// Represents the context for intellisense operations in policy parsing.
    /// </summary>
    /// <remarks>
    /// This structure provides access to the parser, diagnostics, and script information 
    /// needed for intellisense features during policy analysis.
    /// </remarks>
    public struct IntellisenseContext
    {
        /// <summary>
        /// Gets the default instance of the <see cref="IntellisenseContext"/> structure.
        /// </summary>
        /// <remarks>
        /// This instance has all properties set to their default values and NotNull set to false.
        /// </remarks>
        public static readonly IntellisenseContext Default = new IntellisenseContext();

        /// <summary>
        /// Initializes a new instance of the <see cref="IntellisenseContext"/> structure with default values.
        /// </summary>
        /// <remarks>
        /// This constructor initializes all properties to their default values and sets NotNull to false.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var context = new IntellisenseContext();
        /// </code>
        /// </example>
        public IntellisenseContext()
        {
            Parser = default;
            Diagnostics = default;
            ScriptPath = default;
            NotNull = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntellisenseContext"/> structure with the specified parameters.
        /// </summary>
        /// <param name="parser">The ANTLR parser instance to use for parsing.</param>
        /// <param name="diagnostics">The diagnostics collector for reporting issues.</param>
        /// <param name="path">The path to the script being analyzed.</param>
        /// <remarks>
        /// This constructor initializes the context with specific parser, diagnostics, and script path information.
        /// NotNull is set to true to indicate that the context contains valid data.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when parser or diagnostics is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var parser = new SomeParser();
        /// var diagnostics = new ScriptDiagnostics();
        /// var context = new IntellisenseContext(parser, diagnostics, "C:\\path\\to\\script.policy");
        /// </code>
        /// </example>
        public IntellisenseContext(Antlr4.Runtime.Parser parser, ScriptDiagnostics diagnostics, string path)
        {
            Parser = parser;
            Diagnostics = diagnostics;
            ScriptPath = path;
            NotNull = true;
        }

        /// <summary>
        /// Gets the ANTLR parser used for parsing policy scripts.
        /// </summary>
        /// <remarks>
        /// The parser provides access to the grammar rules and vocabulary needed for parsing and analyzing the policy language.
        /// </remarks>
        /// <returns>
        /// An instance of <see cref="Antlr4.Runtime.Parser"/> used for parsing policy scripts.
        /// </returns>
        public Antlr4.Runtime.Parser Parser { get; }

        /// <summary>
        /// Gets the diagnostics collector for reporting parsing and analysis issues.
        /// </summary>
        /// <remarks>
        /// The diagnostics collector stores warnings, errors, and other issues encountered during policy analysis.
        /// </remarks>
        /// <returns>
        /// An instance of <see cref="ScriptDiagnostics"/> used for collecting analysis issues.
        /// </returns>
        public ScriptDiagnostics Diagnostics { get; }

        /// <summary>
        /// Gets the file path of the script being analyzed.
        /// </summary>
        /// <remarks>
        /// This path is used for error reporting and resource resolution during policy analysis.
        /// </remarks>
        /// <returns>
        /// A string representing the file path of the script being analyzed.
        /// </returns>
        public string ScriptPath { get; }

        /// <summary>
        /// Gets a value indicating whether this context contains valid data.
        /// </summary>
        /// <remarks>
        /// When true, the context was initialized with specific values rather than defaults.
        /// This property helps distinguish between the default context and a properly initialized one.
        /// </remarks>
        /// <returns>
        /// A boolean value indicating whether this context contains valid data.
        /// </returns>
        public bool NotNull { get; }
    }
}
