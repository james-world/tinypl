# tinypl
Tiny Programming Language created to demonstrate creating a trivial DSL with FParsec

TinyPL is a very *very* simple language that evaluates simple expressions containing variables. Here is an example:

    IN A IN B BEGIN A * (B + 4) END
    
Running this program will prompt for values for `A` and `B` and then evaluate the expression `A * (B + 4)`.

The code is a mixture of C# and F# using Visual Studio 2015. Open the solution and compile (which should restore all necessary nuget packages). 

This code accompanied a talk I gave at Brighton ALT.NET in 2015. It demonstrates creating a very simple DSL using the [FParsec](http://www.quanttec.com/fparsec/) library.

The solution includes BDD tests written using [Specflow](http://www.specflow.org/) that demonstrate the DSL. Ensure you have the extension "Specflow for Visual Studio 2015" if you want to play with the tests. Open the feature file "Language.Feature" in the project "Tiny.Language.Tests" to see how the language works.

To run the code, set Tiny.Host as the start project. This allows you to type in Tiny Programs, demonstrates parsing (with nice error feedback), shows the JSON representation of the AST, and then runs the programs prompting for variable values.

Have fun!

James

P.S. For those who care about such things, here is a grammar for TinyPL:

    letter = 'A' | 'B' | 'C' | 'D' | 'E' | 'F' | 'G' | 'H' | 'I' | 'J' | 'K' |
             'L' | 'M' | 'N' | 'O' | 'P' | 'Q' | 'R' | 'S' | 'T' | 'U' | 'V' |
             'W' | 'X' | 'Y' | 'Z' ;
    digit  = '0' | '1' | '2' | '3' | '4' |'5' | '6' | '7' | '8' | '9' ;
    identifier = letter ; 
    positive-integer = digit { digit } ;
    program = { variable-declaration } 'BEGIN' expression 'END' ; 
    variable-declaration = 'IN' identifier ;
    expression = positive-integer
                 | expression binary-op expression
                 | variable
                 | '(' expression ')' ;
    variable = identifier ; 
    binary-op = '+' | '*' ;
