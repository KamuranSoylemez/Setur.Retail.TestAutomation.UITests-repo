package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.SupplierPages.ContractConfirmationPage;

public class ContractConfirmationStepdefs {

    ContractConfirmationPage contractConfirmationPage = new ContractConfirmationPage();

    @Then("verify contract confirmation page is displayed")
    public void verifyContractConfirmationPageIsDisplayed() {
        contractConfirmationPage.verifyContractConfirmationPageIsDisplayed();
    }

    @Then("fill out the form")
    public void fillOutTheForm() {
        contractConfirmationPage.fillFirmCode();

    }

    @Then("click to search button")
    public void clickToSearchButton() {
        contractConfirmationPage.clicktoSearchButton();
    }

    @And("click to first edit")
    public void clickToEdit() {
        contractConfirmationPage.clicktoEdit();
    }

    @Then("verify contract approve button is visible")
    public void verifyContractApproveButtonIsVisible() {
        contractConfirmationPage.verifyContractApproveButtonIsVisible();
    }
}
