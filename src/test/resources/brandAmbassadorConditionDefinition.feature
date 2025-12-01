Feature: Brand Ambassador Kondisyon Oluşturma ve Tanımlama

  Background: Navigate Login Page
    Given navigate to login page
    When login as special user "MELIKE_GORGUN" "Alaz060624."
    And click login button
    Then verify successful login
    And click supplier dropdown toggle
    And click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "PMI-2025-DAP"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to brand ambassador condition tab
    And click to new brand ambassador condition button
    Then verify brand ambassador condition form is displayed

  @TEST1 @BRAND_AMBASSADOR @BUG
  Scenario: TEST1 - Salary + Hesaplamasız - Detaylı alan kontrolü
    When select brand ambassador condition type "Salary"
    And select brand ambassador calculation type "Hesaplamasız"
    And wait for 2 seconds

    # Girilmesi zorunlu alanlar (6 alan)
    Then verify brand ambassador field "Başlangıç Tarihi" is mandatory
    And verify brand ambassador field "Hesaplama Periyodu" is mandatory
    And verify brand ambassador field "Hesaplama Para Birimi" is mandatory
    And verify brand ambassador field "Bitiş Tarihi" is mandatory
    And verify brand ambassador field "Faturalama Para Birimi" is mandatory
    And verify brand ambassador field "Kdv Dahil mi?" is mandatory

    # Girilemez (disabled) alanlar (7 alan)
    And verify brand ambassador field "Kademeli mi?" is disabled
    And verify brand ambassador field "Hedefli mi?" is disabled
    And verify brand ambassador field "Temel Ölçü Birimi" is disabled
    And verify brand ambassador field "Birim Çarpanı" is disabled
    And verify brand ambassador field "Hesaplama Tutar" is disabled
    And verify brand ambassador field "Tutar Çarpan Var mı?" is disabled
    And verify brand ambassador field "Hesaplama Oran" is disabled

    # Girilebilir (optional) alanlar (2 alan)
    And verify brand ambassador field "Marka" is optional
    And verify brand ambassador field "Açıklama" is optional

    # Görünmeyen alanlar (2 alan)
    And verify brand ambassador field "Hedef Ciro" is not shown
    And verify brand ambassador field "Hedef Miktar" is not shown

  @TEST2 @BRAND_AMBASSADOR @BUG
  Scenario: TEST2 - Bonus + Hesaplamasız - Detaylı alan kontrolü
    When select brand ambassador condition type "Bonus"
    And select brand ambassador calculation type "Hesaplamasız"
    And wait for 2 seconds

    # Girilmesi zorunlu alanlar (8 alan)
    Then verify brand ambassador field "Başlangıç Tarihi" is mandatory
    And verify brand ambassador field "Bitiş Tarihi" is mandatory
    And verify brand ambassador field "Hesaplama Para Birimi" is mandatory
    And verify brand ambassador field "Faturalama Para Birimi" is mandatory
    And verify brand ambassador field "Kdv Dahil mi?" is mandatory
    And verify brand ambassador field "Birim Çarpanı" is mandatory
    And verify brand ambassador field "Tutar Çarpan Var mı?" is mandatory

    # Girilemez (disabled) alanlar (3 alan)
    And verify brand ambassador field "Kademeli mi?" is disabled
    And verify brand ambassador field "Hedefli mi?" is disabled
    And verify brand ambassador field "Hesaplama Tutar" is disabled
    And verify brand ambassador field "Hesaplama Oran" is disabled

    # Girilebilir (optional) alanlar (3 alan)
    And verify brand ambassador field "Hesaplama Periyodu" is optional
    And verify brand ambassador field "Temel Ölçü Birimi" is optional
    And verify brand ambassador field "Marka" is optional
    And verify brand ambassador field "Açıklama" is optional

    # Görünmeyen alanlar (2 alan)
    And verify brand ambassador field "Hedef Ciro" is not shown
    And verify brand ambassador field "Hedef Miktar" is not shown

  @TEST3 @BRAND_AMBASSADOR @BUG
  Scenario: TEST3 - Commission + Satış Adedi - Detaylı alan kontrolü
    When select brand ambassador condition type "Commission"
    And select brand ambassador calculation type "Satış Adedi"
    And wait for 2 seconds

    # Girilmesi zorunlu alanlar (12 alan)
    Then verify brand ambassador field "Başlangıç Tarihi" is mandatory
    And verify brand ambassador field "Hesaplama Periyodu" is mandatory
    And verify brand ambassador field "Kademeli mi?" is mandatory
    And verify brand ambassador field "Hesaplama Para Birimi" is mandatory
    And verify brand ambassador field "Hedefli mi?" is mandatory
    And verify brand ambassador field "Birim Çarpanı" is mandatory
    And verify brand ambassador field "Hesaplama Tutar" is mandatory
    And verify brand ambassador field "Bitiş Tarihi" is mandatory
    And verify brand ambassador field "Faturalama Para Birimi" is mandatory
    And verify brand ambassador field "Kdv Dahil mi?" is mandatory
    And verify brand ambassador field "Tutar Çarpan Var mı?" is mandatory
    And verify brand ambassador field "Hedef Miktar" is mandatory
    And verify brand ambassador field "Hesaplama Oran" is mandatory

    # Girilemez (disabled) alanlar (1 alan)
    And verify brand ambassador field "Hedef Ciro" is disabled

    # Girilebilir (optional) alanlar (3 alan)
    And verify brand ambassador field "Temel Ölçü Birimi" is optional
    And verify brand ambassador field "Marka" is optional
    And verify brand ambassador field "Açıklama" is optional

  @TEST4 @BRAND_AMBASSADOR @BUG
  Scenario: TEST4 - Commission + Satış Adedi (Hedefsiz) - Detaylı alan kontrolü
    When select brand ambassador condition type "Commission"
    And select brand ambassador calculation type "Satış Adedi"
    And wait for 2 seconds

    # Girilmesi zorunlu alanlar (11 alan)
    Then verify brand ambassador field "Başlangıç Tarihi" is mandatory
    And verify brand ambassador field "Hesaplama Periyodu" is mandatory
    And verify brand ambassador field "Kademeli mi?" is mandatory
    And verify brand ambassador field "Hesaplama Para Birimi" is mandatory
    And verify brand ambassador field "Hedefli mi?" is mandatory
    And verify brand ambassador field "Birim Çarpanı" is mandatory
    And verify brand ambassador field "Hesaplama Tutar" is mandatory
    And verify brand ambassador field "Bitiş Tarihi" is mandatory
    And verify brand ambassador field "Faturalama Para Birimi" is mandatory
    And verify brand ambassador field "Kdv Dahil mi?" is mandatory
    And verify brand ambassador field "Tutar Çarpan Var mı?" is mandatory
    And verify brand ambassador field "Hesaplama Oran" is mandatory

    # Görünmeyen alanlar (2 alan)
    And verify brand ambassador field "Hedef Ciro" is not shown
    And verify brand ambassador field "Hedef Miktar" is not shown

    # Girilebilir (optional) alanlar (3 alan)
    And verify brand ambassador field "Temel Ölçü Birimi" is optional
    And verify brand ambassador field "Marka" is optional
    And verify brand ambassador field "Açıklama" is optional

  @TEST5 @BRAND_AMBASSADOR
  Scenario: TEST5 - Commission + Hesaplamasız
    When select brand ambassador condition type "Commission"
    And select brand ambassador calculation type "Hesaplamasız"
    And wait for 2 seconds

    # Girilemez alanlar
    Then verify brand ambassador field "Kademe" is disabled
    And verify brand ambassador field "Hedef Adet" is disabled
    And verify brand ambassador field "Hedef Ciro" is disabled
    And verify brand ambassador field "Oran" is disabled

    # Girilmeli alanlar
    And verify brand ambassador field "Tutar" is mandatory

    # Girilebilir alanlar
    And verify brand ambassador field "Marka" is optional
    And verify brand ambassador field "Açıklama" is optional

  @TEST6 @BRAND_AMBASSADOR
  Scenario: TEST6 - Incentive + Satış Adedi
    When select brand ambassador condition type "Incentive"
    And select brand ambassador calculation type "Satış Adedi"
    And wait for 2 seconds

    # Girilemez alanlar
    Then verify brand ambassador field "Hedef Ciro" is disabled

    # Girilmeli alanlar
    And verify brand ambassador field "Hedef Adet" is mandatory
    And verify brand ambassador field "Tutar" is mandatory
    And verify brand ambassador field "Oran" is mandatory

    # Girilebilir alanlar
    And verify brand ambassador field "Kademe" is optional
    And verify brand ambassador field "Marka" is optional
    And verify brand ambassador field "Açıklama" is optional

  @TEST7 @BRAND_AMBASSADOR
  Scenario: TEST7 - Incentive + Satış Cirosu
    When select brand ambassador condition type "Incentive"
    And select brand ambassador calculation type "Satış Cirosu"
    And wait for 2 seconds

    # Girilemez alanlar
    Then verify brand ambassador field "Hedef Adet" is disabled

    # Girilmeli alanlar
    And verify brand ambassador field "Hedef Ciro" is mandatory
    And verify brand ambassador field "Tutar" is mandatory
    And verify brand ambassador field "Oran" is mandatory

    # Girilebilir alanlar
    And verify brand ambassador field "Kademe" is optional
    And verify brand ambassador field "Marka" is optional
    And verify brand ambassador field "Açıklama" is optional

  @TEST8 @BRAND_AMBASSADOR
  Scenario: TEST8 - Incentive + Hesaplamasız
    When select brand ambassador condition type "Incentive"
    And select brand ambassador calculation type "Hesaplamasız"
    And wait for 2 seconds

    # Girilemez alanlar
    Then verify brand ambassador field "Kademe" is disabled
    And verify brand ambassador field "Hedef Adet" is disabled
    And verify brand ambassador field "Hedef Ciro" is disabled
    And verify brand ambassador field "Oran" is disabled

    # Girilmeli alanlar
    And verify brand ambassador field "Tutar" is mandatory

    # Girilebilir alanlar
    And verify brand ambassador field "Marka" is optional
    And verify brand ambassador field "Açıklama" is optional
