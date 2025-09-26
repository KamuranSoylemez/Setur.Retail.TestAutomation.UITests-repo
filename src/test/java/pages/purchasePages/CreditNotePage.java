package pages.purchasePages;

import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
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
    Locator purchaseOrderInput = page.locator("#PurchaseOrderId_wrapper input.k-input");

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


    public int testAllColumnSorting() {
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
                return 0;
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

            return linkCount;

        } catch (Exception e) {
            System.out.println("Error in column sorting test: " + e.getMessage());
            return -1;
        }

    }

}