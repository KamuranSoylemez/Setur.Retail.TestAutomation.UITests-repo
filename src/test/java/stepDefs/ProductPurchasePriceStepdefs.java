package stepDefs;

import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.purchasePages.ProductPurchasePricePage;

public class ProductPurchasePriceStepdefs {

    ProductPurchasePricePage purchasePricePage = new ProductPurchasePricePage();

    @Then("verify purchase price page")
    public void verifyPurchasePricePage() {
        purchasePricePage.verifyPurchasePricePage();
    }

    @When("new record purchase price")
    public void newRecordPurchasePrice() {
        purchasePricePage.newRecordPurchasePrice();
    }
}
