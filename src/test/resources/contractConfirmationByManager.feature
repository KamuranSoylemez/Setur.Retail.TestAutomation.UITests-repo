Feature: Contract Confirmation Page

  Background: Navigate Login Page
    Given navigate to login page
    When login as special user "RETAIL_MANAGER" "rt2390RT!!"
    And click login button
    And click supplier dropdown toggle

  @supplierTest
  Scenario: Manager Confirmation Test for Manager Approval Status
    When click contract confirmation link
    Then verify contract confirmation page is displayed
    Then fill out the form "Müdür Onayı Bekleniyor"
    Then click to search button
    Then click to first edit
    Then verify contract approve button is visible
    Then verify contract reject button is visible


  @supplierTest
  Scenario: Manager Confirmation Test for Cancellation Approval Status
    When click contract confirmation link
    Then verify contract confirmation page is displayed
    Then fill out the form "İptal Onayı Bekleniyor"
    Then click to search button
    Then click to first edit
    Then check button count

  @supplierTest
  Scenario: Manager Confirmation Test for Director Approval Status
    When click contract confirmation link
    Then verify contract confirmation page is displayed
    Then fill out the form "Direktör Onayı Bekleniyor"
    Then click to search button
    Then click to first edit


