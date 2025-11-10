Feature: Genel Kondisyon Oluşturma ve Tanımlama

  Background: Navigate Login Page
    Given navigate to login page
    When login as special user "RETAIL_SPECIALIST" "rt2390RT!!"
    And click login button
    Then verify successful login
    And click supplier dropdown toggle


  @TEST1
  Scenario: Open new general condition definition screen
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    And  save new general condition form

  @TEST2
  Scenario: Verify field states for Rebate Fixed Margin and Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Fixed Margin"
    And select calculation type "Hesaplamasız"
    And wait for 2 seconds
    Then verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Tutar" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Ciro Para Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Marj Tipi" is enabled
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Marj" is mandatory
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Kdv Dahil mi?" has required asterisk

  @TEST3
  Scenario: Verify field states for Rebate Target Purchase Bonus, Alım adeti and Kademeli mi Hayır
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Purchase Bonus"
    And select calculation type "Alım adeti"
    
    # Select "Kademeli mi?" = "Hayır" (this is a condition, not a field to validate)
    And select "Hayır" for field "Kademeli mi?"
    
    # Disabled alanlar (1):
    # NOT: Marj Tipi ve Marj alanları bu kombinasyonda disabled değil - farklı kombinasyonda test edilmeli
    Then verify field "Hedef Ciro" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required attribute works (2)
    And verify field "Tutar" is mandatory
    And verify field "Oran" is mandatory
    
    # Conditional field test: Çarpan Var mı? controls Birim Çarpanı
    # When "Çarpan Var mı? = Evet", Birim Çarpanı becomes mandatory
    When select "Evet" for field "Çarpan Var mı?"
    Then verify field "Birim Çarpanı" is mandatory
    
    # Optional fields (4)
    # Note: Ciro Para Birimi is pre-filled with "EUR", so not testing it
    And verify field "Temel Ölçü Birimi" is optional
    And verify field "Brüt Satın Alma Tipi" is optional
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    
    # Mandatory field with CSS asterisk (1)
    # Hedef Miktar: Has red asterisk in UI (CSS :after), but not in HTML
    # Validation: Click save button and check for validation error (red border)
    And verify field "Hedef Miktar" shows validation error on save
    
    # Mutual Exclusion Test: Tutar ve Oran birbirini exclude ediyor
    # Test 1: Tutar girildiğinde Oran disabled olmalı
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    
    # Test 2: Tutar temizlendiğinde Oran enabled olmalı
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    
    # Test 3: Oran girildiğinde Tutar disabled olmalı
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST4
  Scenario: Verify field states for Rebate Target Purchase Bonus, Alım adeti and Kademeli mi Evet
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Purchase Bonus"
    And select calculation type "Alım adeti"
    
    # Select "Kademeli mi?" = "Evet" (this is a condition, not a field to validate)
    And select "Evet" for field "Kademeli mi?"
    
    # Disabled alanlar (7):
    # Kademeli mi? = Evet seçildiğinde çok fazla alan disabled oluyor
    # NOT: Marj Tipi ve Marj hem "Hayır" hem "Evet" kombinasyonunda enabled kalıyor
    Then verify field "Hedef Ciro" is disabled
    And verify field "Tutar" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Optional fields (4)
    # NOT: Ciro Para Birimi hem TEST3 hem TEST4'te pre-filled geliyor, mandatory değil
    And verify field "Ciro Para Birimi" is optional
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Satın Alma Tipi" is optional
  
  @TEST5
  Scenario: Verify field states for Rebate Target Purchase Bonus, Alım Tutarı and Kademeli mi Hayır
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Purchase Bonus"
    And select calculation type "Alım Tutarı"
    
    # Select "Kademeli mi?" = "Hayır" (this is a condition, not a field to validate)
    And select "Hayır" for field "Kademeli mi?"
    
    # Disabled alanlar (4):
    # NOT: Alım Tutarı seçilince Temel Ölçü Birimi, Hedef Miktar, Çarpan Var mı?, Birim Çarpanı disabled
    Then verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (3)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hedef Ciro" has required asterisk
    
    # Mandatory fields - HTML required attribute (2)
    And verify field "Tutar" is mandatory
    And verify field "Oran" is mandatory
    
    # Optional fields (3)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Satın Alma Tipi" is optional
    
    # Mutual Exclusion tests - Tutar ↔ Oran (3)
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST6
  Scenario: Verify field states for Rebate Target Purchase Bonus, Alım Tutarı and Kademeli mi Evet
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Purchase Bonus"
    And select calculation type "Alım Tutarı"
    And select "Evet" for field "Kademeli mi?"
    
    # Disabled alanlar (8)
    Then verify field "Hedef Ciro" is disabled
    And verify field "Tutar" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Optional fields (3)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Satın Alma Tipi" is optional

  @TEST7
  Scenario: Verify field states for Rebate Target Purchase Bonus and Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Purchase Bonus"
    And select calculation type "Hesaplamasız"
    
    # Disabled alanlar (7) - Kademeli mi? is disabled/default hayır
    Then verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required (1)
    And verify field "Tutar" is mandatory
    
    # Optional fields (3)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Satın Alma Tipi" is optional

  @TEST8
  Scenario: Verify field states for Rebate Purchase Bonus, Alım adeti and Kademeli mi Hayır
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Purchase Bonus"
    And select calculation type "Alım adeti"
    And select "Hayır" for field "Kademeli mi?"
    
    # Disabled alanlar (2)
    Then verify field "Hedef Ciro" is disabled
    And verify field "Hedef Miktar" is disabled
    
    # Set Çarpan Var mı? to Evet (makes Birim Çarpanı mandatory)
    And select "Evet" for field "Çarpan Var mı?"
    
    # Mandatory fields with asterisk (3)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Birim Çarpanı" has required asterisk
    
    # Mandatory fields - HTML required (2)
    And verify field "Tutar" is mandatory
    And verify field "Oran" is mandatory
    
    # Optional fields (4)
    And verify field "Temel Ölçü Birimi" is optional
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Satın Alma Tipi" is optional
    
    # Mutual Exclusion tests - Tutar ↔ Oran (3)
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST9
  Scenario: Verify field states for Rebate Purchase Bonus, Alım adeti and Kademeli mi Evet
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Purchase Bonus"
    And select calculation type "Alım adeti"
    And select "Evet" for field "Kademeli mi?"
    
    # Disabled alanlar (8)
    Then verify field "Hedef Ciro" is disabled
    And verify field "Tutar" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Optional fields (3)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Satın Alma Tipi" is optional

  @TEST10
  Scenario: Verify field states for Rebate Purchase Bonus, Alım Tutarı and Kademeli mi Hayır
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Purchase Bonus"
    And select calculation type "Alım Tutarı"
    And select "Hayır" for field "Kademeli mi?"
    
    # Disabled alanlar (5)
    Then verify field "Hedef Ciro" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required (2)
    And verify field "Tutar" is mandatory
    And verify field "Oran" is mandatory
    
    # Optional fields (3)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Satın Alma Tipi" is optional
    
    # Mutual Exclusion tests - Tutar ↔ Oran (3)
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST11
  Scenario: Verify field states for Rebate Purchase Bonus, Alım Tutarı and Kademeli mi Evet
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Purchase Bonus"
    And select calculation type "Alım Tutarı"
    And select "Evet" for field "Kademeli mi?"
    
    # Disabled alanlar (8)
    Then verify field "Hedef Ciro" is disabled
    And verify field "Tutar" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Optional fields (3)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Satın Alma Tipi" is optional

  @TEST12
  Scenario: Verify field states for Rebate Purchase Bonus and Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Purchase Bonus"
    And select calculation type "Hesaplamasız"
    
    # Disabled alanlar (7) - Kademeli mi? is disabled/default hayır
    Then verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required (1)
    And verify field "Tutar" is mandatory
    
    # Optional fields (3)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Satın Alma Tipi" is optional





  @TEST13
  Scenario: Verify field states for Rebate Target Sales Bonus, Satış Adeti and Kademeli mi Hayır
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Sales Bonus"
    And select calculation type "Satış Adeti"
    And select "Hayır" for field "Kademeli mi?"
    
    # Disabled alanlar (1)
    Then verify field "Hedef Ciro" is disabled
    
    # Set Çarpan Var mı? to Evet (makes Birim Çarpanı mandatory)
    And select "Evet" for field "Çarpan Var mı?"
    
    # Mandatory fields with asterisk (4)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hedef Miktar" has required asterisk
    And verify field "Birim Çarpanı" has required asterisk
    
    # Mandatory fields - HTML required (2)
    And verify field "Tutar" is mandatory
    And verify field "Oran" is mandatory
    
    # Optional fields (3)
    And verify field "Temel Ölçü Birimi" is optional
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    
    # Mutual Exclusion tests - Tutar ↔ Oran (3)
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST14
  Scenario: Verify field states for Rebate Target Sales Bonus, Satış Adeti and Kademeli mi Evet
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Sales Bonus"
    And select calculation type "Satış Adeti"
    And select "Evet" for field "Kademeli mi?"
    
    # Disabled alanlar (7)
    Then verify field "Hedef Ciro" is disabled
    And verify field "Tutar" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional

  @TEST15
  Scenario: Verify field states for Rebate Target Sales Bonus, Satış Cirosu and Kademeli mi Hayır
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Sales Bonus"
    And select calculation type "Satış Cirosu"
    And select "Hayır" for field "Kademeli mi?"
    
    # Disabled alanlar (4)
    Then verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (3)
    And verify field "Hedef Ciro" has required asterisk
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required (2)
    And verify field "Tutar" is mandatory
    And verify field "Oran" is mandatory
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    
    # Mutual Exclusion tests - Tutar ↔ Oran (3)
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST16
  Scenario: Verify field states for Rebate Target Sales Bonus, Satış Cirosu and Kademeli mi Evet
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Sales Bonus"
    And select calculation type "Satış Cirosu"
    And select "Evet" for field "Kademeli mi?"
    
    # Disabled alanlar (7)
    Then verify field "Hedef Ciro" is disabled
    And verify field "Tutar" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional

  @TEST17
  Scenario: Verify field states for Rebate Target Sales Bonus and Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Sales Bonus"
    And select calculation type "Hesaplamasız"
    
    # Disabled alanlar (7)
    Then verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required (1)
    And verify field "Tutar" is mandatory
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional

  @TEST18
  Scenario: Verify field states for Rebate Sales Bonus, Satış Adeti and Kademeli mi Hayır
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Sales Bonus"
    And select calculation type "Satış Adeti"
    And select "Hayır" for field "Kademeli mi?"
    
    # Disabled alanlar (2)
    Then verify field "Hedef Ciro" is disabled
    And verify field "Hedef Miktar" is disabled
    
    # Set Çarpan Var mı? to Evet (makes Birim Çarpanı mandatory)
    And select "Evet" for field "Çarpan Var mı?"
    
    # Mandatory fields with asterisk (3)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Birim Çarpanı" has required asterisk
    
    # Mandatory fields - HTML required (2)
    And verify field "Tutar" is mandatory
    And verify field "Oran" is mandatory
    
    # Optional fields (3)
    And verify field "Temel Ölçü Birimi" is optional
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    
    # Mutual Exclusion tests - Tutar ↔ Oran (3)
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST19
  Scenario: Verify field states for Rebate Sales Bonus, Satış Adeti and Kademeli mi Evet
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Sales Bonus"
    And select calculation type "Satış Adeti"
    And select "Evet" for field "Kademeli mi?"
    
    # Disabled alanlar (7)
    Then verify field "Hedef Ciro" is disabled
    And verify field "Tutar" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional

  @TEST20
  Scenario: Verify field states for Rebate Sales Bonus, Satış Cirosu and Kademeli mi Hayır
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Sales Bonus"
    And select calculation type "Satış Cirosu"
    And select "Hayır" for field "Kademeli mi?"
    
    # Disabled alanlar (5)
    Then verify field "Hedef Ciro" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required (2)
    And verify field "Tutar" is mandatory
    And verify field "Oran" is mandatory
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    
    # Mutual Exclusion tests - Tutar ↔ Oran (3)
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST21
  Scenario: Verify field states for Rebate Sales Bonus, Satış Cirosu and Kademeli mi Evet
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Sales Bonus"
    And select calculation type "Satış Cirosu"
    And select "Evet" for field "Kademeli mi?"
    
    # Disabled alanlar (7)
    Then verify field "Hedef Ciro" is disabled
    And verify field "Tutar" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional

  @TEST22
  Scenario: Verify field states for Rebate Sales Bonus and Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Sales Bonus"
    And select calculation type "Hesaplamasız"
    
    # Disabled alanlar (7)
    Then verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required (1)
    And verify field "Tutar" is mandatory
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional

  @TEST23
  Scenario: Verify field states for Rental Fee and Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rental Fee"
    And select calculation type "Hesaplamasız"
    
    # Disabled alanlar (7)
    Then verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required (1)
    And verify field "Tutar" is mandatory
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional

  @TEST24
  Scenario: Verify field states for Promotion Rental Fee and Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Promotion Rental Fee"
    And select calculation type "Hesaplamasız"
    
    # Disabled alanlar (7)
    Then verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required (1)
    And verify field "Tutar" is mandatory
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional

  @TEST25
  Scenario: Verify field states for Marketing Activity and Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Marketing Activity"
    And select calculation type "Hesaplamasız"
    
    # Disabled alanlar (7)
    Then verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required (1)
    And verify field "Tutar" is mandatory
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional

  @TEST26
  Scenario: Verify field states for Promotion Marketing Activity and Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "FOS-2025-FCA"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Promotion Marketing Activity"
    And select calculation type "Hesaplamasız"
    
    # Disabled alanlar (7)
    Then verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    
    # Mandatory fields with asterisk (2)
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    
    # Mandatory fields - HTML required (1)
    And verify field "Tutar" is mandatory
    
    # Optional fields (2)
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
