package pages.SupplierPages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.WaitForSelectorState;
import pages.commonPages.BasePage;

public class ConditionUpdatePage extends BasePage {

    public void verifyGeneralConditionDetailWithStatus(String expectedStatus) {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Wait for grid to be visible
        Locator generalConditionGrid = modalFrame.locator("#ContractRebateGridId");
        generalConditionGrid.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(10000));
        
        // Find first row
        Locator firstRow = modalFrame.locator("#ContractRebateGridId tbody tr[role='row']").first();
        firstRow.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(5000));
        
        // Get the status from the last cell (usually status column)
        Locator statusCell = firstRow.locator("td").last();
        String actualStatus = statusCell.textContent().trim();
        
        System.out.println("🔍 Beklenen durum: '" + expectedStatus + "', Bulunan durum: '" + actualStatus + "'");
        
        if (!actualStatus.equals(expectedStatus)) {
            throw new AssertionError("Genel kondisyon durumu '" + expectedStatus + "' değil. Bulunan: '" + actualStatus + "'");
        }
        
        System.out.println("✅ Genel kondisyon detayı '" + expectedStatus + "' durumunda görüntülendi");
    }
    
    public void openGeneralConditionDetailWithIdAndStatus(String conditionId, String expectedStatus) {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Wait for grid to be visible
        Locator generalConditionGrid = modalFrame.locator("#ContractRebateGridId");
        generalConditionGrid.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(10000));
        
        page.waitForTimeout(2000);
        
        // Find all rows
        Locator allRows = modalFrame.locator("#ContractRebateGridId tbody tr[role='row']");
        int rowCount = allRows.count();
        System.out.println("🔍 Toplam " + rowCount + " satır bulundu, ID=" + conditionId + " ve durum='" + expectedStatus + "' arıyoruz");
        
        boolean found = false;
        for (int i = 0; i < rowCount; i++) {
            Locator row = allRows.nth(i);
            String rowText = row.textContent();
            
            // Check if row contains the ID and status
            if (rowText.contains(conditionId) && rowText.contains(expectedStatus)) {
                System.out.println("✅ ID=" + conditionId + " ve durum='" + expectedStatus + "' olan satır bulundu");
                
                // Click on the row or detail button
                Locator detailButton = row.locator("a:has-text('Detay'), button:has-text('Detay')").first();
                if (detailButton.count() > 0) {
                    detailButton.click();
                    System.out.println("✅ Detay butonuna tıklandı");
                } else {
                    // If no detail button, click the row itself
                    row.click();
                    System.out.println("✅ Satıra tıklandı");
                }
                
                found = true;
                page.waitForTimeout(2000);
                break;
            }
        }
        
        if (!found) {
            throw new AssertionError("ID=" + conditionId + " ve durum='" + expectedStatus + "' olan kondisyon bulunamadı!");
        }
    }
    
    public void verifyUpdateButtonIsVisible() {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Look for "Güncelle" button in the grid
        Locator updateButton = modalFrame.locator(
            "a:has-text('Güncelle'), " +
            "button:has-text('Güncelle'), " +
            "a.k-button:has-text('Güncelle')"
        ).first();
        
        updateButton.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(5000));
        
        System.out.println("✅ 'Güncelle' butonu görünür durumda");
    }
    
    public void clickSettingsButtonForConditionWithStatus(String expectedStatus) {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Wait for grid to be visible
        Locator generalConditionGrid = modalFrame.locator("#ContractRebateGridId");
        generalConditionGrid.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(10000));
        
        page.waitForTimeout(2000);
        
        // Find all rows
        Locator allRows = modalFrame.locator("#ContractRebateGridId tbody tr[role='row']");
        int rowCount = allRows.count();
        System.out.println("🔍 Toplam " + rowCount + " satır bulundu, durum='" + expectedStatus + "' için ayar butonu arıyoruz");
        
        boolean found = false;
        for (int i = 0; i < rowCount; i++) {
            Locator row = allRows.nth(i);
            String rowText = row.textContent();
            
            // Check if row contains the status
            if (rowText.contains(expectedStatus)) {
                System.out.println("✅ Durum='" + expectedStatus + "' olan satır bulundu");
                
                // Look for settings/gear button (ayar butonu)
                Locator settingsButton = row.locator(
                    "a.k-grid-edit, " +
                    "a:has-text('Ayar'), " +
                    "button:has-text('Ayar'), " +
                    "a[title*='Ayar'], " +
                    "a.k-button.k-grid-edit, " +
                    "span.k-icon.k-i-gear"
                ).first();
                
                if (settingsButton.count() > 0) {
                    settingsButton.click();
                    System.out.println("✅ Ayar butonuna tıklandı");
                    found = true;
                    page.waitForTimeout(2000);
                    break;
                } else {
                    System.out.println("⚠️ Ayar butonu bulunamadı, tüm butonları listeliyorum...");
                    Locator allButtons = row.locator("a, button");
                    int buttonCount = allButtons.count();
                    for (int j = 0; j < buttonCount; j++) {
                        String buttonText = allButtons.nth(j).textContent();
                        System.out.println("  Button " + j + ": " + buttonText);
                    }
                }
            }
        }
        
        if (!found) {
            throw new AssertionError("Durum='" + expectedStatus + "' olan kondisyon için ayar butonu bulunamadı!");
        }
    }
    
    public void verifyUpdateButtonIsVisibleInSettingsMenu() {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Wait for popup menu to appear
        page.waitForTimeout(1000);
        
        // Look for update button in various possible locations
        Locator updateButton = modalFrame.locator(
            "ul.k-menu-group a:has-text('Güncelle'), " +
            "div.k-animation-container a:has-text('Güncelle'), " +
            "li:has-text('Güncelle'), " +
            "a:has-text('Güncelle'), " +
            "button:has-text('Güncelle')"
        ).first();
        
        updateButton.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(5000));
        
        System.out.println("✅ Ayar menüsünde 'Güncelle' butonu görünür durumda");
    }
    
    public void verifyUpdateButtonIsVisibleForConditionWithStatus(String expectedStatus) {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Wait for grid to be visible
        Locator generalConditionGrid = modalFrame.locator("#ContractRebateGridId");
        generalConditionGrid.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(10000));
        
        page.waitForTimeout(2000);
        
        // Find all rows
        Locator allRows = modalFrame.locator("#ContractRebateGridId tbody tr[role='row']");
        int rowCount = allRows.count();
        System.out.println("🔍 Toplam " + rowCount + " satır bulundu, durum='" + expectedStatus + "' için Güncelle butonu arıyoruz");
        
        boolean found = false;
        for (int i = 0; i < rowCount; i++) {
            Locator row = allRows.nth(i);
            String rowText = row.textContent();
            
            // Check if row contains the status
            if (rowText.contains(expectedStatus)) {
                System.out.println("✅ Durum='" + expectedStatus + "' olan satır bulundu");
                
                // Check if "Güncelle" text exists in the row
                if (rowText.contains("Güncelle")) {
                    System.out.println("✅ 'Güncelle' butonu durum='" + expectedStatus + "' olan satırda mevcut");
                    
                    // Also check for "Tarihçe" button
                    if (rowText.contains("Tarihçe")) {
                        System.out.println("✅ 'Tarihçe' butonu da mevcut");
                    }
                    
                    found = true;
                    break;
                } else {
                    throw new AssertionError("Durum='" + expectedStatus + "' olan satırda 'Güncelle' butonu bulunamadı!");
                }
            }
        }
        
        if (!found) {
            throw new AssertionError("Durum='" + expectedStatus + "' olan kondisyon bulunamadı!");
        }
    }
    
    public void verifyHistoryButtonIsVisibleForConditionWithStatus(String expectedStatus) {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Wait for grid to be visible
        Locator generalConditionGrid = modalFrame.locator("#ContractRebateGridId");
        generalConditionGrid.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(10000));
        
        page.waitForTimeout(1000);
        
        // Find all rows
        Locator allRows = modalFrame.locator("#ContractRebateGridId tbody tr[role='row']");
        int rowCount = allRows.count();
        System.out.println("🔍 Toplam " + rowCount + " satır bulundu, durum='" + expectedStatus + "' için Tarihçe butonu arıyoruz");
        
        boolean found = false;
        for (int i = 0; i < rowCount; i++) {
            Locator row = allRows.nth(i);
            String rowText = row.textContent();
            
            // Check if row contains the status
            if (rowText.contains(expectedStatus)) {
                System.out.println("✅ Durum='" + expectedStatus + "' olan satır bulundu");
                
                // Check if "Tarihçe" text exists in the row
                if (rowText.contains("Tarihçe")) {
                    System.out.println("✅ 'Tarihçe' butonu durum='" + expectedStatus + "' olan satırda mevcut");
                    found = true;
                    break;
                } else {
                    throw new AssertionError("Durum='" + expectedStatus + "' olan satırda 'Tarihçe' butonu bulunamadı!");
                }
            }
        }
        
        if (!found) {
            throw new AssertionError("Durum='" + expectedStatus + "' olan kondisyon bulunamadı!");
        }
    }
    
    public void clickUpdateButtonForConditionWithStatus(String expectedStatus) {
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Wait for grid to be visible
        Locator generalConditionGrid = modalFrame.locator("#ContractRebateGridId");
        generalConditionGrid.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(10000));
        
        page.waitForTimeout(2000);
        
        // Find all rows
        Locator allRows = modalFrame.locator("#ContractRebateGridId tbody tr[role='row']");
        int rowCount = allRows.count();
        System.out.println("🔍 Toplam " + rowCount + " satır bulundu, durum='" + expectedStatus + "' için Güncelle butonuna tıklayacağız");
        
        boolean found = false;
        for (int i = 0; i < rowCount; i++) {
            Locator row = allRows.nth(i);
            String rowText = row.textContent();
            
            // Check if row contains the status
            if (rowText.contains(expectedStatus)) {
                System.out.println("✅ Durum='" + expectedStatus + "' olan satır bulundu");
                
                // Try to click the green edit button with glyphicon-edit icon
                // The button structure: <a class="k-button gridCmdBtn k-success"><i class="glyphicon glyphicon-edit"></i></a>
                Locator editButton = row.locator(
                    "a.k-success:has(i.glyphicon-edit), " +
                    "a#Edit, " +
                    "a.gridCmdBtn.k-success"
                ).first();
                
                if (editButton.count() > 0) {
                    editButton.click();
                    System.out.println("✅ Yeşil düzenleme butonuna (Edit) tıklandı");
                    found = true;
                    page.waitForTimeout(2000);
                    break;
                } else {
                    throw new AssertionError("Durum='" + expectedStatus + "' olan satırda yeşil düzenleme butonu bulunamadı!");
                }
            }
        }
        
        if (!found) {
            throw new AssertionError("Durum='" + expectedStatus + "' olan kondisyon bulunamadı!");
        }
    }
    
    public void verifyConditionUpdatePopupIsDisplayed() {
        // Look for update popup - it could be in a new window or modal
        Locator updatePopup = page.locator(
            "span.k-window-title:has-text('Güncelleme'), " +
            "span.k-window-title:has-text('Kondisyon Güncelleme'), " +
            "span.k-window-title:has-text('Genel Kondisyon Güncelleme'), " +
            "div.k-window:has-text('Güncelleme')"
        ).first();
        
        updatePopup.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(5000));
        
        String popupTitle = updatePopup.textContent();
        System.out.println("✅ Kondisyon güncelleme pop-up açıldı: '" + popupTitle + "'");
    }
    
}
