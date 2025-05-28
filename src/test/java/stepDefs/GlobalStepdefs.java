package stepDefs;

import io.cucumber.java.en.Given;
import pages.commonPages.GlobalPage;

public class GlobalStepdefs {

    GlobalPage globalPage = new GlobalPage();

    @Given("navigate to login page")
    public void navigateToLoginPage() {
        globalPage.navigateToHomePage();
    }
}
