Feature: Language
	Tiny Programming Language

Scenario: Simplest Program
	Given the program "BEGIN 1 END"
	When I compile the program
	And I run the program
	Then the result should be 1

Scenario: Simply using a variable
	Given the program "IN A BEGIN A END"
	When I compile the program
	And I set the variable "A" to be 4
	And I run the program
	Then the result should be 4

Scenario: Adding and multiplying precedence
	Given the program "BEGIN 5 + 3 * 2 END"
	When I compile the program
	And I run the program
	Then the result should be 11

Scenario: Brackets changes precedence
	Given the program "BEGIN (4 + 3) * 2 END"
	When I compile the program
	And I run the program
	Then the result should be 14

Scenario: Using two variables
	Given the program "IN A IN B BEGIN A * B END"
	When I compile the program
	And I set the variable "A" to be 4
	And I set the variable "B" to be 6
	And I run the program
	Then the result should be 24

Scenario: Syntax error with incorrect operator
	Given the program "BEGIN (4 - 3) * 2 END"
	When I compile the program
	Then the error should be
		"""
		Parser error: Error in Ln: 1 Col: 10
		Error in Ln: 1 Col: 10
		BEGIN (4 - 3) * 2 END
		         ^
		Expecting: infix operator or ')'

		"""

Scenario: Syntax error with missing BEGIN operator
	Given the program "(4 + 3) * 2 END"
	When I compile the program
	Then the error should be
		"""
		Parser error: Error in Ln: 1 Col: 1
		Error in Ln: 1 Col: 1
		(4 + 3) * 2 END
		^
		Expecting: 'BEGIN' or 'IN'

		"""

Scenario: Syntax error with missing close bracket
	Given the program "BEGIN (4 + 3 * 2 END"
	When I compile the program
	Then the error should be
		"""
		Parser error: Error in Ln: 1 Col: 18
		Error in Ln: 1 Col: 18
		BEGIN (4 + 3 * 2 END
		                 ^
		Expecting: infix operator or ')'

		"""

Scenario: Semantic error with unknown variable
	Given the program "BEGIN (4 + 3) * A END"
	When I compile the program
	Then the error should be
		"""
		Parser error: Error in Ln: 1 Col: 17
		BEGIN (4 + 3) * A END
		                ^
		Unknown variable 'A'

		"""
