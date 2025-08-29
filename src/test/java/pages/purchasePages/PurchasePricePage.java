package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import com.microsoft.playwright.options.LoadState;
import enums.DistributorInfo;
import org.junit.Assert;
import pages.commonPages.BasePage;

public class PurchasePricePage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator newRecord = page.locator(".glyphicon.glyphicon-plus");
    FrameLocator purchasePriceFrame = getFrameByDialogTitle("Satınalma Fiyatı Oluştur");
    FrameLocator productDefFrame = getFrameByDialogTitle("Ürün Tanımlama");
    Locator priceFrameName = page.locator("#SeturModalWin_wnd_title");
    FrameLocator undefinedDistributorFirm = getFrameByDialogTitle("Firma Tanımlama");


    /**
     * Satın Alma Fiyatları sayfasını doğrular.
     */
    public void verifyPurchasePricePage() {
        verifyTextElementUseTrim("Satınalma Fiyatları",pageTitle);
    }

    /**
     * Satınalma Fiyatları yeni kayıt açar.
     */
    public void newRecordPurchasePrice() {
        clickElement(newRecord);
    }

    /**
     * Satınalma Fiyatı Oluştur frame doğrular
     */
    public void verifyCreatePurchasePriceFrame(){
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        verifyTextElement("Satınalma Fiyatı Oluştur", priceFrameName);
    }

    /**
     * Ürün Tanımlama frame açar.
     */
    public void openProductDescriptionFrame() {
        purchasePriceFrame.locator("#ProductIdButtonId").click();
    }

    /**
     * Ürün Tanımlama frame doğrular.
     */
    public void verifyProductDescFrame(){
        Locator productFrameName = page.locator("span.k-window-title", new Page.LocatorOptions()
                .setHasText("Ürün Tanımlama"));
        verifyTextElement("Ürün Tanımlama", productFrameName);
    }

    /**
     * Ürün Kodu alanını doldurur.
     */
    public void fillProductCode(String productCode) {
        setKendoNumericTextBoxValue(productDefFrame, "#FilterProductId", productCode);
    }

    /**
     * Girilen ürün kodunu sorgular.
     */
    public void searchProduct(){
        productDefFrame.locator("#FilterButtonId").click();
    }

    /**
     * Sorgu sonucu gelen ürünü seçer. Ürün Tanımlama frame kapanır.
     */
    public void selectProduct(){
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        productDefFrame.locator("td[data-field-name='ProductId'] input[type='button']").click();
    }

    /**
     *  Satınalma Fiyatı Oluştur frame başlangıç tarihini doldurur.
     */
    public void fillStartDate() {
        String randomDate = generateRandomDate();
        purchasePriceFrame.locator("#StartDate").fill(randomDate);
    }

    /**
     * Satınalma Fiyatı doldurur.
     */
    public void fillPurchasePrice(){
        int number = generateRandomNumber();
        setKendoNumericTextBoxValue(purchasePriceFrame,"#Amount", String.valueOf(number));
    }

    /**
     * Tutar değerini ilerde kullanmak için saklar.
     */
    public void getValueOfAmount(){
        String newAmount = purchasePriceFrame.locator("#Amount").getAttribute("aria-valuenow");
        addString("newAmount",newAmount);
    }

    /**
     * Fiyat Türü seçer.
     */
    public void selectPriceType(){
        purchasePriceFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(1).click();
        purchasePriceFrame.locator("#PriceTypeId_listbox li").nth(1).click();
    }

    /**
     * Satınalma Fiyatı Oluştur işlemini kaydeder.
     */
    public void saveCreatePurchasePrice(){
        purchasePriceFrame.locator("#Save").click();
    }

    /**
     * Ana sayfada Ürün Tanımlam frame açar.
     */
    public void openProductDescFrame() {
        page.locator("#FilterProductIdButtonId").click();
    }

    /**
     * Ana sayfada ürün sorgular.
     */
    public void searchProductInMainPage(){
        page.locator("#FilterButtonId").click();
    }

    /**
     * Satın Alma Fiyatını karşılaştırır.
     */
    public void verifyPurchasePriceAmount() {
        String amount = getString("newAmount"); // örn: "9091"
        Locator mainPageAmount = page.locator("td[data-field-name='Amount']").nth(0);

        String actualText = mainPageAmount.textContent().replace(".", ""); // "9.091,000000" → "9091,000000"
        Assert.assertEquals(amount + ",000000", actualText);
    }

    /**
     * Tamınsız ürün seçer.
     */
    public void selectUndefinedProduct() {
        purchasePriceFrame.locator("#no_DefUndefProduct").click();
    }

    /**
     * Tanımsız ürün Firma Tanımı frame açar.
     */
    public void openCompanyIdentification(){
        purchasePriceFrame.locator("#UndefinedDistributorFirmIdButtonId").click();
    }

    /**
     * Kategori seçimine göre firma kodu doldurur.
     * @param distributorInfo DistributorInfo enum klasından istenilen firma kodu
     */
    public void fillCompanyCode(DistributorInfo distributorInfo){
        undefinedDistributorFirm.locator("#FilterFirmCode").fill(distributorInfo.getFirmCode());
    }

    /**
     * Firma sorgular.
     */
    public void searchCompany(){
        undefinedDistributorFirm.locator("#FilterButtonId").click();
    }

    /**
     * Sorgulanan firmayı seçer.
     */
    public void selectCompany(){
        undefinedDistributorFirm.locator("input[type='button'][name*='FirmGrid']").click();
    }

    /**
     * Tanımsız ürün için üretici firma Firma tanımlama frame açar.
     */
    public void openManufacturerCompany(){
        purchasePriceFrame.locator("#UndefinedProducerFirmIdButtonId").click();
    }
    /**
     * Kategori seçimine göre firma kodu doldurur.
     * @param distributorInfo DistributorInfo enum sınıfında istenilen firma kodu
     */
    public void fillManufacturerCompany(DistributorInfo distributorInfo){
        undefinedDistributorFirm.locator("#FilterFirmCode").fill(distributorInfo.getFirmCode());
    }

    /**
     * Barkod numarası üretir.
     */
    public void fillUnidentifiedProductBarcode() {
        String randomNumber = generateBarcodeNumber();
        purchasePriceFrame.locator("#UndefinedBarcode").fill(randomNumber);
    }

    /**
     * KDV tutarı doldurur.
     */
    public void setVatAmount(){
        setKendoNumericTextBoxValue(purchasePriceFrame,"#VatAmount","7");
    }

    /**
     * Ana sayfada firma tanımlama frame açar.
     */
    public void openCompanyIdentificationFrame() {
        page.locator("#FilterDistributorFirmIdButtonId").click();
    }
}
