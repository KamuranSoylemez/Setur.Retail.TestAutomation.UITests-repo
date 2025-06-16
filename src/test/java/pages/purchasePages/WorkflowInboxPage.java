package pages.purchasePages;

import com.microsoft.playwright.Locator;
import pages.commonPages.BasePage;


public class WorkflowInboxPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator purchaseDropdown = page.locator(".glyphicon.glyphicon-tags");
    Locator purchaseOrderLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrder/Index']");
    Locator purchaseOrderInvoiceLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrderInvoice/Index']");

    public void verifySuccessfulLogin() {

        //page.waitForSelector("#PageTitle", new Page.WaitForSelectorOptions().setTimeout(60000));
        verifyTextElementUseTrim("Akış Gelen Kutusu", pageTitle);
    }

    public void clickPurchaseDropdownToggle() {

        //page.waitForSelector(".glyphicon.glyphicon-refresh",new Page.WaitForSelectorOptions().setTimeout(60000));
        clickElement(purchaseDropdown);
    }

    public void clickOrderLink() {

        //page.waitForSelector(".glyphicon.glyphicon-refresh",new Page.WaitForSelectorOptions().setTimeout(60000));
        clickElement(purchaseOrderLink);
    }

    public void clickPurchaseOrderInvoice() {

        clickElement(purchaseOrderInvoiceLink);
    }
}
