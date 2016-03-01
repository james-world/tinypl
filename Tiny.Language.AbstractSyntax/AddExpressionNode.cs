namespace Tiny.Language.AbstractSyntax
{
    public class AddExpressionNode : BinaryExpressionNode
    {
        public AddExpressionNode(ExpressionNode left, ExpressionNode right)
            : base(left, right)
        {
            
        }

        public override void Accept(IAstNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}