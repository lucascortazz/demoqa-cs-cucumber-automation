Feature: DemoQA text box form and public API validation
  As a QA automation engineer
  I want to validate a UI form and a public API response
  So that the original challenge behavior is covered with C# and Cucumber style BDD

  Scenario: Validate text box form and API response
    Given I open the DemoQA text box page
    When I submit the text box form with valid data
    Then the submitted form values should be displayed correctly
    When I request post 1 from the public API
    Then the API response should be valid
    And I print the success message
