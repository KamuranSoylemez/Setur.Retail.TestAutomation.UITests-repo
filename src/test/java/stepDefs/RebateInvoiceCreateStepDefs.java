package stepDefs;

import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.junit.jupiter.api.Assertions;
import pages.SupplierPages.ReceivablePoolPage;

public class RebateInvoiceCreateStepDefs {
    
    ReceivablePoolPage receivablePoolPage = new ReceivablePoolPage();
    
    // ============ RECEIVABLE POOL PAGE STEPS ============
    
    @When("fill receivable pool calculation date {string}")
    public void fillReceivablePoolCalculationDate(String date) {
        receivablePoolPage.fillCalculationDate(date);
    }
    
    @When("fill receivable pool contract name {string}")
    public void fillReceivablePoolContractName(String contractName) {
        receivablePoolPage.fillContractName(contractName);
    }
    
    @When("click receivable pool search button")
    public void clickReceivablePoolSearchButton() {
        receivablePoolPage.clickSearchButton();
    }
    
    @Then("verify receivable pool page is displayed")
    public void verifyReceivablePoolPageIsDisplayed() {
        boolean isVisible = receivablePoolPage.verifySearchFormVisible();
        Assertions.assertTrue(isVisible, "Alacak Havuzu sayfası görünür olmalı!");
        System.out.println("✅ Alacak Havuzu sayfası görüntülendi");
    }
    
    // ============ CHECKBOX AND BUTTON STEPS ============
    
    @When("click first row checkbox in receivable pool")
    public void clickFirstRowCheckboxInReceivablePool() {
        receivablePoolPage.clickFirstRowCheckbox();
    }
    
    @When("click create rebate invoice button")
    public void clickCreateRebateInvoiceButton() {
        receivablePoolPage.clickCreateRebateInvoiceButtonWithoutSelection();
    }
    
    // ============ CREATE REBATE INVOICE FRAME STEPS ============
    
    @Then("verify create rebate invoice frame is opened")
    public void verifyCreateRebateInvoiceFrameIsOpened() {
        boolean isOpened = receivablePoolPage.verifyCreateRebateInvoiceFrameOpened();
        Assertions.assertTrue(isOpened, "'Rebate Faturası Oluştur' frame'i açılmalı!");
        System.out.println("✅ 'Rebate Faturası Oluştur' frame'i açıldığı doğrulandı");
    }
    
    @When("fill description in rebate invoice frame {string}")
    public void fillDescriptionInRebateInvoiceFrame(String description) {
        receivablePoolPage.fillDescriptionInFrame(description);
    }
    
    @When("click save button in rebate invoice frame")
    public void clickSaveButtonInRebateInvoiceFrame() {
        receivablePoolPage.clickSaveButtonInFrame();
    }
    
    // ============ INVOICE NUMBER LINK STEPS ============
    
    @When("click invoice number link in receivable pool")
    public void clickInvoiceNumberLinkInReceivablePool() {
        receivablePoolPage.clickInvoiceNumberLink();
    }
    
    // ============ UPDATE REBATE INVOICE FRAME STEPS ============
    
    @Then("verify update rebate invoice frame is opened")
    public void verifyUpdateRebateInvoiceFrameIsOpened() {
        boolean isOpened = receivablePoolPage.verifyUpdateRebateInvoiceFrameOpened();
        Assertions.assertTrue(isOpened, "'Rebate Fatura Güncelleme' frame'i açılmalı!");
        System.out.println("✅ 'Rebate Fatura Güncelleme' frame'i açıldığı doğrulandı");
    }
    
    @When("click reverse button in rebate invoice frame")
    public void clickReverseButtonInRebateInvoiceFrame() {
        receivablePoolPage.clickReverseButton();
    }
    
    // ============ REVERSE REASON POPUP STEPS ============
    
    @Then("verify reverse reason popup is displayed")
    public void verifyReverseReasonPopupIsDisplayed() {
        boolean isDisplayed = receivablePoolPage.verifyReverseReasonPopupDisplayed();
        Assertions.assertTrue(isDisplayed, "Geri çekme nedeni pop-up'ı açılmalı!");
        System.out.println("✅ Geri çekme nedeni pop-up'ı açıldığı doğrulandı");
    }
    
    @When("fill reverse reason in popup {string}")
    public void fillReverseReasonInPopup(String reason) {
        receivablePoolPage.fillReverseReasonInPopup(reason);
    }
    
    @When("click confirm button in reverse popup")
    public void clickConfirmButtonInReversePopup() {
        receivablePoolPage.clickConfirmButtonInPopup();
    }
}
