package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.SupplierPage.ContractDefinitionPage;

public class SupplierStepdefs {

    ContractDefinitionPage contractDefinitionPage = new ContractDefinitionPage();

    @Then("verify contract definition page is displayed")
    public void verifyContractDefinitionPageIsDisplayed() {
        contractDefinitionPage.verifyContractDefinitionPageIsDisplayed();
    }

    @And("open new contract definition form")
    public void openNewContractDefinitionForm() {
        contractDefinitionPage.openNewContractDefinitionForm();
    }

    @Then("fill out the form and save {string}")
    public void fillOutTheFormAndSave(String category) {
        contractDefinitionPage.openCompanyIdentificationFrame();
        contractDefinitionPage.fillCompanyCode(category);
        contractDefinitionPage.searchCompany();
        contractDefinitionPage.selectCompanyFromList();
        contractDefinitionPage.selectCategory();
        contractDefinitionPage.selectCategoryOption();
        contractDefinitionPage.selectMultiSelectOption("BEDELSİZ BUTİK");
        contractDefinitionPage.selectStartDate();
        contractDefinitionPage.selectFirstDayOfMonth();
        contractDefinitionPage.selectFiscalMonthStart();
        contractDefinitionPage.selectIncoterms();
        contractDefinitionPage.fillTermDays();
        contractDefinitionPage.fillDescription();

    }

    @Then("save contract definition")
    public void saveContractDefinition() {
        contractDefinitionPage.saveContractDefinition();
    }
}
