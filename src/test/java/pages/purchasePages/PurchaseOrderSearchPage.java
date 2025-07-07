package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.LoadState;
import com.microsoft.playwright.options.WaitForSelectorState;
import org.junit.Assert;
import pages.commonPages.BasePage;

public class PurchaseOrderSearchPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator purchaseOrderIdField = page.locator("#FilterPurchaseOrderCode");
    Locator filterButtonId = page.locator("#FilterButtonId");
    Locator edit = page.locator("#Edit");
    FrameLocator orderTransactionsFrame = getFrameByDialogTitle("Sipariş İşlemleri");
    FrameLocator savingProformaFrame = getFrameByDialogTitle("Proforma Kaydetme");
    FrameLocator updateProformaFrame = getFrameByDialogTitle("Proforma Güncelleme");
    FrameLocator createInvoiceFrame = getFrameByDialogTitle("Fatura Oluşturma");
    FrameLocator updateInvoiceFrame = getFrameByDialogTitle("Fatura Güncelleme");
    Locator orderTransactionsFrameName = page.locator("#SeturModalWin_wnd_title");
    Locator frameName = page.locator(".k-window-title").nth(1);
    Locator invoiceText = orderTransactionsFrame.locator("td[data-field-name='InvoiceNo']");
    Locator infoMessage = page.locator(".ajs-message.ajs-warning.ajs-visible");


    /**
     * Sipariş Sorgulama sayfasını doğrular.
     */
    public void verifyPurchaseOrderSearchPage() {
        verifyTextElementUseTrim("Sipariş Sorgulama", pageTitle);
    }
    /**
     * Sipariş Sorgulama sayfası Sipariş No alanını doldurur.
     */
    public void fillOrderNumberToOrderIdField() {
        purchaseOrderIdField.click();
        String orderID = getString("orderCode");
        purchaseOrderIdField.fill(orderID);
        //purchaseOrderIdField.fill("1-2025-DPL-00000062"); // test ederken örnek data
    }
    /**
     * Sipariş Sorgulama sayfası Sorgula butonuna tıklar.
     */
    public void searchByOrderNumber(){
        clickElement(filterButtonId);
    }
    /**
     * Sorgulama sonucunda gelen sipariş detayını açar ve Toplam Sipariş Tutarını saklar.
     */
    public void openOrderProcessingFrame(){
        String totalAmount = page.locator("td[data-field-name='TotalAmount']").textContent();
        addString("totalAmount", totalAmount);
        clickElement(edit);
    }
    /**
     * Sipariş işlemleri frame'inde Sipariş Proformaları tabına geçer.
     */
    public void clickProformaTab() {
        Locator proformaTab = orderTransactionsFrame.locator("li:has(a:has-text('Sipariş Proformaları'))");
        proformaTab.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        proformaTab.scrollIntoViewIfNeeded();
        proformaTab.click();
    }
    /**
     * Sipariş İşlemleri frame'ini doğrular.
     */
    public void verifyOrderProcessingFrame(){
        verifyTextElement("Sipariş İşlemleri", orderTransactionsFrameName);
    }
    /**
     * Sipariş İşlemleri frame sipariş oluşturma işleminden gelen sipariş no'yu doğrular.
     */
    public void verifyOrderNumberInOrderProcessingFrame(){
        String orderNo = orderTransactionsFrame.locator("#PurchaseOrderCode").getAttribute("value");
        String orderID = getString("orderCode");
        Assert.assertEquals(orderID,orderNo);
    }
    /**
     * Sipariş İşlemleri Sipariş Proformaları tabında yeni kayıt açar.
     */
    public void openSavingProformaFrame(){
        orderTransactionsFrame.locator(".k-button.k-button-icontext.k-grid-ProformaReceiptGridIdAddNew")
                .click();
    }
    /**
     * Proforma Kaydetme frame proforma no doldurur.
     */
    public void fillProformaNo() {
        int randomNumber = generateRandomNumber();
        String formatted = String.format("KMRN-%05d", randomNumber);
        savingProformaFrame.locator("#ProformaNo").fill(formatted);
    }
    /**
     * Proforma Kaydetme frame proforma tarihi seçer.
     */
    public void selectProformaDate(){
        savingProformaFrame.locator(".k-icon.k-i-calendar").click();
        savingProformaFrame.locator(".k-link.k-nav-today").click();
    }
    /**
     * Proforma Kaydetme frame proforma tutarı doldurur.
     */
    public void fillProformaAmount(){
        String totalAmount = getString("totalAmount");
        setKendoNumericTextBoxValue(savingProformaFrame, "#ProformaTotalAmount", totalAmount);
    }
    /**
     * Proforma Kaydetme frame doğrular.
     */
    public void verifyProformaSaveFrame(){
        verifyTextElement("Proforma Kaydetme", frameName);
    }
    /**
     * Proforma Kaydetme proforma kaydeder.
     */
    public void saveProforma(){
        savingProformaFrame.locator("#btnSave").click();
    }
    /**
     * Proforma Güncelleme sipariş ürünlerini kopyala butonuna tıklar.
     */
    public void copyOrderProductsForProforma() {

        updateProformaFrame.locator("#ProductCopyId").waitFor();
        updateProformaFrame.locator("#ProductCopyId").click();
    }
    /**
     * Proforma Güncelleme pop-up onaylar.
     */
    public void confirmPopUpForProforma(){
        updateProformaFrame.locator(".ajs-button.ajs-ok").click();
    }
    /**
     * Proforma Güncelleme onayla butonuna tıklar.
     */
    public void approveProductsForProforma(){
        updateProformaFrame.locator("#approveButton").click();
    }
    /**
     * Proforma Güncelleme frame doğrular.
     */
    public void verifyProformaUpdateFrame(){
        verifyTextElement("Proforma Güncelleme", frameName);
    }
    /**
     * Proforma Güncelleme frame kapatır.
     */
    public void closeProformaUpdateFrame(){
        updateProformaFrame.locator("#ClosePopupBtn").click();
    }
    /**
     * Sipariş İşlemleri Sipariş Faturaları tabına geçer.
     */
    public void clickOrderInvoicesTab() {
        Locator proformaTab = orderTransactionsFrame.locator("li:has(a:has-text('Sipariş Faturaları'))");
        proformaTab.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        proformaTab.scrollIntoViewIfNeeded();
        proformaTab.click();
    }
    /**
     * Sipariş İşlemleri Fatura Oluşturma frame açar.
     */
    public void openCreateInvoiceFrame(){
        orderTransactionsFrame.locator(".k-button.k-button-icontext.k-grid-InvoiceGridIdAddNew")
                .click();
    }
    /**
     * Fatura Oluşturma frame doğrular.
     */
    public void verifyCreateInvoiceFrame(){
        verifyTextElement("Fatura Oluşturma", frameName);
    }
    /**
     * Fatura Oluşturma frame fatura no doldurur.
     */
    public void fillInvoiceNo() {

        int randomNumber = generateRandomNumber();
        String formatted = String.format("KMRN-%05d", randomNumber);
        createInvoiceFrame.locator("#InvoiceNo").fill(formatted);
    }
    /**
     * Fatura Oluşturma frame fatura tarihi doldurur.
     */
    public void selectInvoiceDate(){
        createInvoiceFrame.locator(".k-icon.k-i-calendar").click();
        createInvoiceFrame.locator(".k-link.k-nav-today").click();
    }
    /**
     * Fatura Oluşturma frame fatura tutarı doldurur.
     */
    public void fillInvoiceAmount(){
        String totalAmount = getString("totalAmount");
        setKendoNumericTextBoxValue(createInvoiceFrame, "#InvoiceTotalAmount", totalAmount);
    }
    /**
     * Fatura Oluşturma frame rejim no seçer.
     */
    public void selectRegimeNo(){
        createInvoiceFrame.locator("span.k-dropdown-wrap").last().click();
        createInvoiceFrame.locator("#RegimeNoSourceId_listbox li").nth(1).click();
    }
    /**
     * Fatura Oluşturma frame kaydeder.
     */
    public void saveInvoiceInCreateInvoiceFrame(){
        createInvoiceFrame.locator("#SaveBtn").click();
    }
    /**
     * Fatura Güncellem frame açar.
     */
    public void editOrderInvoice() {

        orderTransactionsFrame.locator(".k-button.gridCmdBtn.k-success.cmdLink.InvoiceGridIdCmd")
                .nth(0).click();
    }
    /**
     * Fatura Güncellem frame sipariş ürünlerini kopyalar.
     */
    public void copyOrderProductsForInvoice(){
        updateInvoiceFrame.locator("#ProductCopyId").click();
    }
    /**
     * Fatura Güncellem frame çıkan pop-up onaylar.
     */
    public void confirmPopUpForInvoice(){
        updateInvoiceFrame.locator(".ajs-button.ajs-ok").click();
    }
    /**
     * Fatura Güncellem frame fatura tamamlama işlemi yapar.
     */
    public void completeInvoice(){
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        Locator completeButton = updateInvoiceFrame.locator("#completeButton");

        completeButton.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        completeButton.scrollIntoViewIfNeeded();

        if (completeButton.isEnabled()) {
            completeButton.click();
        } else {
            System.out.println("Complete butonu aktif değil.");
        }
    }
    /**
     * Fatura Güncellem frame fatura tamamlama kontrol eder.
     */
    public void checkIfInvoiceIsCompleted(){
        if (updateInvoiceFrame.locator("#revokeButton").isVisible()){
            System.out.println("Fatura Tamamlandı");
        }else {
            updateInvoiceFrame.locator("#completeButton").click();
        }
    }
    /**
     * Fatura Güncellem fatura kaydeder.
     */
    public void saveInvoiceInInvoiceUpdateFrame(){
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        Locator saveBtn = updateInvoiceFrame.locator("#SaveBtn");

        saveBtn.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        saveBtn.scrollIntoViewIfNeeded();

        if (saveBtn.isEnabled()) {
            saveBtn.click();
        } else {
            System.out.println("Kaydet butonu aktif değil.");
        }
    }
    /**
     * Sipariş İşlemleri frame Fatura İşlemleri için fatura no saklar.
     */
    public void storeInvoiceNoForInvoiceTransaction() {

        addString("Fatura No",invoiceText.textContent());
        System.out.println("Invoice No: " + invoiceText.textContent());
    }
    /**
     * Sipariş İşlemleri frame çıkan bilgi pop-up ını kapatır.
     */
    public void closeInformationPopup(){
        if (infoMessage.isVisible()){
            page.evaluate("document.querySelector('.ajs-message.ajs-warning.ajs-visible')?.remove()");
        }
    }
    /**
     * Sipariş İşlemleri frame kapatır.
     */
    public void closeOrderProcessFrame() {
        Locator toastMessage = page.locator(".ajs-message.ajs-success").last();
        if (toastMessage.isVisible()) {
            toastMessage.click();
            toastMessage.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.HIDDEN));
        }
        page.locator(".k-window-actions .k-i-close").nth(0).evaluate("element => element.click()");

    }
}
