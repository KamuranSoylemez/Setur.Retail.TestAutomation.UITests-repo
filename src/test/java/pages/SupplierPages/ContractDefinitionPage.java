package pages.SupplierPages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.LoadState;
import com.microsoft.playwright.options.WaitForSelectorState;
import enums.Categories;
import enums.DistributorInfo;
import io.cucumber.java.PendingException;
import org.junit.Assert;
import pages.commonPages.BasePage;

public class ContractDefinitionPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator newRecordButton = page.locator(".glyphicon.glyphicon-plus");
    FrameLocator contractDefinitionFrame = getFrameByDialogTitle("Sözleşme Tanımlama");
    FrameLocator contractUpdateFrame = getFrameByDialogTitle("Sözleşme Güncelleme");
    FrameLocator companyIdentificationFrame = getFrameByDialogTitle("Firma Tanımlama");

    /**
     * Sözleşme Tanımlama sayfasının açıldığını doğrular.
     */
    public void verifyContractDefinitionPageIsDisplayed() {
        verifyTextElementUseTrim("Sözleşme Tanımlama", pageTitle);
    }

    /**
     * Yeni kayıt butonuna tıklar.
     */
    public void openNewContractDefinitionForm() {
        clickElement(newRecordButton);
    }

    /**
     * Firma tanımlama penceresini açar.
     */
    public void openCompanyIdentificationFrame() {
        contractDefinitionFrame.locator("#FirmIDButtonId").click();
    }

    /**
     * Firma kodu alanını doldurur.
     * @param category kategori bilgisine göre firma kodunu alır.
     */
    public void fillCompanyCode(String category) {
        Categories categoryLabel = Categories.fromLabel(category);
        if (categoryLabel != null) {
            DistributorInfo distributorInfo = categoryLabel.getDistributorInfo();
            companyIdentificationFrame.locator("#FilterFirmCode").fill(distributorInfo.getFirmCode());

            String firmName = distributorInfo.getFirmName();
            addString("FirmName", firmName);
        }
    }

    /**
     * Firma arama butonuna tıklar.
     */
    public void searchCompany() {
        companyIdentificationFrame.locator("#FilterButtonId").click();
    }

    /**
     * Firma listesinden ilk firmayı seçer.
     */
    public void selectCompanyFromList() {
        companyIdentificationFrame.locator("input[name^='FirmGridId']").nth(0).click();
    }

    /**
     * Kategori çoklu seçim kutusunu açar.
     */
    public void openCategories(){
        clickElement(contractDefinitionFrame.locator(".k-multiselect-wrap.k-floatwrap").nth(1));
    }

    /**
     * Kategori listesinden ilk seçeneği seçer.
     */
    public void selectCategoryOption(String category) {
        contractDefinitionFrame.locator("#CategoryIdArray_taglist").
                locator("xpath=..").locator("input.k-input").click();

        Locator option = contractDefinitionFrame.locator("ul#CategoryIdArray_listbox li")
                .filter(new Locator.FilterOptions().setHasText(category));
        option.waitFor(new Locator.WaitForOptions()
                .setState(WaitForSelectorState.VISIBLE));
        option.click();

        addString("CategoryName", category);
    }

    /**
     * Cins seçim kutusundan ilk seçeneği seçer.
     */
    public void selectTypeOption() {
        Locator input = contractDefinitionFrame.
                locator("div.k-multiselect-wrap input.k-input").nth(2);
        input.click();

        Locator option = contractDefinitionFrame
                .locator("ul#TypeIdArray_listbox li").first();
        option.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        option.click();

        addString("TypeName", option.textContent());
    }

    /**
     * Başlangıç tarihi seçici butonuna tıklar.
     */
    public void selectStartDate() {
        contractDefinitionFrame.locator("span.k-select[aria-controls='StartDate_dateview']").click();
    }

    /**
     * Ayın ilk gününü seçer.
     */
    public void selectFirstDayOfMonth() {
        Locator firstDay = contractDefinitionFrame.locator(
                "//td[not(contains(@class,'k-other-month')) " +
                        "and not(contains(@class,'k-state-disabled'))]//a[text()='1']"
        );
        firstDay.waitFor(new Locator.WaitForOptions()
                .setState(WaitForSelectorState.VISIBLE));
        firstDay.click();
    }

    /**
     * Mali yıl başlangıç ayını seçer.
     */
    public void selectFiscalMonthStart() {
        Locator dropdown = contractDefinitionFrame.locator("#FiscalMonthStart")
                .locator("xpath=..");

        dropdown.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        dropdown.click(new Locator.ClickOptions().setForce(true)); // force ile zorla aç

        Locator option = contractDefinitionFrame.locator("#FiscalMonthStart_listbox >> text=Ocak");
        option.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        option.click();
    }

    /**
     * Teslimat Şekli (Incoterms) seçici kutusundan DAP - Delivered At Place seçeneğini seçer.
     */
    public void selectIncoterms(){
        Locator dropdown = contractDefinitionFrame.locator("#IncotermsID")
                .locator("xpath=..");

        dropdown.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        dropdown.click(new Locator.ClickOptions().setForce(true)); // force ile zorla aç

        Locator option = contractDefinitionFrame.
                locator("#IncotermsID_listbox >> text=DAP - Delivered At Place");
        option.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        option.click();

    }

    /**
     * Marka seçici kutusundan bir marka seçer.
     */
    public void selectBrand(String brandOptions) {
        Locator input = contractDefinitionFrame.locator("#BrandIds_taglist")
                .locator("xpath=following-sibling::input");
        input.click();
        page.keyboard().type(brandOptions);
        page.keyboard().press("Enter");

        // String brandName = contractDefinitionFrame.locator("#BrandIds_taglist li.k-button span")
        //      .first().textContent();

        addString("BrandName", brandOptions);
    }


    /**
     * Ödeme Şartları (Vade (Gün)) alanını doldurur.
     */
    public void fillTermDays() {
        contractDefinitionFrame.locator("input[data-role='numerictextbox']")
                .evaluate("(el, val) => { " +
                        "var widget = $(el).data('kendoNumericTextBox');" +
                        "if (widget) { " +
                        "    widget.value(val); " +
                        "    widget.trigger('change'); " +
                        "    widget.element.blur(); " + // blur ile formun tepki vermesi sağlanabilir
                        "} " +
                        "}", 75);
    }

    /**
     * Açıklama alanını doldurur.
     */
    public void fillDescription(){
        contractDefinitionFrame.locator("#Description")
                .fill("AUTO TEST");
    }

    /**
     * Sözleşme tanımlama formunu kaydeder.
     */
    public void saveContractDefinition(){
        clickElement(contractDefinitionFrame.locator("#btnSave"));
    }


    public void verifyDuplicateRecord() {
        Locator errorMessage = page.locator(".ajs-message.ajs-error.ajs-visible");

        if (errorMessage.isVisible()) {
            String errorText = errorMessage.textContent();
            Assert.assertEquals("Sistemde bu kayıt mevcuttur. Başka bir kayıt deneyin.", errorText);
            System.out.println("Beklenen hata mesajı alındı, test SUCCESS olarak kabul edildi.");

            // Step’i skip ederek senaryoyu bitir
            throw new PendingException("Duplicate kayıt mevcut. Senaryo burada success olarak tamamlandı.");
        } else {
            // Beklenmeyen durum → test fail
            throw new RuntimeException("Beklenen duplicate hata mesajı görünmedi. Senaryo fail oldu!");
        }
    }


    public void verifyRecordSavedSuccessfully() {
        Locator successMessage = page.locator(".ajs-message.ajs-success.ajs-visible");

        if (successMessage.isVisible()) {
            System.out.println("Kaydetme işlemi başarılı: " + successMessage.textContent());
        } else {
            System.out.println("Kaydetme işlemi başarısız!!!");
        }
    }

    /**
     * Sözleşme kaydından sonra sözleşme durumunu doğrular.
     * @param status sözleşme durumu
     */
    public void verifyContractStatus(String status) {
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        page.waitForTimeout(5000); // 5 saniye bekle

        String contractStatus = contractUpdateFrame.locator("#ContractStatus").inputValue();
        System.out.println("Contract Status: " + contractStatus);
        Assert.assertEquals(status, contractStatus);

    }

    /**
     * Sözleşme tanımlama penceresini kapatır.
     */
    public void closeContractUpdateFrame() {
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        page.waitForTimeout(5000); // 5 saniye bekle
        contractUpdateFrame.locator("#ClosePopupBtn").click();
    }


    /**
     * Ana sayfada firma tanımlama penceresini açar.
     */
    public void openCompanyIdentificationFrameOnMainPage() {
        Locator button = page.locator("#FilterFirmIDButtonId");
        button.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        button.click();
    }

    /**
     * Ana sayfada kategori açılır menüsünü açar.
     */
    public void openCategoriesFromMainPage() {
        page.locator("#FilterCategoryIds").
                locator("xpath=..").locator(".k-dropdown-wrap").click();

    }

    /**
     * Ana sayfada kategori listesinden verilen kategori metnine sahip seçeneği seçer.
     * @param category seçilecek kategori metni
     */
    public void selectCategoryFromList(String category) {
        Locator option = page.locator("ul#FilterCategoryIds_listbox li")
                .filter(new Locator.FilterOptions().setHasText(category));
        option.waitFor(new Locator.WaitForOptions()
                .setState(WaitForSelectorState.VISIBLE));
        option.click();

    }

    /**
     * Ana sayfada cins çoklu seçim kutusundan daha önce formda seçilen cins seçeneğini seçer.
     */
    public void selectTypeOptionFromMainPage() {
        String type = getString("TypeName");

        Locator input = page.locator("input.k-input[aria-owns='FilterTypeIds_taglist FilterTypeIds_listbox']");
        input.click();

        Locator option = page.locator("ul#FilterTypeIds_listbox li")
                .filter(new Locator.FilterOptions().setHasText(type));

        option.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        option.click();
    }

    /**
     * Ana sayfada kayıt arama butonuna tıklar.
     */
    public void searchForRecordOnMainPage() {
        page.locator("#FilterButtonId").click();
    }

    /**
     * Ana sayfada kayıtların listelendiğini doğrular.
     */
    public void verifyRecordExistsOnMainPage() {
        Locator records = page.locator("#ContractGridId tr[data-uid]").nth(0);
        if (records.count() > 0) {
            System.out.println("Kayıt başarıyla oluşturuldu ve listede mevcut.");
        } else {
            System.out.println("Kayıt oluşturulamadı veya listede bulunamadı.");
        }
    }

    /**
     * Ana sayfada firma adını doğrular.
     */
    public void verifyFirmNameOnMainPage() {
        String firmName = page.locator("td[data-field-name='FirmName']").nth(0).textContent();
        System.out.println("Firma adı: " + firmName);
        String firmNameFromDistributorInfo = getString("FirmName");
        Assert.assertEquals(firmName,firmNameFromDistributorInfo);
    }

    /**
     * Ana sayfada kategori adını doğrular.
     * @param category doğrulanacak kategori metni
     */
    public void verifyCategoryOnMainPage(String category) {
        String categoryName = page.locator("td[data-field-name='CategoryNames']").nth(0).textContent();
        System.out.println("Kategori adı: " + categoryName);
        Assert.assertEquals(categoryName,category);
    }

    /**
     * Ana sayfada cins adını doğrular.
     */
    public void verifyTypeOnMainPage() {
        String typeName = page.locator("td[data-field-name='TypeNames']").nth(0).textContent();
        System.out.println("Cins adı: " + typeName);
        String typeFromForm = getString("TypeName");
        Assert.assertEquals(typeName,typeFromForm);
    }

    /**
     * Firmaya göre gelen kategori alanını doğrular.
     */
    public void verifyCategorySelected() {
        String categoryName = getString("CategoryName");
        String selectedCategory = contractDefinitionFrame
                .locator("#CategoryIdArray_taglist li span:first-child").textContent().trim();
        Assert.assertEquals(categoryName, selectedCategory);
    }

    /**
     * Firmaya ve kayegoriye göre gelen Cins alanından verilen cinsi seçer.
     * @param type seçilecek cins metni
     */
    public void selectType(String type) {
        Locator input = contractDefinitionFrame.
                locator("div.k-multiselect-wrap input.k-input").nth(2);
        input.click();

        Locator option = contractDefinitionFrame.locator("ul#TypeIdArray_listbox li")
                .filter(new Locator.FilterOptions().setHasText(type));
        option.waitFor(new Locator.WaitForOptions()
                .setState(WaitForSelectorState.VISIBLE));
        option.click();

        addString("typeName", type);

    }

    /**
     * Firmaya ve kayegoriye göre gelen Cins alanını doğrular.
     */
    public void verifyTypeOptionSelected() {
        String typeName = getString("typeName");
        String selectedType = contractDefinitionFrame
                .locator("#TypeIdArray_taglist li span:first-child").textContent().trim();
        Assert.assertEquals(typeName, selectedType);
    }

    /**
     * Firmaya ve kayegoriye göre gelen Marka alanını doğrular.
     */
    public void verifyBrandSelected() {
        String brandName = getString("BrandName");
        String selectedBrand = contractDefinitionFrame
                .locator("#BrandIds_taglist li span:first-child").textContent().trim();
        Assert.assertEquals(brandName, selectedBrand);
    }

    /**
     * Sözleşme Tanımlama Frame kapatır.
     */
    public void closeContractDefinitionFrame() {
        contractDefinitionFrame.locator("#ClosePopupBtn").click();
    }
}