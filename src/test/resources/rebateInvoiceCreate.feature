Feature: Rebate Faturası Oluşturma ve Geri Çekme

  Background: Navigate Login Page
    Given navigate to login page
    When login as special user "MELIKE_GORGUN" "Alaz060624."
    And click login button
    Then verify successful login
    And click supplier dropdown toggle
    And click receivable pool link
    Then verify receivable pool page is displayed

  @REBATE_INVOICE_CREATE @TEST1
  Scenario: Rebate Faturası Oluşturma ve Geri Çekme Testi
    # Alacak Havuzu ekranında arama
    When fill receivable pool calculation date "31.05.2026"
    And fill receivable pool contract name "PMI-2026-FCA"
    And click receivable pool search button
    And wait for 2 seconds
    
    # İlk kaydı seç ve Rebate Faturası Oluştur
    When click first row checkbox in receivable pool
    And click create rebate invoice button
    Then verify create rebate invoice frame is opened
    
    # Frame içinde açıklama yaz ve kaydet
    When fill description in rebate invoice frame "TEST AUTOMATION"
    And click save button in rebate invoice frame
    And wait for 3 seconds
    
    # Alacak Havuzu ekranına geri dön ve fatura linkine tıkla
    When click invoice number link in receivable pool
    Then verify update rebate invoice frame is opened
    
    # Geri Çek butonuna tıkla
    When click reverse button in rebate invoice frame
    And wait for 5 seconds
    
    # Pop-up'ta geri çekme nedenini yaz ve onayla
    When fill reverse reason in popup "TEST REVERSE"
    And click confirm button in reverse popup
    And wait for 2 seconds
    
    # Başarı mesajını doğrula
    Then verify success message is displayed
