Feature: Login Page

  Background: Navigate Login Page
    Given navigate to login page

    Scenario Outline: Multiple Login
      When fill "<username>" and "<password>"
      And click login
      Then verify successful login

      Examples: Successful Login
        | username         | password    |
        | KAMURAN_SOYLEMEZ | ks1221KS!!3 |
