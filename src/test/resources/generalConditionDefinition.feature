
Feature: Genel Kondisyon Oluşturma ve Tanımlama

  Background: Navigate Login Page
    Given navigate to login page
    When login as special user "MELIKE_GORGUN" "Alaz060624."
    And click login button
    Then verify successful login
    And click supplier dropdown toggle

  @TEST1
  Scenario: Auto-generated for Rebate Fixed Margin, Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Fixed Margin"
    And select calculation type "Hesaplamasız"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is disabled
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" has required asterisk
    And verify field "Marj Tipi" has required asterisk
    And verify field "Marj" has required asterisk
    And verify field "Çoklu Ödül mü?" is disabled

  @TEST2
  Scenario: Auto-generated for Rebate Fixed Margin, Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Fixed Margin"
    And select calculation type "Hesaplamasız"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is disabled
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" has required asterisk
    And verify field "Marj Tipi" has required asterisk
    And verify field "Marj" is disabled
    And verify field "Çoklu Ödül mü?" is disabled

  @TEST3
  Scenario: Auto-generated for Rebate Target Purchase Bonus, Alım adedi
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Purchase Bonus"
    And select calculation type "Alım adedi"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is optional
    And verify field "Hedef Miktar" has required asterisk
    And verify field "Tutar Çarpan Var mı?" has required asterisk
    And verify field "Birim Çarpanı" has required asterisk
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is optional
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is optional
    And verify field "Marj" has required asterisk
    And verify field "Çoklu Ödül mü?" is optional

    # Mutual Exclusion: Tutar ↔ Oran
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST4
  Scenario: Auto-generated for Rebate Target Purchase Bonus, Alım Tutarı
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Purchase Bonus"
    And select calculation type "Alım Tutarı"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is required with asterisk
    And verify field "Hesaplama Oran" is mandatory
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is optional
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled

    # Mutual Exclusion: Tutar ↔ Oran
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST5
  Scenario: Auto-generated for Rebate Target Purchase Bonus, Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Purchase Bonus"
    And select calculation type "Hesaplamasız"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is optional
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled

  @TEST6
  Scenario: Auto-generated for Rebate Purchase Bonus, Alım adedi
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Purchase Bonus"
    And select calculation type "Alım adedi"
    And verify field "Kademeli mi?" has required asterisk
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is mandatory
    And verify field "Temel Ölçü Birimi" is optional
    And verify field "Hedef Miktar" has required asterisk
    And verify field "Tutar Çarpan Var mı?" has required asterisk
    And verify field "Birim Çarpanı" has required asterisk
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is optional
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is optional
    And verify field "Marj" has required asterisk
    And verify field "Çoklu Ödül mü?" is optional

    # Mutual Exclusion: Tutar ↔ Oran
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST7
  Scenario: Auto-generated for Rebate Purchase Bonus, Alım adedi
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Purchase Bonus"
    And select calculation type "Alım adedi"
    And verify field "Kademeli mi?" has required asterisk
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is optional
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is disabled
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is optional
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled
    And verify field "Çoklu Ödül mü?" is optional

  @TEST8
  Scenario: Auto-generated for Rebate Purchase Bonus, Alım Tutarı
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Purchase Bonus"
    And select calculation type "Alım Tutarı"
    And verify field "Kademeli mi?" has required asterisk
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is mandatory
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is optional
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled

    # Mutual Exclusion: Tutar ↔ Oran
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST9
  Scenario: Auto-generated for Rebate Purchase Bonus, Alım Tutarı
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Purchase Bonus"
    And select calculation type "Alım Tutarı"
    And verify field "Kademeli mi?" has required asterisk
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is disabled
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is optional
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled

  @TEST10
  Scenario: Auto-generated for Rebate Purchase Bonus, Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Purchase Bonus"
    And select calculation type "Hesaplamasız"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is optional
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled

  @TEST11
  Scenario: Auto-generated for Rebate Target Sales Bonus, Satış Adedi
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Sales Bonus"
    And select calculation type "Satış Adedi"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is mandatory
    And verify field "Temel Ölçü Birimi" is optional
    And verify field "Hedef Miktar" has required asterisk
    And verify field "Tutar Çarpan Var mı?" has required asterisk
    And verify field "Birim Çarpanı" has required asterisk
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is optional
    And verify field "Marj" has required asterisk
    And verify field "Çoklu Ödül mü?" has required asterisk

    # Mutual Exclusion: Tutar ↔ Oran
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST12
  Scenario: Auto-generated for Rebate Target Sales Bonus, Satış Cirosu
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Sales Bonus"
    And select calculation type "Satış Cirosu"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" has required asterisk
    And verify field "Hesaplama Oran" is mandatory
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is required with asterisk
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled

    # Mutual Exclusion: Tutar ↔ Oran
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST13
  Scenario: Auto-generated for Rebate Target Sales Bonus, Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Target Sales Bonus"
    And select calculation type "Hesaplamasız"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled

  @TEST14
  Scenario: Auto-generated for Rebate Sales Bonus, Satış Adedi
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Sales Bonus"
    And select calculation type "Satış Adedi"
    And verify field "Kademeli mi?" has required asterisk
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is mandatory
    And verify field "Temel Ölçü Birimi" is optional
    And verify field "Hedef Miktar" has required asterisk
    And verify field "Tutar Çarpan Var mı?" has required asterisk
    And verify field "Birim Çarpanı" has required asterisk
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is optional
    And verify field "Marj" has required asterisk
    And verify field "Çoklu Ödül mü?" has required asterisk

    # Mutual Exclusion: Tutar ↔ Oran
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST15
  Scenario: Auto-generated for Rebate Sales Bonus, Satış Adedi
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Sales Bonus"
    And select calculation type "Satış Adedi"
    And verify field "Kademeli mi?" has required asterisk
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is optional
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is disabled
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled
    And verify field "Çoklu Ödül mü?" is disabled

  @TEST16
  Scenario: Auto-generated for Rebate Sales Bonus, Satış Cirosu
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Sales Bonus"
    And select calculation type "Satış Cirosu"
    And verify field "Kademeli mi?" has required asterisk
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is mandatory
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled

    # Mutual Exclusion: Tutar ↔ Oran
    When fill field "Tutar" with "100"
    Then verify field "Oran" is disabled
    When clear field "Tutar"
    Then verify field "Oran" is not disabled
    When fill field "Oran" with "10"
    Then verify field "Tutar" is disabled

  @TEST17
  Scenario: Auto-generated for Rebate Sales Bonus, Satış Cirosu
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Sales Bonus"
    And select calculation type "Satış Cirosu"
    And verify field "Kademeli mi?" has required asterisk
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is disabled
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled
    And verify field "Çoklu Ödül mü?" is disabled

  @TEST18
  Scenario: Auto-generated for Rebate Sales Bonus, Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rebate Sales Bonus"
    And select calculation type "Hesaplamasız"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled

  @TEST19
  Scenario: Auto-generated for Rental Fee, Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Rental Fee"
    And select calculation type "Hesaplamasız"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled

  @TEST20
  Scenario: Auto-generated for Marketing Activity, Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Marketing Activity"
    And select calculation type "Hesaplamasız"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is not displayed
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is mandatory
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is disabled
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" is disabled
    And verify field "Marj" is disabled

  @TEST21
  Scenario: Auto-generated for Contract Margin, Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "AL LIBA-2025-CFR"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to new general condition button
    Then verify new general condition form is displayed
    When select condition type "Contract Margin"
    And select calculation type "Hesaplamasız"
    And verify field "Kademeli mi?" is disabled
    And verify field "Hedef Ciro" is disabled
    And verify field "Hesaplama Oran" is disabled
    And verify field "Temel Ölçü Birimi" is disabled
    And verify field "Hedef Miktar" is disabled
    And verify field "Tutar Çarpan Var mı?" is disabled
    And verify field "Birim Çarpanı" is disabled
    And verify field "Kdv Dahil mi?" has required asterisk
    And verify field "Hesaplama Periyodu" has required asterisk
    And verify field "Hesaplama Tutar Para Birimi" is disabled
    And verify field "Hesaplama Tutar" is disabled
    And verify field "Marka" is optional
    And verify field "Açıklama" is optional
    And verify field "Brüt Alım Kalem Tipi" is not displayed
    And verify field "İşlem Para Birimi" has required asterisk
    And verify field "Faturalama Para Birimi" is disabled
    And verify field "Marj Tipi" has required asterisk
    And verify field "Marj" has required asterisk
