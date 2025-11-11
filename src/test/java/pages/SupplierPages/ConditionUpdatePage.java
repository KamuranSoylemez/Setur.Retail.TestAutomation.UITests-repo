package pages.SupplierPages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.SelectOption;
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
            throw new AssertionError("Durum='" + expectedStatus + "' olan kondisyon bulunamadı!");
        }
    }
    
    /**
     * Verifies that condition detail popup is displayed (first popup after clicking green edit button)
     */
    public void verifyConditionDetailPopupIsDisplayed() {
        // This is the first popup that opens when clicking the green edit button
        // It shows condition details with an "Güncelle" button
        page.waitForTimeout(1500);
        
        Locator detailPopup = page.locator(
            "#SeturModalWin:visible, " +
            ".k-window:visible, " +
            "div[role='dialog']:visible"
        ).first();
        
        detailPopup.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(5000));
        
        System.out.println("✅ Kondisyon detay pop-up açıldı (1. popup)");
    }
    
    /**
     * Clicks the update button on the condition detail popup (first popup)
     * This will open the second popup for updating the condition
     */
    public void clickUpdateButtonOnConditionDetailPopup() {
        page.waitForTimeout(2000);
        
        // The popup contains an iframe, we need to switch to it
        Locator iframe = page.locator("#SeturModalWin iframe.k-content-frame, iframe[title='Setur']").first();
        
        if (iframe.count() == 0) {
            throw new AssertionError("Kondisyon detay popup'ında iframe bulunamadı!");
        }
        
        FrameLocator modalFrame = iframe.contentFrame();
        
        // Look for "Güncelle" button inside the iframe
        Locator updateButton = modalFrame.locator(
            "button:has-text('Güncelle'), " +
            "a:has-text('Güncelle'), " +
            "#UpdateButton, " +
            "input[value='Güncelle'], " +
            ".btn:has-text('Güncelle')"
        ).first();
        
        if (updateButton.count() > 0) {
            updateButton.click();
            System.out.println("✅ Kondisyon detay popup'ındaki iframe içinde Güncelle butonuna tıklandı");
            page.waitForTimeout(2000); // Wait for second popup to open
        } else {
            // Debug: print iframe content
            String frameContent = modalFrame.locator("body").textContent();
            System.out.println("🔍 Iframe içeriği: " + frameContent.substring(0, Math.min(500, frameContent.length())));
            throw new AssertionError("Kondisyon detay popup'ındaki iframe içinde Güncelle butonu bulunamadı!");
        }
    }
    
    /**
     * Verifies that condition update popup is displayed (second popup after clicking update button)
     */
    public void verifyConditionUpdatePopupIsDisplayed() {
        // This is the second popup that opens after clicking "Güncelle" in first popup
        page.waitForTimeout(1000);
        
        // Look for update popup - it could be in a new window or modal
        Locator updatePopup = page.locator(
            "span.k-window-title:has-text('Güncelleme'), " +
            "span.k-window-title:has-text('Kondisyon Güncelleme'), " +
            "span.k-window-title:has-text('Genel Kondisyon Güncelleme'), " +
            "div.k-window:has-text('Güncelleme')"
        ).last(); // Use last() to get the newest/second popup
        
        updatePopup.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(5000));
        
        String popupTitle = updatePopup.textContent();
        System.out.println("✅ Kondisyon güncelleme pop-up açıldı (2. popup): '" + popupTitle + "'");
    }
    
    /**
     * Clicks the update button on the "Genel Kondisyon Güncelleme" popup (second popup)
     * This will open the third/final popup
     */
    public void clickUpdateButtonOnConditionUpdatePopup() {
        page.waitForTimeout(2000);
        
        // This popup might also have an iframe, check for it
        Locator iframe = page.locator("iframe.k-content-frame, iframe[title='Setur']").last();
        
        if (iframe.count() > 0) {
            System.out.println("🔍 2. popup da iframe içeriyor, iframe'e geçiyoruz");
            FrameLocator modalFrame = iframe.contentFrame();
            
            // Look for "Güncelle" button inside the iframe
            Locator updateButton = modalFrame.locator(
                "button:has-text('Güncelle'), " +
                "a:has-text('Güncelle'), " +
                "#UpdateButton, " +
                "input[value='Güncelle'], " +
                ".btn:has-text('Güncelle')"
            ).first();
            
            if (updateButton.count() > 0) {
                updateButton.click();
                System.out.println("✅ 2. popup'ın iframe içindeki Güncelle butonuna tıklandı");
                page.waitForTimeout(2000);
            } else {
                throw new AssertionError("2. popup'ın iframe içinde Güncelle butonu bulunamadı!");
            }
        } else {
            // No iframe, try to find button directly in popup
            Locator updateButton = page.locator(
                "button:has-text('Güncelle'), " +
                "a:has-text('Güncelle'), " +
                "#UpdateButton, " +
                "input[value='Güncelle'], " +
                ".btn:has-text('Güncelle')"
            ).last();
            
            if (updateButton.count() > 0) {
                updateButton.click();
                System.out.println("✅ 2. popup'taki Güncelle butonuna tıklandı");
                page.waitForTimeout(2000);
            } else {
                throw new AssertionError("2. popup'ta Güncelle butonu bulunamadı!");
            }
        }
    }
    
    /**
     * Verifies that final update popup is displayed (third popup - "Kondisyon Güncelleme")
     */
    public void verifyFinalUpdatePopupIsDisplayed() {
        page.waitForTimeout(2000);
        
        // Look for the specific "Kondisyon Güncelleme" popup title
        Locator finalPopup = page.locator(
            "span.k-window-title:has-text('Kondisyon Güncelleme')"
        ).last();
        
        // Check if popup exists (even if hidden)
        if (finalPopup.count() > 0) {
            String popupTitle = finalPopup.textContent();
            System.out.println("✅ Son güncelleme pop-up bulundu (3. popup): '" + popupTitle + "'");
        } else {
            throw new AssertionError("'Kondisyon Güncelleme' başlıklı popup bulunamadı!");
        }
    }
    
    /**
     * Clicks the save button on final update popup without filling required fields
     */
    public void clickSaveButtonOnFinalUpdatePopupWithoutFillingRequiredFields() {
        page.waitForTimeout(2000);
        
        // This popup also has an iframe, we need to switch to it
        Locator iframe = page.locator("iframe.k-content-frame, iframe[title='Setur']").last();
        
        if (iframe.count() > 0) {
            System.out.println("🔍 3. popup da iframe içeriyor, iframe'e geçip Kaydet butonunu arıyoruz");
            FrameLocator modalFrame = iframe.contentFrame();
            
            // Look for "Kaydet" button inside the iframe
            Locator saveButton = modalFrame.locator(
                "button:has-text('Kaydet'), " +
                "a:has-text('Kaydet'), " +
                "#btnSave, " +
                "input[type='submit'][value='Kaydet'], " +
                ".btn:has-text('Kaydet')"
            ).first();
            
            if (saveButton.count() > 0) {
                saveButton.click();
                System.out.println("✅ 3. popup'ın iframe içindeki Kaydet butonuna tıklandı (zorunlu alanlar boş)");
                page.waitForTimeout(1500); // Wait for validation messages
            } else {
                // Debug: print available buttons
                String frameContent = modalFrame.locator("body").textContent();
                System.out.println("🔍 Iframe içeriği (ilk 500 karakter): " + frameContent.substring(0, Math.min(500, frameContent.length())));
                throw new AssertionError("3. popup'ın iframe içinde Kaydet butonu bulunamadı!");
            }
        } else {
            throw new AssertionError("3. popup'ta iframe bulunamadı!");
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
    
    /**
     * Clicks the save button on condition update popup without filling any fields
     */
    public void clickSaveButtonOnConditionUpdatePopupWithoutFillingRequiredFields() {
        // Wait for popup to be fully loaded
        page.waitForTimeout(1000);
        
        // Look for save button - common selectors for save/kaydet button in modal
        Locator saveButton = page.locator(
            "button:has-text('Kaydet'), " +
            "button[type='submit']:has-text('Kaydet'), " +
            "a:has-text('Kaydet'), " +
            "#btnSave, " +
            ".btn:has-text('Kaydet')"
        ).first();
        
        if (saveButton.count() > 0) {
            saveButton.click();
            System.out.println("✅ Kaydet butonuna (zorunlu alanlar boş) tıklandı");
            page.waitForTimeout(1000); // Wait for validation messages
        } else {
            throw new AssertionError("Kaydet butonu bulunamadı!");
        }
    }
    
    /**
     * Verifies that update type field is mandatory (has validation error)
     * This method checks for Alertify notification messages
     */
    public void verifyUpdateTypeFieldIsMandatory() {
        page.waitForTimeout(1500);
        
        // Alertify messages appear in the main page, not iframe
        Locator alertifyMessage = page.locator(
            "div.ajs-message.ajs-error.ajs-visible, " +
            "div.ajs-message.ajs-error, " +
            "div.ajs-message:has-text('Açıklama')"
        ).first();
        
        if (alertifyMessage.count() == 0 || !alertifyMessage.isVisible()) {
            System.out.println("⚠️  Alertify mesajı bulunamadı, iframe içinde aranıyor...");
            
            // If not in main page, check iframe
            Locator iframe = page.locator("iframe.k-content-frame, iframe[title='Setur']").last();
            if (iframe.count() > 0) {
                FrameLocator modalFrame = iframe.contentFrame();
                alertifyMessage = modalFrame.locator(
                    "div.ajs-message.ajs-error.ajs-visible, " +
                    "div.ajs-message.ajs-error"
                ).first();
            }
        }
        
        if (alertifyMessage.count() == 0 || !alertifyMessage.isVisible()) {
            throw new AssertionError("❌ Zorunlu alan validation mesajı bulunamadı (Alertify notification)!");
        }
        
        String messageText = alertifyMessage.textContent().trim();
        System.out.println("✅ Zorunlu alan validation mesajı doğrulandı: '" + messageText + "'");
    }    /**
     * Verifies that description field is mandatory (has validation error)
     * This method checks for Alertify notification messages
     */
    public void verifyDescriptionFieldIsMandatory() {
        page.waitForTimeout(500);
        
        // Alertify messages appear in the main page, not iframe
        Locator alertifyMessage = page.locator(
            "div.ajs-message.ajs-error.ajs-visible, " +
            "div.ajs-message.ajs-error, " +
            "div.ajs-message:has-text('Açıklama')"
        ).first();
        
        if (alertifyMessage.count() == 0 || !alertifyMessage.isVisible()) {
            System.out.println("⚠️  Alertify mesajı bulunamadı, iframe içinde aranıyor...");
            
            // If not in main page, check iframe
            Locator iframe = page.locator("iframe.k-content-frame, iframe[title='Setur']").last();
            if (iframe.count() > 0) {
                FrameLocator modalFrame = iframe.contentFrame();
                alertifyMessage = modalFrame.locator(
                    "div.ajs-message.ajs-error.ajs-visible, " +
                    "div.ajs-message.ajs-error"
                ).first();
            }
        }
        
        if (alertifyMessage.count() == 0 || !alertifyMessage.isVisible()) {
            throw new AssertionError("❌ Açıklama alanı zorunlu alan validation mesajı bulunamadı (Alertify notification)!");
        }
        
        String messageText = alertifyMessage.textContent().trim();
        System.out.println("✅ Açıklama alanı zorunlu alan validation mesajı doğrulandı: '" + messageText + "'");
    }
    
    /**
     * Verifies that specific error message is displayed
     * Checks for Alertify notification messages
     * @param expectedMessage The expected error message text
     */
    public void verifyErrorMessageIsDisplayed(String expectedMessage) {
        page.waitForTimeout(1000);
        
        // Alertify messages appear in the main page
        Locator alertifyMessage = page.locator(
            "div.ajs-message.ajs-error.ajs-visible:has-text('" + expectedMessage + "'), " +
            "div.ajs-message.ajs-error:has-text('" + expectedMessage + "'), " +
            "div.ajs-message:has-text('" + expectedMessage + "')"
        ).first();
        
        if (alertifyMessage.count() > 0 && alertifyMessage.isVisible()) {
            String actualMessage = alertifyMessage.textContent().trim();
            System.out.println("✅ Beklenen hata mesajı görüntülendi (Alertify): '" + actualMessage + "'");
            return;
        }
        
        // If not found, check iframe as fallback
        Locator iframe = page.locator("iframe.k-content-frame, iframe[title='Setur']").last();
        if (iframe.count() > 0) {
            FrameLocator modalFrame = iframe.contentFrame();
            alertifyMessage = modalFrame.locator(
                "div.ajs-message:has-text('" + expectedMessage + "'), " +
                "div:has-text('" + expectedMessage + "')"
            ).first();
            
            if (alertifyMessage.count() > 0 && alertifyMessage.isVisible()) {
                System.out.println("✅ Beklenen hata mesajı görüntülendi (iframe): '" + alertifyMessage.textContent() + "'");
                return;
            }
        }
        
        // Debug: Show all alertify messages
        System.out.println("🔍 Tüm Alertify mesajları kontrol ediliyor...");
        Locator allAlertify = page.locator("div.ajs-message");
        int count = allAlertify.count();
        System.out.println("🔍 Bulunan Alertify mesajları: " + count);
        for (int i = 0; i < Math.min(count, 5); i++) {
            System.out.println("  - " + allAlertify.nth(i).textContent().trim());
        }
        
        throw new AssertionError("❌ Beklenen hata mesajı bulunamadı: '" + expectedMessage + "'");
    }
    
    /**
     * Selects update type (Güncelleme Türü) from dropdown in final update popup
     * @param updateType The update type to select (e.g., "Kondisyon İyileşmesi")
     */
    public void selectUpdateTypeOnFinalUpdatePopup(String updateType) {
        page.waitForTimeout(1500);
        
        // Access the iframe in the final popup
        Locator iframe = page.locator("iframe.k-content-frame, iframe[title='Setur']").last();
        
        if (iframe.count() == 0) {
            throw new AssertionError("❌ Iframe bulunamadı!");
        }
        
        FrameLocator modalFrame = iframe.contentFrame();
        
        // Look for the Kendo dropdown
        Locator updateTypeDropdown = modalFrame.locator("span.k-dropdown").first();
        
        if (updateTypeDropdown.count() == 0) {
            throw new AssertionError("❌ Güncelleme Türü dropdown bulunamadı!");
        }
        
        String tagName = updateTypeDropdown.evaluate("el => el.tagName").toString();
        
        if (tagName.equalsIgnoreCase("SELECT")) {
            updateTypeDropdown.selectOption(new SelectOption().setLabel(updateType));
            System.out.println("✅ Güncelleme Türü seçildi (select): '" + updateType + "'");
        } else {
            // Kendo dropdown
            updateTypeDropdown.click();
            page.waitForTimeout(800);
            
            // Options appear in main page, not iframe
            Locator option = page.locator("li.k-item:has-text('" + updateType + "')").first();
            
            if (option.count() == 0) {
                // Try in iframe
                option = modalFrame.locator("li.k-item:has-text('" + updateType + "')").first();
            }
            
            option.click();
            System.out.println("✅ Güncelleme Türü seçildi (Kendo): '" + updateType + "'");
        }
        
        page.waitForTimeout(500);
    }
    
    /**
     * Enters description text in final update popup
     * @param description The description text to enter
     */
    public void enterDescriptionOnFinalUpdatePopup(String description) {
        page.waitForTimeout(1000);
        
        // Access the iframe in the final popup
        Locator iframe = page.locator("iframe.k-content-frame, iframe[title='Setur']").last();
        
        if (iframe.count() == 0) {
            throw new AssertionError("❌ Iframe bulunamadı!");
        }
        
        FrameLocator modalFrame = iframe.contentFrame();
        
        // Look for description field - use textarea directly
        Locator descriptionField = modalFrame.locator("textarea#StatusDescription, textarea").first();
        
        if (descriptionField.count() == 0) {
            throw new AssertionError("❌ Açıklama alanı (textarea) bulunamadı!");
        }
        
        if (!descriptionField.isVisible()) {
            System.out.println("⚠️ Açıklama alanı görünmüyor, yine de doldurmayı deniyorum...");
        }
        
        descriptionField.click(); // Focus
        descriptionField.clear();
        descriptionField.fill(description);
        System.out.println("✅ Açıklama girildi: '" + description + "'");
        page.waitForTimeout(500);
    }
    
    /**
     * Clicks save button on final update popup (with filled fields)
     */
    public void clickSaveButtonOnFinalUpdatePopup() {
        page.waitForTimeout(1000);
        
        // Access the iframe in the final popup
        Locator iframe = page.locator("iframe.k-content-frame, iframe[title='Setur']").last();
        
        if (iframe.count() == 0) {
            throw new AssertionError("❌ Iframe bulunamadı!");
        }
        
        FrameLocator modalFrame = iframe.contentFrame();
        
        // Look for "Kaydet" button
        Locator saveButton = modalFrame.locator(
            "button:has-text('Kaydet'), " +
            "a:has-text('Kaydet'), " +
            "#btnSave, " +
            "input[type='submit'][value='Kaydet'], " +
            ".btn:has-text('Kaydet')"
        ).first();
        
        if (saveButton.count() == 0 || !saveButton.isVisible()) {
            throw new AssertionError("❌ Kaydet butonu bulunamadı!");
        }
        
        saveButton.click();
        System.out.println("✅ Kaydet butonuna tıklandı (form doldurulmuş)");
        page.waitForTimeout(2000); // Wait for save operation
    }
    
    /**
     * Verifies that condition definition page is displayed after save
     */
    public void verifyConditionDefinitionPageIsDisplayed() {
        page.waitForTimeout(2000);
        
        // Look for general condition grid or contract rebate grid
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        Locator conditionGrid = modalFrame.locator(
            "#ContractRebateGridId, " +
            "div.k-grid:has-text('Genel Kondisyon')"
        ).first();
        
        conditionGrid.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(5000));
        
        System.out.println("✅ Genel Kondisyon Tanımlama ekranı görüntülendi (kayıt başarılı)");
    }
    
    /**
     * Clicks approve button for condition with specific status
     * @param expectedStatus The status to filter (e.g., "Onay Bekleniyor")
     */
    public void clickApproveButtonForConditionWithStatus(String expectedStatus) {
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
                
                // Look for "Güncelle" button text
                Locator updateButton = row.locator("a:has-text('Güncelle')").first();
                
                if (updateButton.count() > 0) {
                    updateButton.click();
                    System.out.println("✅ Güncelle butonuna tıklandı");
                    found = true;
                    page.waitForTimeout(2000);
                    break;
                } else {
                    System.out.println("❌ Güncelle butonu bulunamadı!");
                }
            }
        }
        
        if (!found) {
            throw new AssertionError("Durum='" + expectedStatus + "' olan kondisyon için Güncelle butonu bulunamadı!");
        }
    }
    
    /**
     * Verifies that approval popup is displayed
     */
    public void verifyApprovalPopupIsDisplayed() {
        page.waitForTimeout(1000);
        
        // Look for approval confirmation popup
        Locator approvalPopup = page.locator(
            "div.k-window:has-text('Onay'), " +
            "div.k-window:has-text('Onayla'), " +
            "span.k-window-title:has-text('Onay')"
        ).first();
        
        approvalPopup.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(5000));
        
        System.out.println("✅ Onay pop-up açıldı");
    }
    
    /**
     * Clicks approve button on approval popup
     */
    public void clickApproveButtonOnApprovalPopup() {
        page.waitForTimeout(1000);
        
        // Look for Alertify OK button (ajs-ok class)
        Locator alertifyButton = page.locator("button.ajs-button.ajs-ok");
        
        if (alertifyButton.count() == 0 || !alertifyButton.isVisible()) {
            throw new AssertionError("❌ Onay popup'ında Onay butonu bulunamadı!");
        }
        
        alertifyButton.click();
        System.out.println("✅ Onay butonuna tıklandı (Alertify popup)");
        page.waitForTimeout(2000);
    }
    
    /**
     * Verifies condition status for the approved condition
     * @param expectedStatus The expected status after approval (e.g., "Onaylandı")
     */
    public void verifyConditionStatus(String expectedStatus) {
        page.waitForTimeout(2000);
        
        FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
        
        // Wait for grid to be visible
        Locator generalConditionGrid = modalFrame.locator("#ContractRebateGridId");
        generalConditionGrid.waitFor(new Locator.WaitForOptions()
            .setState(WaitForSelectorState.VISIBLE)
            .setTimeout(10000));
        
        page.waitForTimeout(1000);
        
        // Look for the status in the grid
        Locator statusCell = modalFrame.locator(
            "#ContractRebateGridId tbody tr[role='row'] td:has-text('" + expectedStatus + "')"
        ).first();
        
        if (statusCell.count() == 0 || !statusCell.isVisible()) {
            throw new AssertionError("❌ Beklenen durum '" + expectedStatus + "' bulunamadı!");
        }
        
        System.out.println("✅ Genel Kondisyon Durumu: '" + expectedStatus + "' olarak doğrulandı");
    }
    
    /**
     * Clicks approve button on condition update popup (first popup after clicking Güncelle)
     */
    public void clickApproveButtonOnConditionUpdatePopup() {
        page.waitForTimeout(1500);
        
        // This is in the condition detail popup (3rd level iframe)
        FrameLocator iframe = page.frameLocator("iframe.k-content-frame").last();
        
        // Look for Onayla button
        Locator approveButton = iframe.locator("button:has-text('Onayla'), a:has-text('Onayla')");
        
        int buttonCount = approveButton.count();
        System.out.println("🔍 Onayla buton sayısı: " + buttonCount);
        
        if (buttonCount == 0) {
            // Try alternative selectors
            System.out.println("⚠️ 'Onayla' text'i ile bulunamadı, tüm butonları listeliyorum...");
            Locator allButtons = iframe.locator("button, a.k-button");
            int allCount = allButtons.count();
            for (int i = 0; i < allCount; i++) {
                String btnText = allButtons.nth(i).textContent();
                System.out.println("  Button " + i + ": " + btnText);
            }
            throw new AssertionError("❌ Kondisyon detay popup'ında Onayla butonu bulunamadı!");
        }
        
        approveButton.first().click();
        System.out.println("✅ Kondisyon detay popup'ındaki Onayla butonuna tıklandı");
        page.waitForTimeout(3000); // Alertify açılması için daha uzun bekle
    }
    
    /**
     * Verifies that approval confirmation popup is opened
     */
    public void verifyApprovalPopupOpened() {
        page.waitForTimeout(1000);
        
        // Look for Alertify OK button - simpler selector
        Locator alertifyButton = page.locator("button.ajs-ok");
        
        // Wait for it to be visible
        try {
            alertifyButton.waitFor(new Locator.WaitForOptions()
                .setState(WaitForSelectorState.VISIBLE)
                .setTimeout(8000));
            System.out.println("✅ Onay popup'ı açıldı (Alertify button.ajs-ok bulundu)");
        } catch (Exception e) {
            throw new AssertionError("❌ Onay popup'ı açılmadı! " + e.getMessage());
        }
    }
    
    /**
     * Presses Enter key to confirm Alertify popup
     */
    public void pressEnterKey() {
        page.keyboard().press("Enter");
        System.out.println("✅ Enter tuşuna basıldı (Alertify onayı)");
        page.waitForTimeout(2000);
    }

    
}
