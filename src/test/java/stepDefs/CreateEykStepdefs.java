package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.distrinutionAndTransportation.CreateEykPage;

public class CreateEykStepdefs {

    CreateEykPage createEykPage = new CreateEykPage();

    @Then("verify creating EYK page is displayed")
    public void verifyCreatingEYKPageIsDisplayed() {
        createEykPage.verifyCreatingEYKPageIsDisplayed();
    }

    @And("open EYK update for creation EYK")
    public void openEykUpdateForCreationEyk() {
        createEykPage.openEykUpdateFrame();
        createEykPage.openProductsTab();
        createEykPage.openCopyRequestedToApproved();
        createEykPage.copyRequestedToApproved();
        createEykPage.openCategoriesTab();
        createEykPage.checkCategory();
        createEykPage.createEyk();
        createEykPage.confirmEykCreation();
        createEykPage.saveEyk();
    }

    @And("save EYK no for verify record")
    public void saveEYKNoForVerifyRecord() {
        createEykPage.openEykUpdateFrame();
        createEykPage.getEykTransferNo();
    }
}
