package pages.purchasePages;

import com.microsoft.playwright.Locator;
import pages.commonPages.BasePage;


public class WorkflowInboxPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator purchaseDropdown = page.locator(".glyphicon.glyphicon-tags");
    Locator purchaseOrderLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrder/Index']");
    Locator purchaseOrderSearchLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrderInvoice/Index']");
    Locator purchasePrice = page.locator("//a[@href='/ApplicationManagement/ProductPurchasePrice/Index']");
    Locator purchaseInvoiceTransactions = page.locator("//a[@href='/ApplicationManagement/Invoice/Index']");

    public void verifySuccessfulLogin() {

        verifyTextElementUseTrim("Akış Gelen Kutusu", pageTitle);
    }

    public void clickPurchaseDropdownToggle() {

        clickElement(purchaseDropdown);
    }

    public void clickOrderLink() {

        clickElement(purchaseOrderLink);
    }

    public void clickPurchaseOrderSearch() {

        clickElement(purchaseOrderSearchLink);
    }

    public void clickPurchasePriceLink() {
        clickElement(purchasePrice);
    }

    public void clickInvoiceTransactions() {

        clickElement(purchaseInvoiceTransactions);
    }
}
