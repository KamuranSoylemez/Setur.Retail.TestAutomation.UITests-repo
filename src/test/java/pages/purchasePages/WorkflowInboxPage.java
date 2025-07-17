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

    /**
     * Giriş yaptıktan sonra ilk açılan sayfayı verify eder
     */
    public void verifySuccessfulLogin() {
        verifyTextElementUseTrim("Akış Gelen Kutusu", pageTitle);
    }
    /**
     * Satın Alma dropdown toggle linkine tıklar
     */
    public void clickPurchasingDropdownToggle() {
        clickElement(purchaseDropdown);
    }
    /**
     * Satın Alma dropdown toggle altındaki  Sipariş Oluşturma linkine tıklar
     */
    public void clickCreateOrderLink() {
        clickElement(purchaseOrderLink);
    }
    /**
     * Satın Alma dropdown toggle altındaki  Sipariş Sorgulama linkine tıklar
     */
    public void clickPurchaseOrderSearch() {
        clickElement(purchaseOrderSearchLink);
    }
    /**
     * Satın Alma dropdown toggle altındaki  Satın Alma linkine tıklar
     */
    public void clickPurchasePriceLink() {
        clickElement(purchasePrice);
    }
    /**
     * Satın Alma dropdown toggle altındaki  Fatura İşlemleri linkine tıklar
     */
    public void clickInvoiceTransactions() {
        clickElement(purchaseInvoiceTransactions);
    }
}
