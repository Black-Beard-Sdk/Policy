using Bb.Analysis.DiagTraces;
using System;


namespace Bb.Policies.Asts
{


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
        /// <param name="visitor">The visitor.</param>
        /// <returns></returns>
        public abstract T Accept<T>(IPolicyVisitor<T> visitor);

        /// <summary>
        /// Gets or sets the location of the code source.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public TextLocation? Location { get; set; }

        /// <summary>
        /// Gets the comment's list.
        /// </summary>
        /// <returns></returns>
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
        /// Evaluate text value
        /// </summary>
        /// <param name="text">text to evaluate</param>
        /// <returns><see href="IntellisenseAst"></returns>
        public static IntellisenseAst EvaluateTextForIntellisense(string text)
        {
            var _errors = new ScriptDiagnostics();
            var tree = ScriptParser.EvaluateString(text);
            tree.ParseTree(); 
            return tree;
        }

        /// <summary>
        /// Evaluate text of path value
        /// </summary>
        /// <param name="text"></param>
        /// <returns><see href="IntellisenseAst"></returns>
        public static IntellisenseAst EvaluatePathForIntellisense(string text)
        {
            var _errors = new ScriptDiagnostics();
            var tree = ScriptParser.EvaluatePath(text);
            tree.ParseTree();
            return tree;
        }

        /// <summary>
        /// Evaluate text value
        /// </summary>
        /// <param name="text">text to evaluate</param>
        /// <returns></returns>
        public static PolicyContainer ParseText(string text)
        {
            var _errors = new ScriptDiagnostics();
            var parser = ScriptParser.ParseString(text);
            var visitor = new ScriptBuilderVisitor(parser.Parser, _errors, string.Empty);
            var tree = (PolicyContainer)parser.Visit(visitor);            
            return tree;
        }       

        /// <summary>
        /// Evaluate text of path value
        /// </summary>
        /// <param name="path">path of the file</param>
        /// <returns></returns>
        public static Policy ParsePath(string path)
        {
            var _errors = new ScriptDiagnostics();
            var parser = ScriptParser.ParsePath(path);
            var visitor = new ScriptBuilderVisitor(parser.Parser, _errors, string.Empty);
            var tree = (Policy)parser.Visit(visitor);
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
        /// Converts the tree to json string source code.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="strategy">The strategy.</param>
        /// <returns></returns>
        public abstract bool ToString(Writer writer);

        public abstract bool HasSource();

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

    public enum PolicyKind
    {
        Variable,
        Constant,
        Container,
        Rule,
        Operation,
        IdExpression
    }

    public enum PolicyOperator
    {
        Undefined,
        Equal,
        NotEqual,
        In,
        NotIn,
        Has,
        HasNot,
        AndExclusive,
        OrExclusive,
        Not
    }

}
