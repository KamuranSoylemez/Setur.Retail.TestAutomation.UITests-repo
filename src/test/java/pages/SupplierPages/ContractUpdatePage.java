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

    // Brand Ambassador Condition Methods
    public void clickBrandAmbassadorConditionsTab() {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Debug: List all tabs
        System.out.println("🔍 Mevcut tab'ları kontrol ediyoruz...");
        Locator allTabs = modalFrame.locator("li[role='tab']");
        System.out.println("Toplam " + allTabs.count() + " tab bulundu");
        for (int i = 0; i < allTabs.count(); i++) {
            String tabText = allTabs.nth(i).textContent();
            String ariaSelected = allTabs.nth(i).getAttribute("aria-selected");
            System.out.println("  Tab " + i + ": '" + tabText + "' (aria-selected=" + ariaSelected + ")");
        }
        
        // Use JavaScript inside the frame context to trigger Kendo TabStrip
        System.out.println("🔍 Kendo TabStrip API ile tab switch yapılıyor...");
        Object result = modalFrame.locator("body").evaluate(
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
        
        System.out.println("✅ Kendo TabStrip.select() sonucu: " + result);
        page.waitForTimeout(3000);
        
        // Verify tab switched
        System.out.println("🔍 Tab switch sonucunu kontrol ediyoruz...");
        Locator brandAmbassadorTab = modalFrame.locator("li[role='tab']:has(a.k-link:has-text('Temsilci Kondisyon'))").first();
        String ariaSelected = brandAmbassadorTab.getAttribute("aria-selected");
        System.out.println("🔍 Temsilci Kondisyon tab aria-selected: " + ariaSelected);
        
        // Check tabpanel visibility
        Locator tabPanel = modalFrame.locator("#ContractRebateTab-2");
        if (tabPanel.count() > 0) {
            String ariaHidden = tabPanel.getAttribute("aria-hidden");
            boolean isVisible = tabPanel.isVisible();
            System.out.println("🔍 Temsilci Kondisyon tabpanel: aria-hidden=" + ariaHidden + ", visible=" + isVisible);
        }
        
        // Wait for ContractRepresentativeGridId to become visible
        Locator representativeGrid = modalFrame.locator("#ContractRepresentativeGridId");
        try {
            representativeGrid.waitFor(new Locator.WaitForOptions()
                .setState(WaitForSelectorState.VISIBLE)
                .setTimeout(15000));
            System.out.println("✅ ContractRepresentativeGridId visible oldu");
        } catch (Exception e) {
            System.out.println("❌ ContractRepresentativeGridId 15 saniye içinde visible olmadı!");
            
            // Debug: Check tab content divs
            System.out.println("🔍 Tab content div'lerini kontrol ediyoruz...");
            Locator tabContents = modalFrame.locator("div[role='tabpanel']");
            System.out.println("Toplam " + tabContents.count() + " tabpanel var:");
            for (int i = 0; i < tabContents.count(); i++) {
                String id = tabContents.nth(i).getAttribute("id");
                String ariaHidden = tabContents.nth(i).getAttribute("aria-hidden");
                boolean isVisible = tabContents.nth(i).isVisible();
                System.out.println("  TabPanel " + i + ": id='" + id + "', aria-hidden=" + ariaHidden + ", visible=" + isVisible);
            }
            
            // Debug: List all grids
            Locator allGrids = modalFrame.locator("div[data-role='grid']");
            System.out.println("🔍 Toplam " + allGrids.count() + " grid var:");
            for (int i = 0; i < allGrids.count(); i++) {
                String gridId = allGrids.nth(i).getAttribute("id");
                boolean isVisible = allGrids.nth(i).isVisible();
                System.out.println("  Grid " + i + ": id='" + gridId + "', visible=" + isVisible);
            }
            
            throw new RuntimeException("❌ Temsilci Kondisyon grid'i yüklenmedi! Tab switch başarısız oldu.");
        }
        
        page.waitForTimeout(1000);
        System.out.println("✅ Temsilci Kondisyon tab'ı tamamen yüklendi");
    }

    public void clickToNewBrandAmbassadorConditionButton() {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Debug: Temsilci Kondisyon tab'ındaki button'ları listele
        System.out.println("🔍 Temsilci Kondisyon tab'ındaki button'ları arıyoruz...");
        Locator allButtons = modalFrame.locator("a.k-button, a[class*='k-grid']");
        int buttonCount = allButtons.count();
        System.out.println("Toplam " + buttonCount + " button bulundu");
        
        for (int i = 0; i < Math.min(buttonCount, 15); i++) {
            try {
                String buttonClass = allButtons.nth(i).getAttribute("class");
                String buttonText = allButtons.nth(i).textContent();
                boolean isVisible = allButtons.nth(i).isVisible();
                System.out.println("  Button " + i + ": class='" + buttonClass + "', text='" + buttonText.trim() + "', visible=" + isVisible);
            } catch (Exception e) {
                System.out.println("  Button " + i + " okunamadı");
            }
        }
        
        // "Yeni Kayıt" butonunu farklı selector'larla dene
        Locator newBrandAmbassadorButton = modalFrame.locator(
            "a.k-grid-ContractRepresentativeGridIdAddNew, " +
            "a:has-text('Yeni Kayıt'), " +
            "a.k-button:has-text('Yeni Kayıt')"
        ).first();
        
        if (newBrandAmbassadorButton.count() == 0) {
            throw new RuntimeException("❌ 'Yeni Kayıt' butonu bulunamadı! Yukarıdaki button listesine bakın.");
        }
        
        newBrandAmbassadorButton.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE).setTimeout(5000));
        newBrandAmbassadorButton.click();
        page.waitForTimeout(2000); // Yeni modal açılması için bekle
        System.out.println("✅ 'Yeni Kayıt' butonuna tıklandı");
    }

}
