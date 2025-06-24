package stepDefs;

import io.cucumber.java.en.And;
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

    @And("create purchase price for defined product")
    public void createPurchasePriceForDefinedProduct() {
        purchasePricePage.createPurchasePriceForDefinedProduct();
    }

    @Then("search defined product and verify amount")
    public void searchCreatedRecordAndVerifyAmount() {
        purchasePricePage.searchDefinedProductAndVerifyAmount();
    }

    @And("create purchase price for undefined product")
    public void createPurchasePriceForUndefinedProduct() {
        purchasePricePage.createPurchasePriceForUndefinedProduct();
    }

    @Then("search undefined product and verify amount")
    public void searchUndefinedProductAndVerifyAmount() {
        purchasePricePage.searchUndefinedProductAndVerifyAmount();
    }
}
