package pages.commonPages;

import pages.purchasePages.PurchaseOrderPage;
import pages.purchasePages.PurchaseOrderSearchPage;
import pages.purchasePages.WorkflowInboxPage;
import utils.ConfigDataReader;

public class GlobalPage extends BasePage {

    WorkflowInboxPage workflowInboxPage = new WorkflowInboxPage();
    PurchaseOrderPage purchaseOrderPage = new PurchaseOrderPage();
    PurchaseOrderSearchPage orderSearchPage = new PurchaseOrderSearchPage();

    /**
     * https://dfs-retail-ui-staging.azurewebsites.net/CustomerManagement/Login sayfasını açar
     */
    public void navigateToHomePage() {
        page.navigate(ConfigDataReader.getConfig("baseUrl"));
    }

    public void orderPlacedStatus(String category) {
        workflowInboxPage.clickCreateOrderLink();
        purchaseOrderPage.verifyCreateOrderPage();
        purchaseOrderPage.fillOrderCreationDate();
        purchaseOrderPage.fillOrderNameOrderCreationPage();
        purchaseOrderPage.selectCategoryFromList(category);
        purchaseOrderPage.openCompanyIdentificationFrame();
        purchaseOrderPage.selectCompanyContactPerson();
        purchaseOrderPage.selectDistributionTargetType();
        purchaseOrderPage.openWarehouseDefinitionFrame();
        purchaseOrderPage.selectInvoiceAddress();
        purchaseOrderPage.selectDeliveryAddress();
        purchaseOrderPage.checkOrderCompleteAutomatically();
        purchaseOrderPage.saveOrder();
        purchaseOrderPage.openOrderProductDescriptionFrame();
        purchaseOrderPage.verifyProducts();
        purchaseOrderPage.sendingForApprovalProcess();
        purchaseOrderPage.approveOrder();
        purchaseOrderPage.setOrderPlaced();
        purchaseOrderPage.verifyOrderByOrderId();

    }

    public void setProformaAndInvoice() {
        workflowInboxPage.clickPurchaseOrderSearch();
        orderSearchPage.verifyPurchaseOrderSearchPage();
        orderSearchPage.searchOrderIdAndEditOder();
        orderSearchPage.addProformaToOrder();
        orderSearchPage.addInfoForProformaAndSave();
        orderSearchPage.copyOrderItemsAndApproveProforma();
        orderSearchPage.addOrderInvoices();
        orderSearchPage.addInfoForInvoiceAndSave();
        orderSearchPage.copyProformaItemsAndApproveInvoice();
        orderSearchPage.invoiceCompletionAndApproval();
    }
}
