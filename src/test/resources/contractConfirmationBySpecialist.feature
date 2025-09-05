Feature: Contract Confirmation Page

  Background: Navigate Login Page
    Given navigate to login page
    When login as special user "RETAIL_SPECIALIST" "rt2390RT!!"
    And click login button
    And click supplier dropdown toggle

  @supplierTest
  Scenario: Contract Confirmation Page Test
    When click contract confirmation link
    Then verify contract confirmation page is displayed
    Then fill out the form "İptal Onayı Bekleniyor"
    Then click to search button
    Then click to first edit
    Then verify contract cancellation reject button is visible
    Then verify contract cancellation approve button is visible
