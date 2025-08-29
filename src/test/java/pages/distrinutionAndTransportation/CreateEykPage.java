package pages.distrinutionAndTransportation;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.WaitForSelectorState;
import pages.commonPages.BasePage;

public class CreateEykPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    FrameLocator eykUpdateFrame = getFrameByDialogTitle("EYK Güncelleme");
    FrameLocator copyFrame = page.frameLocator("iframe[src*='ProductFilterBeforeCopy']");

    public void verifyCreatingEYKPageIsDisplayed() {
        verifyTextElementUseTrim("EYK Oluşturma", pageTitle);
    }

    public void openEykUpdateFrame() {
        page.locator("##Edit").first().click();
    }

    public void openProductsTab(){
        eykUpdateFrame.locator("li.k-item.k-last[role='tab'] a.k-link").click();
    }

    public void openCopyRequestedToApproved(){
        eykUpdateFrame.locator("#btnCopyRequestedToApproved").click();
    }
    public void copyRequestedToApproved() {
        copyFrame.locator("#Copy").click();
        copyFrame.locator(".ajs-button.ajs-ok").click();
    }

    public void openCategoriesTab() {
        eykUpdateFrame.locator("li.k-item.k-first[role='tab'] a.k-link").click();
    }
    public void checkCategory() {
        eykUpdateFrame.locator("input[name^='grdShipmentStCategories']").check();
    }

    public void createEyk() {
        eykUpdateFrame.locator("#btnCommitSelectedRows").click();
    }

    public void confirmEykCreation() {
        Locator popUpConf = eykUpdateFrame.locator(".ajs-button.ajs-ok");
        popUpConf.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        popUpConf.click();
    }

    public void saveEyk() {
        eykUpdateFrame.locator("#SaveBtn").click();
    }

    public void getEykTransferNo() {
        Locator transferNoLink = page.locator("td[data-field-name='TransferNo'] a[title='EYK No']");
        String transferNo = transferNoLink.textContent().trim();
        addString("eykTransferNo", transferNo);
    }
}
