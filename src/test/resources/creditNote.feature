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


  @createCreditNote
    Scenario Outline: Create Credit Note Test
    When click credit note link
    Then verify credit note page is displayed "Credit Note"
    When click add new credit note button
    Then create credit note by document no "<documentNo>" and purchase order "<purchaseOrder>" and isBroken "<isBroken>" and description "<description>"
    Then click save button in credit note popup
    Examples:
        | documentNo  | purchaseOrder     | isBroken  | description      |
        | TEST DOC 1  | 1-2025-DPL-00000102| No        | AUTOMATED TEST 1 |


        @editCreditNoteAndAddProduct
    Scenario Outline: Credit Note Edit And Add Product Test
    When click credit note link
    Then verify credit note page is displayed "Credit Note"
    And  fill the form with document no "<documentNo>" and firm code "<firmCode>" and purchase order "<purchaseOrder>" and isBroken "<isBroken>"
    Then edit first one from the credit note list and add product with invoiceNo "<invoiceNo>" and productCode "<productCode>" and quantity "<quantity>" and profitCenter "<profitCenter>" and creditNoteType "<creditNoteType>"
    Then click save button in credit note detail page
    Examples:
      | documentNo | firmCode | purchaseOrder       | isBroken | invoiceNo  | productCode | quantity | profitCenter | creditNoteType |
      | TEST DOC   | DPL      | 1-2025-DPL-00000102 | No        | TEST-61025 | 1107        | 10       | DFS GENEL    | Hasarlı Ürün   |

  @deleteCreditNoteProduct
  Scenario Outline: Delete Product from Credit Note Test
    When click credit note link
    Then verify credit note page is displayed "Credit Note"
    And  fill the form with document no "<documentNo>" and firm code "<firmCode>" and isBroken "<isBroken>"
    Then edit first one from the credit note list
    Then click delete icon on first product row
    Then confirm delete operation
    Then verify product was deleted
    Examples:
      | documentNo | firmCode | isBroken |
      | TEST DOC   | DPL      | No       |