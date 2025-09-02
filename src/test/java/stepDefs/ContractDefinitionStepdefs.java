package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.SupplierPages.ContractDefinitionPage;

public class ContractDefinitionStepdefs {

    ContractDefinitionPage contractDefinitionPage = new ContractDefinitionPage();

    @Then("verify contract definition page is displayed")
    public void verifyContractDefinitionPageIsDisplayed() {
        contractDefinitionPage.verifyContractDefinitionPageIsDisplayed();
    }

    @And("open new contract definition form")
    public void openNewContractDefinitionForm() {
        contractDefinitionPage.openNewContractDefinitionForm();
    }

    @And("fill out the form and save {string}")
    public void fillOutTheFormAndSave(String category) {
        contractDefinitionPage.openCompanyIdentificationFrame();
        contractDefinitionPage.fillCompanyCode(category);
        contractDefinitionPage.searchCompany();
        contractDefinitionPage.selectCompanyFromList();
        contractDefinitionPage.openCategories();
        contractDefinitionPage.selectCategoryOption(category);
        contractDefinitionPage.selectTypeOption("HOME");
        contractDefinitionPage.selectStartDate();
        contractDefinitionPage.selectFirstDayOfMonth();
        contractDefinitionPage.selectFiscalMonthStart();
        contractDefinitionPage.selectIncoterms();
        contractDefinitionPage.selectBrand("***");
        contractDefinitionPage.fillTermDays();
        contractDefinitionPage.fillDescription();

    }

    @And("save contract definition")
    public void saveContractDefinition() {
        contractDefinitionPage.saveContractDefinition();
        contractDefinitionPage.verifyRecordSavedSuccessfully();
        contractDefinitionPage.verifyContractStatus("Hazırlanıyor");
        contractDefinitionPage.closeContractUpdateFrame();
    }

    @Then("verify contract definition is created on main page {string}")
    public void verifyContractDefinitionIsCreatedOnMainPage(String category) {
        contractDefinitionPage.openCompanyIdentificationFrameOnMainPage();
        contractDefinitionPage.fillCompanyCode(category);
        contractDefinitionPage.searchCompany();
        contractDefinitionPage.selectCompanyFromList();
        contractDefinitionPage.openCategoriesFromMainPage();
        contractDefinitionPage.selectCategoryFromList(category);
        contractDefinitionPage.selectTypeOptionFromMainPage();
        contractDefinitionPage.searchForRecordOnMainPage();
        contractDefinitionPage.verifyRecordExistsOnMainPage();
        contractDefinitionPage.verifyFirmNameOnMainPage();
        contractDefinitionPage.verifyCategoryOnMainPage(category);
        contractDefinitionPage.verifyTypeOnMainPage();
    }

    @And("fill out the form for each {string} and {string}")
    public void fillOutTheFormForEachCategory(String category, String type) {
        contractDefinitionPage.openCompanyIdentificationFrame();
        contractDefinitionPage.fillCompanyCode(category);
        contractDefinitionPage.searchCompany();
        contractDefinitionPage.selectCompanyFromList();
        contractDefinitionPage.openCategories();
        contractDefinitionPage.selectCategoryOption(category);
        contractDefinitionPage.verifyCategorySelected();
        contractDefinitionPage.selectTypeOption(type);
        contractDefinitionPage.verifyTypeOptionSelected();
        contractDefinitionPage.selectBrand("***");
        contractDefinitionPage.verifyBrandSelected();
        contractDefinitionPage.closeContractDefinitionFrame();
    }
}
