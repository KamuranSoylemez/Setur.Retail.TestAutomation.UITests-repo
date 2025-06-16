package pages.purchasePages;

import com.microsoft.playwright.Locator;
import pages.commonPages.BasePage;
import utils.GlobalVariables;

public class PurchaseOrderInvoicePage extends BasePage {

    PurchaseOrderPage orderPage = new PurchaseOrderPage();

    Locator pageTitle = page.locator("#PageTitle");
    Locator filterPurchaseOrderCode = page.locator("#FilterPurchaseOrderCode");
    Locator filterButtonId = page.locator("#FilterButtonId");
    Locator edit = page.locator("#Edit");

    public void verifyPurchaseOrderInvoicePage() {
        verifyTextElementUseTrim("Sipariş Sorgulama", pageTitle);
    }

    public void searchOrderIdAndEditOder() {
        filterPurchaseOrderCode.click();

        String orderID = GlobalVariables.getInstance().getString("orderCode");
        filterPurchaseOrderCode.fill(orderID);

        clickElement(filterButtonId);

        clickElement(edit);

    }
    /*
    // Proforma yeni kayıt
    public void addProformaToOrder() {

        FrameLocator orderProcessingFrame = page
                .getByRole(AriaRole.DIALOG, new Page.GetByRoleOptions().setName("Sipariş İşlemleri"))
                .frameLocator("iframe[title='Setur']");

        orderProcessingFrame.locator(".k-loading.k-complete.k-progress").click();
        orderProcessingFrame.locator(".k-button.k-button-icontext.k-grid-ProformaReceiptGridIdAddNew")
                .click();
    }

    // Proforma kaydetme
    public void addInfoForProformaAndSave() {

        FrameLocator proformaFrame = page
                .getByRole(AriaRole.DIALOG, new Page.GetByRoleOptions().setName("Proforma Kaydetme"))
                .frameLocator("iframe[title='Setur']");

        Random random = new Random();
        int randomNumber = random.nextInt(100);
        String formatted = String.format("KMRN-TST-%03d", randomNumber);
        proformaFrame.locator("#ProformaNo").fill(formatted);

        proformaFrame.locator(".k-icon.k-i-calendar").click();
        proformaFrame.locator(".k-link.k-nav-today").click();

        String totalAmount = page.locator("(//td[@data-field-name='TotalAmount'])[1]").textContent();

        proformaFrame.locator("#ProformaTotalAmount").fill(totalAmount);

        proformaFrame.locator("#btnSave").click();
        System.out.println("Proforma kaydeildi");

        proformaFrame.locator("#ProductCopyId").click();
        orderPage.orderApprovalProcess();

        proformaFrame.locator("#approveButton").click();
        System.out.println("Proforma onaylandı");

        proformaFrame.locator("#ClosePopupBtn").click();
    }
    // Sipariş faturalarını ekleme
    public void addOrderInvoices() {

        Locator invoicesTab = page.locator(".k-loading.k-progress.k-complete");
        invoicesTab.nth(2).click();

        Locator newRecord = page.locator(".k-button.k-button-icontext.k-grid-InvoiceGridIdAddNew");
        newRecord.click();
        System.out.println("Sipariş Faturaları yeni kayıt.");
    }*/
}
