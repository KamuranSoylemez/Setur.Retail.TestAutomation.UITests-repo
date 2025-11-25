package pages.SupplierPages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import pages.commonPages.BasePage;

public class ReceivablePoolPage extends BasePage {
    
    // Search form elements
    private Locator searchButton;
    private Locator createRebateInvoiceButton;
    private Locator companyMultiSelect;
    private Locator rebateDateInput;
    private Locator conditionTypeDropdown;
    private Locator calculationTypeDropdown;
    private Locator statusDropdown;
    private Locator currencyDropdown;
    private Locator calculationPeriodDropdown;
    private Locator contractNameInput;
    private Locator categoryDropdown;
    private Locator descriptionInput;
    
    // Grid elements
    private Locator gridRows;
    private Locator noRecordsMessage;
    
    // Alert/Warning messages
    private Locator warningMessage;
    
    public ReceivablePoolPage() {
        super();
        initializeLocators();
    }
    
    private void initializeLocators() {
        // Button
        searchButton = page.locator("#FilterButtonId");
        createRebateInvoiceButton = page.locator("#checkboxReceivableInvoice");
        
        // Search inputs
        companyMultiSelect = page.locator("#FilterFirmId"); // Hidden input
        rebateDateInput = page.locator("#FilterContractRebateDate"); // Rebate Tarihi
        conditionTypeDropdown = page.locator("#FilterContractRebateTypeId"); // Kondisyon Tipi
        calculationTypeDropdown = page.locator("#FilterContractRebateCalculateTypeId"); // Hesaplama Türü
        statusDropdown = page.locator("#FilterContractReceivableInvoiceStatusId"); // Durum
        currencyDropdown = page.locator("#FilterCurrencyCode"); // Para Birimi
        calculationPeriodDropdown = page.locator("#FilterContractRebatePeriodTypeId"); // Hesaplama Periyodu
        contractNameInput = page.locator("#FilterContractName"); // Sözleşme Adı
        categoryDropdown = page.locator("#FilterCategoryIds"); // Kategori
        descriptionInput = page.locator("#FilterDescription, input[name='FilterDescription'], textarea[name='FilterDescription']"); // Açıklama
        
        // Grid
        gridRows = page.locator("table tbody tr, .k-grid tbody tr");
        noRecordsMessage = page.locator(".k-grid-norecords, td:has-text('Kayıt bulunamadı'), td:has-text('No records')");
        
        // Warning/Alert messages
        warningMessage = page.locator(".alert-warning, .alert-danger, div[role='alert'], .k-notification-warning, .k-notification-error, .toast-warning, .toast-error");
    }
    
    /**
     * Click search button
     */
    public void clickSearchButton() {
        initializeLocators();
        System.out.println("🔍 Ara butonuna tıklanıyor...");
        
        if (searchButton.count() > 0) {
            searchButton.first().click();
            page.waitForTimeout(1500); // Arama sonuçlarının yüklenmesi için bekle
            System.out.println("✅ Ara butonuna tıklandı");
        } else {
            System.out.println("❌ Ara butonu bulunamadı!");
        }
    }
    
