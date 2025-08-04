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

    @When("order completion {string}")
    public void orderCompletion(String category, String region) {
        globalPage.orderCompletion(category,region);
    }

    @When("set proforma and invoice")
    public void setProformaAndInvoice() {
        globalPage.setProformaAndInvoice();
    }
}
