package stepDefs;

import io.cucumber.java.en.Given;
import io.cucumber.java.en.When;
import pages.commonPages.GlobalPage;

public class GlobalStepdefs {

    GlobalPage globalPage = new GlobalPage();

    @Given("navigate to login page")
    public void navigateToLoginPage() {
        globalPage.navigateToHomePage();
    }

    @When("order placed status by {string}")
    public void orderPlacedStatusBy(String category) {
        globalPage.orderPlacedStatus(category);
    }
}
