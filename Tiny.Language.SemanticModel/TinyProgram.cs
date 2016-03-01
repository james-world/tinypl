using System;
using System.Collections.Generic;

namespace Tiny.Language.SemanticModel
{
    public class TinyProgram
    {
        private readonly List<char> _variables;
        private readonly Action<char, int> _loader;
        private readonly Func<int> _runner;

        public TinyProgram(
            List<char> variables,
            Action<char, int> loader,
            Func<int> runner)
        {
            _variables = variables;
            _loader = loader;
            _runner = runner;
        }

        public void Run()
        {
            foreach (var variable in _variables)
            {
                Console.Write($"Enter value for {variable}: ");
                string value;
                int intValue;
                do
                {
                    value = Console.ReadLine();
                } while (!int.TryParse(value, out intValue));
                _loader(variable, intValue);
            }

            var result = _runner();

            Console.WriteLine($"Result: {result}");

            Console.WriteLine("Press any key...");
            Console.ReadKey(true);
        }

        public void SetVar(char variable, int value)
        {
            _loader(variable, value);
        }

        public int RunSilently()
        {
            return _runner();
        }
    }
}