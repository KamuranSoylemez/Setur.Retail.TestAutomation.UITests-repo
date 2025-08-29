package pages.distrinutionAndTransportation;

import com.microsoft.playwright.*;
import com.microsoft.playwright.options.LoadState;
import com.microsoft.playwright.options.WaitForSelectorState;
import org.junit.Assert;
import pages.commonPages.BasePage;

public class CreateDistributionPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator targetType = page.locator(".k-icon.k-i-arrow-s");
    FrameLocator warehouseDefinitionFrame = getFrameByDialogTitle("Antrepo Tanımlama");
    FrameLocator productSelection = getFrameByDialogTitle("Ürün Seçimi");
    FrameLocator productDescription = getFrameByDialogTitle("Ürün Tanımlama");
    FrameLocator distributionDetailsFrame = getFrameByDialogTitle("Dağılım Detay");

    /**
     * Dağılım Oluşturma sayfasının görüntülendiğini doğrular.
     */
    public void verifyCreateDistributionPageIsDisplayed() {
        verifyTextElementUseTrim("Dağılım Oluşturma", pageTitle);
    }

    /**
     * Hedef Tipi listesini açar.
     */
    public void clickTargetType() {
        clickElement(targetType.nth(0));
    }

    /**
     * Hedef Tipi listesinden "Antrepo" seçeneğini seçer.
     */
    public void selectTargetType() {
        page.locator("#DistTargetTypeId_listbox li").nth(1).click();
    }

    /**
     * Antrepo Tanımlama frame'ini açar.
     */
    public void openWarehouseDefinitionFrame() {
        page.locator("#DistributionWarehouseIdButtonId").click();
    }

    /**
     * Antrepo Tanımlama frame'inde Setur Bölgesi listesini açar.
     */
    public void openSeturRegionFields() {
        warehouseDefinitionFrame.locator("span.k-select > span.k-icon.k-i-arrow-s")
                    .nth(0).click();
    }

    /**
     * Setur Bölgesi listesinden belirtilen bölgeyi seçer.
     * @param region Seçilecek bölgenin adı
     */
    public void selectSeturRegionFromList(String region) {
        Locator warehouseItems = warehouseDefinitionFrame.locator("ul#FilterSeturRegionID_listbox li");
        Locator targetWarehouse = warehouseItems.filter(new Locator.FilterOptions().setHasText(region));
        targetWarehouse.click(new Locator.ClickOptions().setForce(true));
    }

    /**
     * Antrepo Tanımlama frame'inde sorgulama yapar.
     */
    public void searchWarehouse() {
        warehouseDefinitionFrame.locator("#FilterButtonId").click();
    }

    /**
     * Antrepo Tanımlama frame'inde depo listesinden ilk depoyu seçer.
     */
    public void selectWarehouseFromList() {
        warehouseDefinitionFrame.locator("input[name^='WarehouseGridId']").nth(0).click();
    }

    /**
     * Dağılım tarihi için takvim simgesine tıklar ve bugünün tarihini seçer.
     */
    public void selectDistributionDate() {
        page.locator(".k-icon.k-i-calendar").nth(0).click();
        page.locator(".k-link.k-nav-today").nth(0).click();
    }

    /**
     * Satış Sorgulama Başlangıç Tarihi için takvim simgesine tıklar ve bugünün tarihini seçer.
     */
    public void selectSalesSearchStartDate() {
        page.locator(".k-icon.k-i-calendar").nth(1).click();
        page.locator(".k-link.k-nav-today").nth(1).click();
    }

    /**
     * Açıklama alanına rastgele bir sayı ekleyerek açıklama girer.
     */
    public void enterDescription() {
        int number = generateRandomNumber();
        page.locator("#Description").fill("OTOTEST"+ number);
    }

    /**
     * Yeni kayıt oluşturmak için kaydet butonuna tıklar.
     */
    public void saveNewRecord() {
        page.locator("#btnSave").click();
    }

    /**
     * Dağılım numarasının oluşturulduğunu doğrular.
     */
    public void verifyDistributionNumberGenerated() {
        Locator container = page.locator("#lblDistributionFirmId").locator("..");
        String fullText = container.textContent().trim();
        String numberText = fullText.replace("Dağılım No", "").trim();
        Assert.assertNotEquals("Kayıt oluşturulamadı, dağılım numarası 0!", "0", numberText);
    }

    /**
     * Ürün Belirleme tabının görüntülendiğini doğrular.
     */
    public void verifyProductsFrame() {
        Assert.assertTrue(page.locator("#DistributionTabs").isVisible());
    }

    /**
     * Ürün Seçimi frame'ini açar.
     */
    public void openProductSelectionFrame() {
        page.locator(".k-grid-DistributionProductsGridAddNew").click();
    }

    /**
     * Ürün Seçimi frame'inde Ürün Tanımlama butonuna tıklar.
     */
    public void openProductDescriptionFrame() {
        productSelection.locator("#ProductIdButtonId").click();
    }

    /**
     * Ürün Tanımlama frame'inde ürün kodunu doldurur.
     * @param productCode Ürün kodu
     */
    public void fillProductCode(String productCode) {
        setKendoNumericTextBoxValue(productDescription, "#FilterProductId", productCode);
        addString("productCode", productCode);
    }

    /**
     * Ürün Tanımlama frame'inde ürün kodu sorgular.
     */
    public void searchProduct() {
        productDescription.locator("#FilterButtonId").click();
        page.waitForTimeout(1000);
    }

    /**
     * Sorgulanan ilk ürünü işaretler.
     */
    public void checkProduct() {
        productDescription.locator("input[name='ProductGridIdProductId']").first().check();
    }

    /**
     * Ürün Tanımlama frame'inde işaretlenmiş ürünü seçer.
     */
    public void selectProduct() {
        productDescription.locator("#SelectId").click();
    }

    /**
     * Ürün Seçimi frame'inde seçilen ürünü kaydeder.
     */
    public void saveProductSelection() {
        productSelection.locator("#btnProductSave").click();
    }

    /**
     * Eklenen ürünün dağılıma eklendiğini doğrular.
     */
    public void verifyProductAddedToDistribution() {
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        String productId = page.locator("td[data-field-name='ProductId']").innerText();
        String productCode = getString("productCode");
        Assert.assertEquals(productCode, productId);
        Assert.assertTrue("Ürün eklenemedi, ürün kodu: " + productId,
                          productId.contains(productCode));
    }

    /**
     * Eklenmiş ürün için detay butonuna tıklar.
     */
    public void clickDetailButton() {
        page.locator("a.glyphicon.glyphicon-cog").click();
    }

    /**
     * Eklenmiş ürün için detayda açılan sil butonuna tıklar.
     */
    public void deleteDistributionProduct() {
        page.locator("#Delete").click();
    }

    /**
     * Silme işlemini onaylar.
     */
    public void confirmDeleteProduct() {
        popUpConfirmationProcess();
    }

    /**
     * Dağılım detayları frame'ini açar.
     */
    public void openDistributionDetailsFrame() {
        page.locator("#ProductDetail").click();
        page.waitForTimeout(1000);
    }

    /**
     * Dağılım detayları frame'inde EYK için dağılım numarasını doldurur.
     */
    public void fillDistributionNumber() {
        Locator distributionNumberInput = distributionDetailsFrame
                .locator("input.k-formatted-value.AmountTextBox.k-input").nth(1);
        distributionNumberInput.click();
        page.keyboard().type("1");
        page.keyboard().press("Enter");

    }


    /**
     * Dağılım detayları frame'inde kaydet butonuna tıklar.
     */
    public void saveDistributionDetails() {
        distributionDetailsFrame.locator("#btnSaveTop").click();
    }

    /**
     * Dağılım detayları frame'inde kaydetme işlemi sonrası başarılı mesajını kapatır.
     */
    public void closeSuccessMessage() {
        page.locator(".ajs-message.ajs-success.ajs-visible").click();
        page.waitForTimeout(1000);
    }

    /**
     * Dağılım detayları frame'i kapatır.
     */
    public void closeDistributionDetailsFrame() {
        page.locator("div.k-window-actions a.k-window-action span.k-i-close").nth(0).click();
    }

    /**
     * Dağılım yapılacak ürün sayısını kontrol eder.
     */
    public void verifyDistributionNumber() {
        page.waitForLoadState(LoadState.DOMCONTENTLOADED);
        page.waitForTimeout(1000);

        Locator tdLocator = page.locator("td[data-field-name='TotalDistributionQuantity']");
        tdLocator.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        String quantityText = tdLocator.innerText().trim();

        System.out.println("Nakliyeye gönderilecek ürün sayısı: " + quantityText);
        Assert.assertNotEquals("0", quantityText);
        int quantity = Integer.parseInt(quantityText);
        Assert.assertTrue("Ürün sayısı 0'dan büyük olmalı", quantity > 0);
    }

    /**
     * Nakliyeye gönder butonuna tıklar.
     */
    public void sentToTransportation() {
        page.locator("#btnSendTransport").click();
    }

    /**
     * Nakliyeye gönderme işlemi için onaylama işlemini yapar.
     */
    public void confirmTransportationProcess() {
        popUpConfirmationProcess();
    }

    /**
     * Nakliyeye gönderme işleminin başarılı olduğunu doğrular.
     */
    public void verifyTransportationProcessSuccess() {
        page.locator(".ajs-message.ajs-success.ajs-visible").click();
    }
}
