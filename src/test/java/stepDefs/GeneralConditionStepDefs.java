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

    // TEST1 Steps
    @Given("click to sample contract {string}")
    public void clickToSampleContract(String contractName) {
       contractDefinitionPage.fillContractName();
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
        contractUpdatePage.clickToNewGeneralCondition();
        System.out.println("✅ Yeni genel kondisyon butonuna tıklandı");
    }


}
