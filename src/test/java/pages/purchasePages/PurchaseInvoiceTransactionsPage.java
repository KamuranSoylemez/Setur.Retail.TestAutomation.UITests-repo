package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.WaitForSelectorState;
import pages.commonPages.BasePage;

public class PurchaseInvoiceTransactionsPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator filterBtn = page.locator("#FilterButtonId");
    FrameLocator updateDeclarationFrame = getFrameByDialogTitle("Beyanname Güncelleme");
    FrameLocator createCountingFrame = getFrameByDialogTitle("Sayım Oluşturma");
    FrameLocator updateCountingFrame = getFrameByDialogTitle("Sayım Güncelleme");
    FrameLocator stockProcess = getFrameByDialogTitle("Stoğa Al");
    FrameLocator orderProcess = getFrameByDialogTitle("Sipariş İşlemleri");

    public void verifyPurchaseInvoiceTransactionPage() {
        verifyTextElementUseTrim("Fatura İşlemleri",pageTitle);
    }

    // Fatura işlemleri Sipariş no ile arama
    /*public void searchByOrderNumber() {

        // kendo componenti nedeni ile böyle etkileşim oluyor
        Locator input = page.locator("input[aria-owns*='PurchaseOrderId_listbox']");
        input.click();

        String orderNo = "3-2025-JTI-00000008"; //örnek data
        page.keyboard().type(orderNo, new Keyboard.TypeOptions().setDelay(100)); //kendo input
        Locator listItem = page.locator("ul[id*='PurchaseOrderId_listbox'] li.k-item");
        listItem.first().waitFor();
        listItem.first().click();

        Locator selectedTag = page.locator(
                "ul#PurchaseOrderId_taglist span",
                new Page.LocatorOptions().setHasText(orderNo));
        Assert.assertTrue(selectedTag.isVisible());

        clickElement(filterBtn);
        System.out.println("Sorgulama işlemi yapıldı: " +orderNo);

    } */

    // Fatura işlemleri Fatura No ile arama
    public void searchByInvoiceNumber() {

        Locator invoiceNo = page.locator("#FilterInvoiceNo");
        invoiceNo.click();

        String invoiceNumber = getString("Fatura No");
        invoiceNo.fill(invoiceNumber);
        //invoiceNo.fill("KMRN-05710");//örnek data

        clickElement(filterBtn);

        String invoiceContent = page.locator("td[data-field-name='InvoiceNo']").textContent();
        System.out.println("Fatura No ile arama yapıldı: " +invoiceContent);

    }

    // checkbox işaretleme ve beyanname oluşturma
    public void openInvoiceUpdateFrame() {

        Locator checkBox = page.locator("input[type='checkbox'][name^='InvoiceGridId']");
        checkBox.click();

        Locator checkboxDeclaration = page.locator("#checkboxDeclaration");
        checkboxDeclaration.nth(0).click();
        orderApprovalProcess();

        System.out.println("Beyannme oluşturma işlemi tamamlandı");

    }

    // Sayım işlemi başlatma - açıklama girme
    public void completeTheCountingProcess() {

        Locator proformaTab = updateDeclarationFrame.locator(".k-item.k-state-default").nth(2);
        proformaTab.scrollIntoViewIfNeeded();
        proformaTab.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.ATTACHED));
        proformaTab.click(new Locator.ClickOptions().setForce(true));

        updateDeclarationFrame.locator("a.k-grid-DeclarationCountGridIdAddNew").click();

        int randomNumber = generateRandomNumber();
        String formatted = String.format("SYM-%05d", randomNumber);
        createCountingFrame.locator("#Description").fill(formatted);
        createCountingFrame.locator("#SaveBtn").click();

        System.out.println("Sayım için açıklama girildi: " +formatted);

    }

    // Sayım işlemini tamamlama
    public void editCountingProcess() {

        updateDeclarationFrame.locator("#Edit").click();
        updateCountingFrame.locator("#sendCountingClickButton").click();

        updateCountingFrame.locator(".ajs-button.ajs-ok").click();

        updateCountingFrame.locator("#btnCopyRequestedToApproved").click();

        FrameLocator copyFrame = page.frameLocator("iframe[src*='ProductFilterBeforeCopy']");
        copyFrame.locator("#Copy").click();

        copyFrame.locator(".ajs-button.ajs-ok").click();

        updateCountingFrame.locator("#completeButton").click();
        updateCountingFrame.locator(".ajs-button.ajs-ok").click();
        updateCountingFrame.locator("#SaveBtn").click();

        System.out.println("Sayım işlemi tamamlandı");

    }

    // Sevkiyat dışı tut ve kaydet
    public void excludeShippingAndSave() {

        updateDeclarationFrame.locator("#yes_ExcludeDispatch").click();
        updateDeclarationFrame.locator("#SaveBtn").click();
        System.out.println("Sevkiyat dışı tut: Evet");
    }

    // Stoğa alma işlemi
    public void putInStockProcess() {

        page.waitForSelector("a[title='Beyanname Sistem No']");
        page.locator("a[title='Beyanname Sistem No']").click();

        updateDeclarationFrame.locator("#StockEntry").click();
        String customsNo = generateCustomHouseNo();
        stockProcess.locator("#CustomHouseNo").fill(customsNo);

        stockProcess.locator(".k-icon.k-i-calendar").nth(0).click();
        stockProcess.locator(".k-link.k-nav-today").nth(0).click();

        int randomNo = generateRandomNumber();
        String dormitoryEntryNo = String.format("DEN-%05d", randomNo);
        stockProcess.locator("#DeclarationEntryNo").fill(dormitoryEntryNo);

        stockProcess.locator(".k-icon.k-i-calendar").nth(1).click();
        stockProcess.locator(".k-link.k-nav-today").nth(1).click();

        stockProcess.locator("span.k-dropdown-wrap").nth(0).click();
        stockProcess.locator("#RegimeNoSourceId_listbox li").nth(1).click();

        stockProcess.locator("#btnCountAndSave").click();

        Locator saveBtn = updateDeclarationFrame.locator("#SaveBtn");
        saveBtn.scrollIntoViewIfNeeded();
        saveBtn.click();


        System.out.println("Stoğa Al işlemi tamamlandı");

    }

    // siparişi tamamla
    public void completeOrderProcess() {

        page.waitForSelector("a[title='Sipariş']");
        page.locator("a[title='Sipariş']").click();

        orderProcess.locator("#CompleteOrderBtn").click();
        orderProcess.locator(".ajs-button.ajs-ok").click();


        Locator successPopups = page.locator(".ajs-message.ajs-success");

        int count = successPopups.count();
        for (int i = 0; i < count; i++) {
            Locator popup = successPopups.nth(i);

            if (successPopups.isVisible()) {
                popup.click();
            } else {
                System.out.println("mesajlar kapatıldı");
            }
        }

        System.out.println("Sipariş Tamamlandı.");

        page.locator("div.k-window-actions a.k-window-action").click();
        
    }
}
