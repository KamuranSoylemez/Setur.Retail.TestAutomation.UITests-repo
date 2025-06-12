package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import com.microsoft.playwright.options.AriaRole;
import com.microsoft.playwright.options.WaitForSelectorState;
import enums.Categories;
import enums.DistributorInfo;
import org.junit.Assert;
import pages.commonPages.BasePage;

public class PurchaseOrderPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator clickDropdownForCategory = page.locator("(//span[text()='Lütfen Seçiniz'])[1]");
    Locator calendar = page.locator("(//span[@role='button']//span)[1]");
    Locator selectToday = page.locator(".k-link.k-nav-today");
    Locator openFrame = page.locator("#FirmIdButtonId");
    Locator selectedCategory = page.locator("(//span[@class='k-input'])[1]");
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

    //Sipariş İşlemleri sayfasını doğrular
    public void verifyPurchaseOrderPage() {

        //Assert.assertEquals("Sipariş İşlemleri", pageTitle.textContent().trim());
        verifyTextElementUseTrim(pageTitle,"Sipariş İşlemleri");
    }
    //Sipariş Tarihini doldurur
    public void fillOrderDate() {

        clickElement(calendar.nth(0));
        //kendo component nedeni ile SimpleDateFormat işe yaramıyor!!!
        selectToday.click(new Locator.ClickOptions().setForce(true));

        //otomasyon test belli olması adına sipariş adı ekledim
        Locator purchaseOrderName = page.locator("#PurchaseOrderName");
        purchaseOrderName.fill("KMRN TST AUTO");
    }
    //Kategori alanından seçim yapar
    public void selectCategoryFromList(String category) {

        clickElement(clickDropdownForCategory.nth(0));
        page.waitForSelector("#CategoryId_option_selected");

        Locator selectCategory = page.locator("#CategoryId_listbox li[role='option'].k-item",
                new Page.LocatorOptions().setHasText(category));
        selectCategory.click(new Locator.ClickOptions().setForce(true));

        verifyTextElementUseTrim(selectCategory,category);

    }
    private void selectCategoryCode(){

        frameLocator.locator("#FilterButtonId").click();
        frameLocator.locator("//td[@role='gridcell']//input[1]").nth(0).click();
    }
    //Dağıtıcı Firma alanı seçimi (Kategoriye göre)
    public void setDistributorCompany() {

        //page.evaluate("document.body.style.overflow = 'hidden'");
        Number scrollY = (Number) page.evaluate("() => window.scrollY");
        page.evaluate("scrollY => window.scrollTo(0, scrollY)", scrollY.doubleValue());

        clickElement(openFrame);

        String categoryLabel = selectedCategory.nth(0).textContent();
        Categories category = Categories.fromLabel(categoryLabel);

        if (category != null) {
            DistributorInfo distributor = category.getDistributorInfo();
            frameLocator.locator("#FilterFirmCode").fill(distributor.getFirmCode());
            selectCategoryCode();
            Locator firmName = page.locator("(//li[@unselectable='on']//span)[1]");
            verifyTextElementUseTrim(firmName, distributor.getFirmName());
            System.out.println("Dağıtıcı Firma seçildi");
        } else {
            System.out.println("Uygun Kategori Bulunamadı!");
        }
        page.evaluate("scrollY => window.scrollTo(0, scrollY)", scrollY);
        //page.evaluate("document.body.style.overflow = 'auto'");
    }
    //Firma İlgili Kişi
    public void selectFirmResponsibleUser() {

        //page.evaluate("document.body.style.overflow = 'hidden'");
        Number scrollY = (Number) page.evaluate("() => window.scrollY");
        page.evaluate("scrollY => window.scrollTo(0, scrollY)", scrollY.doubleValue());

        //selectUser.nth(3).click(new Locator.ClickOptions().setForce(true));
        clickElement(selectUser.nth(3));

        page.waitForSelector("#FirmResponsibleUserId_listbox");
        clickElement(selectFirmUser.nth(0));

        //page.evaluate("document.body.style.overflow = 'auto'");
        page.evaluate("scrollY => window.scrollTo(0, scrollY)", scrollY);

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

        page.evaluate("document.body.style.overflow = 'hidden'");

        clickElement(wareHouseBtn);

        frameLocator.locator("#FilterWarehouseCode").fill("639");
        frameLocator.locator("#FilterButtonId").click();
        page.locator("body").scrollIntoViewIfNeeded();

        frameLocator.locator("//td[@role='gridcell']//input[1]").nth(0).click();

        // "Giriş antrepo" input alanının dolmasını bekle
        page.waitForFunction("document.querySelectorAll('#EntryWarehouseId_taglist li').length > 0");

        page.evaluate("document.body.style.overflow = 'auto'");

        System.out.println("Giriş Antrepo Seçildi");
    }
    //Fatura Adresi
    public void selectCompanyAddress() {

        page.evaluate("document.body.style.overflow = 'hidden'");

        clickElement(companyAddress.nth(3));
        page.waitForSelector("#CompanyAddressId_listbox li");
        clickElement(selectBillingAddress.nth(1));

        page.evaluate("document.body.style.overflow = 'auto'");

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

        pageScroll(); // Sayfayı aşağı kaydırır 10000
        clickElement(checkCanAutoComplete);

        System.out.println("Sipariş Otomatik Olarak Tamamlansın mı? işaretlendi");

        clickElement(saveOrderBtn);
        page.locator("body").scrollIntoViewIfNeeded();

        System.out.println("Kaydet butonuna tıklandı");

        page.waitForSelector("#SendApproveBtn",
                new Page.WaitForSelectorOptions().setTimeout(60000));

        // Sayfa yüklendikten sonra scroll yap
        pageScroll();// Sayfayı aşağı kaydırır 10000

        Locator purchaseOrderTabs = page.locator("#PurchaseOrderTabs");
        page.waitForSelector("#PurchaseOrderTabs");
        verifyIsVisible(purchaseOrderTabs);

        System.out.println("Sipariş oluşturuldu!");
    }
    //kendo component özelliğinden dolayı input girişi için kod
    private void setKendoNumericTextBoxValue(FrameLocator frame, String inputSelector, int value) {
        Locator input = frame.locator(inputSelector);
        input.evaluate("(el, val) => {" +
                "  const widget = $(el).data('kendoNumericTextBox');" +
                "  if (widget) {" +
                "    widget.value(val);" +
                "    $(el).trigger('change');" +
                "  }" +
                "}", value);

    }
    //Ürün ekleme (Yeni Kayıt)
    public void addProductToOrder() {
        Locator newProductBtn = page.locator("//div[@id='PurchaseOrderProductGridId']/div[1]/a[1]");
        clickElement(newProductBtn);

        // Sipariş Ürünü Tanımlama iframe
        FrameLocator productFrame = page
                .getByRole(AriaRole.DIALOG, new Page.GetByRoleOptions().setName("Sipariş Ürünü Tanımlama"))
                .frameLocator("iframe[title='Setur']");

        productFrame.locator("#ProductIdButtonId").click();

        // Ürün Tanımlama iframe
        FrameLocator productDescription = page
                .getByRole(AriaRole.DIALOG, new Page.GetByRoleOptions().setName("Ürün Tanımlama"))
                .frameLocator("iframe[title='Setur']");

        setKendoNumericTextBoxValue(productDescription, "#FilterProductId", 397);

        Locator filterBtn = productDescription.locator("#FilterButtonId");
        while (!filterBtn.isVisible()) {
            pageScroll();
        }
        filterBtn.click();

        // Ürünü seç ve miktar gir
        productDescription.locator("(//input[@type='button'])[4]").click();
        setKendoNumericTextBoxValue(productFrame,"#Quantity",10);
        productFrame.locator("#SaveBtn").click();

        pageScroll();

        System.out.println("ürün eklendi");
    }
    //seçilen ürün/leri verify etme
    public void verifyProducts() {
        Locator productItems = page.locator("(//td[@data-field-name='ProductName'])");

        int itemCount = productItems.count();

        for(int i=0; i<itemCount; i++){

            verifyTextElementUseTrim(productItems.nth(i), "WINSTON BLUE KS 600S");
        }
    }

    private void orderApprovalProcess(){
        Locator popup = page.locator(".ajs-dialog");
        popup.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));

        Locator okButton = popup.locator(".ajs-button.ajs-ok");
        okButton.click();
    }

    private void orderCancellationProcess(){
        Locator popup = page.locator(".ajs-dialog");
        popup.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));

        Locator cancelButton = popup.locator(".ajs-button.ajs-cancel");
        cancelButton.click();
    }

    public void sendingForApprovalProcess() {
        Locator sendApproveBtn = page.locator("#SendApproveBtn");
        sendApproveBtn.click();

        orderApprovalProcess();
        //orderCancellationProcess();
    }

    public void approveOrder() {
        Locator approveBtn = page.locator("#ApproveBtn");
        approveBtn.click();

        orderApprovalProcess();

        Assert.assertTrue(page.locator("#SetOrderGivenBtn").isEnabled());
        //orderCancellationProcess();
    }

    public void setOrderPlaced() {
        Locator setOrderGivenBtn = page.locator("#SetOrderGivenBtn");
        setOrderGivenBtn.click();

        orderApprovalProcess();
        System.out.println("Kayıt sipariş verildi durumuna getirildi");
    }
}
