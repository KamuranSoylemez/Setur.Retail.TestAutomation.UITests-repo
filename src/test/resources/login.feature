Feature: Login Page

  Background: Navigate Login Page
    Given navigate to login page

    Scenario: Successful Login
      When fill username and password
      And click login button
      Then verify successful login

  Scenario Outline: Unsuccessful Login
    When try login with incorrect "<username>" or "<password>"
    And click login button
    Then verify unsuccessful login

    Examples: User and Pass
      | username         | password        |
      | USERNAME         | PASSWORD        |
      |                  |                 |
      | ADMINUSER        | 1234567         |
      | KAMURAN_SÖYLEMEZ |                 |
      |                  | correctPassword |
      | KAMURAN_SÖYLEMEZ | xxx             |
      | xxxxxx           | correctPassword |

