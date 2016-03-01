namespace Tiny.Language.AbstractSyntax
{
    public class ParserError
    {
        public ParserError(string message, Position position)
        {
            Message = message;
            Position = position;
        }

        public string Message { get; }
        public Position Position { get; }
    }
}