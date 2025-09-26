Feature: Credit Note Definition And Search Page

  Background: Navigate Login Page
    Given navigate to login page
    When  login as special user "RETAIL_SPECIALIST" "rt2390RT!!"
    And   click login button
    Then  verify successful login
    Then  click purchasing dropdown toggle

  @searchCreditNoteForDifferentStatus
  Scenario: Credit Note Definition And Search Page Test
    When click credit note link
    Then verify credit note page is displayed "Credit Note"
    And  fill out the form to search credit notes with different status
      | status           |
      | Hazırlanıyor     |
      | Onaya Gönderildi |
      | Onaylandı        |
      | Muhasebeleşti    |
    Then sort the credit note list by all available columns

  @searchCreditNoteForOtherFields
  Scenario Outline: Credit Note Definition And Search Page Test
    When click credit note link
    Then verify credit note page is displayed "Credit Note"
    And fill the form with document no "<documentNo>" and document date "<documentDate>" and firm code "<firmCode>" and purchase order "<purchaseOrder>" and isBroken "<isBroken>"
    Examples:
      | documentNo     | documentDate | firmCode | purchaseOrder       | isBroken |
      | AUTOMATED TEST | 26.09.2025   | DPL      | 1-2025-DPL-00000102 | No       |