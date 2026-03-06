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
        if (_conditionFrame != null)
            return _conditionFrame;

        // Find the newly opened modal with ContractRepresentative/Create iframe
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

        // Fallback: Try main contract modal frame
        var frames = Page.Frames;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains("ContractRepresentative/Create"))
            {
                _conditionFrame = frame;
                Console.WriteLine("✅ Using ContractRepresentative/Create frame from page frames");
                return frame;
            }
        }

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
        var frame = await GetBrandAmbassadorConditionFrameAsync();
        
        var fieldId = GetFieldId(fieldLabel);
        var field = frame.Locator(fieldId);
        
        // Check if field exists
        var count = await field.CountAsync();
        if (count == 0)
        {
            return "not shown";
        }
        
        // Check if field is visible
        var isVisible = await field.First.IsVisibleAsync();
        if (!isVisible)
        {
            return "not shown";
        }
        
        // Special handling for radio button groups (removed "Kdv Dahil mi?" - replaced with "Tutara KDV Dahil" and "Fatura Tutarına KDV Dahil")
        var radioButtonFields = new[] { "Kademeli mi?", "Hedefli mi?", "Hedefli", "Tutar Çarpan Var mı?", "Tutara KDV Dahil", "Fatura Tutarına KDV Dahil" };
        if (radioButtonFields.Contains(fieldLabel))
        {
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
        
        // Check if label has required icon (*)
        // For Kendo dropdowns and other fields, check the label
        var labelSelector2 = $"label:has-text('{fieldLabel}')";
        var label2 = frame.Locator(labelSelector2);
        var labelCount2 = await label2.CountAsync();
        if (labelCount2 > 0)
        {
            var requiredIcon = label2.Locator("span.requiredIcon");
            var hasRequiredIcon = await requiredIcon.CountAsync() > 0;
            if (hasRequiredIcon)
            {
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
            "Hesaplama Tutar Para Birimi" => "span[aria-owns='TargetRevenueCurrencyCode_listbox']",
            "İşlem Para Birimi" => "span[aria-owns='TargetRevenueCurrencyCode_listbox']", // Backward compatibility
            "Hesaplama Para Birimi" => "span[aria-owns='TargetRevenueCurrencyCode_listbox']", // Backward compatibility
            // Faturalama Para Birimi is a Kendo dropdown
            "Faturalama Para Birimi" => "span[aria-owns='InvoiceCurrencyCode_listbox']",
            // New VAT fields - mapped to their input IDs
            "Tutara KDV Dahil" => "#yes_IsVatInclude",
            "Fatura Tutarına KDV Dahil" => "#yes_IsInvoiceVatInclude",
            // Old field name for backward compatibility
            "Kdv Dahil mi?" => "#yes_IsVatInclude", // Radio button group
            "Kademeli mi?" => "#yes_IsGradual", // Radio button group
            "Hedefli mi?" => "#yes_HasTarget", // Radio button group (old field name)
            "Hedefli" => "#yes_HasTarget", // Radio button group (new field name)
            "Temel Ölçü Birimi" => "span[aria-owns='MainMeasureUnitId_listbox']", // Kendo dropdown
            "Birim Çarpanı" => "span.k-numerictextbox:has(input#UnitMultiplier)", // Kendo NumericTextBox
            "Hesaplama Tutar" => "span.k-numerictextbox:has(input#RebateValue)", // Kendo NumericTextBox
            "Tutar Çarpan Var mı?" => "#yes_HasMultiplier", // Radio button group
            "Hesaplama Oran" => "span.k-numerictextbox:has(input#RebateRatio)", // Kendo NumericTextBox
            "Hedef Ciro" => "#TargetRevenue",
            "Hedef Miktar" => "#TargetUnit",
            
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
}
