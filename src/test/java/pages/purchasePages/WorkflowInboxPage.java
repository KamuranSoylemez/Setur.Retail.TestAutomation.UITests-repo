package pages.purchasePages;

import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import pages.commonPages.BasePage;


public class WorkflowInboxPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator purchaseDropdownToggle = page.locator(".glyphicon.glyphicon-tags");
    Locator purchaseOrderLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrder/Index']");
    Locator purchaseOrderSearchLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrderInvoice/Index']");
    Locator purchasePrice = page.locator("//a[@href='/ApplicationManagement/ProductPurchasePrice/Index']");
    Locator purchaseInvoiceTransactions = page.locator("//a[@href='/ApplicationManagement/Invoice/Index']");
    Locator productDefinitionLink = page.locator("//a[@href='/ApplicationManagement/Product/Index']");

    /**
     * Giriş yaptıktan sonra ilk açılan sayfayı verify eder.
     */
    public void verifySuccessfulLogin() {
        verifyTextElementUseTrim("Akış Gelen Kutusu", pageTitle);
    }
    /**
     * Satın Alma dropdown toggle linkine tıklar.
     */
    public void clickPurchasingDropdownToggle() {
        clickElement(purchaseDropdownToggle);
    }
    /**
     * Satın Alma altındaki  Sipariş Oluşturma linkine tıklar.
     */
    public void clickCreateOrderLink() {
        clickElement(purchaseOrderLink);
    }
    /**
     * Satın Alma altındaki  Sipariş Sorgulama linkine tıklar.
     */
    public void clickPurchaseOrderSearch() {
        clickElement(purchaseOrderSearchLink);
    }
    /**
     * Satın Alma altındaki  Satın Alma linkine tıklar.
     */
    public void clickPurchasePriceLink() {
        clickElement(purchasePrice);
    }
    /**
     * Satın Alma altındaki  Fatura İşlemleri linkine tıklar.
     */
    public void clickInvoiceTransactions() {
        clickElement(purchaseInvoiceTransactions);
    }

    /**
     * Retail Tanımları dropdown toggle linkine tıklar.
     */
    public void clickRetailDefinitionDropdownToggle() {
        Locator retailMenu = page.locator("li.dropdown > a.dropdown-toggle",
                new Page.LocatorOptions().setHasText("Retail Tanımları"));
        retailMenu.click();
    }

    /**
     * Retail Tanımları altındaki Ürün Tanımlama linkine tıklar.
     */
    public void clickProductDefinitionLink() {
        clickElement(productDefinitionLink);
    }
}
