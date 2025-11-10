package stepDefs;

import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.junit.jupiter.api.Assertions;
import pages.SupplierPages.RebateInvoicePoolPage;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

public class RebateInvoicePoolStepDefs {
    
    RebateInvoicePoolPage rebateInvoicePoolPage = new RebateInvoicePoolPage();
    
    @Then("verify rebate invoice pool search form is visible")
    public void verifySearchFormIsVisible() {
        boolean isVisible = rebateInvoicePoolPage.verifySearchFormVisible();
        Assertions.assertTrue(isVisible, "Arama formu görünür olmalı!");
        System.out.println("✅ Rebate Invoice Pool arama formu görünür olduğu doğrulandı");
    }
    
    @When("user clicks rebate invoice pool search button")
    public void userClicksSearchButton() {
        rebateInvoicePoolPage.clickSearchButton();
    }
    
    // Firma
    @When("user selects rebate invoice pool company {string}")
    public void userSelectsCompany(String companyName) {
        rebateInvoicePoolPage.selectCompany(companyName);
    }
    
    // Rebate Tarihi
    @When("user fills rebate invoice pool rebate date with {string}")
    public void userFillsRebateDateWith(String rebateDate) {
        rebateInvoicePoolPage.fillRebateDate(rebateDate);
    }
    
    // Kondisyon Tipi
    @When("user selects rebate invoice pool condition type {string}")
    public void userSelectsConditionType(String conditionType) {
        rebateInvoicePoolPage.selectConditionType(conditionType);
    }
    
    // Hesaplama Türü
    @When("user selects rebate invoice pool calculation type {string}")
    public void userSelectsCalculationType(String calculationType) {
        rebateInvoicePoolPage.selectCalculationType(calculationType);
    }
    
    // Durum
    @When("user selects rebate invoice pool status {string}")
    public void userSelectsStatus(String status) {
        rebateInvoicePoolPage.selectStatus(status);
    }
    
    // Para Birimi
    @When("user selects rebate invoice pool currency {string}")
    public void userSelectsCurrency(String currency) {
        rebateInvoicePoolPage.selectCurrency(currency);
    }
    
    // Hesaplama Periyodu
    @When("user selects rebate invoice pool calculation period {string}")
    public void userSelectsCalculationPeriod(String calculationPeriod) {
        rebateInvoicePoolPage.selectCalculationPeriod(calculationPeriod);
    }
    
    // Sözleşme Adı
    @When("user fills rebate invoice pool contract name with {string}")
    public void userFillsContractNameWith(String contractName) {
        rebateInvoicePoolPage.fillContractName(contractName);
    }
    
    // Kategori
    @When("user selects rebate invoice pool category {string}")
    public void userSelectsCategory(String category) {
        rebateInvoicePoolPage.selectCategory(category);
    }
    
    // Açıklama
    @When("user fills rebate invoice pool description with {string}")
    public void userFillsDescriptionWith(String description) {
        rebateInvoicePoolPage.fillDescription(description);
    }
    
    // Assertions
    @Then("verify rebate invoice pool grid has results")
    public void verifyGridHasResults() {
        boolean hasResults = rebateInvoicePoolPage.verifyGridHasResults();
        Assertions.assertTrue(hasResults, "Grid'de sonuç olmalı ama bulunamadı!");
        System.out.println("✅ Rebate Invoice Pool Grid'de sonuç olduğu doğrulandı");
    }
    
    @Then("verify rebate invoice pool grid has results or no records message")
    public void verifyGridHasResultsOrNoRecordsMessage() {
        boolean hasResults = rebateInvoicePoolPage.verifyGridHasResults();
        boolean noRecordsMessageDisplayed = rebateInvoicePoolPage.verifyNoRecordsMessageDisplayed();
        
        System.out.println("🔍 Grid'de sonuç var mı: " + hasResults);
        System.out.println("🔍 'Kayıt bulunamadı' mesajı görünür mü: " + noRecordsMessageDisplayed);
        
        assertTrue(hasResults || noRecordsMessageDisplayed, 
            "Grid'de ya sonuç olmalı ya da 'Kayıt bulunamadı' mesajı görünmeli!");
    }

