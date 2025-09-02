Feature: Contract Confirmation Page

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login button
    Then verify successful login
    And click supplier dropdown toggle

  @supplierTest
  Scenario: Contract Confirmation Page Test
    When click contract confirmation link
    Then verify contract confirmation page is displayed
    Then fill out the form
    Then click to search button
    Then click to first edit
    Then verify contract approve button is visible
