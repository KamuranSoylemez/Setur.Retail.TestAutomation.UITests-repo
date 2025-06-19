Feature: Purchase Order Invoice Page

  Scenario Outline: Adding Proforma and Invoices to Order
    When order placed status by "<category>"
    And click Purchase dropdown toggle
    And click purchase order invoice link
    Then verify purchase order invoice page
    And search order by id and edit order
    And add proforma to order
    And add info for proforma and save
    And copy order items and approve proforma
    Then add order invoices
    And add info for invoice and save
    And copy proforma items and approve invoice

    Examples: Categories
      | category       |
      | TÜTÜN ÜRÜNLERİ |