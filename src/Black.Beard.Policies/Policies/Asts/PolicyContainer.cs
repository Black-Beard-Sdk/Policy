using Bb.Analysis.DiagTraces;

namespace Bb.Policies.Asts
{



    /// <summary>
    /// Represents a container for policy elements including variables and rules.
    /// </summary>
    /// <remarks>
    /// This class serves as the root node for a policy AST, containing collections of variables and rules.
    /// It provides methods to add, access, and resolve policy elements.
    /// </remarks>
    public class PolicyContainer : Policy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolicyContainer"/> class.
        /// </summary>
        /// <remarks>
        /// Creates an empty policy container with initialized collections for variables and rules.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// // Create a new policy container
        /// var container = new PolicyContainer();
        /// </code>
        /// </example>
        public PolicyContainer()
        {
            this._dicVariable = new Dictionary<string, PolicyVariable>();
            this._dicRule = new Dictionary<string, PolicyRule>();
            this._dicInclude = new Dictionary<string, PolicyInclude>();
            this.Kind = PolicyKind.Container;
        }

        /// <summary>
        /// Accepts a visitor to process this policy container.
        /// </summary>
        /// <typeparam name="T">The return type of the visitor processing.</typeparam>
        /// <param name="visitor">The visitor that will process this container. Must not be null.</param>
        /// <returns>The result of the visitor's processing of this container.</returns>
        /// <remarks>
        /// This method implements the visitor pattern for traversing and processing the policy AST.
        /// It calls the visitor's VisitContainer method with this container as the argument.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when visitor is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var container = new PolicyContainer();
        /// var visitor = new PolicyEvaluator&lt;bool&gt;();
        /// bool result = container.Accept(visitor);
        /// </code>
        /// </example>
        public override T Accept<T>(IPolicyVisitor<T> visitor)
        {
            return visitor.VisitContainer(this);
        }

        /// <summary>
        /// Writes this policy container to the specified writer.
        /// </summary>
        /// <param name="writer">The writer to which the container should be written. Must not be null.</param>
        /// <returns><c>true</c> if the writing operation was successful.</returns>
        /// <remarks>
        /// This method writes all variables and rules in the container to the specified writer.
        /// Variables are written first, followed by rules.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when writer is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var container = new PolicyContainer();
        /// var writer = new Writer();
        /// container.ToString(writer);
        /// string result = writer.ToString();
        /// </code>
        /// </example>
        public override bool ToString(Writer writer)
        {

            var position = writer.Count;
            bool next = false;

            foreach (var item in _dicVariable)
            {

                if (next)
                    writer.AppendEndLine();

                if (item.Value.ToString(writer))
                    next = true;

            }

            foreach (var item in _dicRule)
            {

                if (next)
                    writer.AppendEndLine();

                if (item.Value.ToString(writer))
                    next = true;

            }

            return position != writer.Count;

        }

        /// <summary>
        /// Adds a policy element to this container.
        /// </summary>
        /// <param name="o">The policy element to add. Must be either a PolicyVariable or a PolicyRule.</param>
        /// <returns><c>true</c> if the element was successfully added; <c>false</c> otherwise.</returns>
        /// <remarks>
        /// This method determines the type of the policy element and adds it to the appropriate collection.
        /// If the element is neither a variable nor a rule, the method returns false.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when o is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var container = new PolicyContainer();
        /// var variable = new PolicyVariable("varName", new PolicyConstant("value", ConstantType.String));
        /// bool success = container.Add(variable);
        /// </code>
        /// </example>
        public bool Add(PolicyNamed o)
        {

            if (o is PolicyVariable v)
                return Add(v);

            if (o is PolicyRule r)
                return Add(r);

            if (o is PolicyInclude i)
                return Add(i);

            return false;

        }


        /// <summary>
        /// Adds a policy variable to this container.
        /// </summary>
        /// <param name="item">The policy variable to add. Must not be null.</param>
        /// <returns><c>true</c> if the variable was successfully added; <c>false</c> if a variable with the same name already exists.</returns>
        /// <remarks>
        /// This method adds the specified variable to the container's variable collection.
        /// If a variable with the same name already exists, the method returns false without modifying the collection.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when o is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var container = new PolicyContainer();
        /// var variable = new PolicyVariable("varName", new PolicyConstant("value", ConstantType.String));
        /// bool success = container.Add(variable);
        /// </code>
        /// </example>
        public bool Add(PolicyVariable item)
        {

            if (_dicVariable.ContainsKey(item.Name))
                return false;

            _dicVariable.Add(item.Name, item);

            return true;

        }


        /// <summary>
        /// Adds a policy rule to this container.
        /// </summary>
        /// <param name="item">The policy rule to add. Must not be null.</param>
        /// <returns><c>true</c> if the rule was successfully added; <c>false</c> if a rule with the same name already exists.</returns>
        /// <remarks>
        /// This method adds the specified rule to the container's rule collection.
        /// If a rule with the same name already exists, the method returns false without modifying the collection.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when rule is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// var container = new PolicyContainer();
        /// var rule = new PolicyRule("ruleName");
        /// bool success = container.Add(rule);
        /// </code>
        /// </example>
        public bool Add(PolicyRule item)
        {

            if (item.Name == "default")
                this.DefaultRule = item;

            else if (item.Name == "fallback")
                this.FallbackRule = item;

            else if (_dicRule.ContainsKey(item.Name))
                return false;
            else
                _dicRule.Add(item.Name, item);

            return true;

        }


