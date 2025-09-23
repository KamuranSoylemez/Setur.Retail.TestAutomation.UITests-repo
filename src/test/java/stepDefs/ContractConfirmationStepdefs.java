package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.SupplierPages.ContractConfirmationPage;

public class ContractConfirmationStepdefs {

    ContractConfirmationPage contractConfirmationPage = new ContractConfirmationPage();

    @Then("verify contract confirmation page is displayed {string}")
    public void verifyContractConfirmationPageIsDisplayed(String expectedTitle) {
        contractConfirmationPage.verifyContractConfirmationPageIsDisplayed(expectedTitle);
    }

    @Then("fill out the form by dates and status {string} {string} {string}")
    public void fillOutTheForm(String status,String startDate, String endDate) {
        contractConfirmationPage.selectStartDate(startDate);
        contractConfirmationPage.selectEndDate(endDate);
        contractConfirmationPage.selectIncoterm();
        contractConfirmationPage.selectContractStatus(status);

    }

    @Then("fill firm field {string} {string}")
    public void fillFirmField(String firmCode, String firmName) {
        contractConfirmationPage.fillFirmCode(firmCode);
        contractConfirmationPage.fillFirmName(firmName);
    }

    @Then("fill sample contract field {string}")
    public void fillContractField(String contractName) {
        contractConfirmationPage.fillContractName(contractName);
    }

    @Then("click to search button")
    public void clickToSearchButton() {
        contractConfirmationPage.clicktoSearchButton();
    }

    @And("click to first edit")
    public void clickToEdit() {
        contractConfirmationPage.clicktoEdit();
    }

    @Then("verify contract cancellation approve button is visible")
    public void verifyContractCancellationApproveButtonIsVisible() {
        contractConfirmationPage.verifyContractCancellationApproveButtonIsVisible();
    }

    @And("verify contract cancellation reject button is visible")
    public void verifyContractCancellationRejectButtonIsVisible() {
        contractConfirmationPage.verifyContractCancellationRejectButtonIsVisible();
    }

    @Then("verify contract approve button is visible")
    public void verifyContractApproveButtonIsVisible() {
        contractConfirmationPage.verifyContractApproveButtonIsVisible();
    }

    @And("verify contract reject button is visible")
    public void verifyContractRejectButtonIsVisible() {
        contractConfirmationPage.verifyContractRejectButtonIsVisible();
    }

    @And("check button count")
    public void checkButtonCount() {
        contractConfirmationPage.countButtons();
    }

    @And("verify callBack button is visible")
    public void verifyCallBackButtonIsVisible() {
        contractConfirmationPage.verifyCallBackButtonIsVisible();
    }

    @And("verify contract director reject button is visible")
    public void verifyContractDirectorRejectButtonIsVisible() {
        contractConfirmationPage.verifyContractDirectorRejectButtonIsVisible();
    }

    @And("verify contract director approve button is visible")
    public void verifyContractDirectorApproveButtonIsVisible() {
        contractConfirmationPage.verifyContractDirectorApproveButtonIsVisible();

    }
}
