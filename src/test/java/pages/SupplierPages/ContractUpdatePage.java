package pages.SupplierPages;
import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import pages.commonPages.BasePage;

public class ContractUpdatePage extends BasePage{

    public void clickToNewGeneralCondition() {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        Locator newGeneralConditionButton = modalFrame.locator("a.k-grid-ContractRebateGridIdAddNew");
        clickElement(newGeneralConditionButton);
    }
}
