package stepDefs;

import enums.DistributorInfo;
import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.purchasePages.PurchasePricePage;

public class PurchasePriceStepdefs {

    PurchasePricePage purchasePricePage = new PurchasePricePage();

    @Then("verify purchase price page")
    public void verifyPurchasePricePage() {
        purchasePricePage.verifyPurchasePricePage();
    }

    @When("new record purchase price")
    public void newRecordPurchasePrice() {
        purchasePricePage.newRecordPurchasePrice();
        purchasePricePage.verifyCreatePurchasePriceFrame();
    }

    @And("create purchase price for defined product")
    public void createPurchasePriceForDefinedProduct() {
        purchasePricePage.openProductDescriptionFrame();
    }

    @And("select defined product")
    public void selectDefinedProduct() {
        purchasePricePage.verifyProductDescFrame();
        purchasePricePage.fillProductCode();
        purchasePricePage.searchProduct();
        purchasePricePage.selectProduct();
    }

    @And("fill purchase price for defined product")
    public void fillPurchasePriceForDefinedProduct() {
        purchasePricePage.fillStartDate();
        purchasePricePage.fillPurchasePrice();
        purchasePricePage.getValueOfAmount();
        purchasePricePage.selectPriceType();
        purchasePricePage.saveCreatePurchasePrice();
        //purchasePricePage.closeSuccessMessage();
    }

    @Then("search defined product and verify amount")
    public void searchCreatedRecordAndVerifyAmount() {
        purchasePricePage.openProductDescFrame();
        purchasePricePage.fillProductCode();
        purchasePricePage.searchProduct();
        purchasePricePage.selectProduct();
        purchasePricePage.searchProductInMainPage();
        purchasePricePage.verifyPurchasePriceAmount();
    }

    @And("select purchase price for undefined product")
    public void createPurchasePriceForUndefinedProduct() {
        purchasePricePage.verifyCreatePurchasePriceFrame();
        purchasePricePage.selectUndefinedProduct();
    }

    @And("select distributor company")
    public void selectDistributorCompany() {
        purchasePricePage.openCompanyIdentification();
        purchasePricePage.fillCompanyCode(DistributorInfo.TUTUN_URUNLERI);
        purchasePricePage.searchCompany();
        purchasePricePage.selectCompany();
    }

    @And("select undefined product manufacturer company")
    public void selectUndefinedProductManufacturerCompany() {
        purchasePricePage.openManufacturerCompany();
        purchasePricePage.fillManufacturerCompany(DistributorInfo.TUTUN_URUNLERI);
        purchasePricePage.searchCompany();
        purchasePricePage.selectCompany();

    }

    @And("fill purchase price for undefined product")
    public void fillPurchasePriceForUndefinedProduct() {
        purchasePricePage.fillUnidentifiedProductBarcode();
        purchasePricePage.fillStartDate();
        purchasePricePage.fillPurchasePrice();
        purchasePricePage.getValueOfAmount();
        purchasePricePage.setVatAmount();
        purchasePricePage.selectPriceType();
        purchasePricePage.saveCreatePurchasePrice();
        //purchasePricePage.closeSuccessMessage();
    }

    @Then("search undefined product and verify amount")
    public void searchUndefinedProductAndVerifyAmount() {
        purchasePricePage.openCompanyIdentificationFrame();
        purchasePricePage.fillCompanyCode(DistributorInfo.TUTUN_URUNLERI);
        purchasePricePage.searchCompany();
        purchasePricePage.selectCompany();
        purchasePricePage.searchProductInMainPage();
        purchasePricePage.verifyPurchasePriceAmount();
    }

}