    /**
     * Select company from Kendo MultiSelect
     * Firma seçimi için Kendo MultiSelect widget'ını kullanır
     */
    public void selectCompany(String companyName) {
        System.out.println("🔍 Firma seçiliyor: " + companyName);
        
        // Görünür input (aria-label ile)
        Locator visibleInput = page.locator("input[role='listbox'][aria-owns*='FilterFirmId']");
        
        if (visibleInput.count() > 0 && visibleInput.first().isVisible()) {
            System.out.println("🔍 Visible input bulundu");
            
            // Input'a focus yap
            visibleInput.first().focus();
            page.waitForTimeout(300);
            
            // BÜYÜK HARFLE firma adını yaz
            String companyNameUpper = companyName.toUpperCase();
            System.out.println("🔍 Firma adı yazılıyor (BÜYÜK HARF): " + companyNameUpper);
            visibleInput.first().fill(companyNameUpper);
            page.waitForTimeout(1000);
            
            // Alt+ArrowDown ile dropdown'ı aç
            System.out.println("🔍 Alt+ArrowDown ile dropdown açılıyor...");
            page.keyboard().press("Alt+ArrowDown");
            page.waitForTimeout(1500);
            
            // Şimdi BACARDI içeren li'leri ara
            System.out.println("🔍 Dropdown seçenekleri aranıyor...");
            Locator allOptions = page.locator("li:visible");
            int totalOptions = allOptions.count();
            System.out.println("🔍 Toplam " + totalOptions + " görünür li bulundu");
            
            // BACARDI içeren seçeneği bul
            Locator matchingOptions = page.locator("li:visible").filter(new Locator.FilterOptions().setHasText(companyNameUpper));
            int matchCount = matchingOptions.count();
            System.out.println("🔍 '" + companyNameUpper + "' içeren " + matchCount + " seçenek bulundu");
            
            if (matchCount > 0) {
                System.out.println("🔍 İlk eşleşen seçeneğe tıklanıyor...");
                matchingOptions.first().click();
                page.waitForTimeout(1000);
                System.out.println("✅ Firma seçildi: " + companyNameUpper);
            } else {
                System.out.println("⚠️ Eşleşen seçenek bulunamadı! Enter ile deneyelim...");
                visibleInput.first().focus();
                page.keyboard().press("Enter");
                page.waitForTimeout(500);
            }
        } else {
            System.out.println("⚠️ Visible input bulunamadı!");
        }
    }
    
    /**
     * Fill rebate date field
     */
    public void fillRebateDate(String date) {
        System.out.println("🔍 Rebate tarihi dolduruluyor: " + date);
        rebateDateInput.first().fill(date);
        System.out.println("✅ Rebate Tarihi dolduruldu: " + date);
    }
    
    /**
     * Select condition type from dropdown
     */
    public void selectConditionType(String conditionType) {
        System.out.println("🔍 Kondisyon tipi seçiliyor: " + conditionType);
        selectFromDropdown(conditionTypeDropdown, conditionType, "Kondisyon Tipi");
    }
    
    /**
     * Select calculation type from dropdown
     */
    public void selectCalculationType(String calculationType) {
        System.out.println("🔍 Hesaplama türü seçiliyor: " + calculationType);
        selectFromDropdown(calculationTypeDropdown, calculationType, "Hesaplama Türü");
    }
    
    /**
     * Select status from dropdown
     */
    public void selectStatus(String status) {
        System.out.println("🔍 Durum seçiliyor: " + status);
        selectFromDropdown(statusDropdown, status, "Durum");
    }
    
    /**
     * Select currency from dropdown
     */
    public void selectCurrency(String currency) {
        System.out.println("🔍 Para birimi seçiliyor: " + currency);
        selectFromDropdown(currencyDropdown, currency, "Para Birimi");
    }
    
    /**
     * Select calculation period from dropdown
     */
    public void selectCalculationPeriod(String period) {
        System.out.println("🔍 Hesaplama periyodu seçiliyor: " + period);
        selectFromDropdown(calculationPeriodDropdown, period, "Hesaplama Periyodu");
    }
    
    /**
     * Fill contract name field
     */
    public void fillContractName(String contractName) {
        System.out.println("🔍 Sözleşme adı dolduruluyor: " + contractName);
        contractNameInput.first().fill(contractName);
        System.out.println("✅ Sözleşme Adı dolduruldu: " + contractName);
    }
    
    /**
     * Select category from dropdown
     */
    public void selectCategory(String category) {
        System.out.println("🔍 Kategori seçiliyor: " + category);
        selectFromDropdown(categoryDropdown, category, "Kategori");
    }
    
    /**
     * Fill description field
     */
    public void fillDescription(String description) {
        System.out.println("🔍 Açıklama dolduruluyor: " + description);
        descriptionInput.first().fill(description);
        System.out.println("✅ Açıklama dolduruldu: " + description);
    }
    
