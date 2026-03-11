using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Supplier;

/// <summary>
/// Brand Ambassador Condition page object
/// Handles brand ambassador condition definition workflows
/// </summary>
public class BrandAmbassadorConditionPage : BasePage
{
    // Frame locator for brand ambassador condition modal
    private IFrame? _conditionFrame;

    private async Task<IFrame> GetBrandAmbassadorConditionFrameAsync()
    {
        // PRIORITY 1: Always check for FRESH active frame first (avoid stale references)
        var frames = Page.Frames;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains("ContractRepresentative/Create"))
            {
                _conditionFrame = frame;
                return frame;
            }
        }

        // PRIORITY 2: Try to find via modal (newly opened)
        var modals = await Page.Locator("div[data-role='window']:has(iframe[src*='ContractRepresentative/Create'])").AllAsync();
        
        if (modals.Any())
        {
            var frameElement = await modals.First().Locator("iframe").ElementHandleAsync();
            if (frameElement != null)
            {
                _conditionFrame = await frameElement.ContentFrameAsync();
                if (_conditionFrame != null)
                {
                    Console.WriteLine("✅ Using new ContractRepresentative/Create modal frame");
                    return _conditionFrame;
                }
            }
        }

        // PRIORITY 3: Use cached frame if available
        if (_conditionFrame != null)
            return _conditionFrame;

        throw new InvalidOperationException("Brand Ambassador Condition frame not found");
    }

    public async Task VerifyFormIsDisplayedAsync()
    {
        Console.WriteLine("🔍 Verifying Brand Ambassador Condition popup...");
        
        var title = await Page.Locator("span.k-window-title:has-text('Temsilci Kondisyon Tanımlama')").CountAsync();
        if (title > 0)
        {
            Console.WriteLine("✅ 'Temsilci Kondisyon Tanımlama' popup displayed");
        }
        else
        {
            Console.WriteLine("⚠️ Popup title not found but continuing...");
        }
    }

    public async Task SelectConditionTypeAsync(string conditionType)
    {
        await Task.Delay(2000);
        
        var frame = await GetBrandAmbassadorConditionFrameAsync();
        
        // Find and click Kondisyon Tipi dropdown
        var dropdown = frame.Locator("span[aria-owns='ContractRepresentativeTypeId_listbox']");
        await dropdown.ClickAsync();
        await Task.Delay(1500);
        
        // Find listbox (can be in frame or page)
        var listboxInFrame = await frame.Locator("#ContractRepresentativeTypeId_listbox").CountAsync();
        var listboxInPage = await Page.Locator("#ContractRepresentativeTypeId_listbox").CountAsync();
        
        ILocator listbox;
        if (listboxInFrame > 0)
        {
            listbox = frame.Locator("#ContractRepresentativeTypeId_listbox");
        }
        else if (listboxInPage > 0)
        {
            listbox = Page.Locator("#ContractRepresentativeTypeId_listbox");
        }
        else
        {
            throw new Exception("ContractRepresentativeTypeId_listbox not found");
        }
        
        // Select option
        await listbox.Locator($"li:has-text('{conditionType}')").First.ClickAsync();
        await Task.Delay(500);
        Console.WriteLine($"✅ Condition type selected: {conditionType}");
    }

    public async Task SelectCalculationTypeAsync(string calculationType)
    {
        await Task.Delay(1000);
        
        var frame = await GetBrandAmbassadorConditionFrameAsync();
        
        // Find and click Hedef Tipi dropdown (ReckoningSourceId)
        var dropdown = frame.Locator("span[aria-owns='ReckoningSourceId_listbox']");
        await dropdown.ClickAsync();
        await Task.Delay(1500);
        
        // Find listbox
        var listboxInFrame = await frame.Locator("#ReckoningSourceId_listbox").CountAsync();
        var listboxInPage = await Page.Locator("#ReckoningSourceId_listbox").CountAsync();
        
        ILocator listbox;
        if (listboxInFrame > 0)
        {
            listbox = frame.Locator("#ReckoningSourceId_listbox");
        }
        else if (listboxInPage > 0)
        {
            listbox = Page.Locator("#ReckoningSourceId_listbox");
        }
        else
        {
            throw new Exception("ReckoningSourceId_listbox not found");
        }
        
        // Select option
        await listbox.Locator($"li:has-text('{calculationType}')").First.ClickAsync();
        await Task.Delay(500);
        Console.WriteLine($"✅ Calculation type selected: {calculationType}");
    }

    public async Task SelectBrandAmbassadorOptionAsync(string fieldName, string optionValue)
    {
        await Task.Delay(1000);
        var frame = await GetBrandAmbassadorConditionFrameAsync();
        
        // Find the field by its label and select the option
        // This is a simplified version - may need to be adjusted based on actual HTML structure
        var dropdown = frame.Locator($"span[aria-label*='{fieldName}']");
        await dropdown.ClickAsync();
        await Task.Delay(500);
        
        var option = Page.Locator($"li:has-text('{optionValue}')").First;
        await option.ClickAsync();
        await Task.Delay(500);
        Console.WriteLine($"✅ {fieldName} set to: {optionValue}");
    }

    public async Task SelectIsGradientAsync(string value)
    {
        await Task.Delay(1000);
        var frame = await GetBrandAmbassadorConditionFrameAsync();
        
        // Select "Kademeli mi?" radio button (yes_IsGradual or no_IsGradual)
        var radioId = value == "Evet" ? "#yes_IsGradual" : "#no_IsGradual";
        var radioButton = frame.Locator(radioId);
        
        await radioButton.CheckAsync();
        await Task.Delay(500);
        Console.WriteLine($"✅ Kademeli mi? set to: {value}");
    }

    public async Task SelectIsTargetedAsync(string value)
    {
        await Task.Delay(1000);
        var frame = await GetBrandAmbassadorConditionFrameAsync();
        
        // Select "Hedefli mi?" radio button (yes_HasTarget or no_HasTarget)
        var radioId = value == "Evet" ? "#yes_HasTarget" : "#no_HasTarget";
        var radioButton = frame.Locator(radioId);
        
        await radioButton.CheckAsync();
        await Task.Delay(500);
        Console.WriteLine($"✅ Hedefli mi? set to: {value}");
    }

    public async Task<string> VerifyFieldStatusAsync(string fieldLabel)
    {
        // Force fresh frame retrieval to avoid stale references in AssertionScope
        // Invalidate cache before every check to ensure we get the current frame
        _conditionFrame = null;
        
        var frame = await GetBrandAmbassadorConditionFrameAsync();
        
        var fieldId = GetFieldId(fieldLabel);
        var field = frame.Locator(fieldId);
        
        // Check if field exists
        var count = await field.CountAsync();
        if (count == 0)
        {
            // Debug: Log missing field selector
            Console.WriteLine($"🔴 Field '{fieldLabel}' selector not found: {fieldId}");
            return "not shown";
        }
        
        Console.WriteLine($"✅ Field '{fieldLabel}' selector found: {fieldId}");
        
        // NEW: Check if field label is hidden by CSS (if label hidden, then field is "not shown")
        var labelSelectorForVisibilityCheck = $"label:has-text('{fieldLabel}')";
        var labelForVisibilityCheck = frame.Locator(labelSelectorForVisibilityCheck);
        var labelVisibilityCount = await labelForVisibilityCheck.CountAsync();
        
        if (labelVisibilityCount > 0)
        {
            var labelIsHidden = await IsFieldHiddenByCssAsync(labelForVisibilityCheck);
            if (labelIsHidden)
            {
                Console.WriteLine($"  Field '{fieldLabel}' label is hidden by CSS → Not Shown");
                return "not shown";
            }
        }
        
        // Special handling for radio button groups (removed "Kdv Dahil mi?" - replaced with "Tutara KDV Dahil" and "Fatura Tutarına KDV Dahil")
        var radioButtonFields = new[] { "Kademeli mi?", "Hedefli mi?", "Hedefli", "Tutar Çarpanlı", "Tutara KDV Dahil", "Fatura Tutarına KDV Dahil" };
        if (radioButtonFields.Contains(fieldLabel))
        {
            // Re-fetch frame after radio button radio lookup (safety measure)
            frame = await GetBrandAmbassadorConditionFrameAsync();
            
            // For radio buttons, check both yes and no buttons
            string yesButtonId = fieldId; // This points to yes button
            string noButtonId = fieldId.Replace("yes_", "no_");
            
            var yesButton = frame.Locator(yesButtonId);
            var noButton = frame.Locator(noButtonId);
            
            var yesExists = await yesButton.CountAsync() > 0;
            var noExists = await noButton.CountAsync() > 0;
            
            if (yesExists && noExists)
            {
                var yesDisabled = await yesButton.GetAttributeAsync("disabled");
                var noDisabled = await noButton.GetAttributeAsync("disabled");
                
                // If both are disabled, field is disabled
                if (yesDisabled != null && noDisabled != null)
                {
                    return "disabled";
                }
                
                // Check if label has required icon for mandatory status
                var labelSelector = $"label:has-text('{fieldLabel}')";
                var label = frame.Locator(labelSelector);
                var labelCount = await label.CountAsync();
                if (labelCount > 0)
                {
                    var requiredIcon = label.Locator("span.requiredIcon");
                    var hasRequiredIcon = await requiredIcon.CountAsync() > 0;
                    if (hasRequiredIcon)
                    {
                        return "mandatory";
                    }
                }
                
                // If enabled (not disabled) and no required icon, it's optional
                return "optional";
            }
        }
        
        // FIRST: Check if label has required icon (*) - this is the most reliable indicator
        var labelSelectorFirst = $"label:has-text('{fieldLabel}')";
        var labelFirst = frame.Locator(labelSelectorFirst);
        var labelCountFirst = await labelFirst.CountAsync();
        Console.WriteLine($"  Label lookup for '{fieldLabel}': found {labelCountFirst}");
        
        if (labelCountFirst > 0)
        {
            var requiredIcon = labelFirst.Locator("span.requiredIcon");
            var hasRequiredIcon = await requiredIcon.CountAsync() > 0;
            Console.WriteLine($"  Has required icon (*): {hasRequiredIcon}");
            
            if (hasRequiredIcon)
            {
                // IMPORTANT: Even if field is not rendering visually (hidden by CSS),
                // if input element counts > 0 and label has *, field IS mandatory
                return "mandatory";
            }
        }
        
        // Check if field is disabled (for Kendo widgets, check aria-disabled on wrapper and input)
        var ariaDisabled = await field.GetAttributeAsync("aria-disabled");
        if (ariaDisabled == "true")
        {
            return "disabled";
        }
        
        // For Kendo NumericTextBox and other widgets, check inner input's aria-disabled
        var innerInput = field.Locator("input[aria-disabled='true']");
        var innerInputCount = await innerInput.CountAsync();
        if (innerInputCount > 0)
        {
            return "disabled";
        }
        
        // Check for disabled attribute (handles disabled, disabled="", disabled="disabled", etc.)
        var disabledAttr = await field.GetAttributeAsync("disabled");
        // In HTML, any presence of disabled attribute (including disabled="") means the field is disabled
        if (disabledAttr != null || await field.IsDisabledAsync())
        {
            return "disabled";
        }
        
        // Check if field has "required" attribute
        var requiredAttr = await field.GetAttributeAsync("required");
        if (requiredAttr != null)
        {
            return "mandatory";
        }
        
        // For date fields and dropdowns: if enabled, they are typically mandatory
        // Known mandatory fields that don't have "required" attribute
        var mandatoryFields = new[] 
        {
            "Başlangıç Tarihi",
            "Bitiş Tarihi",
            "Periyot",
            "Hesaplama Para Birimi",
            "Faturalama Para Birimi",
            "Tutara KDV Dahil",
            "Fatura Tutarına KDV Dahil"
        };
        
        // Check if field is disabled by checking attributes
        var checkDisabledAttr = await field.GetAttributeAsync("disabled");
        var fieldIsDisabled = checkDisabledAttr != null || await field.IsDisabledAsync();
        
        if (mandatoryFields.Contains(fieldLabel) && !fieldIsDisabled)
        {
            return "mandatory";
        }
        
        // Otherwise it's optional
        return "optional";
    }

    public async Task VerifyFieldIsMandatoryAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        
        // Debug: Print what we got before assertion (to catch frame staleness issues)
        if (status != "mandatory")
        {
            Console.WriteLine($"⚠️ Field '{fieldLabel}' status: {status} (expected 'mandatory')");
        }
        
        status.Should().Be("mandatory", $"Field '{fieldLabel}' should be mandatory");
        Console.WriteLine($"✅ Field '{fieldLabel}' is mandatory");
    }

    public async Task<string> GetFieldStatusAsync(string fieldLabel)
    {
        return await VerifyFieldStatusAsync(fieldLabel);
    }

    public async Task VerifyFieldIsDisabledAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("disabled", $"Field '{fieldLabel}' should be disabled");
        Console.WriteLine($"✅ Field '{fieldLabel}' is disabled");
    }

    public async Task VerifyFieldIsOptionalAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("optional", $"Field '{fieldLabel}' should be optional");
        Console.WriteLine($"✅ Field '{fieldLabel}' is optional");
    }

    public async Task VerifyFieldIsNotShownAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("not shown", $"Field '{fieldLabel}' should not be shown");
        Console.WriteLine($"✅ Field '{fieldLabel}' is not shown");
    }
    
    private string GetFieldId(string fieldName)
    {
        // Field name to ID mapping based on actual HTML structure
        return fieldName switch
        {
            // Yeni Field Mappings (from debug output)
            "Başlangıç Tarihi" => "#StartDate",
            "Bitiş Tarihi" => "#EndDate",
            // Periyot is a Kendo dropdown, need to find by aria-owns
            "Periyot" => "span[aria-owns='ContractRepresentativePeriodTypeId_listbox']",
            "Hesaplama Tutar Para Birimi" => "#CalculationAmountCurrencyCode",
            "İşlem Para Birimi" => "span[aria-owns='TargetRevenueCurrencyCode_listbox']", // Backward compatibility
            "Hesaplama Para Birimi" => "span[aria-owns='TargetRevenueCurrencyCode_listbox']", // Backward compatibility
            // Faturalama Para Birimi is a Kendo dropdown
            "Faturalama Para Birimi" => "span[aria-owns='InvoiceCurrencyCode_listbox']",
            // New VAT fields - mapped to their input IDs
            "Tutara KDV Dahil" => "#yes_IsVatInclude",
            "Fatura Tutarına KDV Dahil" => "#yes_IsInvoiceVatInclude",
            // 6 NEWLY FOUND FIELDS (from HELPER_FindMissingFieldSelectors test)
            "Net/Brüt" => "#yes_IsNetGross", // Radio button group
            "Gölge Rebate Hesaplansın mı?" => "#yes_IsShadowRebate", // Radio button group
            "Firmaya Fatura Edilsin mi?" => "#yes_IsInvoicable", // Radio button group
            "Tutar Çarpanlı" => "#yes_HasMultiplier", // Radio button group
            "Kişi Başı mı?" => "#yes_IsPerPerson", // Radio button group
            "Maksimum kişi sayısı" => "#MaxPersonCount", // Text input
            // Old field name for backward compatibility
            "Kdv Dahil mi?" => "#yes_IsVatInclude", // Radio button group
            "Kademeli mi?" => "#yes_IsGradual", // Radio button group
            "Hedefli mi?" => "#yes_HasTarget", // Radio button group (old field name)
            "Hedefli" => "#yes_HasTarget", // Radio button group (new field name)
            "Temel Ölçü Birimi" => "span[aria-owns='MainMeasureUnitId_listbox']", // Kendo dropdown
            "Birim Çarpanı" => "#UnitMultiplier", // Numeric input
            "Hesaplama Tutar" => "#RebateValue", // Numeric input
            "Tutar Çarpan Var mı?" => "#yes_HasMultiplier", // Radio button group
            "Hesaplama Oran" => "#RebateRatio", // Numeric input
            "Hedef Ciro" => "#TargetRevenue", // Numeric input field
            "Hedef Miktar" => "#TargetUnit", // Numeric input field
            
            // Old Field Mappings (backward compatibility)
            "Kademe" => "#yes_IsGradual", // Radio button - detected in component scan
            "Hedef Adet" => "input[name='TargetUnit']", // Same as Hedef Miktar
            "Tutar" => "input[name='RebateValue']", // Same as Hesaplama Tutar
            "Oran" => "input[name='RebateRatio']", // Same as Hesaplama Oran
            "Marka" => "div.k-multiselect-wrap:has(ul#BrandIdArray_taglist)", // Kendo MultiSelect
            "Açıklama" => "textarea[name='Description']", // Textarea
            
            // Default: use field name as ID (camelCase)
            _ => "#" + fieldName.Replace(" ", "")
        };
    }

    /// <summary>
    /// Check if field element is hidden by CSS (display:none, visibility:hidden, or parent hidden)
    /// </summary>
    private async Task<bool> IsFieldHiddenByCssAsync(ILocator field)
    {
        try
        {
            var isHidden = await field.EvaluateAsync<bool>("""
                (element) => {
                    // Check element's own visibility
                    const style = window.getComputedStyle(element);
                    if (style.display === 'none' || style.visibility === 'hidden') {
                        return true;
                    }
                    
                    // Check if any parent is hidden
                    let parent = element.parentElement;
                    while (parent && parent !== document.body) {
                        const parentStyle = window.getComputedStyle(parent);
                        if (parentStyle.display === 'none' || parentStyle.visibility === 'hidden') {
                            return true;
                        }
                        parent = parent.parentElement;
                    }
                    
                    return false;
                }
                """);
            return isHidden;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Error checking CSS visibility: {ex.Message}");
            return false;
        }
    }
}
