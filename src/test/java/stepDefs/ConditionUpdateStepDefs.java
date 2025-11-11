package stepDefs;

import io.cucumber.java.en.*;
import pages.SupplierPages.ConditionUpdatePage;
import pages.SupplierPages.ContractUpdatePage;

public class ConditionUpdateStepDefs {
    
    ConditionUpdatePage conditionUpdatePage = new ConditionUpdatePage();
    ContractUpdatePage contractUpdatePage = new ContractUpdatePage();
    
    @Then("click general condition tab")
    public void clickGeneralConditionTab() {
        contractUpdatePage.clickGeneralConditionTab();
        System.out.println("✅ Genel Kondisyon tab'ına tıklandı");
    }
    
    @Then("verify general condition detail is displayed with status {string}")
    public void verifyGeneralConditionDetailIsDisplayedWithStatus(String status) {
        conditionUpdatePage.verifyGeneralConditionDetailWithStatus(status);
    }
    
    @Then("open general condition detail with id {string} and status {string}")
    public void openGeneralConditionDetailWithIdAndStatus(String conditionId, String status) {
        conditionUpdatePage.openGeneralConditionDetailWithIdAndStatus(conditionId, status);
        System.out.println("✅ ID=" + conditionId + " ve durum='" + status + "' olan genel kondisyon detayı açıldı");
    }
    
    @Then("verify update button is visible on condition detail")
    public void verifyUpdateButtonIsVisibleOnConditionDetail() {
        conditionUpdatePage.verifyUpdateButtonIsVisible();
    }
    
    @Then("click settings button for condition with status {string}")
    public void clickSettingsButtonForConditionWithStatus(String status) {
        conditionUpdatePage.clickSettingsButtonForConditionWithStatus(status);
        System.out.println("✅ Durum='" + status + "' olan kondisyon için ayar butonuna tıklandı");
    }
    
    @Then("verify update button is visible in settings menu")
    public void verifyUpdateButtonIsVisibleInSettingsMenu() {
        conditionUpdatePage.verifyUpdateButtonIsVisibleInSettingsMenu();
    }
    
    @Then("verify update button is visible for condition with status {string}")
    public void verifyUpdateButtonIsVisibleForConditionWithStatus(String status) {
        conditionUpdatePage.verifyUpdateButtonIsVisibleForConditionWithStatus(status);
    }
    
    @Then("verify history button is visible for condition with status {string}")
    public void verifyHistoryButtonIsVisibleForConditionWithStatus(String status) {
        conditionUpdatePage.verifyHistoryButtonIsVisibleForConditionWithStatus(status);
    }
    
    @Then("click update button for condition with status {string}")
    public void clickUpdateButtonForConditionWithStatus(String status) {
        conditionUpdatePage.clickUpdateButtonForConditionWithStatus(status);
        System.out.println("✅ Durum='" + status + "' olan kondisyon için yeşil Edit butonuna tıklandı");
    }
    
    @Then("verify condition detail popup is displayed")
    public void verifyConditionDetailPopupIsDisplayed() {
        conditionUpdatePage.verifyConditionDetailPopupIsDisplayed();
    }
    
    @When("click update button on condition detail popup")
    public void clickUpdateButtonOnConditionDetailPopup() {
        conditionUpdatePage.clickUpdateButtonOnConditionDetailPopup();
    }
    
    @Then("verify condition update popup is displayed")
    public void verifyConditionUpdatePopupIsDisplayed() {
        conditionUpdatePage.verifyConditionUpdatePopupIsDisplayed();
    }
    
    @When("click update button on condition update popup")
    public void clickUpdateButtonOnConditionUpdatePopup() {
        conditionUpdatePage.clickUpdateButtonOnConditionUpdatePopup();
    }
    
    @Then("verify final update popup is displayed")
    public void verifyFinalUpdatePopupIsDisplayed() {
        conditionUpdatePage.verifyFinalUpdatePopupIsDisplayed();
    }
    
    @When("click save button on final update popup without filling required fields")
    public void clickSaveButtonOnFinalUpdatePopupWithoutFillingRequiredFields() {
        conditionUpdatePage.clickSaveButtonOnFinalUpdatePopupWithoutFillingRequiredFields();
    }
    
    @When("click save button on condition update popup without filling required fields")
    public void clickSaveButtonOnConditionUpdatePopupWithoutFillingRequiredFields() {
        conditionUpdatePage.clickSaveButtonOnConditionUpdatePopupWithoutFillingRequiredFields();
    }
    
    @Then("verify update type field is mandatory")
    public void verifyUpdateTypeFieldIsMandatory() {
        conditionUpdatePage.verifyUpdateTypeFieldIsMandatory();
    }
    
    @Then("verify description field is mandatory")
    public void verifyDescriptionFieldIsMandatory() {
        conditionUpdatePage.verifyDescriptionFieldIsMandatory();
    }
    
    @Then("verify error message {string} is displayed")
    public void verifyErrorMessageIsDisplayed(String errorMessage) {
        conditionUpdatePage.verifyErrorMessageIsDisplayed(errorMessage);
    }
    
    @When("select update type {string} on final update popup")
    public void selectUpdateTypeOnFinalUpdatePopup(String updateType) {
        conditionUpdatePage.selectUpdateTypeOnFinalUpdatePopup(updateType);
    }
    
    @When("enter description {string} on final update popup")
    public void enterDescriptionOnFinalUpdatePopup(String description) {
        conditionUpdatePage.enterDescriptionOnFinalUpdatePopup(description);
    }
    
    @When("click save button on final update popup")
    public void clickSaveButtonOnFinalUpdatePopup() {
        conditionUpdatePage.clickSaveButtonOnFinalUpdatePopup();
    }
    
    @Then("verify condition definition page is displayed")
    public void verifyConditionDefinitionPageIsDisplayed() {
        conditionUpdatePage.verifyConditionDefinitionPageIsDisplayed();
    }
    
    @When("click approve button on condition update popup")
    public void clickApproveButtonOnConditionUpdatePopup() {
        conditionUpdatePage.clickApproveButtonOnConditionUpdatePopup();
    }
    
    @Then("verify approval popup opened")
    public void verifyApprovalPopupOpened() {
        conditionUpdatePage.verifyApprovalPopupOpened();
    }
    
    @When("click approve button on approval popup")
    public void clickApproveButtonOnApprovalPopup() {
        conditionUpdatePage.clickApproveButtonOnApprovalPopup();
    }
    
    @When("click to enter from keyboard")
    public void clickToEnterFromKeyboard() {
        conditionUpdatePage.pressEnterKey();
    }
    
    @Then("verify condition status is {string} for the approved condition")
    public void verifyConditionStatusForTheApprovedCondition(String expectedStatus) {
        conditionUpdatePage.verifyConditionStatus(expectedStatus);
    }
    
}
