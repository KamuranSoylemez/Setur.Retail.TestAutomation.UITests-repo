Feature: Supplier Page

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login button
    Then verify successful login
    And click supplier dropdown toggle

  @supplierTest
  Scenario Outline: Supplier Page Test
    When click contract definition link
    Then verify contract definition page is displayed
    And open new contract definition form
    And fill out the form and save "<category>"
    And save contract definition
    Then verify contract definition is created on main page "<category>"

    Examples: category
      | category        |
      | BUTİK-AKSESUAR  |



  @supplierSearchTest
  Scenario Outline: Supplier Main Page Search Test
    When click contract definition link
    Then verify contract definition page is displayed
    And open new contract definition form
    And fill out the form for each "<category>" and "<type>" and "<brand>"


    Examples: Categories
      | category        | type            | brand          |
      | PARFÜM-KOZMETİK | MAKYAJ          | CHRISTIAN DIOR |
      | GIDA            | ÇİKOLATA        | TADELLE        |
      | TÜTÜN ÜRÜNLERİ  | PURO            | LARK           |
      | BUTİK-AKSESUAR  | BUTİK           | FOSSIL         |
      | İÇKİ            | BİRA            | PATRON         |
      | OYUNCAK         | OYUNCAK         | SUNMAN         |
      | BAZAAR          | LOKUM           | DİVAN          |
      | ELEKTRONİK      | TABLET          | ARZUM          |
      | POŞET           | POSET           | ARCE PLASTİK   |
      | EŞANTİYON       | ACC HEDİYE ÜRÜN | MUMAY ÇANTA    |
