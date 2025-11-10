Feature: Kondisyon Güncelleme

  Background: Navigate Login Page
    Given navigate to login page
    When login as special user "MELIKE_GORGUN" "Alaz060624."
    And click login button
    Then verify successful login
    And click supplier dropdown toggle
    And click contract definition link
    Then verify contract definition page is displayed

  @TEST1 @CONDITION_UPDATE
  Scenario: TEST1 - Genel Kondisyon Güncelle Butonu Kontrolü
    Given click to sample contract "PMI-2026-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    And click general condition tab
    Then open general condition detail with id "632" and status "Onaylandı"
    And verify update button is visible on condition detail

  @TEST2 @CONDITION_UPDATE
  Scenario: TEST2 - Genel Kondisyon Ayar Menüsünde Güncelle Butonu Kontrolü
    Given click to sample contract "PMI-2026-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    And click general condition tab
    Then verify update button is visible for condition with status "Onaylandı"
    And verify history button is visible for condition with status "Onaylandı"

  @TEST3 @CONDITION_UPDATE
  Scenario: TEST3 - Kondisyon Güncelleme Pop-up Kontrolü 
    Given click to sample contract "PMI-2026-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    And click general condition tab
    And click update button for condition with status "Onaylandı"
    Then verify condition detail popup is displayed
    When click update button on condition detail popup
    Then verify condition update popup is displayed
    When click update button on condition update popup
    Then verify final update popup is displayed
   

  @TEST4 @CONDITION_UPDATE
  Scenario: TEST4 - Kondisyon Güncelleme Zorunlu Alan Kontrolü
    Given click to sample contract "PMI-2026-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    And click general condition tab
    And click update button for condition with status "Onaylandı"
    Then verify condition detail popup is displayed
    When click update button on condition detail popup
    Then verify condition update popup is displayed
    When click update button on condition update popup
    Then verify final update popup is displayed
    When click save button on final update popup without filling required fields
    Then verify update type field is mandatory
    And verify description field is mandatory
    And verify error message "Açıklama Alanı Boş Bırakılamaz." is displayed

        
