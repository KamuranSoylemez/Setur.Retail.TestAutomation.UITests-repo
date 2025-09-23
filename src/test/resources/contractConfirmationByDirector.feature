Feature: Contract Confirmation Page

  Background: Navigate Login Page
    Given navigate to login page
    When login as special user "RETAIL_DIRECTOR" "rt2390RT!!"
    And click login button
    And click supplier dropdown toggle

  @supplierTest
  Scenario: Director Confirmation Test for Director Approval Status
    When click contract confirmation link
    Then verify contract confirmation page is displayed "Sözleşme Onay İşlemleri"
    Then fill sample contract field "SWRI-2025-CFR"
    Then fill out the form by dates and status "Direktör Onayı Bekleniyor" "01.09.2025" "31.08.2026"
    Then fill firm field "1350-SWR" "SWAROVSKI INTERNATIONAL"
    Then click to search button
    Then click to first edit
    Then verify contract director reject button is visible
    Then verify contract director approve button is visible


  @supplierTest
  Scenario: Director Confirmation Test for Cancellation Approval Status
    When click contract confirmation link
    Then verify contract confirmation page is displayed "Sözleşme Onay İşlemleri"
    Then fill sample contract field "SWRI-2025-CFR"
    Then fill out the form by dates and status "İptal Onayı Bekleniyor" "01.09.2025" "31.08.2026"
    Then fill firm field "1350-SWR" "SWAROVSKI INTERNATIONAL"
    Then click to search button
    Then click to first edit
    Then verify contract cancellation reject button is visible
    Then verify contract cancellation approve button is visible


  @supplierTest
  Scenario: Director Confirmation Test for Manager Approval Status
    When click contract confirmation link
    Then verify contract confirmation page is displayed "Sözleşme Onay İşlemleri"
    Then fill sample contract field "SWRI-2025-CFR"
    Then fill out the form by dates and status "Müdür Onayı Bekleniyor" "01.09.2025" "31.08.2026"
    Then fill firm field "1350-SWR" "SWAROVSKI INTERNATIONAL"
    Then click to search button
    Then click to first edit
    Then check button count