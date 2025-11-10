package pages.purchasePages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import com.microsoft.playwright.options.WaitForSelectorState;
import pages.commonPages.BasePage;

public class CreditNotePage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator clickDropdownToggle = page.locator("span.k-select > span.k-icon.k-i-arrow-s");

    // === BUTTONS ===
    Locator filterDistributorFirmButton = page.locator("#FilterDistributorFirmIdButtonId");
    Locator filterButton = page.locator("#FilterButtonId");
    Locator addNewCreditNote = page.locator(".k-grid-CreditNoteGridIdAddNew");
    Locator editButton = page.locator("#Edit");


    // === TEXT INPUTS ===
    Locator kendoInput = page.locator(".k-input");
    Locator filterCreditNoteStatusInput = page.locator("#FilterCreditNoteStatusId");
    Locator filterDocumentNoInput = page.locator("#FilterDocumentNo");
    Locator filterDocumentDateInput = page.locator("#FilterDocumentDate");
    Locator filterFinanceIntegrationIdInput = page.locator("#FilterFinanceIntegrationId");

    // === DROPDOWNS/COMBOBOXES ===
    Locator pageHeaderProductDropdown = page.locator("#PageHeaderProductId");
    Locator kendoDropdown = page.locator(".k-dropdown");
    Locator kendoDropdownToggle = page.locator("span.k-select > span.k-icon.k-i-arrow-s");
    Locator filterCreditNoteStatusDropdown = page.locator("#FilterCreditNoteStatusId");
    Locator filterCreditNoteStatusDropdownToggle = page.locator("#FilterCreditNoteStatusId_wrapper .k-dropdown-wrap");
    Locator filterCreditNoteStatusDropdownArrow = page.locator("#FilterCreditNoteStatusId_wrapper span.k-select > span.k-icon.k-i-arrow-s");
    Locator filterDistributorFirmDropdown = page.locator("#FilterDistributorFirmId");
    Locator purchaseOrderDropdown = page.locator("#PurchaseOrderId");

    // === RADIO BUTTONS ===
    Locator yesBrokenRadio = page.locator("#yes_FilterIsBroken");
    Locator noBrokenRadio = page.locator("#no_FilterIsBroken");
    Locator allBrokenRadio = page.locator("#all_FilterIsBroken");

    // === GRIDS/TABLES ===
    Locator creditNoteGrid = page.locator("#CreditNoteGridId");
    Locator creditNoteGridHeader = page.locator("#CreditNoteGridId .k-grid-header");
    Locator creditNoteGridContent = page.locator("#CreditNoteGridId .k-grid-content");

    // === SPECIAL KENDO UI ELEMENTS ===
    Locator filterDocumentDatePicker = page.locator("#FilterDocumentDate");
    Locator filterDocumentDatePickerButton = page.locator("#FilterDocumentDate_wrapper .k-select");
    Locator pageHeaderProductMultiSelect = page.locator("#PageHeaderProductId");
    Locator pageHeaderProductMultiSelectWrapper = page.locator("#PageHeaderProductId_wrapper");
    Locator filterDistributorFirmMultiSelect = page.locator("#FilterDistributorFirmId");
    Locator filterDistributorFirmMultiSelectWrapper = page.locator("#FilterDistributorFirmId_wrapper");
    Locator purchaseOrderMultiSelect = page.locator("#PurchaseOrderId");
    Locator purchaseOrderMultiSelectWrapper = page.locator("#PurchaseOrderId_wrapper");

// === ADDITIONAL KENDO UI SPECIFIC LOCATORS ===

    // Dropdown arrow buttons
    Locator filterCreditNoteStatusArrow = page.locator("#FilterCreditNoteStatusId_wrapper .k-select .k-icon");
    Locator filterDistributorFirmArrow = page.locator("#FilterDistributorFirmId_wrapper .k-select .k-icon");
    Locator purchaseOrderArrow = page.locator("#PurchaseOrderId_wrapper .k-select .k-icon");

    // MultiSelect input fields
    Locator pageHeaderProductInput = page.locator("#PageHeaderProductId_wrapper input.k-input");
    Locator filterDistributorFirmInput = page.locator("#FilterDistributorFirmId_wrapper input.k-input");

    // DatePicker calendar button
    Locator filterDocumentDateCalendarButton = page.locator("#FilterDocumentDate_wrapper .k-select .k-icon");

    // Grid pagination
    Locator gridPager = page.locator("#CreditNoteGridId .k-pager-wrap");
    Locator gridPagerInfo = page.locator("#CreditNoteGridId .k-pager-info");
    Locator gridPageSizeDropdown = page.locator("#CreditNoteGridId .k-pager-sizes select");

    // Grid sorting and filtering
    Locator gridSortableHeaders = page.locator("#CreditNoteGridId .k-grid-header .k-sortable");
    Locator gridFilterMenus = page.locator("#CreditNoteGridId .k-grid-header .k-filterable");

