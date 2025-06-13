Feature: Purchase Order Tests For One Category

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login
    Then verify successful login
    And click Purchase dropdown toggle
    And click Order link
    Then verify PurchaseOrder page

  Scenario Outline:
    When fill order date
    And select "<category>" from list
    And set distributor company by category
    And select firm responsible user
    And select distribution target type
    And select entry warehouse
    And select company address
    And select warehouse address
    Then check can auto complete and save

    Examples: Categories
      | category       |
      | TÜTÜN ÜRÜNLERİ |