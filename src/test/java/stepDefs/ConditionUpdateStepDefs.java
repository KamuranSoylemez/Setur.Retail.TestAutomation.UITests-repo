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
    
}
