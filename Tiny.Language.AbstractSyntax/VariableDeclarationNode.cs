using System;

namespace Tiny.Language.AbstractSyntax
{
    public class VariableDeclarationNode : AstNode, IEquatable<VariableDeclarationNode>
    {
        public char Name { get; }

        public VariableDeclarationNode(char name)
        {
            if(!char.IsUpper(name))
                throw new ArgumentException(
                    $"Variable name 'name' is not upper case.",
                    nameof(name));

            Name = name;
        }

        public bool Equals(VariableDeclarationNode other)
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
            return Equals((VariableDeclarationNode) obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(VariableDeclarationNode left, VariableDeclarationNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VariableDeclarationNode left, VariableDeclarationNode right)
        {
            return !Equals(left, right);
        }

        public override void Accept(IAstNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}