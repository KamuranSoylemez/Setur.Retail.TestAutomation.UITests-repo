Feature: Brand Ambassador Kondisyon Oluşturma ve Tanımlama

  Background: Navigate Login Page
    Given navigate to login page
    When login as special user "MELIKE_GORGUN" "Alaz060624."
    And click login button
    Then verify successful login
    And click supplier dropdown toggle

  @TEST1 @BRAND_AMBASSADOR
  Scenario: TEST1 - Salary + Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "PMI-2025-DAP"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to brand ambassador condition tab
    And click to new brand ambassador condition button
    Then verify brand ambassador condition form is displayed
    When select brand ambassador condition type "Salary"
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

  @TEST2 @BRAND_AMBASSADOR
  Scenario: TEST2 - Bonus + Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "PMI-2025-DAP"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to brand ambassador condition tab
    And click to new brand ambassador condition button
    Then verify brand ambassador condition form is displayed
    When select brand ambassador condition type "Bonus"
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

  @TEST3 @BRAND_AMBASSADOR
  Scenario: TEST3 - Commission + Satış Adedi
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "PMI-2025-DAP"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to brand ambassador condition tab
    And click to new brand ambassador condition button
    Then verify brand ambassador condition form is displayed
    When select brand ambassador condition type "Commission"
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

  @TEST4 @BRAND_AMBASSADOR
  Scenario: TEST4 - Commission + Satış Cirosu
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "PMI-2025-DAP"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to brand ambassador condition tab
    And click to new brand ambassador condition button
    Then verify brand ambassador condition form is displayed
    When select brand ambassador condition type "Commission"
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

  @TEST5 @BRAND_AMBASSADOR
  Scenario: TEST5 - Commission + Hesaplamasız
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "PMI-2025-DAP"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to brand ambassador condition tab
    And click to new brand ambassador condition button
    Then verify brand ambassador condition form is displayed
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
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "PMI-2025-DAP"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to brand ambassador condition tab
    And click to new brand ambassador condition button
    Then verify brand ambassador condition form is displayed
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
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "PMI-2025-DAP"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to brand ambassador condition tab
    And click to new brand ambassador condition button
    Then verify brand ambassador condition form is displayed
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
    When click contract definition link
    Then verify contract definition page is displayed
    Given click to sample contract "PMI-2025-DAP"
    And click to search button on definition page
    And click to first edit button on definition page
    When click to brand ambassador condition tab
    And click to new brand ambassador condition button
    Then verify brand ambassador condition form is displayed
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
