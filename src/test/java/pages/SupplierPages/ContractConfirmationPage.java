package pages.SupplierPages;

import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.WaitForSelectorState;
import pages.commonPages.BasePage;


public class ContractConfirmationPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator firmCodeInput = page.locator("#FilterFirmCode");
    Locator searchButton = page.locator("#FilterButtonId");
    Locator editLinks = page.locator(".gridCmdBtn.cmdLink.ContractWaitingForApprovalGridCmd");
    Locator approveButton = page.locator("div.form-group.FormButton > button#ContractApprove");

    public void verifyContractConfirmationPageIsDisplayed() {
        verifyTextElementUseTrim("Sözleşme Onay İşlemleri", pageTitle);

    }

    /**
     * Açıklama alanını doldurur.
     */
    public void fillFirmCode(){
        firmCodeInput.fill("1703-FNR");
    }

    /**
     * Sorgula butonuna tıklar.
     */
    public void clicktoSearchButton(){
        searchButton.click();
    }

    public void clicktoEdit(){
        editLinks.first().click();
    }

    public void verifyContractApproveButtonIsVisible(){
        approveButton.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE).setTimeout(5000));
        assert approveButton.isVisible();
    }
}
