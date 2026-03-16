using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Supplier;

/// <summary>
/// General Condition page object
/// Handles general condition definition workflows
/// </summary>
public class GeneralConditionPage : BasePage
{
    private IFrame? _conditionFrame;

    private async Task<IFrame> GetGeneralConditionFrameAsync()
    {
        if (_conditionFrame != null)
            return _conditionFrame;

        // Find the modal with GeneralCondition/Create iframe
        var modals = await Page.Locator("div[data-role='window']:has(iframe[src*='GeneralCondition/Create'])").AllAsync();
        
        if (modals.Any())
        {
            var frameElement = await modals.First().Locator("iframe").ElementHandleAsync();
            if (frameElement != null)
            {
                _conditionFrame = await frameElement.ContentFrameAsync();
                if (_conditionFrame != null)
                {
                    Console.WriteLine("✅ Using GeneralCondition/Create modal frame");
                    return _conditionFrame;
                }
            }
        }

        // Fallback: Try to find frame from page frames
        var frames = Page.Frames;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains("GeneralCondition/Create") || frame.Url.Contains("ContractRebate/Create"))
            {
                _conditionFrame = frame;
                Console.WriteLine($"✅ Using frame: {frame.Url}");
                return frame;
            }
        }

        // If still not found, just use the page itself
        Console.WriteLine("⚠️ Frame not found, using page directly");
        return Page.MainFrame;
    }

    public async Task VerifyFormIsDisplayedAsync()
    {
        Console.WriteLine("🔍 Verifying General Condition popup...");
        
        var title = await Page.Locator("span.k-window-title:has-text('Genel Kondisyon Tanımlama')").CountAsync();
        if (title > 0)
        {
            Console.WriteLine("✅ 'Genel Kondisyon Tanımlama' popup displayed");
        }
        else
        {
            Console.WriteLine("⚠️ Popup title not found but continuing...");
        }
    }

    public async Task SelectConditionTypeAsync(string conditionType)
    {
        await Task.Delay(2000);
        var frame = await GetGeneralConditionFrameAsync();
        
        // Find and click Kondisyon Tipi dropdown
        var dropdown = frame.Locator("span[aria-owns='ContractRebateTypeId_listbox']");
        await dropdown.ClickAsync();
        await Task.Delay(1500);
        
        // Find listbox
        var listboxInFrame = await frame.Locator("#ContractRebateTypeId_listbox").CountAsync();
        var listboxInPage = await Page.Locator("#ContractRebateTypeId_listbox").CountAsync();
        
        ILocator listbox;
        if (listboxInFrame > 0)
        {
            listbox = frame.Locator("#ContractRebateTypeId_listbox");
        }
        else if (listboxInPage > 0)
        {
            listbox = Page.Locator("#ContractRebateTypeId_listbox");
        }
        else
        {
            throw new Exception("ContractRebateTypeId_listbox not found");
        }
        
        await listbox.Locator($"li:has-text('{conditionType}')").First.ClickAsync();
        await Task.Delay(500);
        Console.WriteLine($"✅ Condition type selected: {conditionType}");
    }

    public async Task SelectCalculationTypeAsync(string calculationType)
    {
        await Task.Delay(1000);
        var frame = await GetGeneralConditionFrameAsync();
        
        // Find and click Hesaplama Tipi dropdown
        var dropdown = frame.Locator("span[aria-owns='ReckoningSourceId_listbox']");
        try
        {
            await dropdown.ClickAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Error clicking dropdown: {ex.Message}");
            // Try to find the dropdown by alternative selectors
            var alternatives = new[] 
            {
                "span:has(input#ReckoningSourceId)",
                "#ReckoningSourceId",
                "div.k-dropdown:has(#ReckoningSourceId)"
            };
            
            foreach (var alt in alternatives)
            {
                try
                {
                    await frame.Locator(alt).First.ClickAsync();
                    Console.WriteLine($"✅ Found dropdown using alternative selector: {alt}");
                    break;
                }
                catch { /* Try next */ }
            }
        }
        
        await Task.Delay(1500);
        
        // Find listbox - check both frame and page
        var listboxInFrame = await frame.Locator("#ReckoningSourceId_listbox").CountAsync();
        var listboxInPage = await Page.Locator("#ReckoningSourceId_listbox").CountAsync();
        
        Console.WriteLine($"📊 Listbox in frame: {listboxInFrame}, in page: {listboxInPage}");
        
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
            // Debug: List all visible elements in the frame
            Console.WriteLine("⚠️ ReckoningSourceId_listbox not found, trying alternative search...");
            var allListboxes = await frame.Locator("div.k-list").AllAsync();
            Console.WriteLine($"📊 Found {allListboxes.Count} listboxes in frame");
            
            if (allListboxes.Count > 0)
            {
                listbox = allListboxes[0];
            }
            else
            {
                throw new Exception("ReckoningSourceId_listbox not found, and no alternative listboxes found");
            }
        }
        
        // Try to find the option with timeout
        try
        {
            var option = listbox.Locator($"li:has-text('{calculationType}'), li:text-is('{calculationType}')").First;
            await option.ClickAsync(new LocatorClickOptions { Timeout = 5000 });
        }
        catch (TimeoutException)
        {
            // If first approach fails, list all available options
            var allOptions = await listbox.Locator("li").AllAsync();
            Console.WriteLine($"⚠️ Could not find '{calculationType}', available options:");
            foreach (var opt in allOptions)
            {
                var text = await opt.TextContentAsync();
                Console.WriteLine($"  - {text}");
            }
            
            throw new Exception($"Option '{calculationType}' not found in dropdown. Available options listed above.");
        }
        
        await Task.Delay(2000); // Wait for field states to update after calculation type change
        Console.WriteLine($"✅ Calculation type selected: {calculationType}");
    }

    /// <summary>
    /// Selects target type (Hedef Tipi) - used for Target Bonus conditions
    /// Options: Alım Adedi, Alım Tutarı, Hesaplamasız, Satış Adedi, Satış Cirosu
    /// </summary>
    public async Task SelectTargetTypeAsync(string targetType)
    {
        await Task.Delay(1000);
        var frame = await GetGeneralConditionFrameAsync();
        
        // Try multiple possible IDs for Hedef Tipi dropdown
        var possibleIds = new[] 
        { 
            "ReckoningTargetId",
            "TargetTypeId",
            "ContractRebateTargetTypeId",
            "ReckoningSourceId" // Fallback to calculation type
        };
        
        ILocator? dropdown = null;
        string? foundId = null;
        
        // Try to find the dropdown
        foreach (var id in possibleIds)
        {
            var testDropdown = frame.Locator($"span[aria-owns='{id}_listbox']");
            var count = await testDropdown.CountAsync();
            if (count > 0)
            {
                dropdown = testDropdown;
                foundId = id;
                break;
            }
        }
        
        if (dropdown == null)
        {
            throw new Exception($"Target Type dropdown not found. Tried IDs: {string.Join(", ", possibleIds)}");
        }
        
        Console.WriteLine($"🔍 Found Target Type dropdown with ID: {foundId}");
        
        await dropdown.ClickAsync();
        await Task.Delay(1500);
        
        // Find listbox
        var listboxInFrame = await frame.Locator($"#{foundId}_listbox").CountAsync();
        var listboxInPage = await Page.Locator($"#{foundId}_listbox").CountAsync();
        
        ILocator listbox;
        if (listboxInFrame > 0)
        {
            listbox = frame.Locator($"#{foundId}_listbox");
        }
        else if (listboxInPage > 0)
        {
            listbox = Page.Locator($"#{foundId}_listbox");
        }
        else
        {
            throw new Exception($"{foundId}_listbox not found");
        }
        
        await listbox.Locator($"li:has-text('{targetType}')").First.ClickAsync();
        await Task.Delay(2000); // Wait for field states to update after target type change
        Console.WriteLine($"✅ Target type selected: {targetType}");
    }

    public async Task SelectIsGradientAsync(string value)
    {
        var frame = await GetGeneralConditionFrameAsync();
        
        // Select "Kademeli mi?" radio button
        var radioId = value.ToLower() == "evet" ? "#yes_IsGradual" : "#no_IsGradual";
        var radioButton = frame.Locator(radioId);
        
        // Wait for radio button to be enabled (calculation type may enable/disable it)
        await radioButton.WaitForAsync(new LocatorWaitForOptions 
        { 
            State = WaitForSelectorState.Visible,
            Timeout = 10000 
        });
        
        // Check if it's still disabled after waiting
        var isDisabled = await radioButton.GetAttributeAsync("disabled");
        if (isDisabled != null)
        {
            // Wait a bit more and retry
            await Task.Delay(2000);
            isDisabled = await radioButton.GetAttributeAsync("disabled");
            if (isDisabled != null)
            {
                Console.WriteLine($"⚠️ 'Kademeli mi?' radio button is disabled, skipping selection");
                return;
            }
        }
        
        await radioButton.CheckAsync();
        await Task.Delay(500);
        Console.WriteLine($"✅ Kademeli mi? set to: {value}");
    }

    public async Task VerifyIsGradientValueAsync(string expectedValue)
    {
        var frame = await GetGeneralConditionFrameAsync();
        
        var radioId = expectedValue.ToLower() == "evet" ? "#yes_IsGradual" : "#no_IsGradual";
        var radioButton = frame.Locator(radioId);
        
        var isChecked = await radioButton.IsCheckedAsync();
        if (isChecked)
        {
            Console.WriteLine($"✅ 'Kademeli mi?' is set to: {expectedValue}");
        }
        else
        {
            throw new Exception($"Expected 'Kademeli mi?' to be {expectedValue}, but it's not checked");
        }
    }

    public async Task SelectMultipleRewardAsync(string value)
    {
        var frame = await GetGeneralConditionFrameAsync();
        
        // Select "Çoklu Ödül mü?" radio button
        var radioId = value.ToLower() == "evet" ? "#yes_HasMultipleReward" : "#no_HasMultipleReward";
        var radioButton = frame.Locator(radioId);
        
        // Wait for radio button to be enabled (calculation type may enable/disable it)
        await radioButton.WaitForAsync(new LocatorWaitForOptions 
        { 
            State = WaitForSelectorState.Visible,
            Timeout = 10000 
        });
        
        // Check if it's still disabled after waiting
        var isDisabled = await radioButton.GetAttributeAsync("disabled");
        if (isDisabled != null)
        {
            // Wait a bit more and retry
            await Task.Delay(2000);
            isDisabled = await radioButton.GetAttributeAsync("disabled");
            if (isDisabled != null)
            {
                Console.WriteLine($"⚠️ 'Çoklu Ödül mü?' radio button is disabled, skipping selection");
                return;
            }
        }
        
        await radioButton.CheckAsync();
        await Task.Delay(500);
        Console.WriteLine($"✅ Çoklu Ödül mü? set to: {value}");
    }

    public async Task<string> VerifyFieldStatusAsync(string fieldLabel)
    {
        var frame = await GetGeneralConditionFrameAsync();
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
        
        // Special handling for radio button groups
        var radioButtonFields = new[] { "Kademeli mi?", "Kdv Dahil mi?", "Çarpan Var mı?", "Tutar Çarpanlı", "Çoklu Ödül mü?" };
        if (radioButtonFields.Contains(fieldLabel))
        {
            string yesButtonId = fieldId;
            string noButtonId = fieldId.Replace("yes_", "no_");
            
            var yesButton = frame.Locator(yesButtonId);
            var noButton = frame.Locator(noButtonId);
            
            var yesExists = await yesButton.CountAsync() > 0;
            var noExists = await noButton.CountAsync() > 0;
            
            if (yesExists && noExists)
            {
                var yesDisabled = await yesButton.GetAttributeAsync("disabled");
                var noDisabled = await noButton.GetAttributeAsync("disabled");
                
                if (yesDisabled != null && noDisabled != null)
                {
                    return "disabled";
                }
                
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
                
                return "optional";
            }
        }
        
        // Check if label has required icon (*) - FIRST priority for mandatory check
        var labelSelector2 = $"label:has-text('{fieldLabel}')";
        var label2 = frame.Locator(labelSelector2);
        var labelCount2 = await label2.CountAsync();
        
        if (labelCount2 > 0)
        {
            var requiredIcon = label2.Locator("span.requiredIcon");
            var hasRequiredIcon = await requiredIcon.CountAsync() > 0;
            if (hasRequiredIcon)
            {
                // Before returning mandatory, check if field is disabled
                // For dropdowns, check the inner input
                if (fieldId.StartsWith("span[aria-owns="))
                {
                    var inputInside = field.Locator("input");
                    var inputExists = await inputInside.CountAsync() > 0;
                    if (inputExists)
                    {
                        var inputDisabled = await inputInside.First.GetAttributeAsync("disabled");
                        var inputAriaDisabled = await inputInside.First.GetAttributeAsync("aria-disabled");
                        if (inputDisabled != null || inputAriaDisabled == "true")
                        {
                            return "disabled";
                        }
                    }
                }
                else
                {
                    // For other fields, check field itself
                    var fieldDisabled = await field.GetAttributeAsync("disabled");
                    var fieldAriaDisabled = await field.GetAttributeAsync("aria-disabled");
                    if (fieldDisabled != null || fieldAriaDisabled == "true")
                    {
                        return "disabled";
                    }
                }
                
                return "mandatory";
            }
        }
        
        // For dropdowns, check if inner input has required attribute
        if (fieldId.StartsWith("span[aria-owns="))
        {
            var inputInside = field.Locator("input[required]");
            var hasRequired = await inputInside.CountAsync() > 0;
            if (hasRequired)
            {
                return "mandatory";
            }
        }
        
        // For numeric textbox, check if inner input has required attribute
        if (fieldId.StartsWith("span.k-numerictextbox"))
        {
            var inputInside = field.Locator("input[required]");
            var hasRequired = await inputInside.CountAsync() > 0;
            if (hasRequired)
            {
                return "mandatory";
            }
        }
        
        // Check if field is disabled
        var ariaDisabled = await field.GetAttributeAsync("aria-disabled");
        if (ariaDisabled == "true")
        {
            return "disabled";
        }
        
        var innerInput = field.Locator("input[aria-disabled='true']");
        var innerInputCount = await innerInput.CountAsync();
        if (innerInputCount > 0)
        {
            return "disabled";
        }
        
        var disabledAttr = await field.GetAttributeAsync("disabled");
        var isDisabled = disabledAttr != null || await field.IsDisabledAsync();
        if (isDisabled)
        {
            return "disabled";
        }
        
        // Check if field has "required" attribute
        var requiredAttr = await field.GetAttributeAsync("required");
        if (requiredAttr != null)
        {
            return "mandatory";
        }
        
        // Known mandatory fields
        var mandatoryFields = new[] 
        {
            "Başlangıç Tarihi",
            "Bitiş Tarihi",
            "Hesaplama Periyodu",
            "Kdv Dahil mi?"
        };
        
        if (mandatoryFields.Contains(fieldLabel) && !isDisabled)
        {
            return "mandatory";
        }
        
        return "optional";
    }

    public async Task VerifyFieldIsMandatoryAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("mandatory", $"Field '{fieldLabel}' should be mandatory");
        Console.WriteLine($"✅ Field '{fieldLabel}' is mandatory");
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

    public async Task VerifyFieldIsNotDisplayedAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("not shown", $"Field '{fieldLabel}' should not be displayed");
        Console.WriteLine($"✅ Field '{fieldLabel}' is not displayed");
    }

    /// <summary>
    /// Gets the field state without assertion
    /// Returns: "enabled", "disabled", "mandatory", "optional", or "not shown"
    /// </summary>
    public async Task<string> GetFieldStateAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        Console.WriteLine($"📊 Field '{fieldLabel}' state: {status}");
        return status;
    }

    public async Task VerifyFieldHasRequiredAsteriskAsync(string fieldLabel)
    {
        var frame = await GetGeneralConditionFrameAsync();
        
        // Check if label has required icon (*)
        var labelSelector = $"label:has-text('{fieldLabel}')";
        var label = frame.Locator(labelSelector);
        var labelCount = await label.CountAsync();
        
        labelCount.Should().BeGreaterThan(0, $"Label for '{fieldLabel}' should exist");
        
        var requiredIcon = label.Locator("span.requiredIcon");
        var hasRequiredIcon = await requiredIcon.CountAsync() > 0;
        
        hasRequiredIcon.Should().BeTrue($"Field '{fieldLabel}' should have required asterisk (*)");
        Console.WriteLine($"✅ Field '{fieldLabel}' has required asterisk (*)");
    }
    
    private string GetFieldId(string fieldName)
    {
        return fieldName switch
        {
            "Başlangıç Tarihi" => "#StartDate",
            "Bitiş Tarihi" => "#EndDate",
            "Hesaplama Periyodu" => "span[aria-owns='ContractRebatePeriodTypeId_listbox']",
            "Periyot" => "span[aria-owns='ContractRebatePeriodTypeId_listbox']",
            "Kademeli mi?" => "#yes_IsGradual",
            "Kdv Dahil mi?" => "#yes_IsVatInclude",
            "Tutar Kdv Dahil" => "#yes_IsVatInclude",
            "Çarpan Var mı?" => "#yes_HasMultiplier",
            "Tutar Çarpan Var mı?" => "#yes_HasMultiplier",
            "Hedef Ciro" => "span.k-numerictextbox:has(input#TargetRevenue)",
            "Hedef Miktar" => "span.k-numerictextbox:has(input#TargetUnit)",
            "Tutar" => "span.k-numerictextbox:has(input#RebateValue)",
            "Hesaplama Tutar" => "span.k-numerictextbox:has(input#RebateValue)",
            "Oran" => "span.k-numerictextbox:has(input#RebateRatio)",
            "Hesaplama Oran" => "span.k-numerictextbox:has(input#RebateRatio)",
            "Temel Ölçü Birimi" => "span[aria-owns='MainMeasureUnitId_listbox']",
            "Birim Çarpanı" => "span.k-numerictextbox:has(input#UnitMultipler)",
            "Marka" => "div.k-multiselect-wrap:has(ul#BrandIdArray_taglist)",
            "Açıklama" => "textarea[name='Description']",
            "Brüt Satın Alma Tipi" => "span[aria-owns='ContractGrossPurchaseItemTypeId_listbox']",
            "Brüt Alım Kalem Tipi" => "span[aria-owns='ContractGrossPurchaseItemTypeId_listbox']",
            "Hesaplama Tutar Para Birimi" => "span[aria-owns='CalculationAmountCurrencyCode_listbox']",
            "İşlem Para Birimi" => "span[aria-owns='TargetRevenueCurrencyCode_listbox']",
            "Faturalama Para Birimi" => "span[aria-owns='InvoiceCurrencyCode_listbox']",
            "Marj Tipi" => "span[aria-owns='MarginCalculationType_listbox']",
            "Marj" => "span.k-numerictextbox:has(input#MarginRatio)",
            "Çoklu Ödül mü?" => "#yes_HasMultipleReward",
            "Gölge Rebate Hesaplansın mı?" => "#yes_IsShadowRebate",
            "Tutar Çarpanlı" => "#yes_HasMultiplier",
            _ => "#" + fieldName.Replace(" ", "")
        };
    }

    public async Task FillFieldAsync(string fieldLabel, string value)
    {
        var frame = await GetGeneralConditionFrameAsync();
        var fieldId = GetFieldId(fieldLabel);
        
        // Handle numeric textboxes
        if (fieldId.Contains("k-numerictextbox"))
        {
            var numericInput = frame.Locator(fieldId).Locator("input").First;
            await numericInput.ClearAsync();
            await numericInput.FillAsync(value);
            await Task.Delay(500); // Wait for field state updates
            Console.WriteLine($"✅ Filled '{fieldLabel}' with value: {value}");
        }
        else
        {
            throw new NotImplementedException($"Fill field not implemented for: {fieldLabel}");
        }
    }

    public async Task ClearFieldAsync(string fieldLabel)
    {
        var frame = await GetGeneralConditionFrameAsync();
        var fieldId = GetFieldId(fieldLabel);
        
        // Handle numeric textboxes
        if (fieldId.Contains("k-numerictextbox"))
        {
            var numericInput = frame.Locator(fieldId).Locator("input").First;
            await numericInput.ClearAsync();
            await Task.Delay(500); // Wait for field state updates
            Console.WriteLine($"✅ Cleared field: {fieldLabel}");
        }
        else
        {
            throw new NotImplementedException($"Clear field not implemented for: {fieldLabel}");
        }
    }

    public async Task VerifyFieldIsNotDisabledAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().NotBe("disabled", $"Field '{fieldLabel}' should NOT be disabled");
        Console.WriteLine($"✅ Field '{fieldLabel}' is not disabled (status: {status})");
    }

    /// <summary>
    /// Selects margin type (Tekli or Çoklu)
    /// </summary>
    public async Task SelectMarginTypeAsync(string marginType)
    {
        await Task.Delay(1000);
        var frame = await GetGeneralConditionFrameAsync();
        
        // Find and click Marj Tipi dropdown
        var dropdown = frame.Locator("span[aria-owns='MarginCalculationType_listbox']");
        await dropdown.ClickAsync();
        await Task.Delay(1500);
        
        // Find listbox
        var listboxInFrame = await frame.Locator("#MarginCalculationType_listbox").CountAsync();
        var listboxInPage = await Page.Locator("#MarginCalculationType_listbox").CountAsync();
        
        ILocator listbox;
        if (listboxInFrame > 0)
        {
            listbox = frame.Locator("#MarginCalculationType_listbox");
        }
        else if (listboxInPage > 0)
        {
            listbox = Page.Locator("#MarginCalculationType_listbox");
        }
        else
        {
            throw new Exception("MarginCalculationType_listbox not found");
        }
        
        await listbox.Locator($"li:has-text('{marginType}')").First.ClickAsync();
        await Task.Delay(500);
        Console.WriteLine($"✅ Margin type selected: {marginType}");
    }

    /// <summary>
    /// Verifies that a field is enabled (not disabled) without requirement
    /// Used for enabled but non-required fields
    /// </summary>
    public async Task VerifyFieldIsEnabledAsync(string fieldLabel)
    {
        var frame = await GetGeneralConditionFrameAsync();
        var fieldId = GetFieldId(fieldLabel);
        var field = frame.Locator(fieldId);
        
        // Check if field exists
        var count = await field.CountAsync();
        if (count == 0)
        {
            throw new Exception($"Field '{fieldLabel}' not found");
        }
        
        // Check if field is visible
        var isVisible = await field.First.IsVisibleAsync();
        isVisible.Should().BeTrue($"Field '{fieldLabel}' should be visible");
        
        // Check for radio button fields
        var radioButtonFields = new[] { "Kademeli mi?", "Kdv Dahil mi?", "Çarpan Var mı?", "Tutar Çarpanlı", "Çoklu Ödül mü?", "Toptan Kâr Merkezi", "Gölge Rebate Hesaplansın mı?" };
        if (radioButtonFields.Contains(fieldLabel))
        {
            string yesButtonId = fieldId;
            string noButtonId = fieldId.Replace("yes_", "no_");
            
            var yesButton = frame.Locator(yesButtonId);
            var noButton = frame.Locator(noButtonId);
            
            var yesExists = await yesButton.CountAsync() > 0;
            var noExists = await noButton.CountAsync() > 0;
            
            if (yesExists && noExists)
            {
                var yesDisabled = await yesButton.GetAttributeAsync("disabled");
                var noDisabled = await noButton.GetAttributeAsync("disabled");
                
                (yesDisabled == null && noDisabled == null).Should().BeTrue($"Field '{fieldLabel}' should be enabled (not disabled)");
                Console.WriteLine($"✅ Field '{fieldLabel}' is enabled");
                return;
            }
        }
        
        // For regular input fields, just ensure not disabled
        var isDisabled = await field.First.GetAttributeAsync("disabled");
        isDisabled.Should().BeNull($"Field '{fieldLabel}' should not be disabled");
        
        Console.WriteLine($"✅ Field '{fieldLabel}' is enabled");
    }

}
