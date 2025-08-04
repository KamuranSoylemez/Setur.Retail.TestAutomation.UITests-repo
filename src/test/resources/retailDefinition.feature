Feature: Retail Definition Page

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login button
    Then verify successful login
    And click retail definition dropdown toggle

  @productDefinitionTest
  Scenario: Product Definition Page Test
    When click product definition link
    Then verify product definition page is displayed
    And click new record button
    Then verify product definition form is displayed
    And fill required fields for product features "PRADA"
    And fill required fields for product common features by "PARFÜM-KOZMETİK" and "PRADA"
    And fill required fields for web
    And fill required fields for corona detail "COSMETIC"
    And save product definition
    And save barcode number
    And select origin "FRANSA"
    And select limit "L - Limitsiz"
    Then update and verify product definition is saved successfully
    And activate new record
    Then verify product definition is activated successfully
    And copy new record
    Then verify product definition is copied successfully


  @excelDownloadTest
  Scenario Outline: Download Excel For <type> Product
    When click product definition link
    Then verify product definition page is displayed
    And download excel format for <type> product
    Then verify excel file is downloaded successfully

    Examples:
      | type               |
      | product_definition |
      | product_update     |

  @excelUploadTest
  Scenario Outline: Upload Excel For <type> Product
    When click product definition link
    Then verify product definition page is displayed
    And upload excel format for <type> product
    Then verify excel file is uploaded successfully

    Examples:
      | type               |
      | product_definition |
      | product_update     |

