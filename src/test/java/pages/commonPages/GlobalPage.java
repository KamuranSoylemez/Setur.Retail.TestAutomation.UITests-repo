package pages.commonPages;

import stepDefs.DistributionStepdefs;
import stepDefs.PurchaseOrderSearchStepdefs;
import stepDefs.PurchaseOrderStepdefs;
import stepDefs.WorkflowInboxStepdefs;
import utils.ConfigDataReader;

public class GlobalPage extends BasePage {

    WorkflowInboxStepdefs workflowInboxStepdefs = new WorkflowInboxStepdefs();
    PurchaseOrderStepdefs orderStepdefs = new PurchaseOrderStepdefs();
    PurchaseOrderSearchStepdefs searchStepdefs = new PurchaseOrderSearchStepdefs();
    DistributionStepdefs distributionStepdefs = new DistributionStepdefs();

    /**
     * https://dfs-retail-ui-staging.azurewebsites.net/CustomerManagement/Login sayfasını açar
     */
    public void navigateToHomePage() {
        page.navigate(ConfigDataReader.getConfig("baseUrl"));
    }

    /**
     * diğer işlemler için sipariş hazırlama kısmı.
     * @param category sipariş hazırlanırken kategory değerini alır.
     */
    public void orderCompletion(String category, String region) {
        workflowInboxStepdefs.clickCreateOrderLink();
        orderStepdefs.verifyCreateOrderPage();
        orderStepdefs.fillOrderCreationDate();
        orderStepdefs.fillOrderName();
        orderStepdefs.selectCategoryFromList(category);
        orderStepdefs.distributorCompanySelection(category);
        orderStepdefs.selectCompanyContactPerson();
        orderStepdefs.selectDistributionTargetType();
        orderStepdefs.selectEntryWarehouse(region);
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

    public void createDistributionAndTransportation(String region, String productCode) {
        workflowInboxStepdefs.clickCreateDistributionLink();
        distributionStepdefs.verifyCreateDistributionPageIsDisplayed();
        distributionStepdefs.fillDistributionFormWithValidData(region);
        distributionStepdefs.clickSaveButton();
        distributionStepdefs.verifyDistributionIsCreatedSuccessfully();
        distributionStepdefs.addProductToDistribution(productCode);
        distributionStepdefs.verifyProductsAddedToDistribution();
        distributionStepdefs.distributionDetailSelection();
        distributionStepdefs.verifyDistributedProducts();
        distributionStepdefs.sendToTransportation();
    }
}
