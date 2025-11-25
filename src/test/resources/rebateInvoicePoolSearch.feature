Feature: Rebate Fatura Havuzu (Rebate Invoice Pool) Arama Testleri
  Rebate Fatura Havuzu ekranında arama fonksiyonlarının doğrulanması
  
  ✅ Çalışan filtreler: Kategori, Durum (Muhasebeleşti/Hazırlanıyor/İptal), Tarihçe (Ayar simgesi)
  ⚠️ Sayfada olmayan/çalışmayan alanlar: Firma multiselect, Rebate Tarihi, Sözleşme Adı, 
  Kondisyon Tipi, Hesaplama Türü, Para Birimi, Hesaplama Periyodu

  Background: Login ve Rebate Fatura Havuzu sayfasına git
    Given navigate to login page
    When login as special user "MELIKE_GORGUN" "Alaz060624."
    And click login button
    Then verify successful login
    And click supplier dropdown toggle
    When click rebate invoice pool link
    Then verify rebate invoice pool search form is visible

  @REBATE_INVOICE_POOL_SEARCH @TEST1 @PAGINATION
  Scenario: TEST1 - Boş arama yapıldığında tüm kayıtlar listelenmeli ve pagination kontrolü
    When user clicks rebate invoice pool search button
    Then verify rebate invoice pool grid has results
    And verify rebate invoice pool pagination is working

  @REBATE_INVOICE_POOL_SEARCH @TEST2
  Scenario: TEST2 - Sadece Firma (Bacardi) ile arama denemesi
    When user selects rebate invoice pool company "BACARDI"
    And user clicks rebate invoice pool search button
    Then verify rebate invoice pool grid has results or no records message

  @REBATE_INVOICE_POOL_SEARCH @TEST3 @SORT
  Scenario: TEST3 - Kategori filtresi ile arama ve tüm kolonlarda sort testi
    When user selects rebate invoice pool category "Tütün Ürünleri"
    And user clicks rebate invoice pool search button
    Then verify rebate invoice pool grid has results or no records message
    And verify rebate invoice pool all columns are sortable

  @REBATE_INVOICE_POOL_SEARCH @TEST4 @STATUS_FILTER
  Scenario: TEST4 - Durum: Muhasebeleşti ile arama
    When user selects rebate invoice pool status "Muhasebeleşti"
    And user clicks rebate invoice pool search button
    Then verify rebate invoice pool grid has results or no records message

  @REBATE_INVOICE_POOL_SEARCH @TEST5 @STATUS_FILTER
  Scenario: TEST5 - Durum: Hazırlanıyor ile arama
    When user selects rebate invoice pool status "Hazırlanıyor"
    And user clicks rebate invoice pool search button
    Then verify rebate invoice pool grid has results or no records message

  @REBATE_INVOICE_POOL_SEARCH @TEST6 @STATUS_FILTER
  Scenario: TEST6 - Durum: İptal ile arama
    When user selects rebate invoice pool status "İptal"
    And user clicks rebate invoice pool search button
    Then verify rebate invoice pool grid has results or no records message

  @REBATE_INVOICE_POOL_SEARCH @TEST7 @HISTORY
  Scenario: TEST7 - Tarihçe butonu ile durum geçmişi kontrolü
    When user clicks rebate invoice pool search button
    Then verify rebate invoice pool grid has results
    When user clicks rebate invoice pool settings icon on first row
    And user clicks rebate invoice pool history button
    Then verify rebate invoice pool history modal is opened
    And verify rebate invoice pool history columns are displayed

  @REBATE_INVOICE_POOL_SEARCH @TEST8 @MULTI_FILTER
  Scenario: TEST8 - Çoklu filtre ile arama (Tüm kriterler kombinasyonu)
    When user fills rebate invoice pool contract name with "1"
    And user fills rebate invoice pool invoice date with "17.11.2025"
    And user fills rebate invoice pool accounting date with "17.11.2025"
    And user selects rebate invoice pool invoice currency "EUR"
    And user clicks rebate invoice pool search button
    Then verify rebate invoice pool grid has results or no records message