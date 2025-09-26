package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import pages.purchasePages.CreditNotePage;

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


    @And("create new credit note")
    public void createNewCreditNote() {
        creditNotePage.clickToNewCreditNoteButton();
//TODO ekran değişiklikleri sonrası devam edecek
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
}