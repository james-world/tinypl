using System;
using Tiny.Language.AbstractSyntax;
using Tiny.Language.Parsing;
using Tiny.Language.SemanticModel;

namespace TinyHost.Language
{
    public class LanguageService
    {
        public static ParserResult ParseRuleSet(
            string input)
        {
            var result = Parser.Parse(input);

            if (result.HasErrors)
                return result;

            var typeChecker = new TypeChecker();
            result = typeChecker.Check(result);
            return result;
        }

        public static TinyProgram Compile(string input)
        {
            var result = Parser.Parse(input);

            if (result.HasErrors)
                throw new Exception($"Parser error: {result.GetErrors()}");

            var typeChecker = new TypeChecker();
            result = typeChecker.Check(result);

            if (result.HasErrors)
                throw new Exception($"Parser error: {result.GetErrors()}");

            var compiler = new Compiler();
            var program = compiler.Compile((ProgramNode)result.AstRoot);
            return program;
        }
    }
}