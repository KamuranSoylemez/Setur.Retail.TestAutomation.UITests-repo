Feature: Purchase Order Tests For One Category

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login
    Then verify successful login
    And click purchase dropdown toggle
    And click purchase order link
    Then verify purchase order page

  Scenario Outline: Tobacco Category Test
    When fill order date
    And select "<category>" from list
    And set distributor company by category
    And select firm responsible user
    And select distribution target type
    And select entry warehouse
    And select company address
    And select warehouse address
    Then check can auto complete and save
    When add product to order
    Then verify products
    And sending for approval process
    And approve order
    Then set order placed

    Examples: Categories
      | category       |
      | TÜTÜN ÜRÜNLERİ |