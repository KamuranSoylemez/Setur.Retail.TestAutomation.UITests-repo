package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.SupplierPages.ContractDefinitionPage;

import java.util.List;
import java.util.Map;

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
        contractDefinitionPage.selectTypeOption();
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
        contractDefinitionPage.verifyDuplicateRecord();
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

    @And("fill out the form for categories")
    public void fillOutTheFormForCategories(io.cucumber.datatable.DataTable dataTable) {
        List<Map<String, String>> rows = dataTable.asMaps(String.class, String.class);

        for (Map<String, String> row : rows) {
            String category = row.get("category");
            String type = row.get("type");
            String brand = row.get("brand");

            contractDefinitionPage.openNewContractDefinitionForm();
            contractDefinitionPage.openCompanyIdentificationFrame();
            contractDefinitionPage.fillCompanyCode(category);
            contractDefinitionPage.searchCompany();
            contractDefinitionPage.selectCompanyFromList();
            contractDefinitionPage.openCategories();
            contractDefinitionPage.selectCategoryOption(category);
            contractDefinitionPage.verifyCategorySelected();
            contractDefinitionPage.selectType(type);
            contractDefinitionPage.verifyTypeOptionSelected();
            contractDefinitionPage.selectBrand(brand);
            contractDefinitionPage.verifyBrandSelected();
            contractDefinitionPage.closeContractDefinitionFrame();
        }
    }


}
