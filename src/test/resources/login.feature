Feature: Login Page

  Background: Navigate Login Page
    Given navigate to login page

    Scenario: Successful Login
      When fill "username_password" and "username_password"
      And click login
      Then verify successful login

