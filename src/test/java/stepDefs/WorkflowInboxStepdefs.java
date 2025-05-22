package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.WorkflowInboxPage;

public class WorkflowInboxStepdefs {

    WorkflowInboxPage inboxPage = new WorkflowInboxPage();

    @Then("verify successful login")
    public void verifySuccessfulLogin() {
        inboxPage.verifySuccessfulLogin();
    }

    @And("click Purchase dropdown toggle")
    public void clickPurchaseDropdownToggle() {
        inboxPage.clickPurchaseDropdownToggle();
    }

    @And("click Order link")
    public void clickOrderLink() {
        inboxPage.clickOrderLink();
    }
}
