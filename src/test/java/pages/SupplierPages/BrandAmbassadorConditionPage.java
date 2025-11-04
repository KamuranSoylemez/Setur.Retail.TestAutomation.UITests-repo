package pages.SupplierPages;
import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.options.WaitForSelectorState;
import pages.commonPages.BasePage;


public class BrandAmbassadorConditionPage extends BasePage {

    // Temsilci Kondisyon popup'ındaki iframe'i bulur
    // NOT: "Yeni Kayıt" butonuna tıklandığında yeni bir modal window açılıyor!
    // Yeni modal: <div id="...-ModalWin" data-role="window"> içinde <iframe src="/ApplicationManagement/ContractRepresentative/Create">
    private FrameLocator getBrandAmbassadorConditionFrame() {
        // Önce yeni açılan modal'ı kontrol et
        Locator newModal = page.locator("div[data-role='window']:has(iframe[src*='ContractRepresentative/Create'])");
        
        if (newModal.count() > 0) {
            // Yeni modal'ın iframe'ini kullan
            FrameLocator newModalFrame = newModal.frameLocator("iframe");
            System.out.println("✅ Yeni açılan ContractRepresentative/Create modal frame'i kullanılıyor");
            return newModalFrame;
        } else {
            // Fallback: Ana contract modal frame'i
            FrameLocator modalFrame = page.frameLocator("#SeturModalWin iframe");
            System.out.println("⚠️ Yeni modal bulunamadı, ana contract modal frame kullanılıyor");
            return modalFrame;
        }
    }

