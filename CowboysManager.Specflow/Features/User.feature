Feature: User
				In order to register my account
				As a user
				I want to be able to know if the details I used are valid

@CreateUser_ExistingUser_ExpectError
Scenario Outline: User already exists
	Given I enter the username 'Username1' that already exists
	When I create the account
	Then The user is presented with the error message 'This User already exists'

@CreateUser_NonExistingUser_ExpectNoError
Scenario Outline: User doesnt exist
	Given I enter the username 'Username' that doesnt exist
	When I create the account
	Then The user is not presented with an error message

@CreateUser_InvalidUsername_ExpectError
Scenario Outline: Invalid username
	Given I enter the an empty username <username>
	When I create the account
	Then The user is presented with the error message <error>

	Examples: 
	| username            | error                   |
	| ''                  | 'Invalid user property' |
	| 'UsernameIsTooLong' | 'Invalid user property' |