using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiny.Language.AbstractSyntax;
using Tiny.Language.SemanticModel;
using TinyHost.Language;
using Xunit;

namespace Tiny.Language.Tests
{
    public class Scenarios
    {
        [Fact]
        public void NoVariableDeclarations()
        {
            var input = "BEGIN 2 END";
            var program = LanguageService.Compile(input);
            Assert.Equal(2, program.RunSilently());

        }

        [Fact]
        public void NoVariableDeclarations2()
        {
            var input = "BEGIN 2 END";
            var program = LanguageService.Compile(input);
            Assert.Equal(2, program.RunSilently());

        }
    }
}