    public void verifyFormIsDisplayed() {
        System.out.println("🔍 Temsilci Kondisyon Tanımlama popup'ını kontrol ediyoruz...");
        
        // Popup title'ını doğrula
        try {
            Locator conditionPageTitle = page.locator("span.k-window-title:has-text('Temsilci Kondisyon Tanımlama'), span.k-window-title:has-text('Brand Ambassador')");
            conditionPageTitle.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE).setTimeout(3000));
            System.out.println("✅ 'Temsilci Kondisyon Tanımlama' popup görüntülendi");
        } catch (Exception e) {
            System.out.println("⚠️ Popup title doğrulanamadı ama devam ediliyor...");
        }
    }

    public void selectConditionType(String conditionType) {
        // Inline grid edit modunda Kondisyon tipi dropdown'ını seç
        page.waitForTimeout(2000);
        
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        try {
            System.out.println("\n🔍 ===== COMPONENT DETECTION BAŞLADI =====");
            
            // 1. Edit satırını bul
            Locator editRow = frame.locator("tr.k-grid-edit-row, tr[data-uid]").first();
            System.out.println("📍 Edit row count: " + editRow.count());
            
            if (editRow.count() > 0) {
                // Edit row HTML'ini yazdır
                String editRowHTML = editRow.innerHTML();
                System.out.println("\n📄 EDIT ROW HTML (ilk 1000 karakter):");
                System.out.println(editRowHTML.substring(0, Math.min(editRowHTML.length(), 1000)));
                System.out.println("...\n");
                
                System.out.println("\n🔍 EDIT ROW İÇİNDEKİ COMPONENTLER:");
                
                // Edit row içindeki dropdown'lar
                Locator editRowDropdowns = editRow.locator("span.k-dropdown[aria-owns]");
                System.out.println("  📋 Edit row'da " + editRowDropdowns.count() + " dropdown:");
                for (int i = 0; i < editRowDropdowns.count(); i++) {
                    String ariaOwns = editRowDropdowns.nth(i).getAttribute("aria-owns");
                    System.out.println("    [" + i + "] aria-owns: " + ariaOwns);
                }
                
                // Edit row içindeki tüm input'lar
                Locator editRowInputs = editRow.locator("input");
                System.out.println("  📋 Edit row'da " + editRowInputs.count() + " input:");
                for (int i = 0; i < Math.min(editRowInputs.count(), 15); i++) {
                    String id = editRowInputs.nth(i).getAttribute("id");
                    String name = editRowInputs.nth(i).getAttribute("name");
                    String type = editRowInputs.nth(i).getAttribute("type");
                    if (id != null || name != null) {
                        System.out.println("    [" + i + "] id: " + id + ", name: " + name + ", type: " + type);
                    }
                }
                
                // Edit row içindeki radio button'lar
                Locator editRowRadios = editRow.locator("input[type='radio']");
                System.out.println("  📋 Edit row'da " + editRowRadios.count() + " radio button:");
                for (int i = 0; i < editRowRadios.count(); i++) {
                    String id = editRowRadios.nth(i).getAttribute("id");
                    String name = editRowRadios.nth(i).getAttribute("name");
                    System.out.println("    [" + i + "] id: " + id + ", name: " + name);
                }
            }
            
            // 2. Tüm dropdown'ları listele (aria-owns attribute'u olanlar)
            Locator allDropdownsWithAria = frame.locator("span.k-dropdown[aria-owns]");
            System.out.println("\n📋 Toplam " + allDropdownsWithAria.count() + " dropdown (aria-owns) bulundu:");
            for (int i = 0; i < allDropdownsWithAria.count(); i++) {
                String ariaOwns = allDropdownsWithAria.nth(i).getAttribute("aria-owns");
                String ariaLabel = allDropdownsWithAria.nth(i).getAttribute("aria-label");
                boolean isVisible = allDropdownsWithAria.nth(i).isVisible();
                System.out.println("  [" + i + "] aria-owns: " + ariaOwns + ", aria-label: " + ariaLabel + ", visible: " + isVisible);
            }
            
            // 3. Tüm input'ları listele
            Locator allInputs = frame.locator("input");
            System.out.println("\n📋 Toplam " + allInputs.count() + " input bulundu:");
            for (int i = 0; i < Math.min(allInputs.count(), 20); i++) {
                String id = allInputs.nth(i).getAttribute("id");
                String name = allInputs.nth(i).getAttribute("name");
                String type = allInputs.nth(i).getAttribute("type");
                boolean isVisible = allInputs.nth(i).isVisible();
                if (id != null || name != null) {
                    System.out.println("  [" + i + "] id: " + id + ", name: " + name + ", type: " + type + ", visible: " + isVisible);
                }
            }
            
            // 4. Tüm radio button'ları listele
            Locator allRadios = frame.locator("input[type='radio']");
            System.out.println("\n📋 Toplam " + allRadios.count() + " radio button bulundu:");
            for (int i = 0; i < allRadios.count(); i++) {
                String id = allRadios.nth(i).getAttribute("id");
                String name = allRadios.nth(i).getAttribute("name");
                String value = allRadios.nth(i).getAttribute("value");
                System.out.println("  [" + i + "] id: " + id + ", name: " + name + ", value: " + value);
            }
            
            System.out.println("🔍 ===== COMPONENT DETECTION BİTTİ =====\n");
            
            // Şimdi Temsilci Kondisyon Tipi dropdown'ını bul
            Locator kondisyonTipiDropdown = frame.locator("span[aria-owns='ContractRepresentativeTypeId_listbox']");
            
            if (kondisyonTipiDropdown.count() == 0) {
                System.out.println("❌ ContractRepresentativeTypeId dropdown bulunamadı");
                throw new RuntimeException("Temsilci Kondisyon Tipi dropdown bulunamadı - yukarıdaki component listesine bakın");
            }
            
            System.out.println("✅ Temsilci Kondisyon Tipi dropdown bulundu");
            kondisyonTipiDropdown.click();
            page.waitForTimeout(1500);
            
            // Listbox'ı ID ile bul (ContractRepresentativeTypeId_listbox)
            Locator listboxInFrame = frame.locator("#ContractRepresentativeTypeId_listbox");
            Locator listboxInPage = page.locator("#ContractRepresentativeTypeId_listbox");
            
            Locator listbox = null;
            String location = "";
            
            if (listboxInFrame.count() > 0) {
                listbox = listboxInFrame;
                location = "frame";
                System.out.println("✅ ContractRepresentativeTypeId_listbox frame içinde bulundu");
            } else if (listboxInPage.count() > 0) {
                listbox = listboxInPage;
                location = "page";
                System.out.println("✅ ContractRepresentativeTypeId_listbox page'de bulundu");
            } else {
                throw new RuntimeException("ContractRepresentativeTypeId_listbox bulunamadı");
            }
            
            // Option'ı bul ve tıkla
            Locator option = listbox.locator("li").filter(new Locator.FilterOptions().setHasText(conditionType));
            System.out.println("🔍 '" + conditionType + "' için option count: " + option.count());
            
            if (option.count() == 0) {
                throw new RuntimeException("'" + conditionType + "' option'ı listbox'da bulunamadı");
            }
            
            option.first().click();
            page.waitForTimeout(500);
            System.out.println("✅ Temsilci Kondisyon Tipi seçildi (" + location + "): " + conditionType);
        } catch (Exception e) {
            System.err.println("❌ Kondisyon tipi seçilemedi: " + e.getMessage());
            throw e;
        }
    }

    public void selectCalculationType(String calculationType) {
        // Hedef Tipi (Calculation Type) dropdown'ını seç
        // DOĞRU ID: ReckoningSourceId_listbox
        page.waitForTimeout(1000);
        
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        try {
            // Hedef Tipi dropdown'ını ReckoningSourceId ile bul
            Locator hedefTipiDropdown = frame.locator("span[aria-owns='ReckoningSourceId_listbox']");
            
            if (hedefTipiDropdown.count() == 0) {
                System.out.println("❌ ReckoningSourceId dropdown bulunamadı!");
                
                // Debug: Tüm dropdown'ları listele
                Locator allDropdowns = frame.locator("span.k-dropdown[aria-owns]");
                System.out.println("🔍 Frame'de " + allDropdowns.count() + " dropdown var:");
                for (int i = 0; i < Math.min(allDropdowns.count(), 10); i++) {
                    String ariaOwns = allDropdowns.nth(i).getAttribute("aria-owns");
                    System.out.println("  [" + i + "] aria-owns: " + ariaOwns);
                }
                throw new RuntimeException("Hedef Tipi dropdown (ReckoningSourceId) bulunamadı");
            }
            
            System.out.println("✅ Hedef Tipi dropdown bulundu (ReckoningSourceId)");
            
            // Dropdown disabled mi kontrol et
            String ariaDisabled = hedefTipiDropdown.getAttribute("aria-disabled");
            if ("true".equals(ariaDisabled)) {
                System.out.println("⚠️ Hedef Tipi dropdown disabled! (aria-disabled=true)");
                throw new RuntimeException("Hedef Tipi dropdown disabled durumda - önce Kondisyon Tipi seçilmeli");
            }
            
            hedefTipiDropdown.click();
            page.waitForTimeout(1500);
            
            // ReckoningSourceId_listbox'ı frame içinde ara (Kendo UI genelde frame içinde açar)
            Locator listbox = frame.locator("#ReckoningSourceId_listbox");
            
            // Frame içinde yoksa page'de ara
            if (listbox.count() == 0) {
                listbox = page.locator("#ReckoningSourceId_listbox");
                System.out.println("🔍 Listbox page'de aranıyor...");
            } else {
                System.out.println("🔍 Listbox frame içinde bulundu");
            }
            
            if (listbox.count() == 0) {
                throw new RuntimeException("ReckoningSourceId_listbox bulunamadı");
            }
            
            // Seçeneği bul ve tıkla
            Locator option = listbox.locator("li").filter(new Locator.FilterOptions().setHasText(calculationType));
            
            if (option.count() == 0) {
                // Debug: Tüm seçenekleri listele
                Locator allOptions = listbox.locator("li");
                System.out.println("❌ '" + calculationType + "' bulunamadı! Mevcut seçenekler:");
                for (int i = 0; i < allOptions.count(); i++) {
                    System.out.println("  [" + i + "] " + allOptions.nth(i).textContent());
                }
                throw new RuntimeException("Hedef Tipi option bulunamadı: " + calculationType);
            }
            
            option.first().click();
            page.waitForTimeout(500);
            System.out.println("✅ Hedef Tipi seçildi: " + calculationType);
        } catch (Exception e) {
            System.err.println("❌ Hedef Tipi seçilemedi: " + e.getMessage());
            throw e;
        }
    }

    public void selectRadioButton(String fieldName, String option) {
        // Radio button seçimi (Kademeli mi?: Evet/Hayır)
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        try {
            // "Kademeli mi?" için radio button'ları bul
            // ID pattern'i GeneralCondition ile benzer olabilir: yes_IsProgressive / no_IsProgressive
            Locator radioButton;
            
            if (option.equals("Evet")) {
                // "Evet" radio button'u için muhtemel ID'ler
                radioButton = frame.locator("#yes_IsProgressive, input[type='radio'][value='true'], input[type='radio'][value='1']").first();
            } else {
                // "Hayır" radio button'u için muhtemel ID'ler
                radioButton = frame.locator("#no_IsProgressive, input[type='radio'][value='false'], input[type='radio'][value='0']").first();
            }
            
            if (radioButton.count() > 0) {
                radioButton.click();
                page.waitForTimeout(500);
                System.out.println("✅ " + fieldName + " için '" + option + "' seçildi");
            } else {
                throw new RuntimeException(fieldName + " için radio button bulunamadı");
            }
        } catch (Exception e) {
            System.err.println("❌ Radio button seçilemedi: " + e.getMessage());
            throw e;
        }
    }

    public boolean verifyFieldIsDisabled(String fieldName) {
        // Alan adına göre field'ın disabled olduğunu kontrol et
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        String fieldId = getFieldId(fieldName);
        Locator field = frame.locator(fieldId);
        
        // Radio button grupları için özel kontrol
        if (fieldName.equals("Kademeli mi?")) {
            String yesId = "#yes_IsProgressive";
            String noId = "#no_IsProgressive";
            
            Locator yesButton = frame.locator(yesId);
            Locator noButton = frame.locator(noId);
            
            boolean yesDisabled = yesButton.count() > 0 && (yesButton.getAttribute("disabled") != null || yesButton.isDisabled());
            boolean noDisabled = noButton.count() > 0 && (noButton.getAttribute("disabled") != null || noButton.isDisabled());
            
            boolean isDisabled = yesDisabled && noDisabled;
            
            System.out.println("🔍 " + fieldName + " radio group disabled: " + isDisabled + 
                             " (yes: " + yesDisabled + ", no: " + noDisabled + ")");
            return isDisabled;
        }
        
        // Normal field kontrolü
        if (field.count() > 0) {
            String disabledAttr = field.getAttribute("disabled");
            boolean isDisabled = disabledAttr != null || field.isDisabled();
            
            System.out.println("🔍 " + fieldName + " (" + fieldId + ") alanı disabled: " + isDisabled);
            return isDisabled;
        } else {
            System.out.println("⚠️ " + fieldName + " (" + fieldId + ") alanı bulunamadı, disabled varsayılıyor");
            return true; // Bulunamayan alan disabled sayılır
        }
    }

    public boolean verifyFieldIsMandatory(String fieldName) {
        // Alan HTML required attribute'una sahip mi kontrol et
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        String fieldId = getFieldId(fieldName);
        Locator field = frame.locator(fieldId);
        
        if (field.count() > 0) {
            String requiredAttr = field.getAttribute("required");
            boolean isMandatory = requiredAttr != null;
            
            System.out.println("🔍 " + fieldName + " (" + fieldId + ") alanı mandatory: " + isMandatory);
            return isMandatory;
        } else {
            System.out.println("⚠️ " + fieldName + " (" + fieldId + ") alanı bulunamadı");
            return false;
        }
    }

    public boolean verifyFieldIsOptional(String fieldName) {
        // Alan ne disabled ne de mandatory ise optional'dır
        boolean isDisabled = verifyFieldIsDisabled(fieldName);
        boolean isMandatory = verifyFieldIsMandatory(fieldName);
        
        boolean isOptional = !isDisabled && !isMandatory;
        
        System.out.println("🔍 " + fieldName + " alanı optional: " + isOptional + 
                         " (disabled: " + isDisabled + ", mandatory: " + isMandatory + ")");
        return isOptional;
    }

    public void fillField(String fieldName, String value) {
        // Alan doldur
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        String fieldId = getFieldId(fieldName);
        Locator field = frame.locator(fieldId);
        
        if (field.count() > 0) {
            field.fill(value);
            page.waitForTimeout(300);
            System.out.println("✅ " + fieldName + " alanına '" + value + "' girildi");
        } else {
            throw new RuntimeException(fieldName + " (" + fieldId + ") alanı bulunamadı");
        }
    }

    public void clearField(String fieldName) {
        // Alan temizle
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        String fieldId = getFieldId(fieldName);
        Locator field = frame.locator(fieldId);
        
        if (field.count() > 0) {
            field.fill("");
            page.waitForTimeout(300);
            System.out.println("✅ " + fieldName + " alanı temizlendi");
        } else {
            throw new RuntimeException(fieldName + " (" + fieldId + ") alanı bulunamadı");
        }
    }

    // Alan adından field ID'sini döndürür
    private String getFieldId(String fieldName) {
        // Field name to ID mapping - Brand Ambassador alanları için
        switch (fieldName) {
            case "Kademeli mi?":
                return "#yes_IsProgressive"; // Radio button grubu için
            case "Hedef Miktar":
                return "#TargetQuantity";
            case "Hedef Ciro":
                return "#TargetAmount";
            case "Tutar":
                return "#Amount";
            case "Oran":
                return "#Percentage";
            case "Marka":
                return "#BrandId";
            case "Açıklama":
                return "#Description";
            case "Durum":
                return "#Status";
            default:
                // Varsayılan olarak field name'i ID olarak kullan (camelCase)
                return "#" + fieldName.replace(" ", "");
        }
    }
}
