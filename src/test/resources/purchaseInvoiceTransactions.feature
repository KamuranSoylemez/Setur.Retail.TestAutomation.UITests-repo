Feature: Purchase Invoice Transactions Page

  #Background: Navigate Login Page
    #Given navigate to login page
    #When fill username and password
    #And click login
    #Then verify successful login

  Scenario: Invoice Completion Process
    #When search by order number
    When order placed status by "PARFÜM-KOZMETİK"
    When set proforma and invoice
    And click purchase dropdown toggle
    And click invoice transactions link
    Then verify purchase invoice transaction page
    When search by invoice number
    And open invoice update frame
    And set the counting process
    And edit counting process
    And exclude shipping and save
    And put in stock process
    Then complete order process