using System;

namespace Tiny.Language.AbstractSyntax
{
    public class VariableExpressionNode : ExpressionNode, IEquatable<VariableExpressionNode>
    {
        public char Name { get; }

        public VariableExpressionNode(char name)
        {
            if (!char.IsUpper(name))
                throw new ArgumentException(
                    $"Variable name 'name' is not upper case.",
                    nameof(name));

            Name = name;
        }

        public bool Equals(VariableExpressionNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((VariableExpressionNode) obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(VariableExpressionNode left, VariableExpressionNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VariableExpressionNode left, VariableExpressionNode right)
        {
            return !Equals(left, right);
        }

        public override void Accept(IAstNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}