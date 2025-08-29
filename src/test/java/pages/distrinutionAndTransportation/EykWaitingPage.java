package pages.distrinutionAndTransportation;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.LoadState;
import com.microsoft.playwright.options.WaitForSelectorState;
import pages.commonPages.BasePage;

public class EykWaitingPage extends BasePage{

    Locator pageTitle = page.locator("#PageTitle");
    FrameLocator warehouseDefinitionFrame = getFrameByDialogTitle("Antrepo Tanımlama");
    FrameLocator eykUpdateFrame = getFrameByDialogTitle("EYK Güncelleme");

    /**
     * EYK Bekleyenler sayfasının görüntülendiğini doğrular.
     */
    public void verifyEykWaitingProcessesPageIsDisplayed() {
        verifyTextElementUseTrim("EYK Bekleyenler", pageTitle);
    }

    /**
     * Antrepo Tanımlama frame'ini çıkış deposu için açar.
     */
    public void openWarehouseDefinitionFrameForExitWarehouse() {
        page.locator("#ExitWarehouseIdButtonId").click();
    }
    /**
     * Antrepo Tanımlama frame'inde Setur Bölgesi listesini açar.
     */
    public void openSeturRegionFields() {
        warehouseDefinitionFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(0).click();
    }

    /**
     * Setur Bölgesi listesinden belirtilen bölgeyi seçer.
     * @param region Seçilecek bölgenin adı
     */
    public void selectSeturRegionFromList(String region) {
        Locator warehouseItems = warehouseDefinitionFrame.locator("ul#FilterSeturRegionID_listbox li");
        Locator targetWarehouse = warehouseItems.filter(new Locator.FilterOptions().setHasText(region));
        targetWarehouse.click(new Locator.ClickOptions().setForce(true));
    }

    /**
     * Antrepo Tanımlama frame'inde depo araması yapar.
     */
    public void searchWarehouse() {
        warehouseDefinitionFrame.locator("#FilterButtonId").click();
    }

    /**
     * Antrepo Tanımlama frame'inde depo listesinden ilk depoyu seçer.
     */
    public void selectWarehouseFromList() {
        warehouseDefinitionFrame.locator("input[name^='WarehouseGridId']").nth(0).click();
    }

    /**
     * Antrepo Tanımlama frame'ini giriş deposu için açar.
     */
    public void openWarehouseDefinitionFrameForEntryWarehouse() {
        page.locator("#EntryWarehouseIdButtonId").click();
    }

    /**
     * EYK Bekleyenler sayfasında kayıt araması yapar.
     */
    public void searchForRecord() {
        page.locator("#FilterButtonId").click();
    }

    /**
     * EYK Bekleyenler sayfasında kaydı görüntüler.
     */
    public void openEykRecord() {
        Locator plusIcon = page.locator("td.k-hierarchy-cell a.k-icon.k-plus");
        plusIcon.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        plusIcon.click();
    }

    /**
     * EYK Bekleyenler sayfasında kaydı işaretler.
     */
    public void checkEykRecord() {
        page.locator("input[name^='grdDistProdWarehouse']").check();
    }

    /**
     * EYK Bekleyenler sayfasında ayar butonuna tıklar.
     */
    public void clickEykSettingButton() {
        page.locator("div.btn-group-vertical.gridCmdBtn.k-info a.glyphicon.glyphicon-cog").click();
    }

    /**
     * EYK Bekleyenler sayfasında EYK Hazırlama Oluştur butonuna tıklar.
     */
    public void createEykPreparation() {
        page.locator("#CreateBatch").click();
    }

    /**
     * EYK Hazırlama Oluşturma işlemini onaylar.
     */
    public void confirmEykPreparation() {
        popUpConfirmationProcess();
    }

    /**
     * EYK Güncelleme framinde sayım sürecine gönderir.
     */
    public void sendToCountingProcess() {
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        eykUpdateFrame.locator("#btnSendToEnumeration").click();
    }

    /**
     * EYK Güncelleme framinde sayım sürecine gönderme işlemini onaylar.
     */
    public void confirmSendToCountingProcess() {
        Locator popUpConf = eykUpdateFrame.locator(".ajs-button.ajs-ok");
        popUpConf.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));

        popUpConf.click();
    }
}
