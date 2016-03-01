using System.Collections.Generic;
using Tiny.Language.AbstractSyntax;

namespace TinyHost.Language
{
    public class TypeChecker : IAstNodeVisitor
    {
        private List<char> _variables;
        private ParserResult _result;

        public ParserResult Check(ParserResult result)
        {
            _result = result;
            _variables = new List<char>();

            result.AstRoot.Accept(this);
            return result;
        } 

        private void VisitChilden(AstNode node)
        {
            foreach (var child in node.GetChildren())
                child.Accept(this);
        }

        public void Visit(VariableDeclarationNode node)
        {
            _variables.Add(node.Name);
        }

        public void Visit(PositiveIntegerLiteralExpressionNode node)
        {
            VisitChilden(node);
        }

        public void Visit(AddExpressionNode node)
        {
            VisitChilden(node);
        }

        public void Visit(MultiplyExpressionNode node)
        {
            VisitChilden(node);
        }

        public void Visit(VariableExpressionNode node)
        {
            if (!_variables.Contains(node.Name))
                _result.AddUnknownVariableError(node.Name, node.Position);
        }

        public void Visit(ProgramNode node)
        {
            VisitChilden(node);
        }
    }
}
