Feature: Contract Definition Page

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login button
    Then verify successful login
    And click contract definition dropdown toggle

  @contractDefinitionNewRecordTest
  Scenario Outline: Contract Definition Page Test
    When click contract definition link
    Then verify contract definition page is displayed
    And open new contract definition form
    And fill out the form and save "<category>"
    And save contract definition
    Then verify contract definition is created on main page "<category>"

    Examples: Category
      | category        |
      | BUTİK-AKSESUAR  |



  @contractDefinitionCompanyBasedControlTest
  Scenario Outline: Contract Definition Main Page Search Test
    When click contract definition link
    Then verify contract definition page is displayed
    And open new contract definition form
    And fill out the form for each "<category>" and "<type>"

    Examples: Categories and Types
      | category        | type            |
      | PARFÜM-KOZMETİK | MAKYAJ          |
      | GIDA            | ÇİKOLATA        |
      | TÜTÜN ÜRÜNLERİ  | PURO            |
      | BUTİK-AKSESUAR  | BUTİK           |
      | İÇKİ            | BİRA            |
      | OYUNCAK         | OYUNCAK         |
      | BAZAAR          | LOKUM           |
      | ELEKTRONİK      | TABLET          |
      | POŞET           | POSET           |
      | EŞANTİYON       | ACC HEDİYE ÜRÜN |
