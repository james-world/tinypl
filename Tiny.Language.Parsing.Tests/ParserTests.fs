module ParserTests

open FParsec
open Tiny.Language.Parsing.Parser
open Tiny.Language.AbstractSyntax
open Xunit;

let assertParserResult<'T> parser input (expected : 'T) =
    match runParserOnString (parser .>> eof) () "" input with
    | Success(result,_,_) -> Assert.Equal<'T>(expected, result)
    | Failure(errorMsg,_,_) -> Assert.True(false, errorMsg)

let assertParserAstValue<'T when 'T :> AstNode> (parser:Parser<'T>) input (expected: 'T) =
    match runParserOnString (parser .>> eof) () "" input with
    | Success(result,_,_) -> Assert.Equal(expected, result)
    | Failure(errorMsg,_,_) -> Assert.True(false, errorMsg)

[<Theory>]
[<InlineData("IN A", 'A')>]
[<InlineData("IN B", 'B')>]
let ``parse variable-declaration`` input name =
    let expected = VariableDeclarationNode(name)
    assertParserResult pVariableDeclaration input expected

[<Fact>]
let ``parse variable-declaration list``() =
    let input = "IN A IN B"
    let expected = [
        VariableDeclarationNode('A')
        VariableDeclarationNode('B')
    ]
    assertParserResult pVariableDeclarationList input expected

[<Theory>]
[<InlineData("A", 'A')>]
[<InlineData("B", 'B')>]
let ``parse variable-expression`` input name =
    let expected = VariableExpressionNode(name)
    assertParserResult pVariable input expected

[<Fact>]
let ``parse expression``() =
    let input = "3 + X * 4"
    let expected = AddExpressionNode(
                       PositiveIntegerLiteralExpressionNode(3),
                       MultiplyExpressionNode(
                         VariableExpressionNode('X'),
                         PositiveIntegerLiteralExpressionNode(4))) :> ExpressionNode
    assertParserResult pExpression input expected

[<Fact>]
let ``parse expression with brackets``() =
    let input = "(3 + X) * 4"
    let expected = MultiplyExpressionNode(
                     AddExpressionNode(
                       PositiveIntegerLiteralExpressionNode(3),
                       VariableExpressionNode('X')),
                     PositiveIntegerLiteralExpressionNode(4)) :> ExpressionNode
    assertParserResult pExpression input expected

[<Fact>]
let ``parse program``() =
    let input = "IN A IN B BEGIN (3 + A) * 4 END"
    let expected =
        ProgramNode(
            [VariableDeclarationNode('A'); VariableDeclarationNode('B')],
               MultiplyExpressionNode(
                 AddExpressionNode(
                   PositiveIntegerLiteralExpressionNode(3),
                   VariableExpressionNode('A')),
                 PositiveIntegerLiteralExpressionNode(4)))
    assertParserResult pProgram input expected