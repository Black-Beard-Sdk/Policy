using Bb.Analysis.DiagTraces;
using Bb;

namespace Bb.Policies.Asts
{

    /// <summary>
    /// Abstract base class for all policy AST nodes.
    /// </summary>
    /// <remarks>
    /// This class provides common functionality for all policy nodes in the abstract syntax tree,
    /// including location tracking, visitor pattern support, and string serialization.
    /// </remarks>
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public abstract class Policy : IWriter
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Policy"/> class.
        /// </summary>
        public Policy()
        {
            //this._comments = new List<PolicyComment>();
        }

        /// <summary>
        /// Gets the kind of the node ast.
        /// </summary>
        /// <value>
        /// The kind.
        /// </value>
        public PolicyKind Kind { get; internal set; }

        /// <summary>
        /// Accepts the specified visitor for parsing the tree.
        /// </summary>
        /// <param name="visitor">The visitor that will traverse this node. Must not be null.</param>
        /// <returns>The result of the visitor's visit.</returns>
        /// <remarks>
        /// Implements the visitor design pattern for traversing the AST structure.
        /// Each concrete policy node type must override this method to provide specific visiting behavior.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Create a visitor
        /// var visitor = new MyPolicyVisitor&lt;string&gt;();
        /// 
        /// // Visit a policy node
        /// string result = policyNode.Accept(visitor);
        /// </code>
        /// </example>
        public abstract T Accept<T>(IPolicyVisitor<T> visitor);

        /// <summary>
        /// Gets or sets the location of the code source.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public TextLocation? Location { get; set; }

        /// <summary>
        /// Gets a copy of the location of the code source.
        /// </summary>
        /// <returns>A copy of the current location or null if no location is set.</returns>
        /// <remarks>
        /// This method returns a copy of the Location property to prevent unintended modifications.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var policy = new ConcretePolicy();
        /// TextLocation? location = policy.GetLocation();
        /// </code>
        /// </example>
        public TextLocation? GetLocation()
        {
            return Location?.Copy();
        }

        /// <summary>
        /// Gets the comment's list.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        //public IEnumerable<PolicyComment> Comments { get => this._comments; }

        /// <summary>
        /// Evaluates text for intellisense purposes.
        /// </summary>
        /// <param name="text">The text to evaluate. Must not be null.</param>
        /// <returns>An <see cref="IntellisenseAst"/> representing the parsed text.</returns>
        /// <remarks>
        /// This method parses the provided text and builds an abstract syntax tree
        /// for use with intellisense features.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when text is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// string code = "policy test : role = Administrator";
        /// IntellisenseAst ast = Policy.EvaluateTextForIntellisense(code);
        /// </code>
        /// </example>
        public static IntellisenseAst EvaluateTextForIntellisense(string text)
        {
            var _errors = new ScriptDiagnostics();
            var tree = ScriptParser.EvaluateString(text);
            tree.ParseTree();
            return tree;
        }

        /// <summary>
        /// Evaluates a file at the given path for intellisense purposes.
        /// </summary>
        /// <param name="text">The file path to evaluate. Must not be null or empty.</param>
        /// <returns>An <see cref="IntellisenseAst"/> representing the parsed file.</returns>
        /// <remarks>
        /// This method reads and parses the file at the specified path and builds
        /// an abstract syntax tree for use with intellisense features.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when text is null.</exception>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when the specified file does not exist.</exception>
        /// <example>
        /// <code lang="C#">
        /// string filePath = @"C:\policies\example.policy";
        /// IntellisenseAst ast = Policy.EvaluatePathForIntellisense(filePath);
        /// </code>
        /// </example>
        public static IntellisenseAst EvaluatePathForIntellisense(string text)
        {
            var _errors = new ScriptDiagnostics();
            var tree = ScriptParser.EvaluatePath(text);
            tree.ParseTree();
            return tree;
        }

        /// <summary>
        /// Parses the specified text into a policy container.
        /// </summary>
        /// <param name="text">The text to parse. Must not be null.</param>
        /// <returns>A <see cref="PolicyContainer"/> representing the parsed policy.</returns>
        /// <remarks>
        /// This method parses the provided text and builds a complete policy container
        /// that contains all the rules and expressions defined in the text.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when text is null.</exception>
        /// <exception cref="Bb.Policies.PolicyParserException">Thrown when parsing errors occur.</exception>
        /// <example>
        /// <code lang="C#">
        /// string policyText = "policy MyRule : role = Administrator";
        /// PolicyContainer container = Policy.ParseText(policyText);
        /// </code>
        /// </example>
        public static PolicyContainer ParseText(string text)
        {

            var errors = new ScriptDiagnostics();
            var container = new PolicyContainer() { Diagnostics = errors };
            var parser = ScriptParser.ParseString(text);

            var visitor = new ScriptBuilderVisitor(parser.Parser, errors, container, null, string.Empty);

            var tree = (PolicyContainer)parser.Visit(visitor);

            while (container.MustLoadIncludes)
                foreach (var item in container.IncludeToLoads)
                {
                    var file = item.ResolveLocation(visitor.ScriptPathDirectory);
                    if (!item.FileExists)
                        errors.AddError(item.Location, item.Name, $"failed to load file {file}");
                    ParsePath(file, errors, container);
                }

            return tree;
        }

