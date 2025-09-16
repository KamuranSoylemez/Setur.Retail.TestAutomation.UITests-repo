Feature: Genel Kondisyon Oluşturma ve Tanımlama

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login button
    Then verify successful login
    And click supplier dropdown toggle

  @TEST1
  Scenario: Open general condition definition screen
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    And click to new general condition button


