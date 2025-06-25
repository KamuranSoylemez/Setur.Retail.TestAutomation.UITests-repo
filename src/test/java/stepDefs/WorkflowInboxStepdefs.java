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

    @And("click purchase dropdown toggle")
    public void clickPurchaseDropdownToggle() {
        inboxPage.clickPurchaseDropdownToggle();
    }

    @And("click purchase create order order link")
    public void clickOrderLink() {
        inboxPage.clickOrderLink();
    }

    @And("click Purchase Order Invoice")
    public void clickPurchaseOrderInvoice() {
        inboxPage.clickPurchaseOrderInvoice();
    }

    @And("click purchase price link")
    public void clickPurchasePriceLink() {
        inboxPage.clickPurchasePriceLink();
    }
}
