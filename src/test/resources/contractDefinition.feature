Feature: Contract Definition Page

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login button
    Then verify successful login
    And click supplier dropdown toggle

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


  @contractDefinitionCompanyBasedControlTest1
  Scenario: Contract Definition Category and Type And Brand Test
    When click contract definition link
    Then verify contract definition page is displayed
    And fill out the form for categories
      | category        | type             | brand          |
      | PARFÜM-KOZMETİK | MAKYAJ           | CHRISTIAN DIOR |
      | GIDA            | ÇİKOLATA         | TADELLE        |
      | TÜTÜN ÜRÜNLERİ  | PURO             | MARLBORO       |
      | BUTİK-AKSESUAR  | AKSESUAR         | FOSSIL         |
      | İÇKİ            | BİRA             | PATRON         |
      | OYUNCAK         | OYUNCAK-BEDELSİZ | SUNMAN         |
      | BAZAAR          | LOKUM            | DİVAN          |
      | ELEKTRONİK      | TABLET           | ARZUM          |
      | POŞET           | POSET            | ARCE PLASTİK   |
      | EŞANTİYON       | ACC HEDİYE ÜRÜN  | MUMAY ÇANTA    |

