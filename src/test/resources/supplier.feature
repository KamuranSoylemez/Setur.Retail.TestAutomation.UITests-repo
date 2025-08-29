Feature: Supplier Page

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login button
    Then verify successful login
    And click supplier dropdown toggle

  @supplierTest
  Scenario: Supplier Page Test
    When click contract definition link
    Then verify contract definition page is displayed
    And open new contract definition form
    Then fill out the form and save "BUTİK-AKSESUAR"
    Then save contract definition