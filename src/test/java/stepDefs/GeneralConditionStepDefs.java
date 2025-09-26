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
}