        public bool Add(PolicyInclude item)
        {

            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (_dicInclude.ContainsKey(item.Name))
                return false;

            _dicInclude.Add(item.Name, item);

            return true;

        }


        /// <summary>
        /// Tries to resolve a variable name to its alias value.
        /// </summary>
        /// <param name="name">The variable name to resolve. Must not be null or empty.</param>
        /// <param name="alias">When this method returns, contains the resolved alias value if the variable was found; otherwise, null.</param>
        /// <returns><c>true</c> if the variable was found and has a non-empty value; otherwise, <c>false</c>.</returns>
        /// <remarks>
        /// This method attempts to find a variable with the specified name and retrieve its value.
        /// If the variable is found and has a non-null, non-empty value, the value is assigned to the alias parameter.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when name is null or empty.</exception>
        /// <example>
        /// <code lang="C#">
        /// var container = new PolicyContainer();
        /// container.Add(new PolicyVariable("varName", new PolicyConstant("aliasValue", ConstantType.String)));
        /// 
        /// if (container.ResolveVariable("varName", out string alias))
        /// {
        ///     Console.WriteLine($"Resolved alias: {alias}");
        /// }
        /// </code>
        /// </example>
        public bool ResolveVariable(string name, out string alias)
        {

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            alias = null;

            if (_dicVariable.TryGetValue(name, out var a))
                if (a.Value != null)
                    alias = a.Value.Value;

            return !string.IsNullOrEmpty(alias);

        }

        /// <summary>
        /// Gets a rule by its name.
        /// </summary>
        /// <param name="v">The name of the rule to get. Must not be null or empty.</param>
        /// <returns>The policy rule with the specified name.</returns>
        /// <remarks>
        /// This method retrieves a rule from the container's rule collection using its name as the key.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when v is null or empty.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown when no rule with the specified name exists.</exception>
        /// <example>
        /// <code lang="C#">
        /// var container = new PolicyContainer();
        /// container.Add(new PolicyRule("myRule"));
        /// 
        /// PolicyRule rule = container.GetRule("myRule");
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="PolicyRule"/> object representing the rule with the specified name.
        /// </returns>
        public PolicyRule GetRule(string v)
        {
            return _dicRule[v];
        }

        internal void EvaluateInclude(string path)
        {
            foreach (var item in this._dicInclude)
                if (item.Value.Fullpath == path)
                {
                    item.Value.IsLoaded = true;
                    break;
                }
        }

        /// <summary>
        /// Gets all rules in this container.
        /// </summary>
        /// <remarks>
        /// This property provides access to all rules stored in the container as an enumerable collection.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var container = new PolicyContainer();
        /// container.Add(new PolicyRule("rule1"));
        /// container.Add(new PolicyRule("rule2"));
        /// 
        /// foreach (var rule in container.Rules)
        /// {
        ///     Console.WriteLine(rule.Name);
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// An <see cref="IEnumerable{PolicyRule}"/> containing all rules in the container.
        /// </returns>
        public IEnumerable<PolicyRule> Rules => _dicRule.Values;


        /// <summary>
        /// Gets or sets the diagnostics associated with this policy container.
        /// </summary>
        /// <remarks>
        /// This property stores any parsing errors, warnings, or other diagnostic information
        /// related to the policy container and its contents.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var container = Policy.ParseText("rule myRule { condition: true; }");
        /// foreach (var diagnostic in container.Diagnostics.Messages)
        /// {
        ///     Console.WriteLine(diagnostic);
        /// }
        /// </code>
        /// </example>
        /// <returns>
        /// A <see cref="ScriptDiagnostics"/> object containing diagnostic information for this container.
        /// </returns>
        public ScriptDiagnostics Diagnostics { get; internal set; }

        /// <summary>
        /// Gets the default rule for this container.
        /// </summary>
        public PolicyRule DefaultRule { get; private set; }

        /// <summary>
        /// Gets the fallback rule for this container.
        /// </summary>
        public PolicyRule FallbackRule { get; private set; }

        /// <summary>
        /// Return true if needs to load include file
        /// </summary>
        public bool MustLoadIncludes => _dicInclude.Any(c => !c.Value.IsLoaded);

        /// <summary>
        /// Return the list of includes to files
        /// </summary>
        public PolicyInclude[] IncludeToLoads => _dicInclude.Where(c => !c.Value.IsLoaded).Select(c => c.Value).ToArray();

        /// <summary>
        /// Gets the path of the policy file that was parsed to create this container.
        /// </summary>
        public string Path { get; internal set; }


        private Dictionary<string, PolicyVariable> _dicVariable;
        private Dictionary<string, PolicyRule> _dicRule;
        private Dictionary<string, PolicyInclude> _dicInclude;

    }

}
