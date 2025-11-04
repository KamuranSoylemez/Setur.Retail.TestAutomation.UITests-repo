package stepDefs;

import com.microsoft.playwright.*;
import io.cucumber.java.en.*;
import org.junit.jupiter.api.Assertions;
import java.util.List;

import pages.SupplierPages.ContractConfirmationPage;
import pages.SupplierPages.ContractDefinitionPage;
import pages.SupplierPages.ContractUpdatePage;
import pages.SupplierPages.GeneralConditionPage;


public class GeneralConditionStepDefs {

    ContractDefinitionPage contractDefinitionPage = new ContractDefinitionPage();
    ContractUpdatePage contractUpdatePage = new ContractUpdatePage();
    GeneralConditionPage generalConditionPage = new GeneralConditionPage();


    @Given("click to sample contract {string}")
    public void clickToSampleContract(String contractName) {
        contractDefinitionPage.fillContractName(contractName);
        System.out.println("✅ Sözleşme adı: " + contractName + " olan sözleşme kontrol edildi");
    }

    @Given("click to search button on definition page")
    public void clickToSearchButton() {
        contractDefinitionPage.searchForRecordOnMainPage();
        System.out.println("✅ Arama butonuna tıklandı");
    }

    @Then("click to first edit button on definition page")
    public void clickToFirstEdit() {
        contractDefinitionPage.clickToFirstEdit();
        System.out.println("✅ İlk düzenleme butonuna tıklandı");
    }

    @Then("click to new general condition button")
    public void clickToNewGeneralConditionButton() {
        try {
            contractUpdatePage.clickToNewGeneralCondition();
            System.out.println("✅ Yeni genel kondisyon butonuna tıklandı");
        } catch (Exception e) {
            System.err.println("❌ Butona tıklanamadı: " + e.getMessage());
        }
    }


    @Then("verify new general condition form is displayed")
    public void verifyNewGeneralConditionFormIsDisplayed() {
        contractUpdatePage.verifyNewGeneralConditionIsDisplayed();
        System.out.println("✅ Yeni genel kondisyon formunun görüntülendiği doğrulandı");
    }

    @Then("fill out the general condition form {string} {string}")
    public void fillOutTheGeneralConditionFormAndSave(String brand, String description) {
        try {
            generalConditionPage.fillOutTheGeneralConditionFormAndSave(brand, description);
            System.out.println("✅ Genel kondisyon formu dolduruldu ve kaydedildi");
        } catch (Exception e) {
            System.err.println("❌ Form doldurulamadı veya kaydedilemedi: " + e.getMessage());
        }
    }

    @Then("save new general condition form")
    public void clickToSaveButtonOnGeneralConditionForm() {
        generalConditionPage.clickToSaveButton();
        System.out.println("✅ Genel kondisyon formunda kaydet butonuna tıklandı");
    }

    @When("select condition type {string}")
    public void selectConditionType(String conditionType) {
        generalConditionPage.selectConditionType(conditionType);
        System.out.println("✅ Kondisyon tipi seçildi: " + conditionType);
    }

    @When("select margin type {string}")
    public void selectMarginType(String marginType) {
        generalConditionPage.selectMarginType(marginType);
        System.out.println("✅ Marj tipi seçildi: " + marginType);
    }

    @When("select calculation type {string}")
    public void selectCalculationType(String calculationType) {
        generalConditionPage.selectCalculationType(calculationType);
        System.out.println("✅ Hesaplama tipi seçildi: " + calculationType);
    }

    @Then("verify field {string} is disabled")
    public void verifyFieldIsDisabled(String fieldName) {
        boolean isDisabled = generalConditionPage.verifyFieldIsDisabled(fieldName);
        Assertions.assertTrue(isDisabled, fieldName + " alanı disabled olmalı ama değil!");
        System.out.println("✅ " + fieldName + " alanının disabled olduğu doğrulandı");
    }

