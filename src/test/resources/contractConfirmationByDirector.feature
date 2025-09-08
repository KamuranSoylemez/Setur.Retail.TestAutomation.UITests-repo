Feature: Contract Confirmation Page

  Background: Navigate Login Page
    Given navigate to login page
    When login as special user "RETAIL_DIRECTOR" "rt2390RT!!"
    And click login button
    And click supplier dropdown toggle

  @supplierTest
  Scenario: Director Confirmation Test for Director Approval Status
    When click contract confirmation link
    Then verify contract confirmation page is displayed
    Then fill out the form "Direktör Onayı Bekleniyor"
    Then click to search button
    Then click to first edit
    Then verify contract director reject button is visible
    Then verify contract director approve button is visible


  @supplierTest
  Scenario: Director Confirmation Test for Cancellation Approval Status
    When click contract confirmation link
    Then verify contract confirmation page is displayed
    Then fill out the form "İptal Onayı Bekleniyor"
    Then click to search button
    Then click to first edit
    Then verify contract cancellation reject button is visible
    Then verify contract cancellation approve button is visible
