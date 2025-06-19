package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.WaitForSelectorState;
import pages.commonPages.BasePage;
import utils.GlobalVariables;

import java.util.Random;

public class PurchaseOrderInvoicePage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator filterPurchaseOrderCode = page.locator("#FilterPurchaseOrderCode");
    Locator filterButtonId = page.locator("#FilterButtonId");
    Locator edit = page.locator("#Edit");

    // sayfa doğrulama
    public void verifyPurchaseOrderSearchPage() {
        verifyTextElementUseTrim("Sipariş Sorgulama", pageTitle);
    }

    // sipariş no ile arama
    public void searchOrderIdAndEditOder() {
        filterPurchaseOrderCode.click();

        String orderID = GlobalVariables.getInstance().getString("orderCode");
        filterPurchaseOrderCode.fill(orderID);
        //filterPurchaseOrderCode.fill("3-2025-JTI-00000113"); // test ederken örnek kullanıldı

        clickElement(filterButtonId);

        String totalAmount = page.locator("td[data-field-name='TotalAmount']").textContent();
        GlobalVariables.getInstance().addString("totalAmount", totalAmount);

        clickElement(edit);
        System.out.println("Sipariş İşlemleri framei açıldı");

    }
    // Proforma yeni kayıt
    public void addProformaToOrder() {

        FrameLocator orderProcessingFrame = getFrameByDialogTitle("Sipariş İşlemleri");

        Locator proformaTab = orderProcessingFrame.locator(".k-item.k-state-default").nth(1);
        proformaTab.scrollIntoViewIfNeeded();
        proformaTab.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.ATTACHED));
        proformaTab.click(new Locator.ClickOptions().setForce(true));

        orderProcessingFrame.locator(".k-button.k-button-icontext.k-grid-ProformaReceiptGridIdAddNew")
                .click();

        Locator frameName = page.locator("#SeturModalWin_wnd_title");
        verifyTextElement("Sipariş İşlemleri", frameName);

        // String orderNo = orderProcessingFrame.locator("#PurchaseOrderCode").getAttribute("value");
        // Assert.assertEquals(GlobalVariables.getInstance().getString("orderCode"),orderNo);

        System.out.println("Proforma Kaydetme sayfası açıldı");
    }

    // Proforma kaydetme
    public void addInfoForProformaAndSave() {

        FrameLocator proformaFrame = getFrameByDialogTitle("Proforma Kaydetme");

        Random random = new Random();
        int randomNumber = random.nextInt(1000);
        String formatted = String.format("KMRN-TST-%04d", randomNumber);
        proformaFrame.locator("#ProformaNo").fill(formatted);

        proformaFrame.locator(".k-icon.k-i-calendar").click();
        proformaFrame.locator(".k-link.k-nav-today").click();

        String amount = GlobalVariables.getInstance().getString("totalAmount");
        setKendoNumericTextBoxValue(proformaFrame, "#ProformaTotalAmount", amount);

        proformaFrame.locator("#btnSave").click();
        System.out.println("Proforma kaydedildi");

    }
    // Sipariş ürünlerini kopyala ve proforma onayla
    public void copyOrderItemsAndApproveProforma() {
        FrameLocator proformaUpdateFrame = getFrameByDialogTitle("Proforma Güncelleme");

        proformaUpdateFrame.locator("#ProductCopyId").waitFor();
        proformaUpdateFrame.locator("#ProductCopyId").click();

        //orderPage.orderApprovalProcess();
        proformaUpdateFrame.locator(".ajs-button.ajs-ok").click();

        proformaUpdateFrame.locator("#approveButton").click();
        System.out.println("Proforma onaylandı");

        proformaUpdateFrame.locator("#ClosePopupBtn").click();
    }
    // Sipariş faturalarını ekleme
    public void addOrderInvoices() {
        FrameLocator orderProcessingFrame = getFrameByDialogTitle("Sipariş İşlemleri");

        Locator invoiceTab = orderProcessingFrame.locator(".k-item.k-state-default").nth(2);
        invoiceTab.scrollIntoViewIfNeeded();
        invoiceTab.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.ATTACHED));
        invoiceTab.click(new Locator.ClickOptions().setForce(true));

        orderProcessingFrame.locator(".k-button.k-button-icontext.k-grid-InvoiceGridIdAddNew")
                .click();
        System.out.println("Fatura Oluşturma framei açıldı");

    }
    // Fatura bilgilerinin girilmesi
    public void addInfoForInvoiceAndSave() {
        FrameLocator invoiceFrame = getFrameByDialogTitle("Fatura Oluşturma");

        Random random = new Random();
        int randomNumber = random.nextInt(1000);
        String formatted = String.format("KMRN-AU-%04d", randomNumber);
        invoiceFrame.locator("#InvoiceNo").fill(formatted);

        invoiceFrame.locator(".k-icon.k-i-calendar").click();
        invoiceFrame.locator(".k-link.k-nav-today").click();

        String amount = GlobalVariables.getInstance().getString("totalAmount");
        setKendoNumericTextBoxValue(invoiceFrame, "#InvoiceTotalAmount", amount);

        invoiceFrame.locator("span.k-dropdown-wrap").last().click();
        invoiceFrame.locator("#RegimeNoSourceId_listbox li").nth(1).click();

        invoiceFrame.locator("#SaveBtn").click();
        System.out.println("Fatura kaydedildi");
    }
    // Proforma ürünlerini kopyalama ve fatura tamamla
    public void copyProformaItemsAndApproveInvoice() {

        FrameLocator orderProcessingFrame = getFrameByDialogTitle("Sipariş İşlemleri");
        orderProcessingFrame.locator(".k-button.gridCmdBtn.k-success.cmdLink.InvoiceGridIdCmd")
                .nth(0).click();

        FrameLocator invoiceUpdateFrame = getFrameByDialogTitle("Fatura Güncelleme");

        invoiceUpdateFrame.locator("#ProformaProductCopyId").click();
        // pop-up onayla -- birden fazla proforma varsa yeni frame açılır!!!
        // Birden fazla proforma kontrolü yok!!!
        invoiceUpdateFrame.locator(".ajs-button.ajs-ok").click();
        
        invoiceUpdateFrame.locator("#completeButton").click();

        Locator saveBtn = invoiceUpdateFrame.locator("#SaveBtn");
        saveBtn.scrollIntoViewIfNeeded();
        saveBtn.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.ATTACHED));
        saveBtn.click(new Locator.ClickOptions().setForce(true));


        // 1. Başarı mesajı görünürse, onun DOM'dan kaybolmasını bekle
        Locator successPopup = page.locator(".ajs-message.ajs-success").last();
        if (successPopup.isVisible()) {
            successPopup.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.HIDDEN));
        }
        // ilk defa proforma fatura eklenince gelen bilgilendirme mesajını kapatır
        // Locator warningPopup = page.locator(".ajs-message.ajs-warning.ajs-visible");
        // warningPopup.click();
        // 2. Mesaj kaybolduktan sonra pencereyi kapat
        Locator closeButton = page.locator(".k-window-actions .k-i-close").nth(0);
        closeButton.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        closeButton.click();
    }
}
