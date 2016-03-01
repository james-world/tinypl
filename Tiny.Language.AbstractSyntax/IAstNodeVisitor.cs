using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiny.Language.AbstractSyntax
{
    public interface IAstNodeVisitor
    {
        void Visit(VariableDeclarationNode node);
        void Visit(PositiveIntegerLiteralExpressionNode node);
        void Visit(AddExpressionNode node);
        void Visit(MultiplyExpressionNode node);
        void Visit(VariableExpressionNode node);
        void Visit(ProgramNode node);
    }
}
