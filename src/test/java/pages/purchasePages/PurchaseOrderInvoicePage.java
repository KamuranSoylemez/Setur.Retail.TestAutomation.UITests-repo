package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.WaitForSelectorState;
import org.junit.Assert;
import pages.commonPages.BasePage;
import utils.GlobalVariables;

public class PurchaseOrderInvoicePage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator filterPurchaseOrderCode = page.locator("#FilterPurchaseOrderCode");
    Locator filterButtonId = page.locator("#FilterButtonId");
    Locator edit = page.locator("#Edit");
    FrameLocator orderProcessingFrame = getFrameByDialogTitle("Sipariş İşlemleri");
    FrameLocator proformaFrame = getFrameByDialogTitle("Proforma Kaydetme");
    FrameLocator proformaUpdateFrame = getFrameByDialogTitle("Proforma Güncelleme");
    FrameLocator invoiceFrame = getFrameByDialogTitle("Fatura Oluşturma");
    FrameLocator invoiceUpdateFrame = getFrameByDialogTitle("Fatura Güncelleme");

    // sayfa doğrulama
    public void verifyPurchaseOrderSearchPage() {
        verifyTextElementUseTrim("Sipariş Sorgulama", pageTitle);
    }

    // sipariş no ile arama
    public void searchOrderIdAndEditOder() {
        filterPurchaseOrderCode.click();

        String orderID = GlobalVariables.getInstance().getString("orderCode");
        filterPurchaseOrderCode.fill(orderID);
        //filterPurchaseOrderCode.fill("3-2025-JTI-00000141"); // test ederken örnek data

        clickElement(filterButtonId);

        String totalAmount = page.locator("td[data-field-name='TotalAmount']").textContent();
        GlobalVariables.getInstance().addString("totalAmount", totalAmount);

        clickElement(edit);
        System.out.println("Sipariş İşlemleri framei açıldı");

    }
    // Proforma yeni kayıt
    public void addProformaToOrder() {

        Locator proformaTab = orderProcessingFrame.locator(".k-item.k-state-default").nth(1);
        proformaTab.scrollIntoViewIfNeeded();
        proformaTab.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.ATTACHED));
        proformaTab.click(new Locator.ClickOptions().setForce(true));

        // Assertion
        Locator frameName = page.locator("#SeturModalWin_wnd_title");
        verifyTextElement("Sipariş İşlemleri", frameName);

        String orderNo = orderProcessingFrame.locator("#PurchaseOrderCode").getAttribute("value");
        String orderCode = GlobalVariables.getInstance().getString("orderCode");
        Assert.assertEquals(orderCode,orderNo);

        orderProcessingFrame.locator(".k-button.k-button-icontext.k-grid-ProformaReceiptGridIdAddNew")
                .click();
        System.out.println("Proforma Kaydetme sayfası açıldı: " +frameName.textContent());
    }

    // Proforma kaydetme
    public void addInfoForProformaAndSave() {

        int randomNumber = generateRandomNumber();
        String formatted = String.format("KMRN-TST-%04d", randomNumber);
        proformaFrame.locator("#ProformaNo").fill(formatted);

        proformaFrame.locator(".k-icon.k-i-calendar").click();
        proformaFrame.locator(".k-link.k-nav-today").click();

        String amount = GlobalVariables.getInstance().getString("totalAmount");
        setKendoNumericTextBoxValue(proformaFrame, "#ProformaTotalAmount", amount);

        Locator frameName = page.locator(".k-window-title").nth(1);
        verifyTextElement("Proforma Kaydetme", frameName);

        proformaFrame.locator("#btnSave").click();
        System.out.println("Proforma kaydedildi: " +frameName.textContent());

    }
    // Sipariş ürünlerini kopyala ve proforma onayla
    public void copyOrderItemsAndApproveProforma() {

        proformaUpdateFrame.locator("#ProductCopyId").waitFor();
        proformaUpdateFrame.locator("#ProductCopyId").click();

        //orderPage.orderApprovalProcess();
        proformaUpdateFrame.locator(".ajs-button.ajs-ok").click();

        proformaUpdateFrame.locator("#approveButton").click();
        System.out.println("Proforma onaylandı");

        Locator frameName = page.locator(".k-window-title").nth(1);
        verifyTextElement("Proforma Güncelleme", frameName);

        proformaUpdateFrame.locator("#ClosePopupBtn").click();

        System.out.println("Proforma Kaydetme işlemi yapıldı:" +frameName.textContent());
    }
    // Sipariş faturalarını ekleme
    public void addOrderInvoices() {

        Locator invoiceTab = orderProcessingFrame.locator(".k-item.k-state-default").nth(2);
        invoiceTab.scrollIntoViewIfNeeded();
        invoiceTab.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.ATTACHED));
        invoiceTab.click(new Locator.ClickOptions().setForce(true));

        orderProcessingFrame.locator(".k-button.k-button-icontext.k-grid-InvoiceGridIdAddNew")
                .click();

        Locator frameName = page.locator(".k-window-title").nth(1);
        verifyTextElement("Fatura Oluşturma", frameName);

        System.out.println("Fatura Oluşturma framei açıldı: " +frameName.textContent());

    }
    // Fatura bilgilerinin girilmesi
    public void addInfoForInvoiceAndSave() {

        int randomNumber = generateRandomNumber();
        String formatted = String.format("KMRN-AU-%04d", randomNumber);
        invoiceFrame.locator("#InvoiceNo").fill(formatted);

        invoiceFrame.locator(".k-icon.k-i-calendar").click();
        invoiceFrame.locator(".k-link.k-nav-today").click();

        String amount = GlobalVariables.getInstance().getString("totalAmount");
        setKendoNumericTextBoxValue(invoiceFrame, "#InvoiceTotalAmount", amount);

        invoiceFrame.locator("span.k-dropdown-wrap").last().click();
        invoiceFrame.locator("#RegimeNoSourceId_listbox li").nth(1).click();

        Locator frameName = page.locator(".k-window-title").nth(1);
        verifyTextElement("Fatura Oluşturma", frameName);

        invoiceFrame.locator("#SaveBtn").click();

        System.out.println("Fatura kaydedildi: " +frameName.textContent());
    }
    // Proforma ürünlerini kopyalama ve fatura tamamla
    public void copyProformaItemsAndApproveInvoice() {

        orderProcessingFrame.locator(".k-button.gridCmdBtn.k-success.cmdLink.InvoiceGridIdCmd")
                .nth(0).click();

        invoiceUpdateFrame.locator("#ProformaProductCopyId").click();
        // pop-up onayla -- birden fazla proforma varsa yeni frame açılır!!!
        // Birden fazla proforma kontrolü yok!!!
        invoiceUpdateFrame.locator(".ajs-button.ajs-ok").click();
        
        invoiceUpdateFrame.locator("#completeButton").click();

        Locator frameName = page.locator(".k-window-title").nth(1);
        verifyTextElement("Fatura Güncelleme", frameName);

        Locator saveBtn = invoiceUpdateFrame.locator("#SaveBtn");
        saveBtn.scrollIntoViewIfNeeded();
        saveBtn.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.ATTACHED));
        saveBtn.click(new Locator.ClickOptions().setForce(true));


        // 1. Başarı mesajı görünürse, onun DOM'dan kaybolmasını bekle
        Locator successPopup = page.locator(".ajs-message.ajs-success").last();
        if (successPopup.isVisible()) {
            successPopup.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.HIDDEN));
        }

        // 2. Mesaj kaybolduktan sonra pencereyi kapat
        Locator closeButton = page.locator(".k-window-actions .k-i-close").nth(0);
        closeButton.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        closeButton.click();

        System.out.println("Fatura Güncelleme yapıldı:" +frameName.textContent());
    }
}
