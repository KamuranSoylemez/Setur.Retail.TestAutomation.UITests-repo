package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import org.junit.Assert;
import pages.commonPages.BasePage;

public class PurchasePricePage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator newRecord = page.locator(".glyphicon.glyphicon-plus");
    FrameLocator purchasePriceFrame = getFrameByDialogTitle("Satınalma Fiyatı Oluştur");
    FrameLocator productDefFrame = getFrameByDialogTitle("Ürün Tanımlama");
    Locator priceFrameName = page.locator("#SeturModalWin_wnd_title");
    FrameLocator undefinedDistributorFirm = getFrameByDialogTitle("Firma Tanımlama");

    public void verifyPurchasePricePage() {
        verifyTextElementUseTrim("Satınalma Fiyatları",pageTitle);
    }

    // Yeni Kayıt
    public void newRecordPurchasePrice() {
        clickElement(newRecord);
    }

    // tanımlı ürün için fiyat oluşturma
    public void createPurchasePriceForDefinedProduct() {

        purchasePriceFrame.locator("#ProductIdButtonId").click();

        verifyTextElement("Satınalma Fiyatı Oluştur", priceFrameName);

        setKendoNumericTextBoxValue(productDefFrame, "#FilterProductId", "209");
        productDefFrame.locator("#FilterButtonId").click();
        productDefFrame.locator("td[data-field-name='ProductId'] input[type='button']").click();

        Locator productFrameName = page.locator("span.k-window-title", new Page.LocatorOptions()
                .setHasText("Ürün Tanımlama"));
        verifyTextElement("Ürün Tanımlama", productFrameName);

        System.out.println("Tanımlı ürün belirlendi: " +productFrameName.textContent());

        String randomDate = generateRandomDate();
        purchasePriceFrame.locator("#StartDate").fill(randomDate);

        int number = generateRandomNumber();
        setKendoNumericTextBoxValue(purchasePriceFrame,"#Amount", String.valueOf(number));

        String newAmount = purchasePriceFrame.locator("#Amount").getAttribute("aria-valuenow");
        //purchasePriceFrame.locator("#Amount").evaluate("el => $(el).data('kendoNumericTextBox').value()").toString();
        addString("newAmount",newAmount);

        System.out.println("Yeni tutar belirlendi: " +newAmount);

        purchasePriceFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(1).click();
        purchasePriceFrame.locator("#PriceTypeId_listbox li").nth(1).click();
        purchasePriceFrame.locator("#Save").click();

        Locator warningPopup = purchasePriceFrame.locator(".ajs-dialog");
        if (warningPopup.isVisible()){
            purchasePriceFrame.locator(".ajs-button.ajs-ok").click();
        }

        System.out.println("Sipariş fiyatı oluşturuldu: " +priceFrameName.textContent());
    }

    // ana ekranda tanımlı ürüne girilen fiyat doğrula
    public void searchDefinedProductAndVerifyAmount() {

        page.locator("#FilterProductIdButtonId").click();

        setKendoNumericTextBoxValue(productDefFrame, "#FilterProductId", "209");
        productDefFrame.locator("#FilterButtonId").click();
        productDefFrame.locator("td[data-field-name='ProductId'] input[type='button']").click();

        page.locator("#FilterButtonId").click();

        String amount =  getString("newAmount");
        Locator mainPageAmount = page.locator("td[data-field-name='Amount']").nth(0);

        Assert.assertEquals(amount+",000000",mainPageAmount.textContent());
        System.out.println("Yeni Tutar: " +mainPageAmount.textContent());

    }

    // tanımsız ürün için fiyat oluşturma
    public void createPurchasePriceForUndefinedProduct() {

        verifyTextElement("Satınalma Fiyatı Oluştur", priceFrameName);

        purchasePriceFrame.locator("#no_DefUndefProduct").click();
        purchasePriceFrame.locator("#UndefinedDistributorFirmIdButtonId").click();
        selectFirmByCode(undefinedDistributorFirm, "JTI", "413");

        purchasePriceFrame.locator("#UndefinedProducerFirmIdButtonId").click();
        selectFirmByCode(undefinedDistributorFirm, "JTI", "413");

        String randomNumber = generateBarcodeNumber();
        purchasePriceFrame.locator("#UndefinedBarcode").fill(randomNumber);

        String randomDate = generateRandomDate();
        purchasePriceFrame.locator("#StartDate").fill(randomDate);

        int randNumber = generateRandomNumber();
        setKendoNumericTextBoxValue(purchasePriceFrame,"#Amount", String.valueOf(randNumber));

        String newProductAmount = purchasePriceFrame.locator("#Amount")
                .getAttribute("aria-valuenow");
        //purchasePriceFrame.locator("#Amount").evaluate("el => $(el).data('kendoNumericTextBox').value()").toString();
        addString("newProAmount",newProductAmount);

        System.out.println("Yeni tutar oluşturuldu: " +newProductAmount);

        setKendoNumericTextBoxValue(purchasePriceFrame,"#VatAmount","7");
        purchasePriceFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(1).click();
        purchasePriceFrame.locator("#PriceTypeId_listbox li").nth(1).click();
        purchasePriceFrame.locator("#Save").click();

        Locator warningPopup = purchasePriceFrame.locator(".ajs-dialog");
        if (warningPopup.isVisible()){
            purchasePriceFrame.locator(".ajs-button.ajs-ok").click();
        }

        System.out.println("Sipariş fiyatı oluştur işlemi tamamlandı: " +priceFrameName.textContent());

    }

    // ana ekranda tanımsız ürüne girilen fiyat doğrula
    public void searchUndefinedProductAndVerifyAmount() {

        page.locator("#FilterDistributorFirmIdButtonId").click();
        selectFirmByCode(undefinedDistributorFirm, "JTI", "413");
        page.locator("#FilterButtonId").click();

        String amount =  getString("newProAmount");
        Locator mainPageProductAmount = page.locator("td[data-field-name='Amount']").nth(0);

        Assert.assertEquals(amount+",000000",mainPageProductAmount.textContent());
        System.out.println("Yeni Tutar: " +mainPageProductAmount.textContent());

    }

    public void selectFirmByCode(FrameLocator frame, String firmCode, String buttonValue) {
        frame.locator("#FilterFirmCode").fill(firmCode);
        frame.locator("#FilterButtonId").click();
        frame.locator("input[type='button'][value='" + buttonValue + "']").click();
    }

}