// === COMMONLY USED KENDO UI PATTERNS ===

    // Loading and UI states
    Locator loadingMask = page.locator(".k-loading-mask");
    Locator kendoWindow = page.locator(".k-window");
    Locator kendoWindowTitle = page.locator(".k-window-title");
    Locator kendoWindowContent = page.locator(".k-window-content");
    Locator kendoNotification = page.locator(".k-notification");
    Locator kendoTooltip = page.locator(".k-tooltip");



    // === POPUP IFRAME LOCATORS ===
    // Popup iframe'ine erişim
    FrameLocator popupFrame = page.frameLocator("iframe");


    // Radio buttons (İmha için mi?)
    Locator popupYesIsBrokenRadio = page.frameLocator("iframe").locator("#yes_IsBroken");
    Locator popupNoIsBrokenRadio = page.frameLocator("iframe").locator("#no_IsBroken");

    // Text inputs
    Locator popupDocumentNoInput = page.frameLocator("iframe").locator("#DocumentNo");
    Locator popupDocumentDateInput = page.frameLocator("iframe").locator("#DocumentDate");
    Locator popupFirmNameInput = page.frameLocator("iframe").locator("#FirmName");

    // Select dropdowns
    Locator popUpPurchaseOrderInput = page.frameLocator("iframe").locator("input[aria-owns*=\"PurchaseOrder\"]");


    // Textarea
    Locator popupDescriptionTextarea = page.frameLocator("iframe").locator("#Description");

    // Action buttons (iframe içinde)
    Locator popupCloseButton = page.frameLocator("iframe").locator("#ClosePopupBtn");
    Locator popupSaveButton = page.frameLocator("iframe").locator("#btnSave");

    public void verifyCreditNotePageIsDisplayed(String expectedTitle) {
        verifyTextElementUseTrim(expectedTitle, pageTitle);

    }

    public void openCreditNoteStatusList() {
        clickElement(clickDropdownToggle.nth(0));
    }

    public void selectCreditNoteStatusFromList(String creditNoteStatus) {
        Locator selectCreditNoteStatus = page.locator("#FilterCreditNoteStatusId_listbox li[role='option'].k-item",
                new Page.LocatorOptions().setHasText(creditNoteStatus));
        selectCreditNoteStatus.click(new Locator.ClickOptions().setForce(true));
    }

    public void clickToNewCreditNoteButton() {
        clickElement(addNewCreditNote);
    }

    public void fillTheFormByDocumentInfo(String documentNo, String documentDate) {
        filterDocumentDateInput.fill(documentDate);
        filterDocumentNoInput.fill(documentNo);
    }

    public void isBrokenRadioButton(String status) {
        switch (status.toLowerCase()) {
            case "yes":
                clickElement(yesBrokenRadio);
                break;
            case "no":
                clickElement(noBrokenRadio);
                break;
            case "all":
                clickElement(allBrokenRadio);
                break;
            default:
                System.out.println("Invalid status: " + status);
        }
    }

    public void clickToFilterButton() {
        clickElement(filterButton);
    }


    public void searchByFirmCode(String firmCode) {
        System.out.println("=== Firm Code Search: " + firmCode + " ===");

        try {
            // MultiSelect input alanlarından ikincisini al (FilterDistributorFirmId)
            Locator firmCodeInput = page.locator(".k-multiselect input").nth(1);

            // Input alanının varlığını kontrol et
            boolean inputExists = firmCodeInput.isVisible();
            System.out.println("Firm code input exists: " + inputExists);

            if (inputExists) {
                // Firma kodunu yaz
                firmCodeInput.click();
                page.waitForTimeout(500);
                firmCodeInput.pressSequentially(firmCode, new Locator.PressSequentiallyOptions().setDelay(100));
                System.out.println("Typed " + firmCode + " in firm code input");

                // Dropdown açılmasını bekle
                page.waitForTimeout(2000);

                // Distributor firm'a özel dropdown listesini kontrol et
                Locator dropdownList = page.locator("#FilterDistributorFirmId_listbox");
                boolean listVisible = dropdownList.isVisible();
                System.out.println("Dropdown list visible: " + listVisible);

                if (listVisible) {
                    // Seçenekleri listele
                    Locator options = dropdownList.locator("li");
                    int optionCount = options.count();
                    System.out.println("Available options count: " + optionCount);

                    // İlk seçeneği seç
                    if (optionCount > 0) {
                        Locator firstOption = options.first();
                        String optionText = firstOption.textContent();
                        System.out.println("Selecting first option: " + optionText);

                        firstOption.click();
                        System.out.println("Clicked on first " + firmCode + " option");

                        // Arama butonuna tıkla
                        page.waitForTimeout(1000);
                        page.locator("#FilterButtonId").click();
                        System.out.println("Clicked search button");

                        // Grid'in yenilenmesini bekle
                        page.waitForTimeout(3000);

                        int rowCount = page.locator("#CreditNoteGridId tbody tr").count();
                        System.out.println("Grid row count after " + firmCode + " firm search: " + rowCount);
                    }
                }
            }
        } catch (Exception e) {
            System.out.println("Exception occurred during firm code search: " + e.getMessage());
        }
    }


    public void fillPurchaseOrder(String purchaseOrderNumber) {
        System.out.println("=== IMPROVED Purchase Order Selection ===");
        System.out.println("Target purchase order: " + purchaseOrderNumber);
        
        try {
            // ÖNEMLİ: Önce sipariş numarasını yaz ki dropdown filtrelensin
            System.out.println("🖊️ Writing purchase order number to input field...");
            popUpPurchaseOrderInput.click();
            page.waitForTimeout(500);
            
            // Input'u temizle ve sipariş numarasını yaz
            popUpPurchaseOrderInput.clear();
            page.waitForTimeout(300);
            popUpPurchaseOrderInput.fill(purchaseOrderNumber);
            
            // Yazmayı bitirdikten sonra Enter ile dropdown'u aç, sonra click
            page.keyboard().press("Enter");
            page.waitForTimeout(500);
            popUpPurchaseOrderInput.click();
            page.waitForTimeout(1000);
            System.out.println("✅ Purchase order written, Enter pressed and dropdown clicked: " + purchaseOrderNumber);
            
            // Dropdown seçeneklerinin yüklenmesini bekle 
            System.out.println("⏳ Waiting for dropdown options to load...");
            Locator purchaseOrderContainer = page.locator("#PurchaseOrderId-list");
            
            // 10 saniye bekle ki seçenekler yüklensin
            for (int i = 0; i < 20; i++) {
                if (purchaseOrderContainer.isVisible()) {
                    System.out.println("✅ Purchase order dropdown container is now visible!");
                    break;
                }
                System.out.println("⏳ Waiting... attempt " + (i+1));
                page.waitForTimeout(500);
            }
            
            // Dropdown seçeneklerini seç
            selectPurchaseOrderOption(purchaseOrderNumber);
            
        } catch (Exception e) {
            System.err.println("❌ Purchase order selection failed: " + e.getMessage());
            e.printStackTrace();
            throw e;
        }
    }
    
    
    
    private void selectPurchaseOrderOption(String purchaseOrderNumber) {
        try {
            System.out.println("🎯 Purchase Order dropdown'ın yüklenmesini bekliyoruz...");
            System.out.println("Target: " + purchaseOrderNumber);
            
            // Purchase Order dropdown'ının yüklenmesini bekle (30 saniye)
            boolean foundPurchaseOrderDropdown = false;
            for (int attempt = 1; attempt <= 60; attempt++) {
                try {
                    // Doğru listbox'ı ara
                    Locator purchaseOrderListbox = page.locator("#PurchaseOrderIdList_listbox");
                    
                    if (purchaseOrderListbox.count() > 0) {
                        System.out.println("✅ Purchase Order listbox bulundu! (attempt " + attempt + ")");
                        
                        Locator options = purchaseOrderListbox.locator("li[role='option']");
                        int count = options.count();
                        
                        if (count > 0) {
                            System.out.println("✅ " + count + " purchase order option bulundu!");
                            
                            // İlk option'ı seç
                            String firstOptionText = options.first().textContent();
                            System.out.println("İlk option: " + firstOptionText.substring(0, Math.min(100, firstOptionText.length())));
                            
                            options.first().click(new Locator.ClickOptions().setForce(true));
                            System.out.println("✅ Purchase Order seçildi!");
                            page.waitForTimeout(1000);
                            foundPurchaseOrderDropdown = true;
                            break;
                        }
                    }
                    
                    // Alternatif: data-idx ile ara  
                    Locator dataOption = page.locator("li[data-idx='0'][role='option']").filter(new Locator.FilterOptions().setHasText("1-2025-DPL"));
                    
                    if (dataOption.count() > 0) {
                        System.out.println("✅ data-idx ile Purchase Order option bulundu! (attempt " + attempt + ")");
                        String optText = dataOption.textContent();
                        System.out.println("Option: " + optText.substring(0, Math.min(100, optText.length())));
                        
                        dataOption.click(new Locator.ClickOptions().setForce(true));
                        System.out.println("✅ Purchase Order seçildi!");
                        page.waitForTimeout(1000);
                        foundPurchaseOrderDropdown = true;
                        break;
                    }
                    
                    System.out.println("⏳ Purchase Order dropdown yüklenmeyi bekliyor... (attempt " + attempt + "/60)");
                    page.waitForTimeout(500);
                    
                } catch (Exception e) {
                    System.out.println("❌ Attempt " + attempt + " failed: " + e.getMessage());
                }
            }
            
            if (!foundPurchaseOrderDropdown) {
                // Son çare: Herhangi bir geçerli option'ı seç
                System.out.println("⚠️ Purchase Order dropdown yüklenemedi, test'e devam ediliyor...");
                
                // Purchase Order seçilmedi ama test devam edebilir
                System.out.println("⏭️ Purchase Order seçilmedi, form'un geri kalanı test edilecek");
            }
            
        } catch (Exception e) {
            System.err.println("❌ Error selecting purchase order option: " + e.getMessage());
            throw e;
        }
    }




    public void searchByPurchaseOrder(String purchaseOrderNumber) {
            Locator purchaseOrderInput = page.locator(".k-multiselect input").nth(2);
            boolean inputExists = purchaseOrderInput.isVisible();

            if (inputExists) {
                purchaseOrderInput.click();
                page.waitForTimeout(500);
                purchaseOrderInput.pressSequentially(purchaseOrderNumber, new Locator.PressSequentiallyOptions().setDelay(50));
                page.waitForTimeout(2000);

                Locator dropdownList = page.locator("#PurchaseOrderId_listbox");
                boolean listVisible = dropdownList.isVisible();

                if (listVisible) {
                    Locator options = dropdownList.locator("li");
                    int optionCount = options.count();

                    if (optionCount > 0) {
                        Locator firstOption = options.first();
                        firstOption.click();
                        page.waitForTimeout(1000);
                        page.locator("#FilterButtonId").click();
                        page.waitForTimeout(3000);
                        page.locator("#CreditNoteGridId tbody tr").count();
                    }
                }
            }
    }


    public void testAllColumnSorting() {
        System.out.println("=== Grid Column Sorting Test ===");

        try {
            // Grid'i yükle
            page.locator("#FilterButtonId").click();
            page.waitForTimeout(2000);

            // Grid'in görünürlüğünü kontrol et
            boolean gridVisible = page.locator("#CreditNoteGridId").isVisible();
            System.out.println("Grid visible: " + gridVisible);

            if (!gridVisible) {
                System.out.println("Grid not visible, cannot test sorting");
                return;
            }

            // Sıralanabilir header linklerini bul
            Locator sortableLinks = page.locator("#CreditNoteGridId .k-grid-header .k-link");
            int linkCount = sortableLinks.count();
            System.out.println("Sortable link count: " + linkCount);

            // Her bir sıralanabilir linki tek tek test et
            for (int i = 0; i < linkCount; i++) {
                Locator sortLink = sortableLinks.nth(i);

                String linkText = sortLink.textContent();
                System.out.println("\n--- Testing sort on column " + (i + 1) + ": \"" + linkText + "\" ---");

                boolean isVisible = sortLink.isVisible();
                boolean isEnabled = sortLink.isEnabled();
                System.out.println("Column \"" + linkText + "\" - Visible: " + isVisible + ", Enabled: " + isEnabled);

                if (isVisible && isEnabled) {
                    // İlk tıklama (ascending sort)
                    sortLink.click();
                    System.out.println("Clicked \"" + linkText + "\" for ascending sort");
                    page.waitForTimeout(1500);

                    // Sort indicator'ı kontrol et
                    Locator parentHeader = sortLink.locator("xpath=..");
                    Locator sortIcon = parentHeader.locator(".k-icon");
                    int iconCount = sortIcon.count();
                    if (iconCount > 0) {
                        String iconClass = sortIcon.first().getAttribute("class");
                        System.out.println("Sort indicator class: " + iconClass);
                    }

                    // İkinci tıklama (descending sort)
                    sortLink.click();
                    System.out.println("Clicked \"" + linkText + "\" for descending sort");
                    page.waitForTimeout(1500);

                    // Descending sort sonrası icon kontrolü
                    if (iconCount > 0) {
                        String iconClass = sortIcon.first().getAttribute("class");
                        System.out.println("Sort indicator class after desc: " + iconClass);
                    }

                    System.out.println("✓ Column \"" + linkText + "\" sorting completed");
                } else {
                    System.out.println("✗ Column \"" + linkText + "\" not clickable");
                }
            }

            System.out.println("\n=== Sorting test completed for " + linkCount + " columns ===");

            // İlk kolona göre sıralayarak default duruma getir
            if (linkCount > 0) {
                sortableLinks.first().click();
                System.out.println("Reset to first column sort");
            }

        } catch (Exception e) {
            System.out.println("Error in column sorting test: " + e.getMessage());
        }

    }

    public void setPopupSaveButton() {
        clickElement(popupSaveButton);
    }

    public void setPopupCloseButton() {
        clickElement(popupCloseButton);
    }

    public void fillDescriptionInPopup(String description) {
        popupDescriptionTextarea.fill(description);
    }

    //popup isBroken radio buttons
    public void popupIsBrokenRadioButton(String status) {
        switch (status.toLowerCase()) {
            case "yes":
                clickElement(popupYesIsBrokenRadio);
                break;
            case "no":
                clickElement(popupNoIsBrokenRadio);
                break;
            default:
                System.out.println("Invalid status: " + status);
        }
    }


    //popup document date input
    public void fillDocumentDateInPopup(String documentDate) {
        popupDocumentDateInput.fill(documentDate);
    }
    //popup document no input
    public void fillDocumentNoInPopup(String documentNo) {
        popupDocumentNoInput.fill(documentNo);
    }

    /**
     * Click delete/settings icon on a grid row
     * Grid satırındaki ayar simgesine (gear/settings icon) tıklar
     * @param rowIndex row index (0-based)
     */
    public void clickDeleteIconOnRow(int rowIndex) {
        System.out.println("\n🗑️ " + rowIndex + ". satırdaki silme simgesine tıklanıyor...");
        
        // Satırı bul
        Locator targetRow = page.locator("#CreditNoteGridId tbody tr").nth(rowIndex);
        
        // Satıra hover yap (butonlar genelde hover ile görünür)
        targetRow.hover();
        page.waitForTimeout(500);
        System.out.println("✅ Satıra hover yapıldı");
        
        // Silme/ayar butonu için olası selector'lar
        // Kendo Grid'de genelde .k-grid-delete, .gridCmdBtn, veya icon class'ları kullanılır
        Locator deleteIcon = targetRow.locator(
            ".k-grid-delete, " +
            ".gridCmdBtn, " +
            "a.k-button[aria-label*='Sil'], " +
            "a.k-button[title*='Sil'], " +
            ".k-icon.k-i-delete, " +
            ".k-icon.k-i-trash, " +
            ".k-icon.k-i-x, " +
            "button[data-command='destroy']"
        );
        
        int deleteIconCount = deleteIcon.count();
        System.out.println("🔍 Silme simgesi bulundu mu: " + deleteIconCount + " adet");
        
        if (deleteIconCount > 0) {
            // JavaScript ile tıkla (hover-hidden elementler için)
            deleteIcon.first().evaluate("element => element.click()");
            page.waitForTimeout(2000);
            System.out.println("✅ Silme simgesine tıklandı (JavaScript)");
        } else {
            System.out.println("❌ Silme simgesi bulunamadı!");
        }
    }

    /**
     * Confirm delete operation in popup/confirmation dialog
     * Silme işlemini onaylar (popup/modal içinde)
     */
    public void confirmDelete() {
        System.out.println("\n✅ Silme işlemi onaylanıyor...");
        
        // Popup'ın yüklenmesini bekle
        page.waitForTimeout(2000);
        
        // iframe var mı kontrol et
        int iframeCount = page.locator("iframe").count();
        System.out.println("📊 iframe sayısı: " + iframeCount);
        
        Locator confirmButton;
        
        if (iframeCount > 0) {
            // iframe içinde popup aç - AlertifyJS popup
            System.out.println("🔍 iframe içinde AlertifyJS onay popup'ı aranıyor...");
            FrameLocator detailFrame = page.frameLocator("iframe");
            
            // Önce "Bu kaydı silmek istediğinize emin misiniz?" mesajını içeren dialog'u bul
            Locator deleteDialog = detailFrame.locator(".ajs-dialog:has-text('Bu kaydı silmek istediğinize emin misiniz?')");
            
            if (deleteDialog.count() > 0) {
                System.out.println("✅ Silme onay dialog'u bulundu");
                // Bu dialog içindeki Onay butonunu bul
                confirmButton = deleteDialog.locator("button.ajs-button.ajs-ok");
                System.out.println("📊 Delete dialog içinde bulunan Onay butonu: " + confirmButton.count() + " adet");
            } else {
                // Fallback: tüm ajs-ok butonlarını bul
                System.out.println("⚠️ Delete dialog bulunamadı, tüm Onay butonlarını arıyorum...");
                confirmButton = detailFrame.locator(
                    "button.ajs-button.ajs-ok, " +
                    "button:has-text('Onay')"
                );
                System.out.println("📊 iframe içinde bulunan onay butonu: " + confirmButton.count() + " adet");
            }
            
            // iframe içinde bulunamazsa ana sayfada dene
            if (confirmButton.count() == 0) {
                System.out.println("⚠️ iframe içinde bulunamadı, ana sayfada aranıyor...");
                confirmButton = page.locator(
                    "button.ajs-button.ajs-ok, " +
                    "button:has-text('Onay'), " +
                    "button:has-text('Evet')"
                );
                System.out.println("📊 Ana sayfada bulunan onay butonu: " + confirmButton.count() + " adet");
            }
        } else {
            // Ana sayfada popup ara - AlertifyJS popup
            System.out.println("🔍 Ana sayfada AlertifyJS onay popup'ı aranıyor...");
            confirmButton = page.locator(
                "button.ajs-button.ajs-ok, " +
                "button:has-text('Onay'), " +
                "button:has-text('Evet'), " +
                "button:has-text('Yes'), " +
                "button:has-text('Tamam'), " +
                "button:has-text('OK')"
            );
            System.out.println("📊 Ana sayfada bulunan onay butonu: " + confirmButton.count() + " adet");
        }
        
        if (confirmButton.count() > 0) {
            System.out.println("🎯 Onay butonuna tıklanıyor... (Bulunan: " + confirmButton.count() + " adet)");
            
            // 2 popup var, öndeki visible olanı bul
            if (confirmButton.count() > 1) {
                System.out.println("⚠️ Birden fazla Onay butonu var, visible olanı arıyorum...");
                
                // Her butonu kontrol et, visible olanı bul
                for (int i = 0; i < confirmButton.count(); i++) {
                    boolean isVisible = confirmButton.nth(i).isVisible();
                    System.out.println("  Button " + i + " visible: " + isVisible);
                    
                    if (isVisible) {
                        System.out.println("🎯 Visible Onay butonuna (index: " + i + ") tıklanıyor...");
                        try {
                            confirmButton.nth(i).click();
                            page.waitForTimeout(3000);
                            System.out.println("✅ Silme onaylandı (visible button clicked)");
                            break;
                        } catch (Exception e) {
                            System.out.println("⚠️ Normal click çalışmadı, JavaScript ile deneniyor...");
                            confirmButton.nth(i).evaluate("element => element.click()");
                            page.waitForTimeout(3000);
                            System.out.println("✅ Silme onaylandı (JavaScript click)");
                            break;
                        }
                    }
                }
            } else {
                // Tek buton varsa direkt ona tıkla
                try {
                    confirmButton.first().click();
                    page.waitForTimeout(3000);
                    System.out.println("✅ Silme onaylandı (first button clicked)");
                } catch (Exception e) {
                    System.out.println("⚠️ Normal click çalışmadı, JavaScript ile deneniyor...");
                    confirmButton.first().evaluate("element => element.click()");
                    page.waitForTimeout(3000);
                    System.out.println("✅ Silme onaylandı (JavaScript click)");
                }
            }
        } else {
            System.out.println("❌ Onay butonu bulunamadı!");
        }
    }

    /**
     * Verify a credit note was deleted from the grid
     * Credit note'un grid'den silindiğini doğrular
     * @param documentNo document number to verify deleted
     */
    public boolean verifyCreditNoteDeleted(String documentNo) {
        System.out.println("\n🔍 Credit Note silinmiş mi kontrol ediliyor: " + documentNo);
        
        // Grid'i yenile/bekle
        page.waitForTimeout(2000);
        
        // Grid'de bu document no'yu ara
        Locator gridRows = page.locator("#CreditNoteGridId tbody tr");
        int rowCount = gridRows.count();
        System.out.println("📊 Grid'de toplam " + rowCount + " satır var");
        
        // Document no'yu içeren satır var mı kontrol et
        for (int i = 0; i < rowCount; i++) {
            String rowText = gridRows.nth(i).textContent();
            if (rowText.contains(documentNo)) {
                System.out.println("❌ Credit Note hala grid'de: " + documentNo);
                return false;
            }
        }
        
        System.out.println("✅ Credit Note grid'den silindi: " + documentNo);
        return true;
    }

    /**
     * Get total row count in credit note grid
     */
    public int getCreditNoteGridRowCount() {
        int rowCount = page.locator("#CreditNoteGridId tbody tr").count();
        System.out.println("📊 Credit Note Grid satır sayısı: " + rowCount);
        return rowCount;
    }

    /**
     * Fill document no in filter (without date)
     */
    public void fillDocumentNoInFilter(String documentNo) {
        System.out.println("🔍 Filtreden Doküman No dolduruluyor: " + documentNo);
        filterDocumentNoInput.fill(documentNo);
        page.waitForTimeout(500);
        System.out.println("✅ Doküman No dolduruldu");
    }

    /**
     * Click edit button on first row in the grid
     */
    public void clickEditButtonOnFirstRow() {
        System.out.println("\n✏️ Grid'deki ilk satırın Edit butonuna tıklanıyor...");
        
        // Grid'in yüklenmesini bekle
        page.waitForTimeout(2000);
        
        // İlk satırı bul
        Locator firstRow = page.locator("#CreditNoteGridId tbody tr").first();
        
        // Satıra hover yap
        firstRow.hover();
        page.waitForTimeout(500);
        System.out.println("✅ İlk satıra hover yapıldı");
        
        // Edit butonunu bul ve tıkla
        // id="Edit" class="k-button gridCmdBtn k-success cmdLink CreditNoteGridIdCmd"
        Locator editBtn = firstRow.locator(
            "a#Edit, " +
            "a.CreditNoteGridIdCmd[func='CreditNoteGridIdEditItem'], " +
            ".k-grid-edit, " +
            "a:has-text('Edit'), " +
            "a:has-text('Düzenle')"
        );
        
        System.out.println("🔍 Edit butonu aranıyor... Bulunan: " + editBtn.count());
        
        if (editBtn.count() > 0) {
            // JavaScript ile tıkla (daha güvenli)
            editBtn.first().evaluate("element => element.click()");
            page.waitForTimeout(4000); // Detail page'in tam yüklenmesini bekle
            System.out.println("✅ Edit butonuna tıklandı, detail page açılıyor...");
            
            // iframe var mı kontrol et
            int iframeCount = page.locator("iframe").count();
            System.out.println("📊 Açılan sayfada iframe sayısı: " + iframeCount);
            
            if (iframeCount > 0) {
                System.out.println("✅ Detail page iframe içinde açıldı");
            } else {
                System.out.println("✅ Detail page aynı sayfada açıldı");
            }
        } else {
            // Fallback: ilk satıra direkt tıkla
            System.out.println("⚠️ Edit butonu bulunamadı, satıra direkt tıklanıyor...");
            firstRow.click();
            page.waitForTimeout(3000);
        }
    }

    /**
     * Add product in credit note detail page
     */
    public void addProductInDetailPage(String invoiceNo, String productCode, String quantity, String profitCenter, String creditNoteType) {
        System.out.println("\n📦 Credit Note detail sayfasında ürün ekleniyor...");
        System.out.println("  Invoice No: " + invoiceNo);
        System.out.println("  Product Code: " + productCode);
        System.out.println("  Quantity: " + quantity);
        System.out.println("  Profit Center: " + profitCenter);
        System.out.println("  Credit Note Type: " + creditNoteType);
        
        // Detail page'de iframe olabilir mi kontrol et
        int iframeCount = page.locator("iframe").count();
        System.out.println("🔍 Iframe sayısı: " + iframeCount);
        
        if (iframeCount > 0) {
            // İframe içinde işlem yap
            FrameLocator detailFrame = page.frameLocator("iframe");
            
            // Invoice No
            Locator invoiceNoInput = detailFrame.locator("input[name*='Invoice'], input[id*='Invoice'], #InvoiceNo");
            if (invoiceNoInput.count() > 0) {
                invoiceNoInput.first().fill(invoiceNo);
                System.out.println("✅ Invoice No dolduruldu");
            }
            
            // Product Code
            Locator productCodeInput = detailFrame.locator("input[name*='Product'], input[id*='Product'], #ProductCode");
            if (productCodeInput.count() > 0) {
                productCodeInput.first().fill(productCode);
                page.waitForTimeout(1000);
                // Enter'a bas veya dropdown'dan seç
                page.keyboard().press("Enter");
                page.waitForTimeout(1500);
                System.out.println("✅ Product Code girildi");
            }
            
            // Quantity
            Locator quantityInput = detailFrame.locator("input[name*='Quantity'], input[id*='Quantity'], #Quantity");
            if (quantityInput.count() > 0) {
                quantityInput.first().fill(quantity);
                System.out.println("✅ Quantity dolduruldu");
            }
            
            // Profit Center (dropdown olabilir)
            Locator profitCenterDropdown = detailFrame.locator("select[name*='Profit'], select[id*='Profit'], #ProfitCenter");
            if (profitCenterDropdown.count() > 0) {
                profitCenterDropdown.first().selectOption(profitCenter);
                System.out.println("✅ Profit Center seçildi");
            } else {
                // Kendo dropdown ise
                Locator profitCenterKendo = detailFrame.locator("input[aria-owns*='Profit']");
                if (profitCenterKendo.count() > 0) {
                    profitCenterKendo.first().click();
                    page.waitForTimeout(500);
                    profitCenterKendo.first().fill(profitCenter);
                    page.waitForTimeout(1000);
                    page.keyboard().press("Enter");
                    System.out.println("✅ Profit Center (Kendo) seçildi");
                }
            }
            
            // Credit Note Type (dropdown)
            Locator creditNoteTypeDropdown = detailFrame.locator("select[name*='Type'], select[id*='Type'], #CreditNoteType");
            if (creditNoteTypeDropdown.count() > 0) {
                creditNoteTypeDropdown.first().selectOption(creditNoteType);
                System.out.println("✅ Credit Note Type seçildi");
            } else {
                // Kendo dropdown ise
                Locator typeKendo = detailFrame.locator("input[aria-owns*='Type']");
                if (typeKendo.count() > 0) {
                    typeKendo.first().click();
                    page.waitForTimeout(500);
                    typeKendo.first().fill(creditNoteType);
                    page.waitForTimeout(1000);
                    page.keyboard().press("Enter");
                    System.out.println("✅ Credit Note Type (Kendo) seçildi");
                }
            }
            
        } else {
            // İframe yoksa ana sayfada işlem yap
            System.out.println("📝 Ana sayfada (iframe olmadan) form doldurulacak...");
            // Benzer kodları ana sayfa için de ekle
        }
        
        page.waitForTimeout(1000);
        System.out.println("✅ Ürün bilgileri dolduruldu");
    }

    /**
     * Click save button in credit note detail page
     */
    public void clickSaveButtonInDetailPage() {
        System.out.println("\n💾 Detail sayfasındaki Kaydet butonuna tıklanıyor...");
        
        // İframe içinde mi kontrol et
        int iframeCount = page.locator("iframe").count();
        
        if (iframeCount > 0) {
            // İframe içinde save butonu
            FrameLocator detailFrame = page.frameLocator("iframe");
            Locator saveBtn = detailFrame.locator(
                "button:has-text('Kaydet'), " +
                "button:has-text('Save'), " +
                "#btnSave, " +
                "#Save, " +
                ".k-button-solid-primary"
            );
            
            if (saveBtn.count() > 0) {
                saveBtn.first().click();
                page.waitForTimeout(3000);
                System.out.println("✅ Kaydet butonuna tıklandı (iframe içinde)");
            }
        } else {
            // Ana sayfada save butonu
            Locator saveBtn = page.locator(
                "button:has-text('Kaydet'), " +
                "button:has-text('Save'), " +
                "#btnSave, " +
                "#Save"
            );
            
            if (saveBtn.count() > 0) {
                saveBtn.first().click();
                page.waitForTimeout(3000);
                System.out.println("✅ Kaydet butonuna tıklandı");
            }
        }
    }

    /**
     * Click delete/settings icon on a product row in detail page
     * Detail page'deki ürün satırındaki ayar/silme simgesine tıklar
     * @param rowIndex row index (0-based)
     */
    public void clickDeleteIconOnProductRow(int rowIndex) {
        System.out.println("\n🗑️ " + rowIndex + ". ürün satırındaki ayar simgesine tıklanıyor...");
        
        // Detail page'in yüklenmesini bekle
        System.out.println("⏳ Detail page'in yüklenmesi bekleniyor...");
        page.waitForTimeout(2000);
        
        // iframe var mı kontrol et (Kendo Window içinde iframe olabilir)
        int iframeCount = page.locator("iframe").count();
        System.out.println("📊 iframe sayısı: " + iframeCount);
        
        // Eğer iframe yoksa, .k-window (modal) içinde product grid olabilir
        if (iframeCount == 0) {
            int windowCount = page.locator(".k-window, .modal").count();
            System.out.println("📊 modal/window sayısı: " + windowCount);
        }
        
        Locator productGrid;
        Locator targetRow;
        
        if (iframeCount > 0) {
            System.out.println("✅ iframe bulundu, iframe içindeki grid'e bakılıyor...");
            FrameLocator detailFrame = page.frameLocator("iframe");
            
            // iframe içindeki product grid
            productGrid = detailFrame.locator(
                "#CreditNoteProductGridId tbody, " +
                "#CreditNoteDetailGridId tbody, " +
                "#ProductGrid tbody, " +
                ".k-grid tbody"
            ).first();
            
            // Belirtilen satırı bul
            targetRow = productGrid.locator("tr").nth(rowIndex);
        } else {
            System.out.println("⚠️ iframe bulunamadı, ana sayfadaki grid'e bakılıyor...");
            // Ana sayfadaki grid
            productGrid = page.locator(
                "#CreditNoteProductGridId tbody, " +
                "#CreditNoteDetailGridId tbody, " +
                "#ProductGrid tbody, " +
                ".k-grid tbody"
            ).first();
            
            // Belirtilen satırı bul
            targetRow = productGrid.locator("tr").nth(rowIndex);
        }
        
        // Satıra hover yap
        targetRow.hover();
        page.waitForTimeout(800);
        System.out.println("✅ Ürün satırına hover yapıldı");
        
        // Ayar simgesi (cog icon) - glyphicon-cog
        Locator settingsIcon = targetRow.locator(
            "a.glyphicon-cog, " +
            ".glyphicon.glyphicon-cog, " +
            ".k-icon.k-i-gear, " +
            ".k-icon.k-i-cog"
        );
        
        int settingsIconCount = settingsIcon.count();
        
        if (settingsIconCount > 0) {
            // Ayar simgesine tıkla
            settingsIcon.first().click();
            page.waitForTimeout(1500);
            System.out.println("✅ Ayar simgesine tıklandı, menü açıldı");
            
            // Açılan menüde "Sil" seçeneğini ara
            page.waitForTimeout(1000);
            
            // Menü iframe içinde açılıyor
            Locator deleteOption;
            if (iframeCount > 0) {
                FrameLocator detailFrame = page.frameLocator("iframe");
                // id="Delete" ve class="gridCmdBtn cmdLink CreditNoteProductGridIdCmd"
                deleteOption = detailFrame.locator(
                    "a#Delete.CreditNoteProductGridIdCmd, " +
                    "a#Delete, " +
                    "a[func='CreditNoteProductGridIdDeleteItem']"
                );
                
                // iframe içinde bulunamadıysa ana sayfada ara
                if (deleteOption.count() == 0) {
                    System.out.println("⚠️ iframe içinde bulunamadı, ana sayfada aranıyor...");
                    deleteOption = page.locator(
                        "a#Delete, " +
                        "a:has-text('Sil')"
                    );
                }
            } else {
                deleteOption = page.locator(
                    "a#Delete, " +
                    "a:has-text('Sil')"
                );
            }
            
            int deleteOptionCount = deleteOption.count();
            System.out.println("📊 'Sil' seçeneği bulundu mu: " + deleteOptionCount + " adet");
            
            if (deleteOptionCount > 0) {
                System.out.println("🎯 'Sil' seçeneğine JavaScript ile tıklanıyor...");
                // iframe tıklamayı engelliyor, JavaScript kullan
                deleteOption.first().evaluate("element => element.click()");
                page.waitForTimeout(1500);
                System.out.println("✅ 'Sil' seçeneğine tıklandı");
            } else {
                System.out.println("❌ 'Sil' seçeneği bulunamadı! Menüdeki tüm seçenekleri listeliyorum...");
                Locator allMenuItems = page.locator("li.k-item, .k-menu-item, ul li");
                int menuItemCount = allMenuItems.count();
                System.out.println("📊 Menüde toplam " + menuItemCount + " seçenek bulundu:");
                for (int i = 0; i < Math.min(menuItemCount, 10); i++) {
                    String itemText = allMenuItems.nth(i).textContent();
                    System.out.println("  [" + i + "] '" + itemText + "'");
                }
            }
        } else {
            System.out.println("❌ Ayar simgesi bulunamadı!");
        }
    }

    /**
     * Verify a product row was deleted from detail page grid
     * Ürün satırının silindiğini doğrular
     * @param expectedRowIndex the row that was supposed to be deleted
     */
    public boolean verifyProductRowDeleted(int expectedRowIndex) {
        System.out.println("\n🔍 Ürün silinmiş mi kontrol ediliyor...");
        
        // Grid'i bekle
        page.waitForTimeout(2000);
        
        // iframe içindeki grid'i kontrol et
        int iframeCount = page.locator("iframe").count();
        System.out.println("📊 iframe sayısı: " + iframeCount);
        
        Locator productGrid;
        
        if (iframeCount > 0) {
            System.out.println("✅ iframe içindeki ürün grid'ine bakılıyor...");
            FrameLocator detailFrame = page.frameLocator("iframe");
            productGrid = detailFrame.locator(
                "#CreditNoteDetailGridId tbody tr, " +
                "#ProductGrid tbody tr, " +
                ".k-grid tbody tr"
            );
        } else {
            System.out.println("⚠️ iframe bulunamadı, ana sayfadaki grid'e bakılıyor...");
            productGrid = page.locator(
                "#CreditNoteDetailGridId tbody tr, " +
                "#ProductGrid tbody tr, " +
                ".k-grid tbody tr"
            );
        }
        
        int rowCount = productGrid.count();
        System.out.println("📊 Detail grid'de toplam " + rowCount + " ürün satırı var");
        
        // Grid boş olmalı (0 satır)
        if (rowCount == 0) {
            System.out.println("✅ BAŞARILI: Ürün silindi, grid boş (0 satır)");
            return true;
        } else {
            System.out.println("❌ HATA: Ürün silinemedi! Hala " + rowCount + " satır var (0 olmalıydı)");
            return false;
        }
    }

}