Feature: Platform
				In order to add a platform
				As a user
				I want to be able to know if the details I used are valid

@CreatePlatform_ExistingPlatform_ExpectError
Scenario Outline: Platform already exists
	Given I enter the platform name 'YouTube' that already exists
	When I create the platform
	Then I am presented with the error message 'This User already exists'

@CreatePlatform_NonExistingPlatform_ExpectNoError
Scenario Outline: User doesnt exist
	Given I enter the platform name 'Twitter' that doesnt exist
	When I create the platform
	Then I am not presented with an error message

@CreatePlatform_InvalidName_ExpectError
Scenario Outline: Invalid name
	Given I enter the an empty platform name ''
	When I create the platform
	Then I am presented with the error message 'Invalid platform property'