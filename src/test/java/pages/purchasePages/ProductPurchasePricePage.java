package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import org.junit.Assert;
import pages.commonPages.BasePage;
import utils.GlobalVariables;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.Random;

public class ProductPurchasePricePage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator newRecord = page.locator(".glyphicon.glyphicon-plus");

    public void verifyPurchasePricePage() {
        verifyTextElementUseTrim("Satınalma Fiyatları",pageTitle);
    }

    public void newRecordPurchasePrice() {
        clickElement(newRecord);
    }

    // tanımlı ürün için fiyat oluşturma
    public void createPurchasePriceForDefinedProduct() {

        FrameLocator purchasePriceFrame = getFrameByDialogTitle("Satınalma Fiyatı Oluştur");
        purchasePriceFrame.locator("#ProductIdButtonId").click();

        Locator priceFrameName = page.locator("#SeturModalWin_wnd_title");
        verifyTextElement("Satınalma Fiyatı Oluştur", priceFrameName);

        FrameLocator productDefFrame = getFrameByDialogTitle("Ürün Tanımlama");
        setKendoNumericTextBoxValue(productDefFrame, "#FilterProductId", "209");
        productDefFrame.locator("#FilterButtonId").click();
        productDefFrame.locator("td[data-field-name='ProductId'] input[type='button']").click();


        Locator productFrameName = page.locator("span.k-window-title", new Page.LocatorOptions()
                .setHasText("Ürün Tanımlama"));
        verifyTextElement("Ürün Tanımlama", productFrameName);

        System.out.println("Tanımlı ürün belirlendi: " +productFrameName.textContent());

        Random random = new Random(); //random date (aynı tarih için farklı fiyat için uyarı veriyor)
        int daysToAdd = random.nextInt(30);
        LocalDate randomDate = LocalDate.now().plusDays(daysToAdd);
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd.MM.yyyy");
        String formattedDate = randomDate.format(formatter);

        purchasePriceFrame.locator("#StartDate").fill(formattedDate);

        int number = generateRandomNumber();

        setKendoNumericTextBoxValue(purchasePriceFrame,"#Amount", String.valueOf(number));

        String newAmount = purchasePriceFrame.locator("#Amount").getAttribute("aria-valuenow");
        //purchasePriceFrame.locator("#Amount").evaluate("el => $(el).data('kendoNumericTextBox').value()").toString();
        GlobalVariables.getInstance().addString("newAmount",newAmount);

        System.out.println("Yeni tutar belirlendi: " +newAmount);

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

    public void searchDefinedProductAndVerifyAmount() {

        page.locator("#FilterProductIdButtonId").click();

        FrameLocator productDefFrame = getFrameByDialogTitle("Ürün Tanımlama");
        setKendoNumericTextBoxValue(productDefFrame, "#FilterProductId", "209");
        productDefFrame.locator("#FilterButtonId").click();
        productDefFrame.locator("td[data-field-name='ProductId'] input[type='button']").click();

        page.locator("#FilterButtonId").click();

        String amount =  GlobalVariables.getInstance().getString("newAmount");
        Locator mainPageAmount = page.locator("td[data-field-name='Amount']").nth(0);

        Assert.assertEquals(amount+",000000",mainPageAmount.textContent());

        System.out.println("Yeni Tutar: " +mainPageAmount.textContent());

    }

    public void createPurchasePriceForUndefinedProduct() {

        FrameLocator purchasePriceFrame = getFrameByDialogTitle("Satınalma Fiyatı Oluştur");

        Locator priceFrameName = page.locator("#SeturModalWin_wnd_title");
        verifyTextElement("Satınalma Fiyatı Oluştur", priceFrameName);

        purchasePriceFrame.locator("#no_DefUndefProduct").click();
        purchasePriceFrame.locator("#UndefinedDistributorFirmIdButtonId").click();

        FrameLocator undefinedDistributorFirm = getFrameByDialogTitle("Firma Tanımlama");
        undefinedDistributorFirm.locator("#FilterFirmCode").fill("JTI");
        undefinedDistributorFirm.locator("#FilterButtonId").click();
        undefinedDistributorFirm.locator("input[type='button'][value='413']").click();

        purchasePriceFrame.locator("#UndefinedProducerFirmIdButtonId").click();
        FrameLocator undefinedProducerFirm = getFrameByDialogTitle("Firma Tanımlama");
        undefinedProducerFirm.locator("#FilterFirmCode").fill("JTI");
        undefinedProducerFirm.locator("#FilterButtonId").click();
        undefinedProducerFirm.locator("input[type='button'][value='413']").click();


        Random randomNum = new Random();
        StringBuilder sb = new StringBuilder();
        // İlk rakam 1–9 arasında olmalı (başta 0 olamaz)
        sb.append(randomNum.nextInt(9) + 1);
        // Geri kalan 12 rakam 0–9
        for (int i = 0; i < 12; i++) {
            sb.append(randomNum.nextInt(10));
        }

        String randomNums = sb.toString();
        System.out.println("13 Haneli Sayı (String): " + randomNums);

        purchasePriceFrame.locator("#UndefinedBarcode").fill(randomNums);

        Random random = new Random();
        int daysToAdd = random.nextInt(30); // 0 ile 29 arasında
        LocalDate randomDate = LocalDate.now().plusDays(daysToAdd); // ← Bu satır önemli
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd.MM.yyyy");
        String formattedDate = randomDate.format(formatter); // ← randomDate burada LocalDate

        purchasePriceFrame.locator("#StartDate").fill(formattedDate);

        int randNumber = generateRandomNumber();

        setKendoNumericTextBoxValue(purchasePriceFrame,"#Amount", String.valueOf(randNumber));

        String newProductAmount = purchasePriceFrame.locator("#Amount")
                .getAttribute("aria-valuenow");
        //purchasePriceFrame.locator("#Amount").evaluate("el => $(el).data('kendoNumericTextBox').value()").toString();
        GlobalVariables.getInstance().addString("newProAmount",newProductAmount);

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

    public void searchUndefinedProductAndVerifyAmount() {

        page.locator("#FilterDistributorFirmIdButtonId").click();

        FrameLocator undefinedDistributorFirm = getFrameByDialogTitle("Firma Tanımlama");
        undefinedDistributorFirm.locator("#FilterFirmCode").fill("JTI");
        undefinedDistributorFirm.locator("#FilterButtonId").click();
        undefinedDistributorFirm.locator("input[type='button'][value='413']").click();

        page.locator("#FilterButtonId").click();

        String amount =  GlobalVariables.getInstance().getString("newProAmount");
        Locator mainPageProductAmount = page.locator("td[data-field-name='Amount']").nth(0);

        Assert.assertEquals(amount+",000000",mainPageProductAmount.textContent());
        System.out.println("Yeni Tutar: " +mainPageProductAmount.textContent());

    }
}
