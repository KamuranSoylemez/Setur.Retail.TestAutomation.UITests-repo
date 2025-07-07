package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import com.microsoft.playwright.options.LoadState;
import enums.Categories;
import enums.DistributorInfo;
import org.junit.Assert;
import pages.commonPages.BasePage;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

public class PurchaseOrderPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator calendar = page.locator(".k-icon.k-i-calendar");
    Locator selectToday = page.locator(".k-link.k-nav-today");
    Locator purchaseOrderName = page.locator("#PurchaseOrderName");
    Locator clickCompanyIdentificationSearchButton = page.locator("#FirmIdButtonId");
    Locator selectedCategoryDropdown = page.locator("span.k-select > span.k-icon.k-i-arrow-s");
    Locator clickResponsibleUserField = page.locator(".k-multiselect-wrap.k-floatwrap");
    Locator selectResponsibleUser = page.locator("#FirmResponsibleUserId_option_selected");
    Locator distributionTypeSelectionField = page.locator(".k-dropdown-wrap.k-state-default");
    Locator clickDistributionTargetType = page.locator("#DistributionTargetTypeId_listbox li");
    Locator entryWarehouseSearchButton = page.locator("#EntryWarehouseIdButtonId");
    Locator clickBillingAddressField = page.locator(".k-dropdown-wrap.k-state-default");
    Locator selectBillingAddress = page.locator("#CompanyAddressId_listbox li");
    Locator clickDeliveryAddressField = page.locator(".k-dropdown-wrap.k-state-default");
    Locator selectDeliveryAddress = page.locator("#WarehouseAddressId_listbox li");
    Locator checkCanAutoCompleteToNo = page.locator("#no_CanAutoComplete");
    Locator saveOrderBtn = page.locator("#SaveBtn");
    FrameLocator frameLocator = page.frameLocator("iframe.k-content-frame");
    FrameLocator productFrame = getFrameByDialogTitle("Sipariş Ürünü Tanımlama");
    Locator purchaseOrderTabs = page.locator("#PurchaseOrderTabs");
    Locator newProductBtn = page.locator("a.k-grid-PurchaseOrderProductGridIdAddNew");
    FrameLocator productDescriptionFrame = getFrameByDialogTitle("Ürün Tanımlama");
    Locator purchaseOrderCode = page.locator("#PurchaseOrderCode");

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
        selectToday.click(new Locator.ClickOptions().setForce(true)); //kendo component nedeni ile SimpleDateFormat işe yaramıyor!!!
    }
    /**
     * Sipariş Oluşturma sayfasında Sipariş Adı doldurur.
     */
    public void fillOrderNameOrderCreationPage() {
        String timestamp = new SimpleDateFormat("yyyyMMddHHmm").format(new Date()); // Zaman bazlı sayaç
        String orderName = "KMRN_TST_AUTO_" + timestamp;
        purchaseOrderName.fill(orderName);
    }
    /**
     * Sipariş Oluşturma sayfasında Kategori alanını seçer.
     */
    public void selectCategoryFromList(String category) {
        clickElement(selectedCategoryDropdown.nth(0));

        Locator selectCategory = page.locator("#CategoryId_listbox li[role='option'].k-item",
                new Page.LocatorOptions().setHasText(category));
        selectCategory.click(new Locator.ClickOptions().setForce(true));

        verifyTextElementUseTrim(category, selectCategory);
    }
    /**
     * Sipariş Oluşturma sayfasında Dağıtıcı Firma frame'ini açar.
     */
    public void openCompanyIdentificationFrame() {
        clickElement(clickCompanyIdentificationSearchButton);
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
     * Framelerde sorgula butonuna tıklar.
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
     * Sipariş Oluşturma sayfasında Firma İlgili Kişi  seçer.
     */
    public void selectCompanyContactPerson() {

        clickElement(clickResponsibleUserField.nth(3));
        page.waitForSelector("#FirmResponsibleUserId_listbox");
        clickElement(selectResponsibleUser.nth(0));
    }
    /**
     * Sipariş Oluşturma sayfasında Dağılım Hedef Tipi seçer.
     */
    public void selectDistributionTargetType() {
        distributionTypeSelectionField.nth(1).click(new Locator.ClickOptions().setForce(true));

        page.waitForSelector("#DistributionTargetTypeId_listbox li");
        clickElement(clickDistributionTargetType.nth(1));
    }
    /**
     * Sipariş Oluşturma sayfasında Giriş Antrepo frame'ini açar.
     */
    public void openWarehouseDefinitionFrame() {
        clickElement(entryWarehouseSearchButton);
    }
    /**
     * Antrepo Tanımlama frame'inde Antrepo Kodu alanını doldurur.
     */
    public void fillWarehouseCodeField(){
        frameLocator.locator("#FilterWarehouseCode").fill("639");
    }
    /**
     * Antrepo Tanımlama frame'inde sorgu sonucunda gelen antrepoyu seçer.
     */
    public void selectWarehouse(){
        frameLocator.locator("//input[starts-with(@name, 'WarehouseGridId')]").nth(0).click();
    }
    /**
     * Sipariş Oluşturma sayfasında Fatura Adresini seçer.
     */
    public void selectInvoiceAddress() {
        clickElement(clickBillingAddressField.nth(3));
        page.waitForSelector("#CompanyAddressId_listbox li");
        clickElement(selectBillingAddress.nth(1));
    }
    /**
     * Sipariş Oluşturma sayfasında Teslimat Adresini seçer.
     */
    public void selectDeliveryAddress() {
        clickElement(clickDeliveryAddressField.nth(4));
        page.waitForSelector("#WarehouseAddressId_listbox li");
        clickElement(selectDeliveryAddress.nth(1));
    }
    /**
     * Sipariş Oluşturma sayfasında Sipariş Otomatik Olarak Tamamlansın mı? Hayır seçer.
     */
    public void checkOrderCompleteAutomatically() {
        clickElement(checkCanAutoCompleteToNo);
    }
    /**
     * Sipariş Oluşturma sayfasında siparişi kaydeder.
     */
    public void saveOrder(){
        clickElement(saveOrderBtn);

        page.waitForSelector("#PurchaseOrderTabs");
        verifyIsVisible(purchaseOrderTabs);
    }
    public void verifyOrderByOrderCode(){
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        String orderID = purchaseOrderCode.getAttribute("value");
        Assert.assertNotNull("Order ID null olmamalı!", orderID);
        addString("orderCode", orderID);
    }
    /**
     * Ürün eklemek için Yeni Kayıt butonuna tıklar ve Sipariş Ürünü Tanımlama frame'ini açar.
     */
    public void openOrderProductDescriptionFrame() {
        clickElement(newProductBtn);
    }
    /**
     * Ürün eklemek için Ürün Tanımlama frame'ini açar.
     */
    public void openProductDescriptionFrame(){
        productFrame.locator("#ProductIdButtonId").click();
    }
    /**
     * Ürün Tanımlama frame'inde ürün kodu girer.
     */
    public void enterProductCode(){
        setKendoNumericTextBoxValue(productDescriptionFrame, "#FilterProductId", "1107");
    }
    /**
     * Ürün Tanımlama frame'inde sorgula butonuna basar.
     */
    public void clickFilterButtonProductDescFrame(){
        productDescriptionFrame.locator("#FilterButtonId").click();
    }
    /**
     * Ürün Tanımlama frame'inde sorgulama sonucunda gelen ürünü seçer.
     */
    public void selectProduct(){
        productDescriptionFrame.locator("//input[starts-with(@name, 'ProductGridId')]").nth(0).click();
    }
    /**
     * Sipariş Ürünü Tanımlama frame'inde Sipariş Adedi girer.
     */
    public void enterQuantityForProduct(){
        setKendoNumericTextBoxValue(productFrame,"#Quantity","2");
        productFrame.locator("#Quantity").press("Enter");
    }
    /**
     * Sipariş Ürünü Tanımlama frame'inde Kaydet butonuna tıklar.
     */
    public void saveOrderProductsDescription(){
        productFrame.locator("#SaveBtn").click();
    }
    /**
     * Siparişe eklenen ürün/ürünleri verify eder.
     */
    public void verifyProducts() {

        page.waitForSelector("//td[@data-field-name='ProductName']");
        List<Locator> items = page.locator("//td[@data-field-name='ProductName']").all();

        for (Locator item : items) {
            verifyTextElementUseTrim("CACHAREL W ANAIS EDT 50ML", item);
        }
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

        String orderID = purchaseOrderCode.getAttribute("value");
        Assert.assertNotNull("Order ID null olmamalı!", orderID);
    }
}
