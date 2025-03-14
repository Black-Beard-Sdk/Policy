using Bb.Policies.Asts;

namespace Bb.Policies
{

    public interface IPolicyVisitor<T>
    {


        T VisitUnaryOperation(PolicyOperationUnary policyOperation);

        T VisitBinaryOperation(PolicyOperationBinary policyBinaryOperation);
        T VisitComment(PolicyComment policyComment);
        
        T VisitConstant(PolicyConstant policyConstant);
        
        T VisitContainer(PolicyContainer policyContainer);
        T VisitRule(PolicyRule policyRule);

        T VisitVariable(PolicyVariable policyVariable);
        T VisitSubExpression(PolicySubExpression policySubExpression);
        T VisitArray(PolicyArray policyArray);
    }

}