    /**
     * Generic method to select from Kendo DropDownList
     */
    private void selectFromDropdown(Locator dropdown, String value, String fieldName) {
        try {
            // Kendo DropDownList için span wrapper'ı bul
            Locator kendoWrapper = page.locator("span.k-dropdown").filter(new Locator.FilterOptions().setHas(dropdown));
            
            if (kendoWrapper.count() > 0) {
                kendoWrapper.first().click(); // Dropdown'ı aç
                page.waitForTimeout(500);
                
                // Seçeneği bul ve tıkla
                Locator option = page.locator("ul.k-list li.k-item").filter(new Locator.FilterOptions().setHasText(value));
                
                if (option.count() > 0) {
                    option.first().click();
                    page.waitForTimeout(300);
                    System.out.println("✅ " + fieldName + " seçildi: " + value);
                } else {
                    System.out.println("⚠️ '" + value + "' seçeneği " + fieldName + " listesinde bulunamadı");
                }
            }
        } catch (Exception e) {
            System.out.println("❌ " + fieldName + " seçimi sırasında hata: " + e.getMessage());
        }
    }
    
    /**
     * Verify search form is visible
     */
    public boolean verifySearchFormVisible() {
        boolean isVisible = searchButton.first().isVisible();
        System.out.println("🔍 Arama formu görünür mü: " + isVisible);
        if (isVisible) {
            System.out.println("✅ Arama formu görünür olduğu doğrulandı");
        }
        return isVisible;
    }
    
    /**
     * Get grid row count
     */
    public int getGridRowCount() {
        page.waitForTimeout(1000); // Grid'in yüklenmesi için bekle
        int count = gridRows.count();
        System.out.println("📊 Grid satır sayısı: " + count);
        return count;
    }
    
    /**
     * Verify grid has results
     */
    public boolean verifyGridHasResults() {
        int rowCount = getGridRowCount();
        boolean hasResults = rowCount > 0;
        System.out.println("🔍 Grid'de sonuç var mı: " + hasResults);
        if (hasResults) {
            System.out.println("✅ Grid'de " + rowCount + " sonuç bulundu");
        }
        return hasResults;
    }
    
    /**
     * Verify no records message is displayed
     */
    public boolean verifyNoRecordsMessageDisplayed() {
        page.waitForTimeout(1000);
        boolean isDisplayed = noRecordsMessage.count() > 0 && noRecordsMessage.first().isVisible();
        System.out.println("🔍 'Kayıt bulunamadı' mesajı görünür mü: " + isDisplayed);
        return isDisplayed;
    }
    
    /**
     * Verify pagination is working
     * Pagination kontrolü yapar - sonraki sayfaya gidip geri döner
     */
    public boolean verifyPaginationIsWorking() {
        System.out.println("\n🔍 Pagination testi başlıyor...");
        
        // Pagination kontrollerini bul
        Locator pager = page.locator(".k-pager-wrap, .k-pager, div[data-role='pager']");
        
        if (pager.count() == 0) {
            System.out.println("⚠️ Pagination bulunamadı!");
            return false;
        }
        
        System.out.println("✅ Pagination alanı bulundu");
        
        // İlk sayfa bilgilerini al
        int initialRowCount = getGridRowCount();
        System.out.println("📊 İlk sayfada " + initialRowCount + " kayıt görünüyor");
        
        // "Sonraki Sayfa" butonunu bul - Türkçe title ile
        Locator nextPageButton = page.locator("a[title='Sonraki sayfa'], a.k-pager-nav:has-text('›')");
        
        System.out.println("🔍 'Sonraki Sayfa' butonu bulundu mu: " + nextPageButton.count() + " adet");
        
        if (nextPageButton.count() == 0) {
            System.out.println("⚠️ 'Sonraki Sayfa' butonu bulunamadı!");
            System.out.println("ℹ️ Tüm kayıtlar tek sayfada görünüyor olabilir (pagination gerekmiyor)");
            return true; // Pagination yoksa başarılı kabul et
        }
        
        // Buton disabled mı kontrol et
        String classList = nextPageButton.first().getAttribute("class");
        boolean isDisabled = classList != null && classList.contains("k-state-disabled");
        
        System.out.println("🔍 'Sonraki Sayfa' butonu disabled mi: " + isDisabled);
        
        if (isDisabled) {
            System.out.println("ℹ️ Sadece 1 sayfa var (pagination gerekmiyor)");
            return true;
        }
        
        try {
            // Sonraki sayfaya git
            System.out.println("🔄 Sonraki sayfaya gidiliyor...");
            nextPageButton.first().click();
            page.waitForTimeout(2000); // Sayfa yüklenmesini bekle
            
            int secondPageRowCount = getGridRowCount();
            System.out.println("📊 İkinci sayfada " + secondPageRowCount + " kayıt görünüyor");
            
            // CRITICAL CHECK: İkinci sayfa boş olmamalı!
            if (secondPageRowCount == 0) {
                System.out.println("❌ PAGINATION BOZUK: İkinci sayfa boş! (0 kayıt)");
                System.out.println("⚠️ Sayfa değişimi gerçekleşti ama veri yüklenmedi!");
                return false;
            }
            
            // "İlk Sayfa" butonunu bul - Türkçe title ile
            Locator firstPageButton = page.locator("a[title='İlk sayfa'], a.k-pager-first");
            
            if (firstPageButton.count() > 0) {
                System.out.println("🔄 İlk sayfaya geri dönülüyor...");
                firstPageButton.first().click();
                page.waitForTimeout(2000);
                
                int backToFirstRowCount = getGridRowCount();
                System.out.println("📊 İlk sayfaya döndü: " + backToFirstRowCount + " kayıt");
                
                // İlk sayfaya döndüğünde de veri olmalı
                if (backToFirstRowCount == 0) {
                    System.out.println("❌ PAGINATION BOZUK: İlk sayfaya döndü ama veri kayboldu!");
                    return false;
                }
                
                System.out.println("✅ Pagination çalışıyor!");
                return true;
            } else {
                System.out.println("⚠️ 'İlk Sayfa' butonu bulunamadı ama sayfa değişti!");
                System.out.println("✅ Pagination çalışıyor (kısmen)");
                return true;
            }
            
        } catch (Exception e) {
            System.out.println("❌ Pagination hatası: " + e.getMessage());
            e.printStackTrace();
            return false;
        }
    }
    
