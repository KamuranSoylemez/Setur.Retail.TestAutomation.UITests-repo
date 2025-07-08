Feature: Purchase Create Order Page

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login button
    Then verify successful login
    And click purchasing dropdown toggle

    Scenario Outline: Order Creation Test
      And click on the purchase order creation link
      Then verify order creation page
      When fill order date
      And fill order name
      And select category from "<category>" list
      And set distributor company by category "<category>"
      And select company contact person
      And select distribution target type
      And select warehouse where the order will enter
      And select invoice address
      And select delivery address
      And complete order automatically mark checkbox to no
      And save order
      When add product to order
      Then verify products added to order
      And sending for approval process
      And approve order process
      And set order placed
      Then verify order by order id

      Examples: Categories
        | category        |
        | PARFÜM-KOZMETİK |
        | GIDA            |
        | TÜTÜN ÜRÜNLERİ  |
        | BUTİK-AKSESUAR  |
        | İÇKİ            |
        | OYUNCAK         |
        | BAZAAR          |
        | ELEKTRONİK      |
        | POŞET           |
        | EŞANTİYON       |


  Scenario: Create Order For One Category
    And click on the purchase order creation link
    Then verify order creation page
    When fill order date
    And fill order name
    And select category from "PARFÜM-KOZMETİK" list
    And set distributor company by category "PARFÜM-KOZMETİK"
    And select company contact person
    And select distribution target type
    And select warehouse where the order will enter
    And select invoice address
    And select delivery address
    And complete order automatically mark checkbox to no
    And save order
    When add product to order
    Then verify products added to order
    And sending for approval process
    And approve order process
    And set order placed
    Then verify order by order id


  Scenario: Adding Proforma and Invoices to Order
    When order placed status by "PARFÜM-KOZMETİK"
    And click purchasing dropdown toggle
    And click purchase order search link
    Then verify purchase order search page
    And search order by order number and edit order
    And go to order proformas tab
    And add info for proforma and save
    And copy order items and approve proforma
    And go to order invoices tab
    And add info for invoice and save
    And copy proforma items and approve invoice
    Then completing and approving invoice


  Scenario: Invoice Completion Process
    When order placed status by "PARFÜM-KOZMETİK"
    When set proforma and invoice
    And click purchasing dropdown toggle
    And click invoice transactions link
    Then verify purchase invoice transaction page
    When search by invoice number
    And open invoice update frame
    And completing counting process
    And edit counting process
    And exclude out of shipping and save
    And put in stock process
    Then complete order process

  Scenario: Create New Purchase Price For Defined Product
    And click purchase price link
    Then verify purchase price page
    When new record purchase price
    And create purchase price for defined product
    And select defined product
    And fill purchase price for defined product
    Then search defined product and verify amount


  Scenario: Create New Purchase Price For Undefined Product
    And click purchase price link
    Then verify purchase price page
    When new record purchase price
    And select purchase price for undefined product
    And select distributor company
    And select undefined product manufacturer company
    And fill purchase price for undefined product
    Then search undefined product and verify amount
