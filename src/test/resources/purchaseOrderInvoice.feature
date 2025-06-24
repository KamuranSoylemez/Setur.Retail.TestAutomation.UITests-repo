Feature: Purchase Order Invoice Page

   #Background:
     #Given navigate to login page
     #When fill username and password
     #And click login
     #Then verify successful login

  Scenario Outline: Adding Proforma and Invoices to Order
    When order placed status by "<category>"
    And click purchase dropdown toggle
    And click purchase order search link
    Then verify purchase order search page
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