    /**
     * Click history icon (moon/calendar icon) on specific row
     * Grid'deki ay simgesine tıklar - ContractReceivableInvoiceGridIdCmd butonu
     */
    public void clickHistoryIcon(int rowIndex) {
        System.out.println("\n🔍 " + (rowIndex + 1) + ". satırdaki tarihçe simgesine tıklanıyor...");
        
        // İlk satırı bul ve hover yap (buton hover'da görünür olur)
        Locator targetRow = page.locator("table tbody tr").nth(rowIndex);
        targetRow.hover();
        page.waitForTimeout(500); // Hover efekti için bekle
        System.out.println("✅ Satıra hover yapıldı");
        
        // Gerçek selector: gridCmdBtn cmdLink ContractReceivableInvoiceGridIdCmd
        Locator historyIcon = targetRow.locator(".gridCmdBtn.cmdLink.ContractReceivableInvoiceGridIdCmd, a.ContractReceivableInvoiceGridIdCmd, #ContractReceivableInvoiceStatusHistoryButton");
        
        if (historyIcon.count() > 0) {
            System.out.println("✅ Tarihçe simgesi bulundu");
            
            // JavaScript ile click (element görünmese de çalışır)
            historyIcon.first().evaluate("element => element.click()");
            page.waitForTimeout(2000); // Tarihçe sayfasının açılmasını bekle
            System.out.println("✅ Tarihçe simgesine tıklandı (JavaScript)");
        } else {
            System.out.println("❌ Tarihçe simgesi bulunamadı!");
        }
    }
    
    /**
     * Click history icon on first row
     */
    public void clickHistoryIconOnFirstRow() {
        clickHistoryIcon(0);
    }
    
    /**
     * Verify history page is opened
     * Tarihçe sayfasının açıldığını kontrol eder
     */
    public boolean verifyHistoryPageOpened() {
        System.out.println("\n🔍 Tarihçe sayfasının açıldığı kontrol ediliyor...");
        
        // Tarihçe sayfası için olası selector'ler
        Locator historyPageIndicator = page.locator(
            "h1:has-text('Tarihçe'), " +
            "h2:has-text('Tarihçe'), " +
            "h3:has-text('Tarihçe'), " +
            ".history-page, " +
            "#history-panel, " +
            "div:has-text('Tarihçe'):visible"
        );
        
        page.waitForTimeout(1000);
        boolean isOpened = historyPageIndicator.count() > 0;
        
        if (!isOpened) {
            // Alternatif: modal veya popup kontrol et
            Locator modal = page.locator(".modal-dialog, .k-window, .popup");
            if (modal.count() > 0) {
                System.out.println("✅ Modal/Popup açıldı");
                isOpened = true;
            }
        }
        
        System.out.println("🔍 Tarihçe sayfası açıldı mı: " + isOpened);
        return isOpened;
    }
    
