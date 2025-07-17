package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
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

    @And("click on the purchase order creation link")
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
}
