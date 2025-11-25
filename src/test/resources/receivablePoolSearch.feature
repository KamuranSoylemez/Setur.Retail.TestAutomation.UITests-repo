Feature: Alacak Havuzu (Receivable Pool) Arama Testleri
  Alacak Havuzu ekranında arama fonksiyonlarının doğrulanması
  
  Alanlar: Firma, Rebate Tarihi, Kondisyon Tipi, Hesaplama Türü, Durum, 
           Para Birimi, Hesaplama Periyodu, Sözleşme Adı, Kategori, Açıklama

  Background: Login ve Alacak Havuzu sayfasına git
    Given navigate to login page
    When login as special user "MELIKE_GORGUN" "Alaz060624."
    And click login button
    Then verify successful login
    And click supplier dropdown toggle
    When click receivable pool link
    Then verify search form is visible

  @RECEIVABLE_POOL_SEARCH @TEST1 @PAGINATION
  Scenario: TEST1 - Boş arama yapıldığında tüm kayıtlar listelenmeli ve pagination kontrolü
    When user clicks search button
    Then verify grid has results
    And verify pagination is working

  @RECEIVABLE_POOL_SEARCH @TEST1A
  Scenario: TEST1A - Sadece Firma (Bacardi) ile arama
    When user selects company "Bacardi"
    And user clicks search button
    Then verify grid has results or no records message

  @RECEIVABLE_POOL_SEARCH @TEST2
  Scenario: TEST2 - Firma (Bacardi), Rebate Tarihi ve Sözleşme Adı ile arama
    When user selects company "Bacardi"
    And user fills rebate date with "31.05.2024"
    And user fills contract name with "BACARDI-2023-CFR"
    And user clicks search button
    Then verify grid has results or no records message

  @RECEIVABLE_POOL_SEARCH @TEST3
  Scenario: TEST3 - Çoklu filtre ile detaylı arama (BACARDI büyük harfle)
    When user selects company "BACARDI"
    And user selects condition type "Rebate Purchase Bonus"
    And user selects calculation type "Alım Adeti"
    And user selects category "Tütün Ürünleri"
    And user selects currency "EUR"
    And user selects calculation period "Aylık"
    And user selects status "Fatura Oluşturuldu"
    And user clicks search button
    Then verify grid has results or no records message

  @RECEIVABLE_POOL_SEARCH @TEST4 @NEGATIVE
  Scenario: TEST4 - Negatif Test: Olmayan sözleşme adıyla arama
    When user fills contract name with "NONEXISTENT-CONTRACT-12345"
    And user clicks search button
    Then verify grid has no results

  @RECEIVABLE_POOL_SEARCH @TEST5 @SORT
  Scenario: TEST5 - Kondisyon Tipi ile filtreleme ve tüm kolonlarda sort testi
    When user selects condition type "Rebate Purchase Bonus"
    And user clicks search button
    Then verify grid has results
    And verify all columns are sortable

  @RECEIVABLE_POOL_SEARCH @TEST6 @HISTORY
  Scenario: TEST6 - Tarihçe sayfası kontrolü - Ay simgesine tıklayıp açıklama alanını kontrol et
    When user clicks search button
    Then verify grid has results
    When user clicks history icon on first row
    Then verify history page is opened
    And verify history description contains condition ids and explanation

  @RECEIVABLE_POOL_SEARCH @TEST10 @CREATE_INVOICE_NEGATIVE
  Scenario: TEST10 - Fatura Oluşturma olumsuz - Hiçbir kayıt seçilmeden butona basılır
    When user clicks search button
    Then verify grid has results
    When user clicks create rebate invoice button without selection
    Then verify warning message "Lütfen en az bir kayıt seçiniz." is displayed
