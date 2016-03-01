using System;
using TechTalk.SpecFlow;
using Tiny.Language.SemanticModel;
using TinyHost.Language;
using Xunit;

namespace Tiny.Language.Tests
{
    [Binding]
    public class LanguageSteps
    {
        [Given(@"the program ""(.*)""")]
        public void GivenTheProgram(string input)
        {
            ScenarioContext.Current["Source"] = input;
        }

        [When(@"I run the program")]
        public void WhenIRunTheProgram()
        {

            var program = (TinyProgram)ScenarioContext.Current["Program"];
            var result = program.RunSilently();
            ScenarioContext.Current["Result"] = result;
        }

        [When(@"I set the variable ""(.*)"" to be (.*)")]
        public void WhenISetTheVariableToBe(string variableName, int value)
        {
            Assert.Equal(1, variableName.Length);
            var program = (TinyProgram)ScenarioContext.Current["Program"];
            program.SetVar(variableName[0], value);
        }

        [When(@"I compile the program")]
        public void WhenICompileTheProgram()
        {
            var source = (string)ScenarioContext.Current["Source"];
            try
            {
                var program = LanguageService.Compile(source);
                ScenarioContext.Current["Program"] = program;
            }
            catch (Exception ex)
            {
                ScenarioContext.Current["Error"] = ex.Message;
            }
        }


        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int expected)
        {
            var result = (int)ScenarioContext.Current["Result"];
            Assert.Equal(expected, result);
        }

        [Then(@"the error should be")]
        public void ThenTheResultShouldBe(string expected)
        {
            var result = (string)ScenarioContext.Current["Error"];
            Assert.Equal(expected, result);
        }
    }
}
