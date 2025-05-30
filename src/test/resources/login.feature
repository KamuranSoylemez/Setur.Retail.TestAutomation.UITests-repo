Feature: Login Page

  Background: Navigate Login Page
    Given navigate to login page

    Scenario: Successful Login
      When fill "username_password" and "username_password"
      And click login
      Then verify successful login

  Scenario Outline: Unsuccessful Login
    When try login with "<username>" and "<password>"
    And click login
    Then verify unsuccessful login

    Examples: User and Pass
      | username | password |
      | user     | pass     |
      |          |          |
      | admin    | 1234     |

