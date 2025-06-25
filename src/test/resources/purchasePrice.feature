Feature: Purchase Price Page

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login
    Then verify successful login
    And click purchase dropdown toggle
    And click purchase price link
    Then verify purchase price page

  Scenario: Create New Purchase Price For Defined Product
      When new record purchase price
      And create purchase price for defined product
      Then search defined product and verify amount


  Scenario: Create New Purchase Price For Undefined Product
    When new record purchase price
    And create purchase price for undefined product
    Then search undefined product and verify amount