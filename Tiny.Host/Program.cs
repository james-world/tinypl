using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiny.Language.AbstractSyntax;
using Tiny.Language.SemanticModel;
using TinyHost.Language;

namespace Tiny.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            TestWithInput(true);
        }

        static void TestWithAst()
        {
            var ast =
                new ProgramNode(
                    new List<VariableDeclarationNode> { new VariableDeclarationNode('X') },
                    new AddExpressionNode(
                        new PositiveIntegerLiteralExpressionNode(4),
                        new VariableExpressionNode('X')));

            var compiler = new Compiler();
            var program = compiler.Compile(ast);

            program.Run();
        }

        static void TestWithInput(bool dumpJson = false)
        {
            while (true)
            {
                Console.WriteLine("Enter program (add blank line when done):\r\n");

                StringBuilder source = new StringBuilder();
                string line;
                while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
                {
                    source.AppendLine(line);
                }

                var input = source.ToString();

                if (string.IsNullOrWhiteSpace(input))
                    break;

                var result = LanguageService.ParseRuleSet(input);

                if (result.HasErrors)
                {
                    Console.WriteLine(result.GetErrors());
                    continue;
                }

                if (dumpJson)
                {
                    Console.WriteLine("\r\nJson representation: ");
                    Console.WriteLine(result.GetJson());
                    Console.WriteLine();
                }
                

                var ast = (ProgramNode) (result.AstRoot);

                var compiler = new Compiler();
                var program = compiler.Compile(ast);

                program.Run();
            }
        }
    }
}