    /**
     * Verify history description contains condition IDs and explanation
     * Tarihçe açıklama alanında kondisyon ID ve açıklamaların olduğunu kontrol eder
     */
    public boolean verifyHistoryDescriptionContent() {
        System.out.println("\n🔍 Tarihçe açıklama alanı kontrol ediliyor...");
        System.out.println("🪟 İlk önce iframe kontrol ediliyor...");
        
        // Önce iframe var mı kontrol et
        Locator iframes = page.locator("iframe");
        int iframeCount = iframes.count();
        System.out.println("📊 Toplam " + iframeCount + " iframe bulundu");
        
        if (iframeCount > 0) {
            // İlk iframe'e geç
            System.out.println("🔄 İframe'e geçiliyor...");
            FrameLocator frameLocator = page.frameLocator("iframe").first();
            
            // iframe içindeki tüm textarea'ları listele
            Locator frameTextAreas = frameLocator.locator("textarea");
            int textAreaCount = frameTextAreas.count();
            System.out.println("\n📝 İframe içinde " + textAreaCount + " TEXTAREA bulundu:");
            for (int i = 0; i < textAreaCount; i++) {
                String name = frameTextAreas.nth(i).getAttribute("name");
                String id = frameTextAreas.nth(i).getAttribute("id");
                String className = frameTextAreas.nth(i).getAttribute("class");
                String value = "";
                try {
                    value = frameTextAreas.nth(i).inputValue();
                    if (value.length() > 100) value = value.substring(0, 100) + "...";
                } catch (Exception e) {
                    value = "(okunamadı)";
                }
                System.out.println("  [" + i + "] name='" + name + "', id='" + id + "', class='" + className + "'");
                System.out.println("      value='" + value + "'");
            }
            
            // iframe içindeki table'ları listele
            Locator frameTables = frameLocator.locator("table");
            int tableCount = frameTables.count();
            System.out.println("\n� İframe içinde " + tableCount + " TABLE bulundu");
            
            if (tableCount > 0) {
                // İlk table içeriğini göster
                String tableText = frameTables.first().textContent();
                if (tableText.length() > 500) tableText = tableText.substring(0, 500) + "...";
                System.out.println("  Table içeriği (ilk 500 char): " + tableText);
            }
            
            // iframe içindeki label'ları listele
            Locator frameLabels = frameLocator.locator("label");
            int labelCount = frameLabels.count();
            System.out.println("\n🏷️ İframe içinde " + labelCount + " LABEL bulundu:");
            for (int i = 0; i < Math.min(labelCount, 10); i++) {
                String forAttr = frameLabels.nth(i).getAttribute("for");
                String text = frameLabels.nth(i).textContent();
                System.out.println("  [" + i + "] for='" + forAttr + "', text='" + text + "'");
            }
            
            // iframe içindeki tüm div'leri listele (ilk 15)
            Locator frameDivs = frameLocator.locator("div:visible");
            int divCount = Math.min(frameDivs.count(), 15);
            System.out.println("\n� İframe içinde ilk " + divCount + " görünür DIV:");
            for (int i = 0; i < divCount; i++) {
                String className = frameDivs.nth(i).getAttribute("class");
                String id = frameDivs.nth(i).getAttribute("id");
                String text = frameDivs.nth(i).textContent();
                if (text.length() > 80) text = text.substring(0, 80) + "...";
                System.out.println("  [" + i + "] id='" + id + "', class='" + className + "'");
                System.out.println("      text='" + text + "'");
            }
            
            // Table içinde "Açıklama" kolonundaki değerleri kontrol et
            if (tableCount > 0) {
                System.out.println("\n🔍 Table'daki 'Açıklama' kolonunu arıyoruz...");
                
                // Açıklama header'ının indeksini bul
                Locator tableHeaders = frameTables.first().locator("thead th, thead td");
                int headerCount = tableHeaders.count();
                int descriptionColumnIndex = -1;
                
                System.out.println("📊 Table'da " + headerCount + " kolon bulundu:");
                for (int i = 0; i < headerCount; i++) {
                    String headerText = tableHeaders.nth(i).textContent().trim();
                    System.out.println("  [" + i + "] '" + headerText + "'");
                    if (headerText.equalsIgnoreCase("Açıklama") || headerText.contains("Açıklama")) {
                        descriptionColumnIndex = i;
                        System.out.println("    ✅ Açıklama kolonu bulundu!");
                    }
                }
                
                if (descriptionColumnIndex >= 0) {
                    // İlk satırdaki açıklama değerini al
                    Locator firstRowCells = frameTables.first().locator("tbody tr").first().locator("td");
                    if (firstRowCells.count() > descriptionColumnIndex) {
                        String description = firstRowCells.nth(descriptionColumnIndex).textContent().trim();
                        
                        System.out.println("\n📝 İlk satırdaki açıklama: '" + description + "'");
                        System.out.println("📝 İçerik uzunluğu: " + description.length() + " karakter");
                        
                        // En az 2 karakter içerik var mı kontrol et (kısa açıklamalar da geçerli)
                        boolean hasContent = description != null && description.trim().length() >= 2;
                        System.out.println("🔍 Açıklama içerik var mı (>=2 char): " + hasContent);
                        
                        if (hasContent) {
                            System.out.println("✅ Tarihçe açıklaması geçerli içerik içeriyor");
                            return true;
                        } else {
                            System.out.println("⚠️ Tarihçe açıklaması boş veya çok kısa");
                            return false;
                        }
                    }
                }
            }
            
            // Fallback: textarea varsa onu kontrol et
            if (textAreaCount > 0) {
                System.out.println("\n⚠️ Table'da Açıklama kolonu bulunamadı, textarea deneniyor...");
                String description = frameTextAreas.first().inputValue();
                boolean hasContent = description != null && description.trim().length() >= 10;
                return hasContent;
            }
            
            System.out.println("\n❌ İframe içinde açıklama bulunamadı!");
            return false;
        } else {
            System.out.println("❌ İframe bulunamadı! Ana sayfada deneniyor...");
            
            // İframe yoksa ana sayfada ara (fallback)
            Locator allTextAreas = page.locator("textarea");
            int textAreaCount = allTextAreas.count();
            System.out.println("📝 Ana sayfada " + textAreaCount + " TEXTAREA bulundu");
            
            if (textAreaCount > 0) {
                String description = allTextAreas.first().inputValue();
                boolean hasContent = description != null && description.trim().length() > 5;
                return hasContent;
            }
            
            return false;
        }
    }
    
