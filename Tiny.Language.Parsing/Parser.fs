module Tiny.Language.Parsing.Parser

open Tiny.Language.AbstractSyntax
open FParsec

type Pos = Tiny.Language.AbstractSyntax.Position

type Parser<'TResult> = Parser<'TResult, Unit>

let toPos (pos:FParsec.Position) = Pos(pos.Index, pos.Line, pos.Column)

let (|>>>) (p:Parser<'Result>) (nodeBuilder : 'Result -> #AstNode) =
    pipe2 getPosition p
          (fun position result ->
                let node = nodeBuilder result
                node.Position <- position |> toPos
                node)

let str = pstring
let str_ws s = str s .>> spaces
let str_ws1 s = str s .>> spaces1
let betweenBrackets p = between (str_ws "(") (str_ws ")") p

let pPositiveInteger : Parser<_> =
    numberLiteral (NumberLiteralOptions.DefaultUnsignedInteger)
        "positive-integer"
    |>> fun pi -> System.Int32.Parse(pi.String)
    |>>> PositiveIntegerLiteralExpressionNode

let pVariableDeclaration : Parser<_> =
    str_ws1 "IN" >>. asciiUpper
    |>>> VariableDeclarationNode

let pVariableDeclarationList : Parser<_> =
    sepBy pVariableDeclaration (spaces1 >>? notFollowedBy(str "BEGIN"))

let pVariable : Parser<_> = asciiUpper |>>> VariableExpressionNode

let opp = OperatorPrecedenceParser<ExpressionNode, Position, Unit>()
let pExpression = opp.ExpressionParser
let toExpr = fun a -> a : ExpressionNode

opp.TermParser <- (pPositiveInteger |>> toExpr)
                  <|> (pVariable |>> toExpr)
                  <|> (betweenBrackets pExpression) .>> spaces

let adjustPosition offset (pos: Position) =
    Position(pos.StreamName, pos.Index + int64 offset,
             pos.Line, pos.Column + int64 offset)

let addInfixOperator str prec assoc (mapping : (ExpressionNode * ExpressionNode) -> #ExpressionNode) =
    let op = InfixOperator(str, getPosition .>> spaces, prec, assoc, (),
                           fun opPos leftTerm rightTerm ->
                               let result = mapping (leftTerm, rightTerm)
                               let position = adjustPosition -str.Length opPos
                               result.Position <- position |> toPos
                               result :> ExpressionNode)
    opp.AddOperator(op)    

addInfixOperator "+" 1 Associativity.Left AddExpressionNode
addInfixOperator "*" 2 Associativity.Left MultiplyExpressionNode

let pProgram =
    pVariableDeclarationList
    .>> spaces
    .>> str_ws1 "BEGIN"
    .>>. (pExpression .>> str_ws "END")
    |>>> ProgramNode

let parseWithParser p input =
    match runParserOnString (p .>> eof) () "" input with
    | Success(result,_,_) -> ParserResult(input, result)
    | Failure(errorMsg, parserError, _) ->
        let pos = parserError.Position |> toPos
        let msg = errorMsg
        ParserResult(input, msg, pos)  
        
let Parse input = parseWithParser (pProgram .>> spaces) input  
