using System;

namespace Tiny.Language.AbstractSyntax
{
    public class PositiveIntegerLiteralExpressionNode : ExpressionNode, IEquatable<PositiveIntegerLiteralExpressionNode>
    {
        public int Value { get; }

        public PositiveIntegerLiteralExpressionNode(int value)
        {
            Value = value;
        }

        public bool Equals(PositiveIntegerLiteralExpressionNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PositiveIntegerLiteralExpressionNode) obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(PositiveIntegerLiteralExpressionNode left, PositiveIntegerLiteralExpressionNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PositiveIntegerLiteralExpressionNode left, PositiveIntegerLiteralExpressionNode right)
        {
            return !Equals(left, right);
        }

        public override void Accept(IAstNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public struct Position
    {
        public Position(long index, long line, long column)
        {
            Index = index;
            Line = line;
            Column = column;
        }

        public long Index { get; }

        public long Line { get; }

        public long Column { get; }
    }
}