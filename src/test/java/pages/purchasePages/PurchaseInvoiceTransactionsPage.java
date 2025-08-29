package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.LoadState;
import com.microsoft.playwright.options.WaitForSelectorState;
import org.junit.Assert;
import pages.commonPages.BasePage;

public class PurchaseInvoiceTransactionsPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator filterBtn = page.locator("#FilterButtonId");
    FrameLocator updateDeclarationFrame = getFrameByDialogTitle("Beyanname Güncelleme");
    FrameLocator createCountingFrame = getFrameByDialogTitle("Sayım Oluşturma");
    FrameLocator updateCountingFrame = getFrameByDialogTitle("Sayım Güncelleme");
    FrameLocator stockProcess = getFrameByDialogTitle("Stoğa Al");
    FrameLocator orderProcess = getFrameByDialogTitle("Sipariş İşlemleri");
    Locator invoiceNo = page.locator("#FilterInvoiceNo");
    Locator checkBox = page.locator("input[type='checkbox'][name^='InvoiceGridId']");
    Locator checkboxDeclaration = page.locator("#checkboxDeclaration");
    FrameLocator copyFrame = page.frameLocator("iframe[src*='ProductFilterBeforeCopy']");

    /**
     * Fatura İşlemleri sayfasını doğrular
     */
    public void verifyPurchaseInvoiceTransactionPage() {
        verifyTextElementUseTrim("Fatura İşlemleri",pageTitle);
    }
    /**
     * Fatura İşlemleri Fatura No alanı doldurur.
     */
    public void fillInvoiceNo() {
        clickElement(invoiceNo);
        String invoiceNumber = getString("Fatura No");
        invoiceNo.fill(invoiceNumber);
        //invoiceNo.fill("ER6788888"); //örnek data
    }
    /**
     * Fatura İşlemleri Fatura No ile sorgular.
     */
    public void searchForInvoiceNo(){
        clickElement(filterBtn);
    }
    /**
     * Fatura İşlemleri Fatura No doğrular.
     */
    public void verifyInvoiceNo(){
        String invoiceContent = page.locator("td[data-field-name='InvoiceNo']").textContent();
        String invoiceNumber = getString("Fatura No");
        Assert.assertEquals(invoiceContent,invoiceNumber);
    }
    /**
     * Fatura İşlemleri sorgu ile gelen Fatura no checkbox işaretler.
     */
    public void clickCheckboxForDeclaration() {
        clickElement(checkBox);
    }
    /**
     * Fatura İşlemleri Beyanname oluşturur. Beyanname Güncelleme frame açılır.
     */
    public void createDeclaration(){
        checkboxDeclaration.nth(0).click();
        popUpConfirmationProcess();
    }
    /**
     * Beyanname Güncelleme frame sayım tabına geçer.
     */
    public void selectCountingTab() {
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        Locator proformaTab = updateDeclarationFrame.locator(".k-item.k-state-default").nth(2);
        proformaTab.scrollIntoViewIfNeeded();
        proformaTab.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.ATTACHED));
        proformaTab.click(new Locator.ClickOptions().setForce(true));
    }
    /**
     * Sayım Oluşturma frame açar.
     */
    public void creatingCount(){
        updateDeclarationFrame.locator("a.k-grid-DeclarationCountGridIdAddNew").click();
    }
    /**
     * Sayım Oluşturma frame açıklama alanını random doldurur.
     */
    public void fillDescriptionField(){
        int randomNumber = generateRandomNumber();
        String formatted = String.format("SYM-%05d", randomNumber);
        createCountingFrame.locator("#Description").fill(formatted);
    }
    /**
     * Sayım Oluşturma frame kaydeder.
     */
    public void saveCountDescription(){
        createCountingFrame.locator("#SaveBtn").click();
    }
    /**
     * Sayım Güncelleme frame açar.
     */
    public void editCountingProcess() {

        updateDeclarationFrame.locator("#Edit").click();
    }
    /**
     * Sayım Güncelleme frame sayıma gönder işlemi yapar.
     */
    public void sendForCount(){
        updateCountingFrame.locator("#sendCountingClickButton").click();
        updateCountingFrame.locator(".ajs-button.ajs-ok").click();
    }
    /**
     * Sayım Güncelleme frame talebi onaylayana kopyala işlemini yapar.
     */
    public void copyRequestToApprover(){
        updateCountingFrame.locator("#btnCopyRequestedToApproved").click();
    }
    /**
     * Talebi onaylayana kopyala işlemini tamamlar.
     */
    public void  copyingProcess(){
        copyFrame.locator("#Copy").click();
        copyFrame.locator(".ajs-button.ajs-ok").click();
    }
    /**
     * Sayım Güncelleme frame sayım tamamlar.
     */
    public void completingCount(){
        updateCountingFrame.locator("#completeButton").click();
        updateCountingFrame.locator(".ajs-button.ajs-ok").click();
    }
    /**
     * Sayım Güncelleme frame kaydeder.
     */
    public void saveUpdateCount(){
        updateCountingFrame.locator("#SaveBtn").click();
    }
    /**
     * Beyanname Güncelleme frame Sevkiyat Dışı Tut checkbox iişaretler.
     */
    public void checkExcludeShipping() {

        updateDeclarationFrame.locator("#yes_ExcludeDispatch").click();
    }
    /**
     * Beyanname Güncelleme frame kaydeder.
     */
    public void saveDeclarationUpdate(){
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        Locator saveBtn = updateDeclarationFrame.locator("#SaveBtn");
        saveBtn.scrollIntoViewIfNeeded();
        saveBtn.click();
    }
    /**
     * Fatura İşlemleri sayfası Beyanname Sistem No ile Beyanname Güncelleme frame açar.
     */
    public void openUpdateDeclarationFrame() {

        page.waitForSelector("a[title='Beyanname Sistem No']");
        page.locator("a[title='Beyanname Sistem No']").click();
    }
    /**
     * Beyanname Güncelleme frame stoğa al frame açar.
     */
    public void openPutInStockFrame(){
        updateDeclarationFrame.locator("#StockEntry").click();
    }
    /**
     * Stoğa Al frame Gümrük Beyanname No belli standarda göre doldurur.
     */
    public void fillCustomsDeclarationNoField(){
        String customsNo = generateCustomHouseNo();
        stockProcess.locator("#CustomHouseNo").fill(customsNo);
    }
    /**
     * Stoğa Al frame Gümrük Tarihi seçer.
     */
    public void selectCustomsDate(){
        stockProcess.locator(".k-icon.k-i-calendar").nth(0).click();
        stockProcess.locator(".k-link.k-nav-today").nth(0).click();
    }
    /**
     * Stoğa Al frame Yurda Giriş No doldurur.
     */
    public void fillDormitoryEntryNo(){
        int randomNo = generateRandomNumber();
        String dormitoryEntryNo = String.format("DEN-%05d", randomNo);
        stockProcess.locator("#DeclarationEntryNo").fill(dormitoryEntryNo);
    }
    /**
     * Stoğa Al frame Yurtiçi Giriş Tarihi seçer.
     */
    public void selectDomesticEntryDate(){
        stockProcess.locator(".k-icon.k-i-calendar").nth(1).click();
        stockProcess.locator(".k-link.k-nav-today").nth(1).click();
    }
    /**
     * Stoğa Al frame Rejim No seçer.
     */
    public void selectRegimeNo(){
        stockProcess.locator("span.k-dropdown-wrap").nth(0).click();
        stockProcess.locator("#RegimeNoSourceId_listbox li").nth(1).click();
    }
    /**
     * Stoğa Al frame bilgileri kaydeder.
     */
    public void savePutInStockProcess(){
        stockProcess.locator("#btnCountAndSave").click();
        Locator stockSuccessMessage = page.locator("ajs-message.ajs-success.ajs-visible");
        if (stockSuccessMessage.isVisible()){
            stockSuccessMessage.click();
        }
    }
    /**
     * Fatura İşlemleri sipariş no ile Sipariş İşlemleri frame açar.
     */
    public void openOrderProcessFrame() {

        page.waitForSelector("a[title='Sipariş']");
        page.locator("a[title='Sipariş']").click();
    }
    /**
     * Sipariş İşlemleri frame sipariş tamamla işlemini yapar.
     */
    public void completeOrder(){
        orderProcess.locator("#CompleteOrderBtn").click();
        Locator message = orderProcess.locator(".ajs-button.ajs-ok");
        if (message.isVisible()){
            message.click();
        }
    }
    /**
     * Sipariş İşlemleri frame success mesajlarını ve frame kapatır.
     */
    public void closeOrderProcessFrame() {
        Locator successPopup = page.locator(".ajs-message.ajs-success").last();

        if (successPopup.isVisible()) {
            successPopup.click();
        } else {
            System.out.println("mesajlar kapatıldı");
        }
        page.locator("div.k-window-actions a.k-window-action").nth(0).click();
    }
}
