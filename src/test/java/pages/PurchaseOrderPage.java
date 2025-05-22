package pages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import org.junit.Assert;


public class PurchaseOrderPage extends BasePage{

    Locator pageTitle = page.locator("#PageTitle");
    Locator clickDropdownForCategory = page.locator("(//span[text()='Lütfen Seçiniz'])[1]");

    public void verifyPurchaseOrderPage() {

        Assert.assertEquals("Sipariş İşlemleri", pageTitle.textContent().trim());
    }

    public void fillOrderDate() {

        Locator calendar = page.locator("(//span[@role='button']//span)[1]");
        calendar.nth(0).click();

        Locator selectToday = page.locator(".k-link.k-nav-today");
        selectToday.click(new Locator.ClickOptions().setForce(true));

        // kendo component nedeni ile SimpleDateFormat işe yaramıyor!!!
    }

    public void selectCategoryFromList(String category) {

        clickDropdownForCategory.nth(0).click();

        page.waitForSelector("#CategoryId_option_selected");

        Locator selectCategory = page.locator("#CategoryId_listbox li[role='option'].k-item",
                new Page.LocatorOptions().setHasText(category));
        selectCategory.click(new Locator.ClickOptions().setForce(true));

        Assert.assertEquals(selectCategory.textContent(),category);
        //System.out.println("Kategori tıklandı: " + category);

    }

    public void setDistributorCompany() {

        Locator openFrame = page.locator("#FirmIdButtonId");
        openFrame.click();

        FrameLocator frameLocator = page.frameLocator("iframe.k-content-frame");
        frameLocator.locator("#FilterFirmCode").fill("CHN");
        frameLocator.locator("#FilterButtonId").click();
        frameLocator.locator("//td[@role='gridcell']//input[1]").nth(0).click();

        System.out.println("Dağıtıcı Firma seçildi");

    }

    public void selectFirmResponsibleUser() {

        Locator selectUser = page.locator(".k-multiselect-wrap.k-floatwrap");
        selectUser.nth(3).click();

        page.waitForSelector("#FirmResponsibleUserId_listbox");

        Locator selectFirmUser = page.locator("#FirmResponsibleUserId_option_selected");
        selectFirmUser.nth(0).click();

        System.out.println("ilgili kişi seçildi");
    }

    public void selectDistributionTargetType() {

        Locator distributionType = page.locator(".k-dropdown-wrap.k-state-default");
        distributionType.nth(1).click();

        page.waitForSelector("#DistributionTargetTypeId_listbox li");

        Locator selectType = page.locator("#DistributionTargetTypeId_listbox li");
        selectType.nth(1).click();

        System.out.println("hedef tipi seçildi");
    }

    public void selectEntryWarehouse() {
        Locator wareHouseBtn = page.locator("#EntryWarehouseIdButtonId");
        wareHouseBtn.click();

        FrameLocator frameLocator = page.frameLocator("iframe.k-content-frame");

        frameLocator.locator("#FilterWarehouseCode").fill("639");
        frameLocator.locator("#FilterButtonId").click();
        frameLocator.locator("//td[@role='gridcell']//input[1]").nth(0).click();

        System.out.println("Giriş Antrepo Seçildi");
    }

    public void selectCompanyAddress() {
        Locator companyAddress = page.locator(".k-dropdown-wrap.k-state-default");
        companyAddress.nth(3).click();

        page.waitForSelector("#CompanyAddressId_listbox li");

        Locator selectCompanyAddress = page.locator("#CompanyAddressId_listbox li");
        selectCompanyAddress.nth(1).click();

        System.out.println("Fatura Adresi seçildi");
    }

    public void selectWarehouseAddress() {
        Locator warehouseAddress = page.locator(".k-dropdown-wrap.k-state-default");
        warehouseAddress.nth(4).click();

        page.waitForSelector("#WarehouseAddressId_listbox li");

        Locator selectCompanyAddress = page.locator("#WarehouseAddressId_listbox li");
        selectCompanyAddress.nth(1).click();

        System.out.println("Teslimat Adresi seçildi");
    }

    public void checkCanAutoComplete() {

        page.evaluate("window.scrollTo(0, document.body.scrollHeight)");

        Locator checkCanAutoComplete = page.locator("#no_CanAutoComplete");
        checkCanAutoComplete.click();

        System.out.println("Sipariş Otomatik Olarak Tamamlansın mı? işaretlendi");

        Locator saveOrderBtn = page.locator("#SaveBtn");
        saveOrderBtn.click();

        System.out.println("sipariş oluşturuldu!");
    }
}
