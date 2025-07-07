package pages.commonPages;

import stepDefs.PurchaseOrderSearchStepdefs;
import stepDefs.PurchaseOrderStepdefs;
import stepDefs.WorkflowInboxStepdefs;
import utils.ConfigDataReader;

public class GlobalPage extends BasePage {

    WorkflowInboxStepdefs workflowInboxStepdefs = new WorkflowInboxStepdefs();
    PurchaseOrderStepdefs orderStepdefs = new PurchaseOrderStepdefs();
    PurchaseOrderSearchStepdefs searchStepdefs = new PurchaseOrderSearchStepdefs();

    /**
     * https://dfs-retail-ui-staging.azurewebsites.net/CustomerManagement/ Login sayfasını açar
     */
    public void navigateToHomePage() {
        page.navigate(ConfigDataReader.getConfig("baseUrl"));
    }

    /**
     * diğer işlemler için sipariş hazırlama kısmı.
     * @param category sipariş hazırlanırken kategory değerini alır.
     */
    public void orderPlacedStatus(String category) {
        workflowInboxStepdefs.clickCreateOrderLink();
        orderStepdefs.verifyCreateOrderPage();
        orderStepdefs.fillOrderCreationDate();
        orderStepdefs.fillOrderName();
        orderStepdefs.selectCategoryFromList(category);
        orderStepdefs.distributorCompanySelection(category);
        orderStepdefs.selectCompanyContactPerson();
        orderStepdefs.selectDistributionTargetType();
        orderStepdefs.selectEntryWarehouse();
        orderStepdefs.selectInvoiceAddress();
        orderStepdefs.selectDeliveryAddress();
        orderStepdefs.checkOrderCompleteAutomatically();
        orderStepdefs.saveOrder();
        orderStepdefs.addProductToOrder();
        orderStepdefs.verifyProducts();
        orderStepdefs.sendingForApprovalProcess();
        orderStepdefs.approveOrder();
        orderStepdefs.setOrderPlaced();
        orderStepdefs.verifyOrderByOrderId();

    }

    /**
     * sipariş içindeki proforma ve fatura hazırlar
     */
    public void setProformaAndInvoice() {
        workflowInboxStepdefs.clickPurchasingDropdownToggle();
        workflowInboxStepdefs.clickPurchaseOrderSearch();
        searchStepdefs.verifyPurchaseOrderSearchPage();
        searchStepdefs.searchOrderByOrderIdAndEditOder();
        searchStepdefs.openProformaTab();
        searchStepdefs.addInfoForProformaAndSave();
        searchStepdefs.copyOrderItemsAndApproveProforma();
        searchStepdefs.addOrderInvoices();
        searchStepdefs.addInfoForInvoiceAndSave();
        searchStepdefs.copyOrderItemsAndApproveInvoice();
        searchStepdefs.invoiceCompletionAndApproval();
    }
}
