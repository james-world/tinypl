using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Tiny.Language.AbstractSyntax
{
    public class ProgramNode : AstNode, IEquatable<ProgramNode>
    {
        
        public ExpressionNode Program { get; }
        private readonly List<VariableDeclarationNode> _variableDeclarations;

        [JsonProperty(Order = -10)]
        public IReadOnlyList<VariableDeclarationNode> VariableDeclarations
            => _variableDeclarations.AsReadOnly();

        public ProgramNode(
            IEnumerable<VariableDeclarationNode> variableDeclarations,
            ExpressionNode program)
        {
            if (program == null) throw new ArgumentNullException(nameof(program));

            Program = program;
            _variableDeclarations =
                variableDeclarations?.ToList()
                ?? Enumerable.Empty<VariableDeclarationNode>().ToList();
        }

        public bool Equals(ProgramNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _variableDeclarations.SequenceEqual(other._variableDeclarations)
                   && Program.Equals(other.Program);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ProgramNode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_variableDeclarations.GetHashCode()*397) ^ Program.GetHashCode();
            }
        }

        public static bool operator ==(ProgramNode left, ProgramNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProgramNode left, ProgramNode right)
        {
            return !Equals(left, right);
        }

        public override IEnumerable<AstNode> GetChildren()
        {
            foreach (var variableDeclaration in _variableDeclarations)
                yield return variableDeclaration;

            yield return Program;
        }

        public override void Accept(IAstNodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}