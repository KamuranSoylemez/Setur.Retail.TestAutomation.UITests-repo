package stepDefs;

import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.SupplierPages.BrandAmbassadorConditionPage;
import pages.SupplierPages.ContractUpdatePage;

public class BrandAmbassadorConditionStepDefs {
    private ContractUpdatePage contractUpdatePage = new ContractUpdatePage();
    private BrandAmbassadorConditionPage brandAmbassadorConditionPage = new BrandAmbassadorConditionPage();

    @When("click to brand ambassador condition tab")
    public void clickToBrandAmbassadorConditionTab() {
        contractUpdatePage.clickBrandAmbassadorConditionsTab();
        System.out.println("✅ Temsilci Kondisyon tab'ına tıklandı");
    }

    @When("click to new brand ambassador condition button")
    public void clickToNewBrandAmbassadorConditionButton() {
        contractUpdatePage.clickToNewBrandAmbassadorConditionButton();
        System.out.println("✅ Yeni Temsilci Kondisyon butonuna tıklandı");
    }

    @Then("verify brand ambassador condition form is displayed")
    public void verifyBrandAmbassadorConditionFormIsDisplayed() {
        brandAmbassadorConditionPage.verifyFormIsDisplayed();
    }

    @When("select brand ambassador condition type {string}")
    public void selectBrandAmbassadorConditionType(String conditionType) {
        brandAmbassadorConditionPage.selectConditionType(conditionType);
    }

    @When("select brand ambassador calculation type {string}")
    public void selectBrandAmbassadorCalculationType(String calculationType) {
        brandAmbassadorConditionPage.selectCalculationType(calculationType);
    }

    @Then("verify brand ambassador field {string} is disabled")
    public void verifyBrandAmbassadorFieldIsDisabled(String fieldName) {
        boolean isDisabled = brandAmbassadorConditionPage.verifyFieldIsDisabled(fieldName);
        if (!isDisabled) {
            throw new AssertionError("❌ Field '" + fieldName + "' disabled olmalıydı ama değil!");
        }
        System.out.println("✅ Field '" + fieldName + "' disabled (girilemez)");
    }

    @Then("verify brand ambassador field {string} is mandatory")
    public void verifyBrandAmbassadorFieldIsMandatory(String fieldName) {
        boolean isMandatory = brandAmbassadorConditionPage.verifyFieldIsMandatory(fieldName);
        if (!isMandatory) {
            throw new AssertionError("❌ Field '" + fieldName + "' mandatory olmalıydı ama değil!");
        }
        System.out.println("✅ Field '" + fieldName + "' mandatory (zorunlu)");
    }

    @Then("verify brand ambassador field {string} is optional")
    public void verifyBrandAmbassadorFieldIsOptional(String fieldName) {
        boolean isOptional = brandAmbassadorConditionPage.verifyFieldIsOptional(fieldName);
        if (!isOptional) {
            throw new AssertionError("❌ Field '" + fieldName + "' optional olmalıydı ama değil!");
        }
        System.out.println("✅ Field '" + fieldName + "' optional (isteğe bağlı)");
    }
}