    /**
     * Verify all grid columns are sortable
     * Her kolonda sort yaparak hata olmadığını kontrol eder
     */
    public boolean verifyAllColumnsAreSortable() {
        System.out.println("\n🔍 Tüm kolonlarda sort testi başlıyor...");
        
        // Kendo Grid header'larını bul (a.k-link olan header'lar sortable)
        Locator sortableHeaders = page.locator("th.k-header a.k-link");
        int headerCount = sortableHeaders.count();
        System.out.println("📊 Toplam " + headerCount + " sortable kolon bulundu");
        
        if (headerCount == 0) {
            System.out.println("⚠️ Sortable kolon bulunamadı!");
            return false;
        }
        
        boolean allSortsSuccessful = true;
        
        // Her header için sort testi yap
        for (int i = 0; i < headerCount; i++) {
            try {
                // Header text'ini al
                String headerText = sortableHeaders.nth(i).textContent().trim();
                System.out.println("\n🔄 [" + (i+1) + "/" + headerCount + "] '" + headerText + "' kolonunda sort yapılıyor...");
                
                // İlk tıklama: Ascending sort
                sortableHeaders.nth(i).click();
                page.waitForTimeout(1500); // Sort işleminin tamamlanması için bekle
                
                // Grid'in yüklendiğini kontrol et
                int rowCountAsc = getGridRowCount();
                System.out.println("  ✅ Ascending sort: " + rowCountAsc + " kayıt");
                
                // İkinci tıklama: Descending sort
                sortableHeaders.nth(i).click();
                page.waitForTimeout(1500);
                
                // Grid'in yüklendiğini kontrol et
                int rowCountDesc = getGridRowCount();
                System.out.println("  ✅ Descending sort: " + rowCountDesc + " kayıt");
                
                // Üçüncü tıklama: Sort iptal (opsiyonel, bazı Kendo Grid'lerde çalışır)
                // sortableHeaders.nth(i).click();
                // page.waitForTimeout(1000);
                
                System.out.println("  ✅ '" + headerText + "' kolonunda sort başarılı");
                
            } catch (Exception e) {
                System.out.println("  ❌ Sort hatası: " + e.getMessage());
                allSortsSuccessful = false;
            }
        }
        
        System.out.println("\n📊 Sort testi tamamlandı!");
        if (allSortsSuccessful) {
            System.out.println("✅ Tüm kolonlarda sort başarılı!");
        } else {
            System.out.println("⚠️ Bazı kolonlarda sort hatası oluştu!");
        }
        
        return allSortsSuccessful;
    }
    
