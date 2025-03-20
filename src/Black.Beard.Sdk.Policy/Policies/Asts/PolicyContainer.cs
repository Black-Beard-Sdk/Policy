using Bb.Analysis.DiagTraces;
using System;
using System.Collections.Generic;

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
            this.Kind = PolicyKind.Container;

            #region alias

            _dicVariable.Add("actor", new PolicyVariable( "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor", true));
            _dicVariable.Add("postalcode", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/postalcode", true));
            _dicVariable.Add("primarygroupsid", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/primarygroupsid", true));
            _dicVariable.Add("primarysid", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid", true));
            _dicVariable.Add("role", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", true));
            _dicVariable.Add("rsa", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa", true));
            _dicVariable.Add("serialnumber", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/serialnumber", true));
            _dicVariable.Add("sid", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid", true));
            _dicVariable.Add("spn", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/spn", true));
            _dicVariable.Add("stateorprovince", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/stateorprovince", true));
            _dicVariable.Add("streetaddress", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/streetaddress", true));
            _dicVariable.Add("surname", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", true));
            _dicVariable.Add("system", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system", true));
            _dicVariable.Add("thumbprint", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/thumbprint", true));
            _dicVariable.Add("upn", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn", true));
            _dicVariable.Add("uri", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/uri", true));
            _dicVariable.Add("userdata", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata", true));
            _dicVariable.Add("version", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/version", true));
            _dicVariable.Add("webpage", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/webpage", true));
            _dicVariable.Add("windowsaccountname", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsaccountname", true));
            _dicVariable.Add("windowsdeviceclaim", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdeviceclaim", true));
            _dicVariable.Add("windowsdevicegroup", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsdevicegroup", true));
            _dicVariable.Add("windowsfqbnversion", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsfqbnversion", true));
            _dicVariable.Add("windowssubauthority", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/windowssubauthority", true));
            _dicVariable.Add("otherphone", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/otherphone", true));
            _dicVariable.Add("nameidentifier", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", true));
            _dicVariable.Add("name", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", true));
            _dicVariable.Add("mobilephone", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone", true));
            _dicVariable.Add("anonymous", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous", true));
            _dicVariable.Add("authentication", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication", true));
            _dicVariable.Add("authenticationinstant", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant", true));
            _dicVariable.Add("authenticationmethod", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", true));
            _dicVariable.Add("authorizationdecision", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision", true));
            _dicVariable.Add("cookiepath", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/cookiepath", true));
            _dicVariable.Add("country", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/country", true));
            _dicVariable.Add("dateofbirth", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth", true));
            _dicVariable.Add("denyonlyprimarygroupsid", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarygroupsid", true));
            _dicVariable.Add("denyonlyprimarysid", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlyprimarysid", true));
            _dicVariable.Add("denyonlysid", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/denyonlysid", true));
            _dicVariable.Add("windowsuserclaim", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/windowsuserclaim", true));
            _dicVariable.Add("denyonlywindowsdevicegroup", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/denyonlywindowsdevicegroup", true));
            _dicVariable.Add("dsa", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/dsa", true));
            _dicVariable.Add("email", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", true));
            _dicVariable.Add("expiration", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration", true));
            _dicVariable.Add("expired", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/expired", true));
            _dicVariable.Add("gender", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/gender", true));
            _dicVariable.Add("givenname", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", true));
            _dicVariable.Add("groupsid", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/groupsid", true));
            _dicVariable.Add("hash", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/hash", true));
            _dicVariable.Add("homephone", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/homephone", true));
            _dicVariable.Add("ispersistent", new PolicyVariable("http://schemas.microsoft.com/ws/2008/06/identity/claims/ispersistent", true));
            _dicVariable.Add("locality", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/locality", true));
            _dicVariable.Add("dns", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dns", true));
            _dicVariable.Add("x500distinguishedname", new PolicyVariable("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/x500distinguishedname", true));

            #endregion alias


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

            return false;

        }

        /// <summary>
        /// Adds a policy variable to this container.
        /// </summary>
        /// <param name="o">The policy variable to add. Must not be null.</param>
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
        public bool Add(PolicyVariable o)
        {

            if (_dicVariable.ContainsKey(o.Name))
                return false;

            _dicVariable.Add(o.Name, o);

            return true;

        }

        /// <summary>
        /// Adds a policy rule to this container.
        /// </summary>
        /// <param name="rule">The policy rule to add. Must not be null.</param>
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
        public bool Add(PolicyRule rule)
        {

            if (rule.Name == "default")
                this.DefaultRule = rule;

            else if (rule.Name == "fallback")
                this.FallbackRule = rule;

            else if (_dicRule.ContainsKey(rule.Name))
                return false;
            else
                _dicRule.Add(rule.Name, rule);

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

        private Dictionary<string, PolicyVariable> _dicVariable;
        private Dictionary<string, PolicyRule> _dicRule;

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

        public PolicyRule DefaultRule { get; private set; }

        public PolicyRule FallbackRule { get; private set; }

    }

}
