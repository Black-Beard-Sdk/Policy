using Bb.Policies.Asts;

namespace Bb.Policies
{



    public interface IPolicyVisitor<T>
    {

        /// <summary>
        /// Visits a unary operation policy.
        /// </summary>
        /// <param name="policyOperation">The unary operation policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the unary operation policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="policyOperation"/> is null.</exception>
        /// <remarks>
        /// This method processes a unary operation policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitUnaryOperation(myUnaryOperation);
        /// </code>
        /// </example>
        T VisitUnaryOperation(PolicyOperationUnary policyOperation);


        /// <summary>
        /// Visits a binary operation policy.
        /// </summary>
        /// <param name="policyBinaryOperation">The binary operation policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the binary operation policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="policyBinaryOperation"/> is null.</exception>
        /// <remarks>
        /// This method processes a binary operation policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitBinaryOperation(myBinaryOperation);
        /// </code>
        /// </example>
        T VisitBinaryOperation(PolicyOperationBinary policyBinaryOperation);

        /// <summary>
        /// Visits a comment policy.
        /// </summary>
        /// <param name="policyComment">The comment policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the comment policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="policyComment"/> is null.</exception>
        /// <remarks>
        /// This method processes a comment policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitComment(myComment);
        /// </code>
        /// </example>
        T VisitComment(PolicyComment policyComment);
                
        /// </summary>
        /// <param name="policyConstant">The constant policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the constant policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="policyConstant"/> is null.</exception>
        /// <remarks>
        /// This method processes a constant policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitConstant(myConstant);
        /// </code>
        /// </example>
        T VisitConstant(PolicyConstant policyConstant);
        
        /// <summary>
        /// Visits a container policy.
        /// </summary>
        /// <param name="policyContainer">The container policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the container policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="policyContainer"/> is null.</exception>
        /// <remarks>
        /// This method processes a container policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitContainer(myContainer);
        /// </code>
        /// </example>
        T VisitContainer(PolicyContainer policyContainer);

        /// <summary>
        /// Visits a rule policy.
        /// </summary>
        /// <param name="policyRule">The rule policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the rule policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="policyRule"/> is null.</exception>
        /// <remarks>
        /// This method processes a rule policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitRule(myRule);
        /// </code>
        /// </example>
        T VisitRule(PolicyRule policyRule);

        /// <summary>
        /// Visits a variable policy.
        /// </summary>
        /// <param name="policyVariable">The variable policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the variable policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="policyVariable"/> is null.</exception>
        /// <remarks>
        /// This method processes a variable policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitVariable(myVariable);
        /// </code>
        /// </example>
        T VisitVariable(PolicyVariable policyVariable);

        /// <summary>
        /// Visits a sub-expression policy.
        /// </summary>
        /// <param name="policySubExpression">The sub-expression policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the sub-expression policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="policySubExpression"/> is null.</exception>
        /// <remarks>
        /// This method processes a sub-expression policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitSubExpression(mySubExpression);
        /// </code>
        /// </example>
        T VisitSubExpression(PolicySubExpression policySubExpression);

        /// <summary>
        /// Visits an array policy.
        /// </summary>
        /// <param name="policyArray">The array policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the array policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="policyArray"/> is null.</exception>
        /// <remarks>
        /// This method processes an array policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitArray(myArray);
        /// </code>
        /// </example>
        T VisitArray(PolicyArray policyArray);
    }

}