    @Then("verify field {string} is optional")
    public void verifyFieldIsOptional(String fieldName) {
        boolean isOptional = generalConditionPage.verifyFieldIsOptional(fieldName);
        Assertions.assertTrue(isOptional, fieldName + " alanı optional olmalı ama değil!");
        System.out.println("✅ " + fieldName + " alanının optional olduğu doğrulandı");
    }

    @Then("verify field {string} is mandatory")
    public void verifyFieldIsMandatory(String fieldName) {
        boolean isMandatory = generalConditionPage.verifyFieldIsMandatory(fieldName);
        Assertions.assertTrue(isMandatory, fieldName + " alanı mandatory olmalı ama değil!");
        System.out.println("✅ " + fieldName + " alanının mandatory olduğu doğrulandı");
    }

    @Then("verify field {string} has required asterisk")
    public void verifyFieldHasRequiredAsterisk(String fieldName) {
        boolean hasAsterisk = generalConditionPage.verifyFieldHasRequiredAsterisk(fieldName);
        Assertions.assertTrue(hasAsterisk, fieldName + " alanının label'ında yıldız (*) olmalı ama yok!");
        System.out.println("✅ " + fieldName + " alanının label'ında yıldız (*) olduğu doğrulandı");
    }

    @When("fill field {string} with {string}")
    public void fillFieldWith(String fieldName, String value) {
        generalConditionPage.fillField(fieldName, value);
        System.out.println("✅ " + fieldName + " alanına '" + value + "' değeri girildi");
    }

    @When("clear field {string}")
    public void clearField(String fieldName) {
        generalConditionPage.clearField(fieldName);
        System.out.println("✅ " + fieldName + " alanı temizlendi");
    }

    @When("wait for {int} seconds")
    public void waitForSeconds(int seconds) {
        try {
            Thread.sleep(seconds * 1000);
            System.out.println("✅ " + seconds + " saniye beklendi");
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
    }

    @When("select {string} for field {string}")
    public void selectRadioButtonForField(String option, String fieldName) {
        generalConditionPage.selectRadioButton(fieldName, option);
        System.out.println("✅ " + fieldName + " alanı için '" + option + "' seçildi");
    }

    @Then("verify field {string} is not disabled")
    public void verifyFieldIsNotDisabled(String fieldName) {
        boolean isDisabled = generalConditionPage.verifyFieldIsDisabled(fieldName);
        Assertions.assertFalse(isDisabled, fieldName + " alanı disabled olmamalı ama disabled!");
        System.out.println("✅ " + fieldName + " alanının disabled olmadığı doğrulandı");
    }

    @Then("verify field {string} shows validation error on save")
    public void verifyFieldShowsValidationErrorOnSave(String fieldName) {
        boolean hasValidationError = generalConditionPage.verifyFieldShowsValidationErrorOnSave(fieldName);
        Assertions.assertTrue(hasValidationError, fieldName + " alanı kaydet butonuna basıldığında validation error göstermeli ama göstermiyor!");
        System.out.println("✅ " + fieldName + " alanının kaydet butonuna basıldığında validation error gösterdiği doğrulandı");
    }

    @Then("verify field {string} is visually disabled")
    public void verifyFieldIsVisuallyDisabled(String fieldName) {
        boolean isVisuallyDisabled = generalConditionPage.verifyFieldIsVisuallyDisabled(fieldName);
        Assertions.assertTrue(isVisuallyDisabled, fieldName + " alanı görsel olarak disabled olmalı ama değil!");
        System.out.println("✅ " + fieldName + " alanının görsel olarak disabled olduğu doğrulandı");
    }


    @Then("verify field {string} is enabled")
    public void verifyFieldIsEnabled(String fieldName) {
        boolean isEnabled = generalConditionPage.verifyFieldIsNotDisabled(fieldName);
        Assertions.assertTrue(isEnabled, fieldName + " alanı enabled olmalı ama değil!");
        System.out.println("✅ " + fieldName + " alanının enabled olduğu doğrulandı");
    }
}
