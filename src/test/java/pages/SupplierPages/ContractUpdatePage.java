package pages.SupplierPages;
import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import com.microsoft.playwright.options.WaitForSelectorState;
import pages.commonPages.BasePage;

public class ContractUpdatePage extends BasePage{

    public void clickToNewGeneralCondition() {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        Locator newGeneralConditionButton = modalFrame.locator("a.k-grid-ContractRebateGridIdAddNew");
        newGeneralConditionButton.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));
        newGeneralConditionButton.click();
    }


    public void verifyNewGeneralConditionIsDisplayed() {
        Locator conditionPageTitle = page.locator("span.k-window-title", new Page.LocatorOptions().setHasText("Genel Kondisyon Tanımlama"));
        conditionPageTitle.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE).setTimeout(5000));
        verifyTextElementUseTrim("Genel Kondisyon Tanımlama", conditionPageTitle);
    }

}
