namespace Tiny.Language.AbstractSyntax
{
    public class MultiplyExpressionNode : BinaryExpressionNode
    {
        public MultiplyExpressionNode(ExpressionNode left, ExpressionNode right)
            : base(left, right)
        {

        }

        public override void Accept(IAstNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}