        /// <summary>
        /// Parses the policy file at the specified path into a policy container.
        /// </summary>
        /// <param name="path">The path of the file to parse. Must not be null or empty.</param>
        /// <returns>A <see cref="PolicyContainer"/> representing the parsed policy.</returns>
        /// <remarks>
        /// This method reads and parses the file at the specified path and builds a complete
        /// policy container that contains all the rules and expressions defined in the file.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when path is null.</exception>
        /// <exception cref="System.IO.FileNotFoundException">Thrown when the specified file does not exist.</exception>
        /// <exception cref="Bb.Policies.PolicyParserException">Thrown when parsing errors occur.</exception>
        /// <example>
        /// <code lang="C#">
        /// string filePath = @"C:\policies\example.policies.txt";
        /// PolicyContainer container = Policy.ParsePath(filePath);
        /// </code>
        /// </example>
        public static PolicyContainer ParsePath(string path)
        {
            var errors = new ScriptDiagnostics();
            var container = new PolicyContainer() { Diagnostics = errors, Path = path };
            return ParsePath(path, errors, container);
        }


        /// <summary>
        /// Parses the policy file at the specified path into a policy container.
        /// </summary>
        /// <param name="path">The path of the file to parse. Must not be null or empty.</param>
        /// <param name="errors">store diagnostics.</param>
        /// <param name="container">The to store rules.</param>
        /// <returns>A <see cref="PolicyContainer"/> representing the parsed policy.</returns>
        /// <remarks>
        /// This method reads and parses the file at the specified path and builds a complete
        /// policy container that contains all the rules and expressions defined in the file.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when path is null.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the specified file does not exist.</exception>
        /// <exception cref="Policies.PolicyParserException">Thrown when parsing errors occur.</exception>
        /// <example>
        /// <code lang="C#">
        /// var errors = new ScriptDiagnostics();
        /// var container = new PolicyContainer();
        /// string filePath = @"C:\policies\example.policies.txt";
        /// PolicyContainer container = Policy.ParsePath(filePath);
        /// </code>
        /// </example>
        public static PolicyContainer ParsePath(string path, ScriptDiagnostics errors, PolicyContainer container)
        {

            var parser = ScriptParser.ParsePath(path);
            var visitor = new ScriptBuilderVisitor(parser.Parser, errors, container, null, path);
            var tree = (PolicyContainer)parser.Visit(visitor);

            container.EvaluateInclude(path);

            while (container.MustLoadIncludes)
                foreach (var item in container.IncludeToLoads)
                {
                    var file = item.ResolveLocation(visitor.ScriptPathDirectory);
                    if (!item.FileExists)
                        errors.AddError(item.Location, item.Name, $"failed to load file {file}");
                    ParsePath(file, errors, container);
                }

            return tree;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            Writer writer = new Writer();
            writer.ToString(this);
            return writer.ToString();
        }

        /// <summary>
        /// Writes this policy node to the specified writer.
        /// </summary>
        /// <param name="writer">The writer to which the node should be written. Must not be null.</param>
        /// <returns><c>true</c> if the writing operation was successful; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method is responsible for formatting and writing the policy node's content
        /// to the specified writer. Each concrete policy node must implement this method.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when writer is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var policy = new ConcretePolicy();
        /// var writer = new Writer();
        /// bool success = policy.ToString(writer);
        /// string result = writer.ToString();
        /// </code>
        /// </example>
        public abstract bool ToString(Writer writer);

        ///// <summary>
        ///// Adds the specified comments.
        ///// </summary>
        ///// <param name="comments">The comments.</param>
        //internal void AddComments(IEnumerable<PolicyComment> comments)
        //{
        //    _comments.AddRange(comments);
        //}      

        //private readonly List<PolicyComment> _comments;
        public const string Quote = "\"";

    }

    /// <summary>
    /// Defines the different kinds of policy nodes in the abstract syntax tree.
    /// </summary>
    /// <remarks>
    /// This enumeration is used to identify and categorize different types of nodes
    /// in the policy AST, which helps with visitor pattern implementations and type checking.
    /// </remarks>
    public enum PolicyKind
    {
        /// <summary>
        /// Represents a variable declaration in the policy.
        /// </summary>
        Variable,

        /// <summary>
        /// Represents a constant declaration in the policy.
        /// </summary>
        Constant,

        /// <summary>
        /// Represents a container that holds multiple policy elements.
        /// </summary>
        Container,

        /// <summary>
        /// Represents a rule definition in the policy.
        /// </summary>
        Rule,

        /// <summary>
        /// Represents an operation or expression in the policy.
        /// </summary>
        Operation,

        /// <summary>
        /// Represents an identifier expression in the policy.
        /// </summary>
        IdExpression
    }

    /// <summary>
    /// Defines the operators that can be used in policy expressions.
    /// </summary>
    /// <remarks>
    /// This enumeration contains all the supported operators for building
    /// expressions and conditions in policy rules.
    /// </remarks>
    public enum PolicyOperator
    {

        /// <summary>
        /// Represents an undefined or invalid operator.
        /// </summary>
        Undefined,

        UnaryCompare,

        /// <summary>
        /// Represents the equality operator (==).
        /// </summary>
        Equal,

        /// <summary>
        /// Represents the inequality operator (!=).
        /// </summary>
        NotEqual,

        Lesser,
        LesserOrEqual,
        Greater,
        GreaterOrEqual,

        /// <summary>
        /// Represents the containment operator (in).
        /// </summary>
        In,

        /// <summary>
        /// Represents the negated containment operator (not in).
        /// </summary>
        NotIn,

        /// <summary>
        /// Represents the has operator, which checks if a collection contains an element.
        /// </summary>
        Has,

        /// <summary>
        /// Represents the negated has operator (has not).
        /// </summary>
        HasNot,

        /// <summary>
        /// Represents the exclusive AND logical operator.
        /// </summary>
        AndExclusive,

        /// <summary>
        /// Represents the exclusive OR logical operator.
        /// </summary>
        OrExclusive,

        /// <summary>
        /// Represents the logical NOT operator.
        /// </summary>
        Not,

        /// <summary>
        /// The claim is required
        /// </summary>
        Required,

    }
}
