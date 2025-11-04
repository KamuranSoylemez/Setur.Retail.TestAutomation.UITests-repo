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
        
        // Önce tab'ları listele
        System.out.println("🔍 Mevcut tab'ları kontrol ediyoruz...");
        Locator allTabs = modalFrame.locator("li[role='tab']");
        int tabCount = allTabs.count();
        System.out.println("Toplam " + tabCount + " tab bulundu");
        
        for (int i = 0; i < Math.min(tabCount, 10); i++) {
            try {
                String tabText = allTabs.nth(i).textContent();
                System.out.println("  Tab " + i + ": '" + tabText.trim() + "'");
            } catch (Exception e) {
                System.out.println("  Tab " + i + " okunamadı");
            }
        }
        
        // "Temsilci Kondisyon" tab'ına tıkla
        Locator brandAmbassadorTab = modalFrame.locator("li[role='tab']:has-text('Temsilci Kondisyon'), .k-tabstrip-items li:has-text('Temsilci Kondisyon')").first();
        
        if (brandAmbassadorTab.count() > 0) {
            brandAmbassadorTab.click();
            System.out.println("✅ 'Temsilci Kondisyon' tab'ına tıklandı");
            page.waitForTimeout(2000); // Tab içeriğinin yüklenmesini bekle
        } else {
            throw new RuntimeException("⚠️ 'Temsilci Kondisyon' tab'ı bulunamadı!");
        }
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
