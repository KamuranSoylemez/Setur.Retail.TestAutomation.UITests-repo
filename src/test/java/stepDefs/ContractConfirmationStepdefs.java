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

    @Then("fill out the form {string}")
    public void fillOutTheForm(String status) {
        contractConfirmationPage.fillFirmCode();
        contractConfirmationPage.fillContractName();
        contractConfirmationPage.fillFirmName();
        contractConfirmationPage.selectStartDate();
        contractConfirmationPage.selectEndDate();
        contractConfirmationPage.selectIncoterm();
        contractConfirmationPage.selectContractStatus(status);

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
