package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.purchasePages.CreditNotePage;
import java.time.LocalDate;
import java.util.List;
import java.util.Map;

public class CreditNoteStepDefs {

    CreditNotePage creditNotePage = new CreditNotePage();


    @Then("verify credit note page is displayed {string}")
    public void verifyCreditNotePageIsDisplayed(String pageTitle) {
        creditNotePage.verifyCreditNotePageIsDisplayed(pageTitle);
    }


    @And("fill out the form to search credit notes with different status")
    public void fillOutTheFormToSearchCreditNotes(io.cucumber.datatable.DataTable dataTable) {
        List<Map<String, String>> rows = dataTable.asMaps(String.class, String.class);
        for (Map<String, String> row : rows) {
            String status = row.get("status");

            creditNotePage.openCreditNoteStatusList();
            creditNotePage.selectCreditNoteStatusFromList(status);
            creditNotePage.clickToFilterButton();
        }
    }


    @And("click add new credit note button")
    public void createNewCreditNote() {
        creditNotePage.clickToNewCreditNoteButton();
    }

    @And("fill the form with document no {string} and document date {string} and firm code {string} and purchase order {string} and isBroken {string}")
    public void fillTheFormToSearchCreditNote(String documentNo, String documentDate, String firmCode, String purchaseOrder, String isBroken) {
        creditNotePage.fillTheFormByDocumentInfo(documentNo, documentDate);
        creditNotePage.isBrokenRadioButton(isBroken);
        creditNotePage.searchByFirmCode(firmCode);
        creditNotePage.searchByPurchaseOrder(purchaseOrder);
        creditNotePage.clickToFilterButton();
    }

    @And("sort the credit note list by all available columns")
    public void sortingTheCreditNoteListByAllAvailableColumns() {
        creditNotePage.testAllColumnSorting();
    }


    @And("create credit note by document no {string} and purchase order {string} and isBroken {string} and description {string}")
    public void fillTheFormToCreteCreditNoteForIsBrokenNo(String documentNo, String purchaseOrder, String isBroken, String description) {
        creditNotePage.fillDescriptionInPopup(description);
        creditNotePage.fillDocumentDateInPopup(LocalDate.now().format(java.time.format.DateTimeFormatter.ofPattern("dd.MM.yyyy")));
        creditNotePage.fillDocumentNoInPopup(documentNo);
        creditNotePage.fillPurchaseOrder(purchaseOrder);

    }

    @And ("click save button in credit note popup")
    public void clickSaveButtonInCreditNotePopup() {
        creditNotePage.setPopupSaveButton();
    }

    @Then("fill the form with document no {string} and firm code {string} and purchase order {string} and isBroken {string}")
    public void fillTheFormWithoutDocumentDate(String documentNo, String firmCode, String purchaseOrder, String isBroken) {
        creditNotePage.fillDocumentNoInFilter(documentNo);
        creditNotePage.isBrokenRadioButton(isBroken);
        creditNotePage.searchByFirmCode(firmCode);
        creditNotePage.searchByPurchaseOrder(purchaseOrder);
        creditNotePage.clickToFilterButton();
    }

    @Then("fill the form with document no {string} and firm code {string} and isBroken {string}")
    public void fillTheFormWithDocumentNoAndFirmCode(String documentNo, String firmCode, String isBroken) {
        creditNotePage.fillDocumentNoInFilter(documentNo);
        creditNotePage.isBrokenRadioButton(isBroken);
        creditNotePage.searchByFirmCode(firmCode);
        creditNotePage.clickToFilterButton();
    }

    @Then("edit first one from the credit note list and add product with invoiceNo {string} and productCode {string} and quantity {string} and profitCenter {string} and creditNoteType {string}")
    public void editFirstCreditNoteAndAddProduct(String invoiceNo, String productCode, String quantity, String profitCenter, String creditNoteType) {
        creditNotePage.clickEditButtonOnFirstRow();
        creditNotePage.addProductInDetailPage(invoiceNo, productCode, quantity, profitCenter, creditNoteType);
    }

    @Then("click save button in credit note detail page")
    public void clickSaveButtonInDetailPage() {
        creditNotePage.clickSaveButtonInDetailPage();
    }

    @Then("edit first one from the credit note list")
    public void editFirstCreditNote() {
        creditNotePage.clickEditButtonOnFirstRow();
    }

    @Then("click delete icon on first product row")
    public void clickDeleteIconOnFirstProductRow() {
        creditNotePage.clickDeleteIconOnProductRow(0);
    }

    @Then("confirm delete operation")
    public void confirmDeleteOperation() {
        creditNotePage.confirmDelete();
    }

    @Then("verify product was deleted")
    public void verifyProductWasDeleted() {
        boolean isDeleted = creditNotePage.verifyProductRowDeleted(0);
        if (!isDeleted) {
            throw new AssertionError("Ürün silinemedi!");
        }
    }

}