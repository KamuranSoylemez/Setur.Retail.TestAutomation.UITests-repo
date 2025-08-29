Feature: Distribution And Transportation Page

  Background: Navigate Login Page
    Given navigate to login page
    When fill username and password
    And click login button
    Then verify successful login
    And click distribution and transportation dropdown toggle

  @distributionAndTransportationTest
  Scenario: Distribution And Transportation Page Test
    When click create distribution link
    Then verify create distribution page is displayed
    And fill distribution form with valid data "KAPIKULE-SANAL"
    And click save button
    Then verify distribution is created successfully
    And add product to distribution "103"
    Then verify products added to distribution
    And distribution detail selection for EYK
    Then verify distributed products
    And send to transportation


  @EYKTransportationTest
  Scenario: EYK Distribution Processes
    When create distribution and transportation with warehouse "KAPIKULE-SANAL" and product "106"
    And click distribution and transportation dropdown toggle
    And click EYK waiting page link
    Then verify EYK waiting processes page is displayed
    And select warehouse and search for EYK waiting "KAPIKULE-SANAL"
    And create EYK preparation
    And send to counting process
    And click distribution and transportation dropdown toggle
    And click creating EYK link
    Then verify creating EYK page is displayed
    And open EYK update for creation EYK
    And save EYK no for verify record
    And click distribution and transportation dropdown toggle
    And click EYK listing page link
    Then verify EYK is completed successfully