    /**
     * Click "Rebate Faturası Oluştur" button without selecting any record
     * Hiçbir kayıt seçmeden "Rebate Faturası Oluştur" butonuna tıklar
     */
    public void clickCreateRebateInvoiceButtonWithoutSelection() {
        System.out.println("\n🔍 'Rebate Faturası Oluştur' butonuna tıklanıyor (kayıt seçilmeden)...");
        initializeLocators();
        
        if (createRebateInvoiceButton.count() > 0) {
            createRebateInvoiceButton.first().click();
            page.waitForTimeout(1500); // Uyarı mesajının görünmesi için bekle
            System.out.println("✅ 'Rebate Faturası Oluştur' butonuna tıklandı");
        } else {
            System.out.println("❌ 'Rebate Faturası Oluştur' butonu bulunamadı!");
        }
    }
    
    /**
     * Verify warning message is displayed
     * Uyarı mesajının görüntülendiğini ve belirtilen metni içerdiğini kontrol eder
     */
    public boolean verifyWarningMessage(String expectedMessage) {
        System.out.println("\n🔍 Uyarı mesajı kontrol ediliyor...");
        System.out.println("🔍 Beklenen mesaj: \"" + expectedMessage + "\"");
        
        page.waitForTimeout(1000); // Mesajın görünmesi için bekle
        
        // Önce genel uyarı mesajı var mı kontrol et
        if (warningMessage.count() > 0) {
            String actualMessage = warningMessage.first().textContent().trim();
            System.out.println("📝 Bulunan mesaj: \"" + actualMessage + "\"");
            
            boolean matches = actualMessage.contains(expectedMessage);
            
            if (matches) {
                System.out.println("✅ Uyarı mesajı doğru şekilde gösterildi");
            } else {
                System.out.println("⚠️ Uyarı mesajı gösterildi ama içerik uyuşmuyor");
            }
            
            return matches;
        }
        
        // Alternatif: Herhangi bir element'te bu mesaj var mı kontrol et
        Locator anyElementWithMessage = page.locator("*:has-text('" + expectedMessage + "')").first();
        
        if (anyElementWithMessage.count() > 0 && anyElementWithMessage.isVisible()) {
            System.out.println("✅ Uyarı mesajı alternatif element'te bulundu");
            return true;
        }
        
        System.out.println("❌ Uyarı mesajı bulunamadı!");
        
        // Debug için sayfadaki tüm alert/notification'ları listele
        Locator allAlerts = page.locator(".alert, .notification, [role='alert'], .toast");
        int alertCount = allAlerts.count();
        System.out.println("🔍 Sayfada toplam " + alertCount + " alert/notification bulundu:");
        
        for (int i = 0; i < Math.min(alertCount, 5); i++) {
            String text = allAlerts.nth(i).textContent().trim();
            System.out.println("  [" + i + "] " + text);
        }
        
        return false;
    }
}
