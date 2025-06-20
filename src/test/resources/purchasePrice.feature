Feature: Purchase Price Page

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login
    Then verify successful login
    And click purchase dropdown toggle
    And click product purchase price link
    Then verify purchase price page

    Scenario: Create New Purchase Price
      When new record purchase price