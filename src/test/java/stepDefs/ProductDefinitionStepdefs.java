package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.retailDefinitionPages.ProductDefinitionPage;

public class ProductDefinitionStepdefs {

    ProductDefinitionPage productDefinitionPage = new ProductDefinitionPage();


    @Then("verify product definition page is displayed")
    public void verifyProductDefinitionPageIsDisplayed() {
        productDefinitionPage.verifyProductDefinitionPageIsDisplayed();
    }

    @And("click new record button")
    public void clickNewRecordButton() {
        productDefinitionPage.clickNewRecordButton();
    }

}