    @Then("verify rebate invoice pool grid has no results")
    public void verifyGridHasNoResults() {
        int rowCount = rebateInvoicePoolPage.getGridRowCount();
        System.out.println("📊 Grid satır sayısı: " + rowCount);
        
        assertEquals(0, rowCount, "Grid boş olmalı ama " + rowCount + " satır bulundu!");
        System.out.println("✅ Rebate Invoice Pool Grid boş olduğu doğrulandı");
    }
    
    @Then("verify rebate invoice pool no records message is displayed")
    public void verifyNoRecordsMessageIsDisplayed() {
        boolean isDisplayed = rebateInvoicePoolPage.verifyNoRecordsMessageDisplayed();
        Assertions.assertTrue(isDisplayed, "'Kayıt bulunamadı' mesajı görünmeli ama görünmüyor!");
        System.out.println("✅ 'Kayıt bulunamadı' mesajı görüntülendiği doğrulandı");
    }
    
    @Then("verify rebate invoice pool all columns are sortable")
    public void verifyAllColumnsAreSortable() {
        boolean allSortable = rebateInvoicePoolPage.verifyAllColumnsAreSortable();
        Assertions.assertTrue(allSortable, "Tüm kolonlarda sort çalışmalı!");
        System.out.println("✅ Rebate Invoice Pool - Tüm kolonlarda sort çalıştığı doğrulandı");
    }
    
    @Then("verify rebate invoice pool pagination is working")
    public void verifyPaginationIsWorking() {
        boolean paginationWorks = rebateInvoicePoolPage.verifyPaginationIsWorking();
        Assertions.assertTrue(paginationWorks, "Pagination çalışmalı!");
        System.out.println("✅ Rebate Invoice Pool Pagination çalıştığı doğrulandı");
    }
    
    @When("user clicks rebate invoice pool history icon on first row")
    public void userClicksHistoryIconOnFirstRow() {
        rebateInvoicePoolPage.clickHistoryIconOnFirstRow();
    }
    
    @Then("verify rebate invoice pool history page is opened")
    public void verifyHistoryPageIsOpened() {
        boolean isOpened = rebateInvoicePoolPage.verifyHistoryPageOpened();
        Assertions.assertTrue(isOpened, "Tarihçe sayfası açılmalı!");
        System.out.println("✅ Rebate Invoice Pool Tarihçe sayfası açıldığı doğrulandı");
    }
    
    @Then("verify rebate invoice pool history description contains condition ids and explanation")
    public void verifyHistoryDescriptionContainsConditionIdsAndExplanation() {
        boolean hasValidContent = rebateInvoicePoolPage.verifyHistoryDescriptionContent();
        Assertions.assertTrue(hasValidContent, "Tarihçe açıklaması kondisyon ID ve açıklama içermeli!");
        System.out.println("✅ Rebate Invoice Pool Tarihçe açıklaması geçerli içerik içerdiği doğrulandı");
    }
    
    // ============ HISTORY (TARIHÇE) STEPS ============
    
    @When("user clicks rebate invoice pool settings icon on first row")
    public void userClicksSettingsIconOnFirstRow() {
        rebateInvoicePoolPage.clickSettingsIconOnFirstRow();
    }
    
    @When("user clicks rebate invoice pool history button")
    public void userClicksHistoryButton() {
        rebateInvoicePoolPage.clickHistoryButton();
    }
    
    @Then("verify rebate invoice pool history modal is opened")
    public void verifyHistoryModalIsOpened() {
        boolean isOpened = rebateInvoicePoolPage.verifyHistoryModalIsOpened();
        Assertions.assertTrue(isOpened, "Tarihçe modal/sayfası açılmalı!");
        System.out.println("✅ Rebate Invoice Pool Tarihçe modal/sayfası açıldığı doğrulandı");
    }
    
    @Then("verify rebate invoice pool history columns are displayed")
    public void verifyHistoryColumnsAreDisplayed() {
        boolean columnsDisplayed = rebateInvoicePoolPage.verifyHistoryColumnsAreDisplayed();
        Assertions.assertTrue(columnsDisplayed, "Tarihçe kolonları (Önceki Durum, Yeni Durum, Açıklama, Kullanıcı, Yaratılma Tarihi) görünür olmalı!");
        System.out.println("✅ Rebate Invoice Pool Tarihçe kolonları görünür olduğu doğrulandı");
    }
}
