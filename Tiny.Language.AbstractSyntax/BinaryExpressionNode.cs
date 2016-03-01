using System;
using System.Collections.Generic;

namespace Tiny.Language.AbstractSyntax
{
    public abstract class BinaryExpressionNode : ExpressionNode, IEquatable<BinaryExpressionNode>
    {
        public ExpressionNode Left { get; }
        public ExpressionNode Right { get; }

        protected BinaryExpressionNode(ExpressionNode left, ExpressionNode right)
        {
            if (left == null) throw new ArgumentNullException(nameof(left));
            if (right == null) throw new ArgumentNullException(nameof(right));

            Left = left;
            Right = right;
        }

        public bool Equals(BinaryExpressionNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Left.Equals(other.Left) && Right.Equals(other.Right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BinaryExpressionNode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashcode = Left.GetHashCode();
                hashcode = (hashcode*397) ^ Right.GetHashCode();
                hashcode = (hashcode*397) ^ GetType().GetHashCode();
                return hashcode;
            }
        }

        public static bool operator ==(BinaryExpressionNode left, BinaryExpressionNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BinaryExpressionNode left, BinaryExpressionNode right)
        {
            return !Equals(left, right);
        }

        public override IEnumerable<AstNode> GetChildren()
        {
            yield return Left;
            yield return Right;
        }
    }
}