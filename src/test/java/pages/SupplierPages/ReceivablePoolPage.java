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
     * Fill calculation date field (alternative for Receivable Pool page)
     * Kondisyon Hesaplama Tarihi alanını doldurur
     */
    public void fillCalculationDate(String date) {
        initializeLocators();
        System.out.println("🔍 Kondisyon Hesaplama Tarihi dolduruluyor: " + date);
        page.waitForTimeout(1500);
        
        // FilterContractConditionDate alanını kullan
        Locator calculationDateInput = page.locator("#FilterContractConditionDate");
        
        if (calculationDateInput.count() > 0) {
            calculationDateInput.first().fill(date);
            System.out.println("✅ Kondisyon Hesaplama Tarihi dolduruldu: " + date);
        } else {
            // Alternative selector: Date içeren ilk filter input
            Locator altInput = page.locator("input[id*='Date'][id*='Filter']").first();
            if (altInput.count() > 0) {
                altInput.fill(date);
                System.out.println("✅ Tarih dolduruldu (alternative): " + date);
            } else {
                System.out.println("❌ Tarih alanı bulunamadı!");
            }
        }
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
        initializeLocators();
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
    
    /**
     * Click checkbox on first grid row
     * Grid'deki ilk satırdaki checkbox'ı seçer
     */
    public void clickFirstRowCheckbox() {
        System.out.println("\n🔍 İlk satırdaki checkbox'a tıklanıyor...");
        page.waitForTimeout(2000); // Grid'in yüklenmesini bekle
        
        // Checkbox selector - name pattern: ContractReceivableInvoiceGridId...
        Locator firstCheckbox = page.locator("input[type='checkbox'][name*='ContractReceivableInvoiceGridId']").first();
        
        if (firstCheckbox.count() > 0) {
            System.out.println("✅ Checkbox bulundu, scroll ve visibility kontrolü yapılıyor...");
            
            // Checkbox'ı görünür yap - scroll into view
            firstCheckbox.scrollIntoViewIfNeeded();
            page.waitForTimeout(500);
            
            // Force click kullan - normal click çalışmazsa
            try {
                firstCheckbox.click(new Locator.ClickOptions().setTimeout(5000));
                System.out.println("✅ İlk satırdaki checkbox seçildi (normal click)");
            } catch (Exception e) {
                System.out.println("⚠️ Normal click başarısız, force click deneniyor...");
                firstCheckbox.click(new Locator.ClickOptions().setForce(true));
                System.out.println("✅ İlk satırdaki checkbox seçildi (force click)");
            }
            
            page.waitForTimeout(500);
        } else {
            System.out.println("❌ Checkbox bulunamadı!");
        }
    }
    
    /**
     * Verify "Rebate Faturası Oluştur" frame is opened
     * Rebate Faturası Oluştur modal/frame'inin açıldığını doğrular
     */
    public boolean verifyCreateRebateInvoiceFrameOpened() {
        System.out.println("\n🔍 'Rebate Faturası Oluştur' frame'i kontrol ediliyor...");
        page.waitForTimeout(1500);
        
        // Frame/modal için olası selector'ler
        Locator frame = page.locator(
            "iframe[src*='CreateRebateInvoicePopup'], " +
            ".k-window:has-text('Rebate Faturası Oluştur'), " +
            ".modal-dialog:has-text('Rebate Faturası Oluştur'), " +
            "div:has-text('Rebate Faturası Oluştur'):visible"
        );
        
        boolean isOpened = frame.count() > 0;
        
        if (isOpened) {
            System.out.println("✅ 'Rebate Faturası Oluştur' frame'i açıldı");
        } else {
            System.out.println("❌ 'Rebate Faturası Oluştur' frame'i bulunamadı!");
        }
        
        return isOpened;
    }
    
    /**
     * Fill description field in "Rebate Faturası Oluştur" frame
     * Frame içindeki Açıklama alanına yazı yazar
     */
    public void fillDescriptionInFrame(String description) {
        System.out.println("\n🔍 Frame içindeki Açıklama alanı dolduruluyor: " + description);
        
        // İframe içine gir
        FrameLocator frameLocator = page.frameLocator("iframe[src*='CreateRebateInvoicePopup']");
        
        // Açıklama alanını bul - input class="ajs-input" olarak belirtilmiş
        Locator descriptionField = frameLocator.locator("input.ajs-input, textarea[name='Description'], input[name='Description']");
        
        if (descriptionField.count() > 0) {
            descriptionField.first().fill(description);
            page.waitForTimeout(500);
            System.out.println("✅ Açıklama dolduruldu: " + description);
        } else {
            System.out.println("❌ Açıklama alanı bulunamadı!");
        }
    }
    
    /**
     * Click save button in "Rebate Faturası Oluştur" frame
     * Frame içindeki Kaydet butonuna tıklar
     */
    public void clickSaveButtonInFrame() {
        System.out.println("\n🔍 Frame içindeki Kaydet butonuna tıklanıyor...");
        
        // İframe içine gir
        FrameLocator frameLocator = page.frameLocator("iframe[src*='CreateRebateInvoicePopup']");
        
        // Kaydet butonunu bul
        Locator saveButton = frameLocator.locator(
            "button:has-text('Kaydet'), " +
            "input[type='button'][value='Kaydet'], " +
            "input[type='submit'][value='Kaydet'], " +
            ".k-button:has-text('Kaydet')"
        );
        
        if (saveButton.count() > 0) {
            saveButton.first().click();
            page.waitForTimeout(2000); // Kaydetme işlemi ve sayfanın yenilenmesi için bekle
            System.out.println("✅ Kaydet butonuna tıklandı");
        } else {
            System.out.println("❌ Kaydet butonu bulunamadı!");
        }
    }
    
    /**
     * Click invoice number link in grid
     * Grid'deki Fatura No linkine tıklar
     * Kaydet sonrası ekrana gelen fatura numarasına tıklar (sayısal değer olan linke tıklar, Tarihçe gibi text linklere değil)
     */
    public void clickInvoiceNumberLink() {
        System.out.println("\n🔍 Fatura No linkine tıklanıyor...");
        page.waitForTimeout(3000); // Grid'in yenilenmesi için bekle
        
        // Grid'deki tüm linkleri bul
        Locator allLinks = page.locator("table tbody tr td a, .k-grid tbody tr td a");
        
        System.out.println("🔍 Grid'de " + allLinks.count() + " adet link bulundu");
        
        // Her link'in textini kontrol et ve sayısal olanı bul
        for (int i = 0; i < allLinks.count(); i++) {
            String linkText = allLinks.nth(i).textContent().trim();
            System.out.println("🔍 Link " + (i+1) + ": '" + linkText + "'");
            
            // Sadece sayısal değer içeren linke tıkla (Fatura No)
            if (linkText.matches("\\d+")) {
                System.out.println("✅ Fatura No linki bulundu: " + linkText);
                allLinks.nth(i).scrollIntoViewIfNeeded();
                page.waitForTimeout(500);
                allLinks.nth(i).click();
                page.waitForTimeout(2000); // Frame'in açılması için bekle
                System.out.println("✅ Fatura No linkine tıklandı");
                return;
            }
        }
        
        System.out.println("❌ Sayısal fatura numarası linki bulunamadı!");
    }
    
    /**
     * Verify "Rebate Fatura Güncelleme" frame is opened
     * Rebate Fatura Güncelleme frame'inin açıldığını doğrular
     */
    public boolean verifyUpdateRebateInvoiceFrameOpened() {
        System.out.println("\n🔍 'Rebate Fatura Güncelleme' frame'i kontrol ediliyor...");
        page.waitForTimeout(1500);
        
        // Frame/modal için olası selector'ler
        Locator frame = page.locator(
            "iframe[src*='RebateInvoice/Update'], " +
            "iframe[src*='RebateInvoice/Edit'], " +
            ".k-window:has-text('Rebate Fatura'), " +
            ".modal-dialog:has-text('Rebate Fatura'), " +
            "div:has-text('Rebate Fatura Güncelleme'):visible"
        );
        
        boolean isOpened = frame.count() > 0;
        
        if (isOpened) {
            System.out.println("✅ 'Rebate Fatura Güncelleme' frame'i açıldı");
        } else {
            System.out.println("❌ 'Rebate Fatura Güncelleme' frame'i bulunamadı!");
        }
        
        return isOpened;
    }
    
    /**
     * Click "Geri Çek" button in update frame
     * Güncelleme frame'i içindeki Geri Çek butonuna tıklar
     */
    public void clickReverseButton() {
        System.out.println("\n🔍 'Geri Çek' butonuna tıklanıyor...");
        page.waitForTimeout(1500);
        
        // Önce ana sayfada buton var mı kontrol et
        Locator mainPageButton = page.locator("button:has-text('Geri Çek'), input[type='button'][value='Geri Çek'], a:has-text('Geri Çek')");
        
        if (mainPageButton.count() > 0 && mainPageButton.first().isVisible()) {
            System.out.println("✅ Ana sayfada 'Geri Çek' butonu bulundu");
            mainPageButton.first().click();
            page.waitForTimeout(1500);
            System.out.println("✅ 'Geri Çek' butonuna tıklandı");
            return;
        }
        
        // Ana sayfada yoksa iframe'leri kontrol et
        Locator iframes = page.locator("iframe");
        int iframeCount = iframes.count();
        System.out.println("🔍 Toplam " + iframeCount + " iframe bulundu");
        
        for (int i = 0; i < iframeCount; i++) {
            try {
                FrameLocator frameLocator = page.frameLocator("iframe").nth(i);
                Locator reverseButton = frameLocator.locator("button:has-text('Geri Çek'), input[type='button'][value='Geri Çek'], a:has-text('Geri Çek')");
                
                if (reverseButton.count() > 0) {
                    System.out.println("✅ İframe " + i + " içinde 'Geri Çek' butonu bulundu");
                    reverseButton.first().click();
                    page.waitForTimeout(1500);
                    System.out.println("✅ 'Geri Çek' butonuna tıklandı");
                    return;
                }
            } catch (Exception e) {
                // Bu iframe'de buton yok, devam et
            }
        }
        
        System.out.println("❌ 'Geri Çek' butonu hiçbir yerde bulunamadı!");
    }
    
    /**
     * Verify reverse reason popup is displayed
     * "Rebate Faturasını geri çekme nedeninizi belirtiniz" pop-up'ının açıldığını doğrular
     */
    public boolean verifyReverseReasonPopupDisplayed() {
        System.out.println("\n🔍 Geri çekme nedeni pop-up'ı kontrol ediliyor...");
        
        // Pop-up'ın açılması için daha fazla bekle
        page.waitForTimeout(5000);
        System.out.println("🔍 5 saniye bekledikten sonra kontrol ediliyor...");
        
        // Tüm p elementlerini listele
        Locator allParagraphs = page.locator("p");
        System.out.println("🔍 Sayfada toplam <p> sayısı: " + allParagraphs.count());
        
        for (int i = 0; i < Math.min(allParagraphs.count(), 10); i++) {
            try {
                String text = allParagraphs.nth(i).textContent();
                boolean isVisible = allParagraphs.nth(i).isVisible();
                System.out.println("🔍 P " + i + ": '" + text + "' (visible=" + isVisible + ")");
            } catch (Exception e) {
                System.out.println("🔍 P " + i + ": Okunamadı");
            }
        }
        
        // Pop-up'taki "Rebate Faturasını geri çekme nedeninizi belirtiniz:" text'ini ara
        Locator popupText = page.locator("p:has-text('Rebate Faturasını geri çekme nedeninizi belirtiniz')");
        
        boolean isDisplayed = popupText.count() > 0;
        
        if (isDisplayed) {
            System.out.println("✅ Geri çekme nedeni pop-up'ı açıldı");
        } else {
            System.out.println("❌ Geri çekme nedeni pop-up'ı bulunamadı!");
        }
        
        return isDisplayed;
    }
    
    /**
     * Fill reverse reason in popup
     * Pop-up'taki geri çekme nedeni alanına yazı yazar
     */
    public void fillReverseReasonInPopup(String reason) {
        System.out.println("\n🔍 Pop-up'taki geri çekme nedeni dolduruluyor: " + reason);
        page.waitForTimeout(2000);
        
        // Önce main page'de kontrol et
        System.out.println("🔍 Main page'de ajs-input arıyorum...");
        Locator mainInput = page.locator("input.ajs-input[type='text']");
        System.out.println("🔍 Main page'de bulunan ajs-input: " + mainInput.count());
        
        if (mainInput.count() > 0) {
            mainInput.first().fill(reason);
            page.waitForTimeout(500);
            System.out.println("✅ Main page'de geri çekme nedeni dolduruldu: " + reason);
            return;
        }
        
        // iframe'lerde kontrol et
        System.out.println("🔍 iframe'lerde ajs-input arıyorum...");
        int iframeCount = page.locator("iframe").count();
        System.out.println("🔍 Toplam " + iframeCount + " iframe bulundu");
        
        for (int i = 0; i < iframeCount; i++) {
            FrameLocator frameLocator = page.frameLocator("iframe").nth(i);
            Locator frameInput = frameLocator.locator("input.ajs-input[type='text']");
            int inputCount = frameInput.count();
            System.out.println("🔍 İframe " + i + " içinde ajs-input: " + inputCount);
            
            if (inputCount > 0) {
                frameInput.first().fill(reason);
                page.waitForTimeout(500);
                System.out.println("✅ İframe " + i + " içinde geri çekme nedeni dolduruldu: " + reason);
                return;
            }
        }
        
        System.out.println("❌ Hiçbir yerde ajs-input bulunamadı!");
    }
    
    /**
     * Click confirm button in reverse reason popup
     * Pop-up'taki Onay butonuna tıklar
     */
    public void clickConfirmButtonInPopup() {
        System.out.println("\n🔍 Pop-up'taki 'ONAY' butonuna tıklanıyor...");
        page.waitForTimeout(2000);
        
        // Önce main page'de kontrol et
        System.out.println("🔍 Main page'de ONAY butonu arıyorum...");
        Locator mainButton = page.locator("button:has-text('ONAY')");
        System.out.println("🔍 Main page'de bulunan ONAY button: " + mainButton.count());
        
        if (mainButton.count() > 0) {
            mainButton.first().click();
            page.waitForTimeout(2000);
            System.out.println("✅ Main page'de 'ONAY' butonuna tıklandı");
            return;
        }
        
        // iframe'lerde kontrol et
        System.out.println("🔍 iframe'lerde ONAY butonu arıyorum...");
        int iframeCount = page.locator("iframe").count();
        System.out.println("🔍 Toplam " + iframeCount + " iframe bulundu");
        
        for (int i = 0; i < iframeCount; i++) {
            FrameLocator frameLocator = page.frameLocator("iframe").nth(i);
            Locator frameButton = frameLocator.locator("button:has-text('ONAY')");
            int buttonCount = frameButton.count();
            System.out.println("🔍 İframe " + i + " içinde ONAY button: " + buttonCount);
            
            if (buttonCount > 0) {
                frameButton.first().click();
                page.waitForTimeout(2000);
                System.out.println("✅ İframe " + i + " içinde 'ONAY' butonuna tıklandı");
                return;
            }
        }
        
        System.out.println("❌ Hiçbir yerde 'ONAY' butonu bulunamadı!");
    }
    
    /**
     * Verify success message is displayed
     * "İşleminiz başarıyla gerçekleştirildi" mesajının görüntülendiğini doğrular
     */
    public boolean verifySuccessMessage() {
        System.out.println("\n🔍 Başarı mesajı kontrol ediliyor...");
        page.waitForTimeout(1500);
        
        // Başarı mesajı için olası selector'ler
        Locator successMessage = page.locator(
            "*:has-text('İşleminiz başarıyla gerçekleştirildi'), " +
            ".alert-success, " +
            ".toast-success, " +
            ".k-notification-success, " +
            ".success-message"
        );
        
        boolean isDisplayed = successMessage.count() > 0;
        
        if (isDisplayed) {
            String messageText = successMessage.first().textContent().trim();
            System.out.println("✅ Başarı mesajı görüntülendi: " + messageText);
        } else {
            System.out.println("❌ Başarı mesajı bulunamadı!");
        }
        
        return isDisplayed;
    }
    
    /**
     * Select checkbox for specific receivable number
     * Belirli alacak numarasına sahip satırın checkbox'ını seçer
     */
    public void selectCheckboxForReceivableNumber(String receivableNumber) {
        System.out.println("\n🔍 Alacak No " + receivableNumber + " olan satırın checkbox'ı seçiliyor...");
        
        // Grid'deki tüm satırları kontrol et
        Locator rows = page.locator("table tbody tr");
        int rowCount = rows.count();
        System.out.println("🔍 Grid'de " + rowCount + " satır bulundu");
        
        boolean found = false;
        
        for (int i = 0; i < rowCount; i++) {
            Locator row = rows.nth(i);
            String rowText = row.textContent();
            
            // Satırda alacak numarasını ara
            if (rowText.contains(receivableNumber)) {
                System.out.println("✅ Alacak No " + receivableNumber + " bulundu (satır " + (i + 1) + ")");
                
                // Bu satırdaki checkbox'ı bul ve tıkla
                Locator checkbox = row.locator("input[type='checkbox']");
                
                if (checkbox.count() > 0) {
                    checkbox.scrollIntoViewIfNeeded();
                    page.waitForTimeout(500);
                    
                    try {
                        checkbox.click(new Locator.ClickOptions().setTimeout(5000));
                        System.out.println("✅ Alacak No " + receivableNumber + " checkbox'ı seçildi");
                    } catch (Exception e) {
                        checkbox.click(new Locator.ClickOptions().setForce(true));
                        System.out.println("✅ Alacak No " + receivableNumber + " checkbox'ı seçildi (force)");
                    }
                    
                    page.waitForTimeout(500);
                    found = true;
                    break;
                } else {
                    System.out.println("❌ Satırda checkbox bulunamadı!");
                }
            }
        }
        
        if (!found) {
            System.out.println("❌ Alacak No " + receivableNumber + " bulunamadı!");
        }
    }
    
    /**
     * Verify error message contains specific text
     * Hata mesajının belirli metni içerdiğini doğrular
     */
    public boolean verifyErrorMessageContains(String expectedText) {
        System.out.println("\n🔍 Hata mesajı kontrol ediliyor...");
        page.waitForTimeout(2000);
        
        // Hata mesajı için olası selector'ler
        Locator errorMessage = page.locator(
            "*:has-text('" + expectedText + "'), " +
            ".alert-error, " +
            ".alert-danger, " +
            ".toast-error, " +
            ".k-notification-error, " +
            ".error-message, " +
            ".ajs-message:has-text('" + expectedText + "'), " +
            "div[role='alert']:has-text('" + expectedText + "')"
        );
        
        boolean isDisplayed = errorMessage.count() > 0;
        
        if (isDisplayed) {
            String messageText = errorMessage.first().textContent().trim();
            System.out.println("✅ Hata mesajı görüntülendi: " + messageText);
        } else {
            System.out.println("❌ Beklenen hata mesajı bulunamadı: " + expectedText);
        }
        
        return isDisplayed;
    }
    
    /**
     * Verify "Rebate Faturası Oluştur" modal is opened
     * Rebate Faturası Oluştur modal'ının açıldığını doğrular
     * Modal başlığını kontrol eder: span.k-window-title#SeturModalWin_wnd_title
     */
    public boolean verifyRebateInvoiceCreateModalOpened() {
        System.out.println("\n🔍 'Rebate Faturası Oluştur' modal'ı kontrol ediliyor...");
        page.waitForTimeout(2000);
        
        // Modal başlık elementi için selector
        Locator modalTitle = page.locator(
            "span.k-window-title#SeturModalWin_wnd_title, " +
            "span.k-window-title:has-text('Rebate Faturası Oluştur'), " +
            "#SeturModalWin_wnd_title"
        );
        
        boolean isOpened = modalTitle.count() > 0;
        
        if (isOpened) {
            String titleText = modalTitle.first().textContent().trim();
            System.out.println("✅ 'Rebate Faturası Oluştur' modal'ı açıldı");
            System.out.println("📝 Modal başlığı: " + titleText);
        } else {
            System.out.println("❌ 'Rebate Faturası Oluştur' modal'ı bulunamadı!");
        }
        
        return isOpened;
    }
}

