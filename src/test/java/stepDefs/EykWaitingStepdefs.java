package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.distrinutionAndTransportation.EykWaitingPage;


public class EykWaitingStepdefs {

    EykWaitingPage eykWaitingPage = new EykWaitingPage();

    @Then("verify EYK waiting processes page is displayed")
    public void verifyEYKWaitingProcessesPageIsDisplayed() {
        eykWaitingPage.verifyEykWaitingProcessesPageIsDisplayed();
    }

    @And("select warehouse and search for EYK waiting {string}")
    public void selectWarehouseAndSearch(String region) {
        selectWarehouse(region, () -> eykWaitingPage.openWarehouseDefinitionFrameForExitWarehouse());
        selectWarehouse(region, () -> eykWaitingPage.openWarehouseDefinitionFrameForEntryWarehouse());
        eykWaitingPage.searchForRecord();
    }

    @And("create EYK preparation")
    public void createEYKPreparation() {
        eykWaitingPage.openEykRecord();
        eykWaitingPage.checkEykRecord();
        eykWaitingPage.clickEykSettingButton();
        eykWaitingPage.createEykPreparation();
        eykWaitingPage.confirmEykPreparation();
    }

    @And("send to counting process")
    public void sendToCountingProcess() {
        eykWaitingPage.sendToCountingProcess();
        eykWaitingPage.confirmSendToCountingProcess();
    }

    /**
     * Giriş ya da çıkış deposunu seçer ve ilgili frame'i açar.
     * @param region    Seçilecek depo bölgesi
     * @param openFrame Runnable, frame'i açmak için kullanılacak fonksiyon
     */
    private void selectWarehouse(String region, Runnable openFrame) {
        openFrame.run(); // Giriş ya da çıkış deposu frame'ini aç
        eykWaitingPage.openSeturRegionFields();
        eykWaitingPage.selectSeturRegionFromList(region);
        eykWaitingPage.searchWarehouse();
        eykWaitingPage.selectWarehouseFromList();
    }
}
