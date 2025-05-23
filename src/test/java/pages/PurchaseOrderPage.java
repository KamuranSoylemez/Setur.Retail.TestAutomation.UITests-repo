package pages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import enums.Categories;
import org.junit.Assert;


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

    private void selectCategoryCode(){ //bu işlem tüm if-else lerde yapılıyor. Method haline getirdim

        frameLocator.locator("#FilterButtonId").click();
        frameLocator.locator("//td[@role='gridcell']//input[1]").nth(0).click();
    }

    public void setDistributorCompany() {

        //openFrame.click();
        clickElement(openFrame);

        //--------------kategoriye göre dağıtıcı firma seçimi-------------
        Locator firmName = page.locator("(//li[@unselectable='on']//span)[1]");

        if (selectedCategory.nth(0).textContent().equals(Categories.PARFUM_KOZMETIK.getLabel())) {

        frameLocator.locator("#FilterFirmCode").fill("CHN");
            selectCategoryCode();
            verifyTextElementUseTrim(firmName,"CHANEL PARFUMS");
        }
        else if (selectedCategory.nth(0).textContent().equals(Categories.GIDA.getLabel())) {
            frameLocator.locator("#FilterFirmCode").fill("MIGRS");
            selectCategoryCode();
            verifyTextElementUseTrim(firmName,"MİGROS TİCARET A.Ş.");
        }
        else if (selectedCategory.nth(0).textContent().equals(Categories.TOBACCO_PRODUCTS.getLabel())) {
            frameLocator.locator("#FilterFirmCode").fill("JTI");
            selectCategoryCode();
            verifyTextElementUseTrim(firmName,"JAPAN TOBACCO INTERNATIONAL");
        }
        else if (selectedCategory.nth(0).textContent().equals(Categories.BUTIK_AKSESUAR.getLabel())) {
            frameLocator.locator("#FilterFirmCode").fill("FOS");
            selectCategoryCode();
            verifyTextElementUseTrim(firmName,"FOSSIL");
        }
        else if (selectedCategory.nth(0).textContent().equals(Categories.SPIRITS.getLabel())) {
            frameLocator.locator("#FilterFirmCode").fill("TUBORG");
            selectCategoryCode();
            verifyTextElementUseTrim(firmName,"TUBORG");
        }
        else if (selectedCategory.nth(0).textContent().equals(Categories.OYUNCAK.getLabel())) {
            frameLocator.locator("#FilterFirmCode").fill("LEG");
            selectCategoryCode();
            verifyTextElementUseTrim(firmName,"LEGO");
        }
        else if (selectedCategory.nth(0).textContent().equals(Categories.BAZAAR.getLabel())) {
            frameLocator.locator("#FilterFirmCode").fill("MPAZ");
            selectCategoryCode();
            verifyTextElementUseTrim(firmName,"MALATYA PAZARI");
        }
        else if (selectedCategory.nth(0).textContent().equals(Categories.ELEKTRONIK.getLabel())) {
            frameLocator.locator("#FilterFirmCode").fill("CAPI");
            selectCategoryCode();
            verifyTextElementUseTrim(firmName,"CAPI");
        }
        else if (selectedCategory.nth(0).textContent().equals(Categories.POSET.getLabel())) {
            frameLocator.locator("#FilterFirmCode").fill("20002560");
            selectCategoryCode();
            verifyTextElementUseTrim(firmName,"ARCE PLASTİK İÇ VE DIŞ TİC");
        }
        else if (selectedCategory.nth(0).textContent().equals(Categories.ESANTIYON.getLabel())) {
            frameLocator.locator("#FilterFirmCode").fill("ÇNRTX");
            selectCategoryCode();
            verifyTextElementUseTrim(firmName,"ÇINARTEKS TAAHHÜT TURİZM VE AMBALAJ SAN.DIŞ TİC.LTD.ŞTİ.");

        }
        else {
            System.out.println("Uygun Kategori Bulunamadı!");
        }
        System.out.println("Dağıtıcı Firma seçildi");

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

        System.out.println("sipariş oluşturuldu!");
    }
}
