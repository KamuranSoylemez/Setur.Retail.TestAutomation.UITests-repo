package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.purchasePages.PurchaseInvoiceTransactionsPage;

public class PurchaseInvoiceTransactionsStepdefs {

    PurchaseInvoiceTransactionsPage invoiceTransactionsPage = new PurchaseInvoiceTransactionsPage();

    @Then("verify purchase invoice transaction page")
    public void verifyPurchaseInvoiceTransactionPage() {
        invoiceTransactionsPage.verifyPurchaseInvoiceTransactionPage();
    }

    @When("search by invoice number")
    public void searchByInvoiceNumber() {
        invoiceTransactionsPage.fillInvoiceNo();
        invoiceTransactionsPage.searchForInvoiceNo();
        invoiceTransactionsPage.verifyInvoiceNo();
    }

    @And("open invoice update frame")
    public void openInvoiceUpdateFrame() {
        invoiceTransactionsPage.clickCheckboxForDeclaration();
        invoiceTransactionsPage.createDeclaration();
    }

    @And("completing counting process")
    public void completeCountingProcess() {
        invoiceTransactionsPage.selectCountingTab();
        invoiceTransactionsPage.creatingCount();
        invoiceTransactionsPage.fillDescriptionField();
        invoiceTransactionsPage.saveCountDescription();
    }

    @And("edit counting process")
    public void editCountingProcess() {
        invoiceTransactionsPage.editCountingProcess();
        invoiceTransactionsPage.sendForCount();
        invoiceTransactionsPage.copyRequestToApprover();
        invoiceTransactionsPage.copyingProcess();
        invoiceTransactionsPage.completingCount();
        invoiceTransactionsPage.saveUpdateCount();
    }

    @And("exclude out of shipping and save")
    public void excludeShippingAndSave() {
        invoiceTransactionsPage.checkExcludeShipping();
        invoiceTransactionsPage.saveDeclarationUpdate();
    }

    @And("put in stock process")
    public void putInStockProcess() {
        invoiceTransactionsPage.openUpdateDeclarationFrame();
        invoiceTransactionsPage.openPutInStockFrame();
        invoiceTransactionsPage.fillCustomsDeclarationNoField();
        invoiceTransactionsPage.selectCustomsDate();
        invoiceTransactionsPage.fillDormitoryEntryNo();
        invoiceTransactionsPage.selectDomesticEntryDate();
        invoiceTransactionsPage.selectRegimeNo();
        invoiceTransactionsPage.savePutInStockProcess();
        invoiceTransactionsPage.saveDeclarationUpdate();
    }

    @Then("complete order process")
    public void completeOrderProcess() {
        invoiceTransactionsPage.openOrderProcessFrame();
        invoiceTransactionsPage.completeOrder();
        invoiceTransactionsPage.closeOrderProcessFrame();
    }
}
