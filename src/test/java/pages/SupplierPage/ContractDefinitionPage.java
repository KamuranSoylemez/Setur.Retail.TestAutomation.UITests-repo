package pages.SupplierPage;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.WaitForSelectorState;
import enums.Categories;
import enums.DistributorInfo;
import pages.commonPages.BasePage;

public class ContractDefinitionPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator newRecordButton = page.locator(".glyphicon.glyphicon-plus");
    FrameLocator contractDefinitionFrame = getFrameByDialogTitle("Sözleşme Tanımlama");
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
        contractDefinitionFrame.locator(".glyphicon.glyphicon-search").click();
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
    public void selectCategory(){
        clickElement(contractDefinitionFrame.locator(".k-multiselect-wrap.k-floatwrap").nth(1));
    }

    /**
     * Kategori listesinden ilk seçeneği seçer.
     */
    public void selectCategoryOption() {
        Locator listBox = contractDefinitionFrame.locator("#CategoryIdArray_listbox");
        listBox.waitFor(new Locator.WaitForOptions()
                .setState(WaitForSelectorState.VISIBLE));
        clickElement(contractDefinitionFrame.locator("#CategoryIdArray_option_selected").nth(0));
    }

    /**
     * Cins seçim kutusundan verilen metne sahip seçeneği seçer.
     * @param optionText seçilecek seçenek metni
     */
    public void selectMultiSelectOption(String optionText) {
        Locator input = contractDefinitionFrame.
                locator("div.k-multiselect-wrap input.k-input").nth(2);
        input.click();

        Locator option = contractDefinitionFrame.locator("ul#TypeIdArray_listbox li")
                .filter(new Locator.FilterOptions().setHasText(optionText));
        option.waitFor(new Locator.WaitForOptions()
                .setState(WaitForSelectorState.VISIBLE));
        option.click();
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
                .fill("KMRN AUTO TEST");
    }

    /**
     * Sözleşme tanımlama formunu kaydeder.
     */
    public void saveContractDefinition(){
        clickElement(contractDefinitionFrame.locator("#btnSave"));
    }

}