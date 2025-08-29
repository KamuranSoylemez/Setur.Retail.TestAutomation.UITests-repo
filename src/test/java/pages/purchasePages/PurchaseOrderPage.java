package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import com.microsoft.playwright.options.LoadState;
import com.microsoft.playwright.options.WaitForSelectorState;
import enums.Categories;
import enums.DistributorInfo;
import org.junit.Assert;
import pages.commonPages.BasePage;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.concurrent.ThreadLocalRandom;

public class PurchaseOrderPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator calendar = page.locator(".k-icon.k-i-calendar");
    Locator selectToday = page.locator(".k-link.k-nav-today");
    Locator clickDropdownToggle = page.locator("span.k-select > span.k-icon.k-i-arrow-s");
    FrameLocator frameLocator = page.frameLocator("iframe.k-content-frame");
    FrameLocator orderProductIdentificationFrame = getFrameByDialogTitle("Sipariş Ürünü Tanımlama");
    FrameLocator productDescriptionFrame = getFrameByDialogTitle("Ürün Tanımlama");

    /**
     * Sipariş Oluşturma sayfasını verify eder.
     */
    public void verifyCreateOrderPage() {
        verifyTextElementUseTrim("Sipariş İşlemleri", pageTitle);
    }

    /**
     * Sipariş Oluşturma sayfasında Sipariş Tarihi doldurur.
     */
    public void fillOrderCreationDate() {
        clickElement(calendar.nth(0));
        selectToday.click(new Locator.ClickOptions().setForce(true));
        //kendo component nedeni ile SimpleDateFormat işe yaramıyor!!!
    }

    /**
     * Sipariş Oluşturma sayfasında Sipariş Adı doldurur.
     */
    public void fillOrderNameOrderCreationPage() {
        String timestamp = new SimpleDateFormat("yyyyMMddHHmm").format(new Date()); // Zaman bazlı sayaç
        String orderName = "KMRN_TST_AUTO_" + timestamp;
        page.locator("#PurchaseOrderName").fill(orderName);
    }

    /**
     * Kategori listesini açar.
     */
    public void openCategoryList(){
        clickElement(clickDropdownToggle.nth(0));
    }

    /**
     * Sipariş Oluşturma sayfasında Kategori alanını seçer.
     */
    public void selectCategoryFromList(String category) {
        Locator selectCategory = page.locator("#CategoryId_listbox li[role='option'].k-item",
                new Page.LocatorOptions().setHasText(category));
        selectCategory.click(new Locator.ClickOptions().setForce(true));
    }
    /**
     * Kategori alanını doğrular.
     */
    public void verifyCategory(String category) {
        Locator selectedCategory = page.locator(".k-dropdown .k-input").nth(0);
        verifyTextElementUseTrim(category, selectedCategory);
    }

    /**
     * Sipariş Oluşturma sayfasında Dağıtıcı Firma frame'ini açar.
     */
    public void openCompanyIdentificationFrame() {
        clickElement(page.locator("#FirmIdButtonId"));
    }

    /**
     * Firma Tanımlama frame'inde Firma Kodu alanını doldurur.
     */
    public void fillCompanyCode(String category){
        Categories categoryLabel = Categories.fromLabel(category);
        if (categoryLabel != null){
            DistributorInfo distributorInfo = categoryLabel.getDistributorInfo();
            frameLocator.locator("#FilterFirmCode").fill(distributorInfo.getFirmCode());
        }
    }

    /**
     * Firma Tanımlama frame sorgula butonuna tıklar.
     */
    public void clickFilterButtonId(){
        frameLocator.locator("#FilterButtonId").click();
    }

    /**
     * Firma Tanımlama frame'inde sorgu sonucunda gelen firmayı seçer.
     */
    public void selectDistributorCompany(){
        frameLocator.locator("input[name^='FirmGridId']").nth(0).click();
    }

    /**
     * Sipariş Oluşturma sayfasında Firma İlgili Kişi  tıklar.
     */
    public void clickCompanyContactPerson(){
        clickElement(page.locator(".k-multiselect-wrap.k-floatwrap").nth(3));
    }

    /**
     * Sipariş Oluşturma sayfasında Firma İlgili Kişi  seçer.
     */
    public void selectCompanyContactPerson() {
        page.waitForSelector("#FirmResponsibleUserId_listbox");
        clickElement(page.locator("#FirmResponsibleUserId_option_selected").nth(0));
    }

    /**
     * Sipariş Oluşturma sayfasında Dağılım Hedef Tipi açar.
     */
    public void openDistributionTargetType(){

        clickDropdownToggle.nth(1).click(new Locator.ClickOptions().setForce(true));
    }

    /**
     * Sipariş Oluşturma sayfasında Dağılım Hedef Tipi seçer.
     */
    public void selectDistributionTargetType() {

        page.waitForSelector("#DistributionTargetTypeId_listbox li");
        clickElement(page.locator("#DistributionTargetTypeId_listbox li").nth(1));
    }

    /**
     * Sipariş Oluşturma sayfasında Giriş Antrepo frame'ini açar.
     */
    public void openWarehouseDefinitionFrame() {
        clickElement(page.locator("#EntryWarehouseIdButtonId"));
    }

    /**
     * Antrepo Tanımlama frame'inde Setur Bölgesi listesini açar.
     */
    public void openSeturRegionFields(){
        frameLocator.locator("span.k-select > span.k-icon.k-i-arrow-s")
                .nth(0).click();
    }

    /**
     * Antrepo Tanımlama frame Setur Bölgesi seçer.
     * @param region setur bölgesi
     */
    public void selectSeturRegionFromList(String region) {
        Locator allItems = frameLocator.locator("ul#FilterSeturRegionID_listbox li");
        Locator targetItem = allItems.filter(new Locator.FilterOptions().setHasText(region));
        targetItem.click(new Locator.ClickOptions().setForce(true));
    }
    /**
     * Antrepo Tanımlama frame Setur Bölgesi doğrular.
     * @param region setur bölgesi
     */
    public void verifySeturRegion(String region) {
        Locator selectedRegion = frameLocator.locator(".k-dropdown .k-input").nth(0);
        verifyTextElementUseTrim(region, selectedRegion);
    }

    /**
     * Antrepo Tanımlama frame'inde sorgu sonucunda gelen antrepoyu seçer.
     */
    public void selectWarehouse(){
        frameLocator.locator("input[name^='WarehouseGridId']").nth(0).click();
    }

    /**
     * Sipariş Oluşturma sayfasında Fatura Adresini açar.
     */
    public void openInvoiceAddress(){
        clickElement(clickDropdownToggle.nth(3));
    }

    /**
     * Sipariş Oluşturma sayfasında Fatura Adresini seçer.
     */
    public void selectInvoiceAddress() {
        page.waitForSelector("#CompanyAddressId_listbox li");
        clickElement( page.locator("#CompanyAddressId_listbox li").nth(1));
    }

    /**
     * Sipariş Oluşturma sayfasında Teslimat Adresini açar.
     */
    public void openDeliveryAddress(){
        clickElement(clickDropdownToggle.nth(4));

    }

    /**
     * Sipariş Oluşturma sayfasında Teslimat Adresini seçer.
     */
    public void selectDeliveryAddress() {
        page.waitForSelector("#WarehouseAddressId_listbox li");
        clickElement(page.locator("#WarehouseAddressId_listbox li").nth(1));
    }

    /**
     * Sipariş Oluşturma sayfasında Sipariş Otomatik Olarak Tamamlansın mı? Hayır seçer.
     */
    public void checkOrderCompleteAutomatically() {
        clickElement( page.locator("#no_CanAutoComplete"));
    }

    /**
     * Sipariş Oluşturma sayfasında siparişi kaydeder.
     */
    public void saveOrder(){
        clickElement(page.locator("#SaveBtn"));
    }
    /**
     * Kaydedilen siparişi doğrular.
     */
    public void verifyPurchaseOrderTabs() {
        page.waitForSelector("#PurchaseOrderTabs");
        verifyIsVisible(page.locator("#PurchaseOrderTabs"));
    }

    /**
     * Sipariş Numarasını doğrular.
     */
    public void verifyOrderByOrderCode(){
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        String orderID = page.locator("#PurchaseOrderCode").getAttribute("value");
        Assert.assertNotNull("Order ID null olmamalı!", orderID);
        addString("orderCode", orderID);
    }

    /**
     * Ürün eklemek için Yeni Kayıt butonuna tıklar ve Sipariş Ürünü Tanımlama frame'ini açar.
     */
    public void openOrderProductDescriptionFrame() {
        clickElement(page.locator("a.k-grid-PurchaseOrderProductGridIdAddNew"));
    }

    /**
     * Ürün eklemek için Ürün Tanımlama frame'ini açar.
     */
    public void openProductDescriptionFrame(){
        //orderProductIdentificationFrame.locator("#ProductIdButtonId").click();
        frameLocator.locator("#ProductIdButtonId").click();
    }

    /**
     * Ürün Tanımlama frame'inde Ürün Adını alır ve kaydeder.
     */
    public void getProductName() {
        Locator productNameLocator = productDescriptionFrame.
                locator("td[data-field-name='ProductNameLong']").nth(0);

        productNameLocator.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        String productName = productNameLocator.textContent().trim();
        System.out.println("Ürün adı: " + productName);
        addString("productName", productName);
    }

    /**
     * Ürün Tanımlama frame'inde sorgulama sonucunda gelen ürünü seçer.
     */
    public void selectProduct(){
        //frame içinde frame (nested iframe) bu nedenle frameLocator kullanılamaz
        productDescriptionFrame.locator("input[name^='ProductGrid']").nth(0).click();
    }

    /**
     * Sipariş Ürünü Tanımlama frame Satın Alma Para Birimi değerini alır.
     */
    public void getCurrencyCodes() {
        Locator currencyLocator = orderProductIdentificationFrame.locator("#ProductCurrencyCode");
        currencyLocator.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));

        String productCurrency = (String) currencyLocator
                .evaluate("el => el.value || el.placeholder || el.textContent || el.innerText");

        addString("productCurrency", productCurrency);
    }


    /**
     * Sipariş Ürünü Tanımlama Sipariş- Ürün para birimleri uyuşmuyorsa frame kapatır.
     */
    public boolean ifCurrencyNotMatchCloseFrame() {
        String currencyCode = orderProductIdentificationFrame.locator("#CurrencyCode").inputValue();
        String productCurrency = getString("productCurrency");

        if (!currencyCode.equals(productCurrency)) {
            page.locator(".k-window-actions .k-window-action").nth(0).click();
            return true; // eşleşmiyor
        }
        return false; // eşleşiyor
    }

    /**
     * Sipariş Para Birimi listesini açar.
     */
    public void openOrderCurrencyCodes() {
        Locator currencyDropdownToggle = page.locator("span.k-select > span.k-icon.k-i-arrow-s");

        currencyDropdownToggle.nth(7).hover();
        currencyDropdownToggle.nth(7).click();
    }

    /**
     * Ürüne uyumlu olan para birimini seçer.
     */
    public void selectCurrencyCode() {
        String productCurrency = getString("productCurrency");

        Locator currencyOption = page.locator("ul#CurrencyCode_listbox li",
                new Page.LocatorOptions().setHasText(productCurrency));
        currencyOption.click(new Locator.ClickOptions().setTimeout(3000).setForce(true));

        //verifyTextElementUseTrim(productCurrency, currencyOption);
    }
    /**
     * Sipariş Ürünü Tanımlama frame'inde seçilen para birimini doğrular.
     */
    public void verifyProductCurrencyCode() {
        Locator selectedCurrency = page.locator(".k-dropdown .k-input").nth(3);
        String productCurrency = getString("productCurrency");
        verifyTextElementUseTrim(productCurrency, selectedCurrency);
    }


    public void confirmPopup() {
        page.locator(".ajs-button.ajs-ok").click();
    }

    /**
     * Sipariş Ürünü Tanımlama frame'inde Sipariş Adedi girer.
     */
    public void enterQuantityForProduct(){
        int randomQuantity = ThreadLocalRandom.current().nextInt(1, 21);
        setKendoNumericTextBoxValue(orderProductIdentificationFrame,"#Quantity",
                String.valueOf(randomQuantity));
        orderProductIdentificationFrame.locator("#Quantity").press("Enter");
    }

    /**
     * Sipariş Ürünü Tanımlama frame'inde Kaydet butonuna tıklar.
     */
    public void saveOrderProductsDescription(){
        orderProductIdentificationFrame.locator("#SaveBtn").click();
    }

    /**
     * Siparişe eklenen ürünü verify eder.
     */
    public void verifyProducts() {
        Locator tableProduct = page.locator("td[data-field-name='ProductName']");
        String productName = getString("productName");
        verifyTextElementUseTrim(productName , tableProduct);
    }
    /**
     * Siparişi onaya gönderme işlemi yapar. Çıkan pop-up onaylar
     */
    public void sendingForApprovalProcess() {
        page.locator("#SendApproveBtn").click();
        popUpConfirmationProcess();
    }
    /**
     * Siparişi onaylama işlemi yapar. Çıkan pop-up onaylar
     */
    public void approveOrder() {
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        page.locator("#ApproveBtn").click();
        popUpConfirmationProcess();
        Assert.assertTrue(page.locator("#SetOrderGivenBtn").isEnabled());
    }

    /**
     * Siparişi verildi işlemi yapar. Çıkan pop-up onaylar
     */
    public void setOrderPlaced() {
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);

        page.locator("#SetOrderGivenBtn").click();
        popUpConfirmationProcess();
    }
    /**
     * Siparişi sipariş no üzerinden doğrular
     */
    public void verifyOrderByOrderId() {
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);

        String orderID = page.locator("#PurchaseOrderCode").getAttribute("value");
        Assert.assertNotNull("Order ID null olmamalı!", orderID);
    }

}
