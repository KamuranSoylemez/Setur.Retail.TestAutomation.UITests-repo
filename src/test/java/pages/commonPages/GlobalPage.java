package pages.commonPages;

import pages.purchasePages.PurchaseOrderPage;
import pages.purchasePages.WorkflowInboxPage;
import utils.ConfigDataReader;

public class GlobalPage extends BasePage {

    LoginPage loginPage = new LoginPage();
    WorkflowInboxPage workflowInboxPage = new WorkflowInboxPage();
    PurchaseOrderPage purchaseOrderPage = new PurchaseOrderPage();

    public void navigateToHomePage() {
        //page.navigate("https://dfs-retail-ui-staging.azurewebsites.net/CustomerManagement/Login");
        page.navigate(ConfigDataReader.getConfig("baseUrl"));
    }

    public void orderPlacedStatus(String category) {
        navigateToHomePage();
        loginPage.fillUserNameAndPassword();
        loginPage.clickLoginButton();
        workflowInboxPage.verifySuccessfulLogin();
        workflowInboxPage.clickPurchaseDropdownToggle();
        workflowInboxPage.clickOrderLink();
        purchaseOrderPage.verifyPurchaseOrderPage();
        purchaseOrderPage.fillOrderDate();
        purchaseOrderPage.selectCategoryFromList(category);
        purchaseOrderPage.setDistributorCompany();
        purchaseOrderPage.selectFirmResponsibleUser();
        purchaseOrderPage.selectDistributionTargetType();
        purchaseOrderPage.selectEntryWarehouse();
        purchaseOrderPage.selectCompanyAddress();
        purchaseOrderPage.selectWarehouseAddress();
        purchaseOrderPage.checkCanAutoCompleteAndSave();
        purchaseOrderPage.addProductToOrder();
        purchaseOrderPage.verifyProducts();
        purchaseOrderPage.sendingForApprovalProcess();
        purchaseOrderPage.approveOrder();
        purchaseOrderPage.setOrderPlaced();

    }
}
