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

    public void clickGeneralConditionTab() {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Use Kendo TabStrip API to switch to Genel Kondisyon tab
        modalFrame.locator("body").evaluate(
            "() => {" +
            "  const tabstrip = document.querySelector('.k-tabstrip');" +
            "  if (!tabstrip) { return 'no_tabstrip'; }" +
            "  const kendoTabStrip = $(tabstrip).data('kendoTabStrip');" +
            "  if (!kendoTabStrip) { return 'no_kendo'; }" +
            "  const tabs = kendoTabStrip.tabGroup.find('li[role=tab]');" +
            "  let targetIndex = -1;" +
            "  tabs.each(function(index) {" +
            "    if ($(this).text().trim() === 'Genel Kondisyon') {" +
            "      targetIndex = index;" +
            "    }" +
            "  });" +
            "  if (targetIndex === -1) { return 'no_tab_found'; }" +
            "  kendoTabStrip.select(targetIndex);" +
            "  return 'success_' + targetIndex;" +
            "}"
        );
        
        page.waitForTimeout(2000);
        
        // Wait for ContractRebateGridId to become visible
        Locator generalConditionGrid = modalFrame.locator("#ContractRebateGridId");
        generalConditionGrid.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(10000));
        
        page.waitForTimeout(1000);
    }

    // Brand Ambassador Condition Methods
    public void clickBrandAmbassadorConditionsTab() {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Use Kendo TabStrip API to switch to Temsilci Kondisyon tab
        modalFrame.locator("body").evaluate(
            "() => {" +
            "  const tabstrip = document.querySelector('.k-tabstrip');" +
            "  if (!tabstrip) { return 'no_tabstrip'; }" +
            "  const kendoTabStrip = $(tabstrip).data('kendoTabStrip');" +
            "  if (!kendoTabStrip) { return 'no_kendo'; }" +
            "  const tabs = kendoTabStrip.tabGroup.find('li[role=tab]');" +
            "  let targetIndex = -1;" +
            "  tabs.each(function(index) {" +
            "    if ($(this).text().trim() === 'Temsilci Kondisyon') {" +
            "      targetIndex = index;" +
            "    }" +
            "  });" +
            "  if (targetIndex === -1) { return 'no_tab_found'; }" +
            "  kendoTabStrip.select(targetIndex);" +
            "  return 'success_' + targetIndex;" +
            "}"
        );
        
        page.waitForTimeout(3000);
        
        // Wait for ContractRepresentativeGridId to become visible
        Locator representativeGrid = modalFrame.locator("#ContractRepresentativeGridId");
        representativeGrid.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(15000));
        
        page.waitForTimeout(1000);
    }

    public void clickToNewBrandAmbassadorConditionButton() {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Find and click "Yeni Kayıt" button
        Locator newBrandAmbassadorButton = modalFrame.locator(
            "a.k-grid-ContractRepresentativeGridIdAddNew, " +
            "a:has-text('Yeni Kayıt'), " +
            "a.k-button:has-text('Yeni Kayıt')"
        ).first();
        
        newBrandAmbassadorButton.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(5000));
        newBrandAmbassadorButton.click();
        page.waitForTimeout(2000);
    }

}
