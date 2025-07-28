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
    And fill required fields for product features
    And fill required fields for product common features by "PARFÜM-KOZMETİK" and "PRADA"
    And fill required fields for web
    And fill required fields for corona detail
    And save product definition
    Then verify product definition is saved successfully
