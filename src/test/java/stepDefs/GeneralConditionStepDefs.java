package stepDefs;

import com.microsoft.playwright.*;
import io.cucumber.java.en.*;
import org.junit.jupiter.api.Assertions;
import java.util.List;

import pages.SupplierPages.ContractConfirmationPage;
import pages.SupplierPages.ContractDefinitionPage;
import pages.SupplierPages.GeneralConditionPage;


public class GeneralConditionStepDefs {
    ContractDefinitionPage contractDefinitionPage = new ContractDefinitionPage();

    // TEST1 Steps
    @Given("click to sample contract {string}")
    public void clickToSampleContract(String contractName) {
       contractDefinitionPage.fillContractName();
        System.out.println("✅ Sözleşme adı: " + contractName + " olan sözleşme kontrol edildi");
    }


}
