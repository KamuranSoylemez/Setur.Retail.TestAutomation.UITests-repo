package pages.purchasePages;

import com.microsoft.playwright.Locator;
import pages.commonPages.BasePage;


public class WorkflowInboxPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator purchaseDropdown = page.locator(".glyphicon.glyphicon-tags");
    Locator purchaseOrderLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrder/Index']");
    Locator purchaseOrderInvoiceLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrderInvoice/Index']");
    Locator purchasePrice = page.locator("//a[@href='/ApplicationManagement/ProductPurchasePrice/Index']");

    public void verifySuccessfulLogin() {

        verifyTextElementUseTrim("Akış Gelen Kutusu", pageTitle);
    }

    public void clickPurchaseDropdownToggle() {

        clickElement(purchaseDropdown);
    }

    public void clickOrderLink() {

        clickElement(purchaseOrderLink);
    }

    public void clickPurchaseOrderInvoice() {

        clickElement(purchaseOrderInvoiceLink);
    }

    public void clickPurchasePriceLink() {
        clickElement(purchasePrice);
    }
}
