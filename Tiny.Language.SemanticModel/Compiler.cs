using System;
using System.Collections.Generic;
using System.Linq;
using Tiny.Language.AbstractSyntax;

namespace Tiny.Language.SemanticModel
{
    public class Compiler : IAstNodeVisitor
    {
        private Dictionary<AstNode, object> _parts;
        private Dictionary<char, int> _variables;
        private Action<char, int> _loader;

        public TinyProgram Compile(ProgramNode programNode)
        {
            _parts = new Dictionary<AstNode, object>();
            _variables = new Dictionary<char, int>();
            _loader = (name, value) => _variables[name] = value;

            programNode.Accept(this);

            var variableList = _variables.Keys.ToList();

            Func<int> runner = (Func<int>) _parts[programNode.Program];

            return new TinyProgram(variableList,
                                   _loader,
                                   runner);
        }

        private void VisitChilden(AstNode node)
        {
            foreach (var child in node.GetChildren())
                child.Accept(this);
        }

        public void Visit(VariableDeclarationNode node)
        {
            _variables.Add(node.Name, 0);
        }

        public void Visit(PositiveIntegerLiteralExpressionNode node)
        {
            _parts[node] = (Func<int>)(() => node.Value);
        }

        public void Visit(AddExpressionNode node)
        {
            VisitChilden(node);
            var left = (Func<int>) _parts[node.Left];
            var right = (Func<int>)_parts[node.Right];
            _parts[node] = (Func<int>) (() => left() + right());
        }

        public void Visit(MultiplyExpressionNode node)
        {
            VisitChilden(node);
            var left = (Func<int>)_parts[node.Left];
            var right = (Func<int>)_parts[node.Right];
            _parts[node] = (Func<int>)(() => left() * right());
        }

        public void Visit(VariableExpressionNode node)
        {
            _parts[node] = (Func<int>) (() => _variables[node.Name]);
        }

        public void Visit(ProgramNode node)
        {
            VisitChilden(node);
        }
    }
}
