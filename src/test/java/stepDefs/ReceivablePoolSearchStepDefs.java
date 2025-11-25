package stepDefs;

import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.junit.jupiter.api.Assertions;
import pages.SupplierPages.ReceivablePoolPage;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

public class ReceivablePoolSearchStepDefs {
    
    ReceivablePoolPage receivablePoolPage = new ReceivablePoolPage();
    
    @Then("verify search form is visible")
    public void verifySearchFormIsVisible() {
        boolean isVisible = receivablePoolPage.verifySearchFormVisible();
        Assertions.assertTrue(isVisible, "Arama formu görünür olmalı!");
        System.out.println("✅ Arama formu görünür olduğu doğrulandı");
    }
    
    @When("user clicks search button")
    public void userClicksSearchButton() {
        receivablePoolPage.clickSearchButton();
    }
    
    // Firma
    @When("user selects company {string}")
    public void userSelectsCompany(String companyName) {
        receivablePoolPage.selectCompany(companyName);
    }
    
    // Rebate Tarihi
    @When("user fills rebate date with {string}")
    public void userFillsRebateDateWith(String rebateDate) {
        receivablePoolPage.fillRebateDate(rebateDate);
    }
    
    // Kondisyon Tipi
    @When("user selects condition type {string}")
    public void userSelectsConditionType(String conditionType) {
        receivablePoolPage.selectConditionType(conditionType);
    }
    
    // Hesaplama Türü
    @When("user selects calculation type {string}")
    public void userSelectsCalculationType(String calculationType) {
        receivablePoolPage.selectCalculationType(calculationType);
    }
    
    // Durum
    @When("user selects status {string}")
    public void userSelectsStatus(String status) {
        receivablePoolPage.selectStatus(status);
    }
    
    // Para Birimi
    @When("user selects currency {string}")
    public void userSelectsCurrency(String currency) {
        receivablePoolPage.selectCurrency(currency);
    }
    
    // Hesaplama Periyodu
    @When("user selects calculation period {string}")
    public void userSelectsCalculationPeriod(String calculationPeriod) {
        receivablePoolPage.selectCalculationPeriod(calculationPeriod);
    }
    
    // Sözleşme Adı
    @When("user fills contract name with {string}")
    public void userFillsContractNameWith(String contractName) {
        receivablePoolPage.fillContractName(contractName);
    }
    
    // Kategori
    @When("user selects category {string}")
    public void userSelectsCategory(String category) {
        receivablePoolPage.selectCategory(category);
    }
    
    // Açıklama
    @When("user fills description with {string}")
    public void userFillsDescriptionWith(String description) {
        receivablePoolPage.fillDescription(description);
    }
    
    // Assertions
    @Then("verify grid has results")
    public void verifyGridHasResults() {
        boolean hasResults = receivablePoolPage.verifyGridHasResults();
        Assertions.assertTrue(hasResults, "Grid'de sonuç olmalı ama bulunamadı!");
        System.out.println("✅ Grid'de sonuç olduğu doğrulandı");
    }
    
    @Then("verify grid has results or no records message")
    public void verifyGridHasResultsOrNoRecordsMessage() {
        boolean hasResults = receivablePoolPage.verifyGridHasResults();
        boolean noRecordsMessageDisplayed = receivablePoolPage.verifyNoRecordsMessageDisplayed();
        
        System.out.println("🔍 Grid'de sonuç var mı: " + hasResults);
        System.out.println("🔍 'Kayıt bulunamadı' mesajı görünür mü: " + noRecordsMessageDisplayed);
        
        assertTrue(hasResults || noRecordsMessageDisplayed, 
            "Grid'de ya sonuç olmalı ya da 'Kayıt bulunamadı' mesajı görünmeli!");
    }

    @Then("verify grid has no results")
    public void verifyGridHasNoResults() {
        int rowCount = receivablePoolPage.getGridRowCount();
        System.out.println("📊 Grid satır sayısı: " + rowCount);
        
        assertEquals(0, rowCount, "Grid boş olmalı ama " + rowCount + " satır bulundu!");
        System.out.println("✅ Grid boş olduğu doğrulandı");
    }
    
    @Then("verify no records message is displayed")
    public void verifyNoRecordsMessageIsDisplayed() {
        boolean isDisplayed = receivablePoolPage.verifyNoRecordsMessageDisplayed();
        Assertions.assertTrue(isDisplayed, "'Kayıt bulunamadı' mesajı görünmeli ama görünmüyor!");
        System.out.println("✅ 'Kayıt bulunamadı' mesajı görüntülendiği doğrulandı");
    }
    
    @Then("verify all columns are sortable")
    public void verifyAllColumnsAreSortable() {
        boolean allSortable = receivablePoolPage.verifyAllColumnsAreSortable();
        Assertions.assertTrue(allSortable, "Tüm kolonlarda sort çalışmalı!");
        System.out.println("✅ Tüm kolonlarda sort çalıştığı doğrulandı");
    }
    
    @Then("verify pagination is working")
    public void verifyPaginationIsWorking() {
        boolean paginationWorks = receivablePoolPage.verifyPaginationIsWorking();
        Assertions.assertTrue(paginationWorks, "Pagination çalışmalı!");
        System.out.println("✅ Pagination çalıştığı doğrulandı");
    }
    
    @When("user clicks history icon on first row")
    public void userClicksHistoryIconOnFirstRow() {
        receivablePoolPage.clickHistoryIconOnFirstRow();
    }
    
    @Then("verify history page is opened")
    public void verifyHistoryPageIsOpened() {
        boolean isOpened = receivablePoolPage.verifyHistoryPageOpened();
        Assertions.assertTrue(isOpened, "Tarihçe sayfası açılmalı!");
        System.out.println("✅ Tarihçe sayfası açıldığı doğrulandı");
    }
    
    @Then("verify history description contains condition ids and explanation")
    public void verifyHistoryDescriptionContainsConditionIdsAndExplanation() {
        boolean hasValidContent = receivablePoolPage.verifyHistoryDescriptionContent();
        Assertions.assertTrue(hasValidContent, "Tarihçe açıklaması kondisyon ID ve açıklama içermeli!");
        System.out.println("✅ Tarihçe açıklaması geçerli içerik içerdiği doğrulandı");
    }
    
    // ============ INVOICE CREATION STEPS (T10-T14) ============
    
    @When("user clicks create rebate invoice button without selection")
    public void userClicksCreateRebateInvoiceButtonWithoutSelection() {
        receivablePoolPage.clickCreateRebateInvoiceButtonWithoutSelection();
    }
    
    @Then("verify warning message {string} is displayed")
    public void verifyWarningMessageIsDisplayed(String expectedMessage) {
        boolean isDisplayed = receivablePoolPage.verifyWarningMessage(expectedMessage);
        Assertions.assertTrue(isDisplayed, "Uyarı mesajı '" + expectedMessage + "' görünmeli!");
        System.out.println("✅ Uyarı mesajı doğrulandı: " + expectedMessage);
    }
}
