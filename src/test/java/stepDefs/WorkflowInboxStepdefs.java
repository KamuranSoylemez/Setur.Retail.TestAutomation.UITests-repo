package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.purchasePages.WorkflowInboxPage;

public class WorkflowInboxStepdefs {

    WorkflowInboxPage inboxPage = new WorkflowInboxPage();

    @Then("verify successful login")
    public void verifySuccessfulLogin() {
        inboxPage.verifySuccessfulLogin();
    }

    @And("click purchasing dropdown toggle")
    public void clickPurchasingDropdownToggle() {
        inboxPage.clickPurchasingDropdownToggle();
    }

    @And("click purchase order creation link")
    public void clickCreateOrderLink() {
        inboxPage.clickCreateOrderLink();
    }

    @And("click purchase order search link")
    public void clickPurchaseOrderSearch() {
        inboxPage.clickPurchaseOrderSearch();
    }

    @And("click purchase price link")
    public void clickPurchasePriceLink() {
        inboxPage.clickPurchasePriceLink();
    }

    @And("click invoice transactions link")
    public void clickInvoiceTransactions() {
        inboxPage.clickInvoiceTransactions();
    }

    @And("click retail definition dropdown toggle")
    public void clickRetailDefinitionDropdownToggle() {
        inboxPage.clickRetailDefinitionDropdownToggle();
    }

    @When("click product definition link")
    public void clickProductDefinitionLink() {
        inboxPage.clickProductDefinitionLink();
    }

    @And("click distribution and transportation dropdown toggle")
    public void clickDistributionAndTransportationDropdownToggle() {
        inboxPage.clickDistributionAndTransportationDropdownToggle();
    }

    @When("click create distribution link")
    public void clickCreateDistributionLink() {
        inboxPage.clickCreateDistributionLink();
    }

    @And("click EYK waiting page link")
    public void clickEYKWaitingProcessesLink() {
        inboxPage.clickEYKWaitingProcessesLink();
    }

    @And("click creating EYK link")
    public void clickCreatingEYKLink() {
        inboxPage.clickCreatingEYKLink();
    }

    @And("click contract definition dropdown toggle")
    public void clickContractDefinitionDropdownToggle() {
        inboxPage.clickContractDefinitionDropdownToggle();
    }

    @When("click contract definition link")
    public void clickContractDefinitionLink() {
        inboxPage.clickContractDefinitionLink();
    }
}
