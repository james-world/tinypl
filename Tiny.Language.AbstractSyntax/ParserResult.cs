using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tiny.Language.AbstractSyntax
{
    public class ParserResult
    {
        private readonly List<ParserError> _errors = new List<ParserError>();

        public string GetErrors()
        {
            var errors = from e in _errors
                let position = e.Position
                let header = $"Error in Ln: {position.Line} Col: {position.Column}"
                let body = e.Message
                select header + Environment.NewLine
                       + body;

            return string.Join(Environment.NewLine, errors);
        }

        public void AddError(string message, Position position)
        {
            _errors.Add(new ParserError(message, position));
        }

        public bool HasErrors => _errors.Count > 0;

        public void AddUnknownVariableError(char name, Position position)
        {
            string message = $"{Input}\r\n{new string(' ', (int)position.Column - 1) + '^'}\r\nUnknown variable '{name}'\r\n";
            AddError(message, position);
        }

        public string Input { get; }
        public AstNode AstRoot { get; }

        public ParserResult(string input, AstNode astRoot)
        {
            Input = input;
            AstRoot = astRoot;
        }

        public ParserResult(string input, string errorMessage, Position position)
        {
            Input = input;
            AddError(errorMessage, position);
        }

        public string GetJson()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
            return JsonConvert.SerializeObject(AstRoot, settings);
        }
    }
}