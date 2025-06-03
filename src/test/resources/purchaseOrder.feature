Feature: Purchase Order Tests

  Background: Navigate Login Page
    Given navigate to login page
    When fill "username_password" and "username_password"
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
      And add product to order
      Then verify products

      Examples: Categories
        | category         |
        | PARFÜM-KOZMETİK  |
        | GIDA             |
        | TOBACCO PRODUCTS |
        | BUTİK-AKSESUAR   |
        | SPIRITS          |
        | OYUNCAK          |
        | BAZAAR           |
        | ELEKTRONİK       |
        | POŞET            |
        | EŞANTİYON        |