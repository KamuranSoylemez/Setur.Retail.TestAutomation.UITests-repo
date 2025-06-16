Feature: Purchase Order Invoice Page

  Scenario Outline: Adding Proforma and Invoices to Order
    When order placed status by "<category>"
    And click Purchase dropdown toggle
    And click purchase order invoice link
    Then verify purchase order invoice page
    And search order by id and edit order

    Examples: Categories
      | category       |
      | TÜTÜN ÜRÜNLERİ |