using Bb.Policies.Asts;

namespace Bb.Policies
{



    public interface IPolicyVisitor<T>
    {

        /// <summary>
        /// Visits a unary operation policy.
        /// </summary>
        /// <param name="e">The unary operation policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the unary operation policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="e"/> is null.</exception>
        /// <remarks>
        /// This method processes a unary operation policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitUnaryOperation(myUnaryOperation);
        /// </code>
        /// </example>
        T VisitUnaryOperation(PolicyOperationUnary e);


        /// <summary>
        /// Visits a binary operation policy.
        /// </summary>
        /// <param name="e">The binary operation policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the binary operation policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="e"/> is null.</exception>
        /// <remarks>
        /// This method processes a binary operation policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitBinaryOperation(myBinaryOperation);
        /// </code>
        /// </example>
        T VisitBinaryOperation(PolicyOperationBinary e);

        /// <summary>
        /// Visits a comment policy.
        /// </summary>
        /// <param name="e">The comment policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the comment policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="e"/> is null.</exception>
        /// <remarks>
        /// This method processes a comment policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitComment(myComment);
        /// </code>
        /// </example>
        T VisitComment(PolicyComment e);
                
        /// </summary>
        /// <param name="e">The constant policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the constant policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="e"/> is null.</exception>
        /// <remarks>
        /// This method processes a constant policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitConstant(myConstant);
        /// </code>
        /// </example>
        T VisitConstant(PolicyConstant e);
        
        /// <summary>
        /// Visits a container policy.
        /// </summary>
        /// <param name="e">The container policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the container policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="e"/> is null.</exception>
        /// <remarks>
        /// This method processes a container policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitContainer(myContainer);
        /// </code>
        /// </example>
        T VisitContainer(PolicyContainer e);

        /// <summary>
        /// Visits a rule policy.
        /// </summary>
        /// <param name="e">The rule policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the rule policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="e"/> is null.</exception>
        /// <remarks>
        /// This method processes a rule policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitRule(myRule);
        /// </code>
        /// </example>
        T VisitRule(PolicyRule e);

        /// <summary>
        /// Visits a variable policy.
        /// </summary>
        /// <param name="e">The variable policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the variable policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="e"/> is null.</exception>
        /// <remarks>
        /// This method processes a variable policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitVariable(myVariable);
        /// </code>
        /// </example>
        T VisitVariable(PolicyVariable e);

        /// <summary>
        /// Visits a sub-expression policy.
        /// </summary>
        /// <param name="e">The sub-expression policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the sub-expression policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="e"/> is null.</exception>
        /// <remarks>
        /// This method processes a sub-expression policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitSubExpression(mySubExpression);
        /// </code>
        /// </example>
        T VisitSubExpression(PolicySubExpression e);

        /// <summary>
        /// Visits an array policy.
        /// </summary>
        /// <param name="e">The array policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the array policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="e"/> is null.</exception>
        /// <remarks>
        /// This method processes an array policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitArray(myArray);
        /// </code>
        /// </example>
        T VisitArray(PolicyArray e);

        /// <summary>
        /// Visits an id policy.
        /// </summary>
        /// <param name="e">The id policy to visit. Cannot be null.</param>
        /// <returns>The result of visiting the id policy.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="e"/> is null.</exception>
        /// <remarks>
        /// This method processes an id policy and returns a result of type <typeparamref name="T"/>.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// var visitor = new MyPolicyVisitor();
        /// var result = visitor.VisitId(myArray);
        /// </code>
        /// </example>
        T VisitId(PolicyIdExpression e);

    }

}