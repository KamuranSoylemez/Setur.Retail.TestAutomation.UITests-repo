package pages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import enums.Categories;
import enums.DistributorInfo;
import org.junit.Assert;

import java.util.Map;


public class PurchaseOrderPage extends BasePage{

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

    public void verifyPurchaseOrderPage() {

        //Assert.assertEquals("Sipariş İşlemleri", pageTitle.textContent().trim());
        verifyTextElementUseTrim(pageTitle,"Sipariş İşlemleri");
    }

    public void fillOrderDate() {

        //calendar.nth(0).click();
        clickElement(calendar.nth(0));
        selectToday.click(new Locator.ClickOptions().setForce(true));
        // kendo component nedeni ile SimpleDateFormat işe yaramıyor!!!
    }

    public void selectCategoryFromList(String category) {

        //clickDropdownForCategory.nth(0).click();
        clickElement(clickDropdownForCategory.nth(0));

        page.waitForSelector("#CategoryId_option_selected");

        Locator selectCategory = page.locator("#CategoryId_listbox li[role='option'].k-item",
                new Page.LocatorOptions().setHasText(category));
        selectCategory.click(new Locator.ClickOptions().setForce(true));

        Assert.assertEquals(selectCategory.textContent(),category);
        //System.out.println("Kategori tıklandı: " + category);

    }

    private void selectCategoryCode(){

        frameLocator.locator("#FilterButtonId").click();
        frameLocator.locator("//td[@role='gridcell']//input[1]").nth(0).click();
    }

    public void setDistributorCompany() {
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
    }

    public void selectFirmResponsibleUser() {

        //selectUser.nth(3).click();
        clickElement(selectUser.nth(3));
        page.waitForSelector("#FirmResponsibleUserId_listbox");
        //selectFirmUser.nth(0).click();
        clickElement(selectFirmUser.nth(0));

        System.out.println("ilgili kişi seçildi");
    }

    public void selectDistributionTargetType() {

        //distributionType.nth(1).click();
        clickElement(distributionType.nth(1));
        page.waitForSelector("#DistributionTargetTypeId_listbox li");
        //selectType.nth(1).click();
        clickElement(selectType.nth(1));

        System.out.println("hedef tipi seçildi");
    }

    public void selectEntryWarehouse() {

        //wareHouseBtn.click();
        clickElement(wareHouseBtn);

        frameLocator.locator("#FilterWarehouseCode").fill("639");
        frameLocator.locator("#FilterButtonId").click();
        frameLocator.locator("//td[@role='gridcell']//input[1]").nth(0).click();

        System.out.println("Giriş Antrepo Seçildi");
    }

    public void selectCompanyAddress() {

        //companyAddress.nth(3).click();
        clickElement(companyAddress.nth(3));
        page.waitForSelector("#CompanyAddressId_listbox li");
        //selectBillingAddress.nth(1).click();
        clickElement(selectBillingAddress.nth(1));

        System.out.println("Fatura Adresi seçildi");
    }

    public void selectWarehouseAddress() {

        //warehouseAddress.nth(4).click();
        clickElement(warehouseAddress.nth(4));
        page.waitForSelector("#WarehouseAddressId_listbox li");
        //selectDeliveryAddress.nth(1).click();
        clickElement(selectDeliveryAddress.nth(1));

        System.out.println("Teslimat Adresi seçildi");
    }

    public void checkCanAutoComplete() {

        page.evaluate("window.scrollTo(0, document.body.scrollHeight)");
        //checkCanAutoComplete.click();
        clickElement(checkCanAutoComplete);

        System.out.println("Sipariş Otomatik Olarak Tamamlansın mı? işaretlendi");

        //saveOrderBtn.click();
        clickElement(saveOrderBtn);

        Locator purchaseOrderTabs = page.locator("#PurchaseOrderTabs");
        page.waitForSelector("#PurchaseOrderTabs");
        Assert.assertTrue(purchaseOrderTabs.isVisible());

        System.out.println("sipariş oluşturuldu!");
    }
}
