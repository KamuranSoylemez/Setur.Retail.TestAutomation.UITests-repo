package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import com.microsoft.playwright.options.WaitForSelectorState;
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
    Locator openFrame = page.locator("#FirmIdButtonId");
    Locator selectedCategoryDropdown = page.locator("span.k-select > span.k-icon.k-i-arrow-s");
    Locator selectUser = page.locator(".k-multiselect-wrap.k-floatwrap");
    Locator selectFirmUser = page.locator("#FirmResponsibleUserId_option_selected");
    Locator distributionType = page.locator(".k-dropdown-wrap.k-state-default");
    Locator selectType = page.locator("#DistributionTargetTypeId_listbox li");
    Locator wareHouseBtn = page.locator("#EntryWarehouseIdButtonId");
    Locator companyAddress = page.locator(".k-dropdown-wrap.k-state-default");
    Locator selectBillingAddress = page.locator("#CompanyAddressId_listbox li");
    Locator selectDeliveryAddress = page.locator("#WarehouseAddressId_listbox li");
    Locator warehouseAddress = page.locator(".k-dropdown-wrap.k-state-default");
    Locator checkCanAutoComplete = page.locator("#no_CanAutoComplete");
    Locator saveOrderBtn = page.locator("#SaveBtn");
    FrameLocator frameLocator = page.frameLocator("iframe.k-content-frame");
    FrameLocator productFrame = getFrameByDialogTitle("Sipariş Ürünü Tanımlama");

    //Sipariş İşlemleri sayfasını doğrular
    public void verifyPurchaseOrderPage() {

        verifyTextElementUseTrim("Sipariş İşlemleri", pageTitle);
    }
    //Sipariş Tarihini doldurur
    public void fillOrderDate() {

        clickElement(calendar.nth(0));
        //kendo component nedeni ile SimpleDateFormat işe yaramıyor!!!
        selectToday.click(new Locator.ClickOptions().setForce(true));

        //otomasyon test belli olması adına sipariş adı ekledim
        Locator purchaseOrderName = page.locator("#PurchaseOrderName");
        // Zaman bazlı sayaç
        String timestamp = new SimpleDateFormat("yyyyMMddHHmm").format(new Date());
        String orderName = "KMRN_TST_AUTO_" + timestamp;
        purchaseOrderName.fill(orderName);
    }

    //Kategori alanından seçim yapar
    public void selectCategoryFromList(String category) {

        clickElement(selectedCategoryDropdown.nth(0));
        //page.waitForSelector("#CategoryId_option_selected");

        Locator selectCategory = page.locator("#CategoryId_listbox li[role='option'].k-item",
                new Page.LocatorOptions().setHasText(category));
        selectCategory.click(new Locator.ClickOptions().setForce(true));

        verifyTextElementUseTrim(category, selectCategory);
        System.out.println("Selected category: " + category);

    }
    //Dağıtıcı Firma alanı seçimi (Kategoriye göre)
    public void setDistributorCompany(String category) {

        clickElement(openFrame);
        Categories categoryLabel = Categories.fromLabel(category);

        if (categoryLabel != null) {
            DistributorInfo distributor = categoryLabel.getDistributorInfo();
            frameLocator.locator("#FilterFirmCode").fill(distributor.getFirmCode());
            frameLocator.locator("#FilterButtonId").click();
            frameLocator.locator("input[type='button'][value='99']")
                    .nth(0).click();
            Locator firmName = page.locator("#FirmId_taglist li.k-button span:not(.k-delete)");
            verifyTextElementUseTrim(distributor.getFirmName(), firmName);
            System.out.println("Dağıtıcı Firma seçildi");
        } else {
            System.out.println("Uygun Kategori Bulunamadı!");
        }
    }
    //Firma İlgili Kişi
    public void selectFirmResponsibleUser() {

        Locator tagItem = page.locator("#FirmId_taglist li span:first-child");
        tagItem.waitFor(new Locator.WaitForOptions().setTimeout(1000));

        clickElement(selectUser.nth(3));

        page.waitForSelector("#FirmResponsibleUserId_listbox");
        clickElement(selectFirmUser.nth(0));

        System.out.println("ilgili kişi seçildi");
    }

    //Dağılım Hedef Tipi
    public void selectDistributionTargetType() {

        distributionType.nth(1).click(new Locator.ClickOptions().setForce(true));

        page.waitForSelector("#DistributionTargetTypeId_listbox li");
        clickElement(selectType.nth(1));

        System.out.println("hedef tipi seçildi");
    }
    //Giriş Antrepo
    public void selectEntryWarehouse() {

        clickElement(wareHouseBtn);

        // giriş antrepo dinamik nasıl yaparız?
        frameLocator.locator("#FilterWarehouseCode").fill("639");
        frameLocator.locator("#FilterButtonId").click();
        page.locator("body").scrollIntoViewIfNeeded();

        frameLocator.locator("//td[@role='gridcell']//input[1]").nth(0).click();
        // "Giriş antrepo" input alanının dolmasını bekle
        //page.waitForFunction("document.querySelectorAll('#EntryWarehouseId_taglist li').length > 0");
        System.out.println("Giriş Antrepo Seçildi");
    }
    //Fatura Adresi
    public void selectCompanyAddress() {

        clickElement(companyAddress.nth(3));
        page.waitForSelector("#CompanyAddressId_listbox li");
        clickElement(selectBillingAddress.nth(1));

        System.out.println("Fatura Adresi seçildi");
    }
    //Teslimat Adresi
    public void selectWarehouseAddress() {

        clickElement(warehouseAddress.nth(4));
        page.waitForSelector("#WarehouseAddressId_listbox li");
        clickElement(selectDeliveryAddress.nth(1));

        System.out.println("Teslimat Adresi seçildi");
    }
    //Sipariş Otomatik Olarak Tamamlansın mı?
    public void checkCanAutoCompleteAndSave() {

        clickElement(checkCanAutoComplete);
        System.out.println("Sipariş Otomatik Olarak Tamamlansın mı? işaretlendi");

        clickElement(saveOrderBtn);
        page.locator("body").scrollIntoViewIfNeeded();
        System.out.println("Kaydet butonuna tıklandı");

        Locator purchaseOrderTabs = page.locator("#PurchaseOrderTabs");
        page.waitForSelector("#PurchaseOrderTabs");
        verifyIsVisible(purchaseOrderTabs);
        System.out.println("Sipariş oluşturuldu!");
    }

    //Ürün ekleme (Yeni Kayıt)
    public void addProductToOrder() {
        Locator newProductBtn = page.locator("a.k-grid-PurchaseOrderProductGridIdAddNew");
        clickElement(newProductBtn);

        // Sipariş Ürünü Tanımlama iframe
        productFrame.locator("#ProductIdButtonId").click();

        // Ürün Tanımlama iframe
        FrameLocator productDescription = getFrameByDialogTitle("Ürün Tanımlama");
        setKendoNumericTextBoxValue(productDescription, "#FilterProductId", "1107");
        Locator filterBtn = productDescription.locator("#FilterButtonId");
        filterBtn.click();

        // Ürünü seç ve miktar gir
        productDescription.locator("(//input[@type='button'])[4]").click();
        setKendoNumericTextBoxValue(productFrame,"#Quantity","2");
        productFrame.locator("#Quantity").press("Enter");

        //Locator totalAmount = productFrame.locator("#TotalAmount");
        //totalAmount.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));

        Locator saveBtn = productFrame.locator("#SaveBtn");
        saveBtn.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        saveBtn.click();
        System.out.println("Ürün eklendi");
    }
    // Seçilen ürün/leri verify etme
    public void verifyProducts() {

        page.waitForSelector("//td[@data-field-name='ProductName']"); // belirli elementin gelmesini bekle
        List<Locator> items = page.locator("//td[@data-field-name='ProductName']").all();

        for (Locator item : items) {
            verifyTextElementUseTrim("CACHAREL W ANAIS EDT 50ML", item);
        }
    }

    // Onaya gönderme işlemi
    public void sendingForApprovalProcess() {
        Locator sendApproveBtn = page.locator("#SendApproveBtn");
        sendApproveBtn.click();

        orderApprovalProcess();
        //orderCancellationProcess();
        System.out.println("Kayıt onaya gönderildi");
    }
    // Onaylama
    public void approveOrder() {
        Locator approveBtn = page.locator("#ApproveBtn");
        approveBtn.click();

        orderApprovalProcess();
        Assert.assertTrue(page.locator("#SetOrderGivenBtn").isEnabled());
        System.out.println("Kayıt onaylandı");
    }

    // Sipariş verildi durumu
    public void setOrderPlaced() {
        Locator setOrderGivenBtn = page.locator("#SetOrderGivenBtn");
        setOrderGivenBtn.click();

        orderApprovalProcess();
         String orderID = page.locator("#PurchaseOrderCode").getAttribute("value");
         addString("orderCode",orderID);
         //GlobalVariables.getInstance().addString("orderCode",orderID);

        System.out.println("Kayıt sipariş verildi durumuna getirildi: " +orderID);
    }

}
