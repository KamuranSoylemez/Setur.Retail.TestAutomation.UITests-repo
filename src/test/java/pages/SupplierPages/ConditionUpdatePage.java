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
    
}
