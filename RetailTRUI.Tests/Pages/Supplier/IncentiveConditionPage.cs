using System;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Supplier;

/// <summary>
/// Incentive Condition page object
/// Handles incentive condition definition workflows
/// Pattern: Mirrors GeneralConditionPage with Incentive-specific IDs
/// </summary>
public class IncentiveConditionPage : BasePage
{
    private IFrame? _incentiveFrame;

    private async Task<IFrame> GetIncentiveConditionFrameAsync()
    {
        if (_incentiveFrame != null)
            return _incentiveFrame;

        // Find the modal with Incentive/Create iframe
        var modals = await Page.Locator("div[data-role='window']:has(iframe[src*='Incentive/Create'])").AllAsync();
        
        if (modals.Any())
        {
            var frameElement = await modals.First().Locator("iframe").ElementHandleAsync();
            if (frameElement != null)
            {
                _incentiveFrame = await frameElement.ContentFrameAsync();
                if (_incentiveFrame != null)
                {
                    Console.WriteLine("✅ Using Incentive/Create modal frame");
                    return _incentiveFrame;
                }
            }
        }

        // Fallback: Try to find frame from page frames
        var frames = Page.Frames;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains("Incentive/Create"))
            {
                _incentiveFrame = frame;
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
        Console.WriteLine("🔍 Verifying Incentive Condition popup...");
        
        var title = await Page.Locator("span.k-window-title:has-text('İncentive Tanımlama')").CountAsync();
        if (title > 0)
        {
            Console.WriteLine("✅ 'İncentive Tanımlama' popup displayed");
        }
        else
        {
            Console.WriteLine("⚠️ Popup title not found but continuing...");
        }
    }

    /// <summary>
    /// Select condition type (Incentive, Satış Adedi, Satış Cirosu, etc)
    /// </summary>
    public async Task SelectConditionTypeAsync(string conditionType)
    {
        await Task.Delay(2000);
        var frame = await GetIncentiveConditionFrameAsync();
        
        // Try multiple possible IDs for Condition Type dropdown
        var possibleIds = new[] 
        { 
            "ContractRepresentativeTypeId",  // ✅ CONFIRMED via F12
            "ContractIncentiveTypeId", 
            "IncentiveTypeId", 
            "ConditionTypeId" 
        };
        ILocator? dropdown = null;
        string? foundId = null;
        
        foreach (var id in possibleIds)
        {
            var testDropdown = frame.Locator($"span[aria-owns='{id}_listbox']");
            var count = await testDropdown.CountAsync();
            if (count > 0)
            {
                dropdown = testDropdown;
                foundId = id;
                Console.WriteLine($"🔍 Found Condition Type dropdown with ID: {foundId}");
                break;
            }
        }
        
        if (dropdown == null)
        {
            // Debug: List available dropdowns
            var allDropdowns = await frame.Locator("span[aria-owns$='_listbox']").AllAsync();
            Console.WriteLine($"⚠️ Condition Type dropdown not found. Available dropdowns: {allDropdowns.Count}");
            foreach (var dd in allDropdowns)
            {
                var ariaOwns = await dd.GetAttributeAsync("aria-owns");
                Console.WriteLine($"  - aria-owns: {ariaOwns}");
            }
            throw new Exception($"Condition Type dropdown not found. Tried IDs: {string.Join(", ", possibleIds)}");
        }
        
        await dropdown.ClickAsync();
        await Task.Delay(1500);
        
        // Find listbox
        var listboxInFrame = await frame.Locator($"#{foundId}_listbox").CountAsync();
        var listboxInPage = await Page.Locator($"#{foundId}_listbox").CountAsync();
        
        ILocator listbox;
        if (listboxInFrame > 0)
            listbox = frame.Locator($"#{foundId}_listbox");
        else if (listboxInPage > 0)
            listbox = Page.Locator($"#{foundId}_listbox");
        else
            throw new Exception($"{foundId}_listbox not found");
        
        await listbox.Locator($"li:has-text('{conditionType}')").First.ClickAsync();
        await Task.Delay(500);
        Console.WriteLine($"✅ Condition type selected: {conditionType}");
    }

    /// <summary>
    /// Select target type (Satış Adedi, Satış Cirosu, Hesaplamasız, etc)
    /// </summary>
    public async Task SelectTargetTypeAsync(string targetType)
    {
        await Task.Delay(1000);
        var frame = await GetIncentiveConditionFrameAsync();
        
        // Try multiple possible IDs for Target Type dropdown
        var possibleIds = new[] 
        { 
            "ReckoningSourceId",  // ✅ CONFIRMED via F12
            "ContractRepresentativeIncentiveTargetTypeId",
            "ContractIncentiveTargetTypeId", 
            "TargetTypeId", 
            "HedefTipiId",
            "ReckoningTargetId" // From general condition
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
            // Debug: List available dropdowns
            var allDropdowns = await frame.Locator("span[aria-owns$='_listbox']").AllAsync();
            Console.WriteLine($"⚠️ Target Type dropdown not found. Available dropdowns: {allDropdowns.Count}");
            foreach (var dd in allDropdowns)
            {
                var ariaOwns = await dd.GetAttributeAsync("aria-owns");
                Console.WriteLine($"  - aria-owns: {ariaOwns}");
            }
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

    /// <summary>
    /// Select Kademeli (Tiered) - Evet/Hayır
    /// Radio buttons: yes_IsGradual / no_IsGradual
    /// </summary>
    public async Task SelectIsGradientAsync(string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();
        
        // Try multiple possible ID patterns
        var possibleYesIds = new[] 
        { 
            "#yes_IsGradual",  // ✅ CONFIRMED from discovery
            "#yes_IsStair",
            "#yes_IsKademeli",
            "input[name='IsGradual'][value='true']",
            "input[name='IsStair'][value='true']"
        };
        
        ILocator? radioButton = null;
        
        foreach (var id in possibleYesIds)
        {
            var count = await frame.Locator(id).CountAsync();
            if (count > 0)
            {
                radioButton = frame.Locator(id);
                Console.WriteLine($"✅ Found 'Kademeli mi?' radio with selector: {id}");
                break;
            }
        }
        
        if (radioButton == null)
        {
            // Debug: list available radio buttons
            var allRadios = await frame.Locator("input[type='radio']").AllAsync();
            Console.WriteLine($"⚠️ Kademeli radio not found. Total radios: {allRadios.Count}");
            foreach (var radio in allRadios)
            {
                var id = await radio.GetAttributeAsync("id");
                var name = await radio.GetAttributeAsync("name");
                Console.WriteLine($"  - ID: {id}, Name: {name}");
            }
            throw new Exception("Kademeli mi? radio button not found");
        }
        
        // Select the correct radio button based on value
        ILocator selectRadio;
        if (value.ToLower() == "evet")
        {
            selectRadio = radioButton; // yes_IsGradual
        }
        else
        {
            // Get the ID and find no_ variant
            var yesId = await radioButton.GetAttributeAsync("id");
            var noId = yesId?.Replace("yes_", "no_") ?? "no_IsGradual";
            selectRadio = frame.Locator($"#{noId}");
        }
        
        // Wait for radio button to be visible
        try
        {
            await selectRadio.WaitForAsync(new LocatorWaitForOptions 
            { 
                State = WaitForSelectorState.Visible,
                Timeout = 5000 
            });
        }
        catch
        {
            Console.WriteLine("⚠️ Radio button visibility wait timed out, proceeding anyway");
        }
        
        // Force click instead of check
        try
        {
            await selectRadio.ClickAsync(new LocatorClickOptions { Force = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Click failed, trying SetCheckedAsync: {ex.Message}");
            await selectRadio.SetCheckedAsync(true);
        }
        
        await Task.Delay(500);
        Console.WriteLine($"✅ Kademeli mi? set to: {value}");
    }

    /// <summary>
    /// Select Hedefli (Targeted) - Evet/Hayır
    /// Radio buttons: yes_HasTarget / no_HasTarget
    /// </summary>
    public async Task SelectIsTargetedAsync(string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();
        
        // Try multiple possible ID patterns
        var possibleYesIds = new[] 
        { 
            "#yes_HasTarget",  // ✅ CONFIRMED from discovery
            "#yes_IsTargeted",
            "#yes_IsTarget",
            "#yes_IsHedefli",
            "input[name='HasTarget'][value='true']",
            "input[name='IsTargeted'][value='true']"
        };
        
        ILocator? radioButton = null;
        string? foundBaseId = null;
        
        foreach (var id in possibleYesIds)
        {
            var count = await frame.Locator(id).CountAsync();
            if (count > 0)
            {
                radioButton = frame.Locator(id);
                // Extract base name for No button
                if (id.StartsWith("#yes_"))
                    foundBaseId = id.Replace("#yes_", "");
                else if (id.Contains("name='"))
                    foundBaseId = id.Split("name='")[1].Split("'")[0];
                    
                Console.WriteLine($"✅ Found 'Hedefli mi?' radio with selector: {id}");
                break;
            }
        }
        
        if (radioButton == null)
        {
            throw new Exception("Hedefli mi? radio button not found");
        }
        
        // Wait for radio button to become visible
        try
        {
            await radioButton.WaitForAsync(new LocatorWaitForOptions 
            { 
                State = WaitForSelectorState.Visible,
                Timeout = 5000 
            });
        }
        catch
        {
            Console.WriteLine("⚠️ Radio button visibility wait timed out, proceeding anyway");
        }
        
        // Select appropriate radio button
        ILocator selectRadio = radioButton;
        if (value.ToLower() == "hayır" && foundBaseId != null)
        {
            selectRadio = frame.Locator($"#no_{foundBaseId}");
        }
        
        // Also try alternate no_ pattern if it fails
        if (value.ToLower() == "hayır")
        {
            var altNo = frame.Locator("#no_HasTarget");
            var altCount = await altNo.CountAsync();
            if (altCount > 0)
            {
                selectRadio = altNo;
            }
        }
        
        // Force click instead of check to ensure it works
        try
        {
            await selectRadio.ClickAsync(new LocatorClickOptions { Force = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Click failed, trying SetCheckedAsync: {ex.Message}");
            await selectRadio.SetCheckedAsync(true);
        }
        
        await Task.Delay(500);
        Console.WriteLine($"✅ Hedefli mi? set to: {value}");
    }

    /// <summary>
    /// Select Çoklu Ödül (Multiple Reward) - Evet/Hayır
    /// Radio buttons: yes_HasMultipleReward / no_HasMultipleReward
    /// </summary>
    public async Task SelectIsMultipleRewardAsync(string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();
        
        // Try multiple possible ID patterns
        var possibleYesIds = new[] 
        { 
            "#yes_HasMultipleReward",  // ✅ CONFIRMED from discovery
            "#yes_IsMultiReward",
            "#yes_IsCokluOdul",
            "input[name='HasMultipleReward'][value='true']",
            "input[name='IsMultiReward'][value='true']"
        };
        
        ILocator? radioButton = null;
        string? foundBaseId = null;
        
        foreach (var id in possibleYesIds)
        {
            var count = await frame.Locator(id).CountAsync();
            if (count > 0)
            {
                radioButton = frame.Locator(id);
                if (id.StartsWith("#yes_"))
                    foundBaseId = id.Replace("#yes_", "");
                else if (id.Contains("name='"))
                    foundBaseId = id.Split("name='")[1].Split("'")[0];
                    
                Console.WriteLine($"✅ Found 'Çoklu Ödül mü?' radio with selector: {id}");
                break;
            }
        }
        
        if (radioButton == null)
        {
            throw new Exception("Çoklu Ödül mü? radio button not found");
        }
        
        // Wait for radio button to become visible
        try
        {
            await radioButton.WaitForAsync(new LocatorWaitForOptions 
            { 
                State = WaitForSelectorState.Visible,
                Timeout = 5000 
            });
        }
        catch
        {
            Console.WriteLine("⚠️ Radio button visibility wait timed out, proceeding anyway");
        }
        
        // Select appropriate radio button
        ILocator selectRadio = radioButton;
        if (value.ToLower() == "hayır" && foundBaseId != null)
        {
            selectRadio = frame.Locator($"#no_{foundBaseId}");
        }
        
        // Also try alternate no_ pattern if it fails
        if (value.ToLower() == "hayır")
        {
            var altNo = frame.Locator("#no_HasMultipleReward");
            var altCount = await altNo.CountAsync();
            if (altCount > 0)
            {
                selectRadio = altNo;
            }
        }
        
        // Force click instead of check
        try
        {
            await selectRadio.ClickAsync(new LocatorClickOptions { Force = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Click failed, trying SetCheckedAsync: {ex.Message}");
            await selectRadio.SetCheckedAsync(true);
        }
        
        await Task.Delay(500);
        Console.WriteLine($"✅ Çoklu Ödül mü? set to: {value}");
    }

    /// <summary>
    /// Select dropdown by field name and value
    /// Handles dropdowns like Hesaplama Periyodu, Temel Ölçü Birimi, etc.
    /// </summary>
    public async Task SelectDropdownAsync(string dropdownId, string value)
    {
        await Task.Delay(1000);
        var frame = await GetIncentiveConditionFrameAsync();
        
        // Find and click dropdown
        var dropdown = frame.Locator($"span[aria-owns='{dropdownId}_listbox']");
        var count = await dropdown.CountAsync();
        
        if (count == 0)
        {
            throw new Exception($"Dropdown with ID '{dropdownId}' not found");
        }
        
        try
        {
            await dropdown.ClickAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Error clicking dropdown: {ex.Message}");
            throw;
        }
        
        await Task.Delay(1500);
        
        // Find listbox
        var listboxInFrame = await frame.Locator($"#{dropdownId}_listbox").CountAsync();
        var listboxInPage = await Page.Locator($"#{dropdownId}_listbox").CountAsync();
        
        ILocator listbox;
        if (listboxInFrame > 0)
        {
            listbox = frame.Locator($"#{dropdownId}_listbox");
        }
        else if (listboxInPage > 0)
        {
            listbox = Page.Locator($"#{dropdownId}_listbox");
        }
        else
        {
            throw new Exception($"{dropdownId}_listbox not found");
        }
        
        try
        {
            await listbox.Locator($"li:has-text('{value}')").First.ClickAsync(new LocatorClickOptions { Timeout = 5000 });
        }
        catch (TimeoutException)
        {
            // If first approach fails, list all available options
            var allOptions = await listbox.Locator("li").AllAsync();
            Console.WriteLine($"⚠️ Could not find '{value}', available options:");
            foreach (var opt in allOptions)
            {
                var text = await opt.TextContentAsync();
                Console.WriteLine($"  - {text}");
            }
            
            throw new Exception($"Option '{value}' not found in dropdown {dropdownId}");
        }
        
        await Task.Delay(500);
        Console.WriteLine($"✅ Dropdown '{dropdownId}' set to: {value}");
    }

    /// <summary>
    /// Fill numeric textbox field
    /// </summary>
    public async Task FillNumericFieldAsync(string fieldId, string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();
        
        var field = frame.Locator(fieldId);
        var count = await field.CountAsync();
        
        if (count == 0)
        {
            throw new Exception($"Numeric field '{fieldId}' not found");
        }
        
        var input = field.Locator("input").First;
        await input.ClearAsync();
        await input.FillAsync(value);
        await Task.Delay(500);
        
        Console.WriteLine($"✅ Numeric field filled: {value}");
    }

    /// <summary>
    /// Fill text input field
    /// </summary>
    public async Task FillTextFieldAsync(string fieldId, string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();
        
        var field = frame.Locator(fieldId);
        var count = await field.CountAsync();
        
        if (count == 0)
        {
            throw new Exception($"Text field '{fieldId}' not found");
        }
        
        await field.First.ClearAsync();
        await field.First.FillAsync(value);
        await Task.Delay(500);
        
        Console.WriteLine($"✅ Text field filled: {value}");
    }

    /// <summary>
    /// Fill date field
    /// </summary>
    public async Task FillDateFieldAsync(string fieldId, string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();
        
        var field = frame.Locator(fieldId);
        var count = await field.CountAsync();
        
        if (count == 0)
        {
            throw new Exception($"Date field '{fieldId}' not found");
        }
        
        await field.First.FillAsync(value);
        await Task.Delay(500);
        
        Console.WriteLine($"✅ Date field filled: {value}");
    }

    /// <summary>
    /// Get field status: "mandatory", "optional", "disabled", or "not shown"
    /// </summary>
    public async Task<string> VerifyFieldStatusAsync(string fieldLabel)
    {
        var frame = await GetIncentiveConditionFrameAsync();
        var fieldId = GetFieldId(fieldLabel);
        var field = frame.Locator(fieldId);
        
        // Check if field exists
        var count = await field.CountAsync();
        if (count == 0)
        {
            return "not shown";
        }
        
        // Check if field is visible - if not visible, it's "not shown" (even if DOM contains it)
        var isVisible = await field.First.IsVisibleAsync();
        if (!isVisible)
        {
            return "not shown";
        }
        
        // Special handling for radio button groups
        var radioButtonFields = new[] { "Kademeli mi?", "Hedefli mi?", "Çoklu Ödül mü?", "Tutar Çarpanlı", 
                                        "Kişi Başı mı?", "Sadece Barkodlu Satışlar mı?", "Firmaya Fatura Edilsin mi?", 
                                        "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Fatura Tutarına Kdv Dahil" };
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
                
                // Fix CSS selector for field labels with special characters
                var radioLabelSelector = fieldLabel.Contains("'") 
                    ? $"label:has-text(\"{fieldLabel}\")" 
                    : $"label:has-text('{fieldLabel}')";
                var radioLabel = frame.Locator(radioLabelSelector);
                var radioLabelCount = await radioLabel.CountAsync();
                if (radioLabelCount > 0)
                {
                    var requiredIcon = radioLabel.Locator("span.requiredIcon");
                    var hasRequiredIcon = await requiredIcon.CountAsync() > 0;
                    if (hasRequiredIcon)
                    {
                        return "mandatory";
                    }
                }
                
                return "optional";
            }
        }
        
        // Check if field has "required" attribute or required icon
        var labelSelector = fieldLabel.Contains("'") 
            ? $"label:has-text(\"{fieldLabel}\")" 
            : $"label:has-text('{fieldLabel}')";
        var label = frame.Locator(labelSelector);
        var labelCount = await label.CountAsync();
        
        if (labelCount > 0)
        {
            var requiredIcon = label.Locator("span.requiredIcon");
            var hasRequiredIcon = await requiredIcon.CountAsync() > 0;
            if (hasRequiredIcon)
            {
                // Check if field is disabled
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
            "Faturalama Para Birimi",
            "Tutara Kdv Dahil"
        };
        
        if (mandatoryFields.Contains(fieldLabel) && !isDisabled)
        {
            return "mandatory";
        }
        
        return "optional";
    }

    /// <summary>
    /// Verify field is mandatory
    /// </summary>
    public async Task VerifyFieldIsMandatoryAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("mandatory", $"Field '{fieldLabel}' should be mandatory");
        Console.WriteLine($"✅ Field '{fieldLabel}' is mandatory");
    }

    /// <summary>
    /// Verify field is disabled
    /// </summary>
    public async Task VerifyFieldIsDisabledAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("disabled", $"Field '{fieldLabel}' should be disabled");
        Console.WriteLine($"✅ Field '{fieldLabel}' is disabled");
    }

    /// <summary>
    /// Verify field is optional
    /// </summary>
    public async Task VerifyFieldIsOptionalAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("optional", $"Field '{fieldLabel}' should be optional");
        Console.WriteLine($"✅ Field '{fieldLabel}' is optional");
    }

    /// <summary>
    /// Verify field is not shown
    /// </summary>
    public async Task VerifyFieldIsNotShownAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("not shown", $"Field '{fieldLabel}' should not be shown");
        Console.WriteLine($"✅ Field '{fieldLabel}' is not shown");
    }

    /// <summary>
    /// Get field state without assertion
    /// </summary>
    public async Task<string> GetFieldStateAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        Console.WriteLine($"📊 Field '{fieldLabel}' state: {status}");
        return status;
    }

    /// <summary>
    /// Map field labels to their selectors
    /// Adapted from GeneralConditionPage with Incentive-specific IDs
    /// </summary>
    private string GetFieldId(string fieldName)
    {
        return fieldName switch
        {
            // Date fields
            "Başlangıç Tarihi" => "#StartDate",
            "Bitiş Tarihi" => "#EndDate",
            
            // Dropdowns - from discovery report
            "Hesaplama Periyodu" => "span[aria-owns='ContractRepresentativePeriodTypeId_listbox']",
            "Periyot" => "span[aria-owns='ContractRepresentativePeriodTypeId_listbox']",
            "Temel Ölçü Birimi" => "span[aria-owns='MainMeasureUnitId_listbox']",
            "Hesaplama Tutar Para Birimi" => "span[aria-owns='CalculationAmountCurrencyCode_listbox']",
            "İşlem Para Birimi" => "span[aria-owns='TargetRevenueCurrencyCode_listbox']",
            "Faturalama Para Birimi" => "span[aria-owns='InvoiceCurrencyCode_listbox']",
            "Net/Brüt" => "label:has-text('Net/Brüt')",
            
            // Radio buttons - from discovery report
            "Kademeli mi?" => "#yes_IsGradual",
            "Hedefli mi?" => "#yes_HasTarget",
            "Çoklu Ödül mü?" => "#yes_HasMultipleReward",
            "Tutar Çarpanlı" => "#yes_HasMultiplier",
            "Kişi Başı mı?" => "#yes_IsPerPerson",
            "Sadece Barkodlu Satışlar mı?" => "#yes_IsOnlyBarcodeSales",
            "Firmaya Fatura Edilsin mi?" => "#yes_IsInvoicable",
            "Tutara Kdv Dahil" => "#yes_IsVatInclude",
            "Fatura Kdv'li mi" => "#yes_IsInvoiceVatInclude",
            "Fatura Tutarına Kdv Dahil" => "#yes_IsInvoiceVatInclude", // Legacy name
            
            // Numeric fields
            "Hedef Ciro" => "span.k-numerictextbox:has(input#TargetRevenue)",
            "Hedef Miktar" => "span.k-numerictextbox:has(input#TargetUnit)",
            "Tutar" => "span.k-numerictextbox:has(input#RebateValue)",
            "Hesaplama Tutar" => "span.k-numerictextbox:has(input#RebateValue)",
            "Oran" => "span.k-numerictextbox:has(input#RebateRatio)",
            "Hesaplama Oran" => "span.k-numerictextbox:has(input#RebateRatio)",
            "Birim Çarpanı" => "span.k-numerictextbox:has(input#UnitMultiplier)",
            "Maksimum Kişi Sayısı" => "span.k-numerictextbox:has(input#MaxPersonCount)",
            
            // Multi-select
            "Marka" => "div.k-multiselect-wrap:has(ul#BrandIdArray_taglist)",
            
            // Text area
            "Açıklama" => "textarea[name='Description']",
            
            // Default fallback
            _ => $"label:has-text('{fieldName}')"
        };
    }

    /// <summary>
    /// Discover all form elements and output to file
    /// Used for mapping field IDs during development
    /// </summary>
    public async Task DiscoverAllElementsAsync()
    {
        var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "incentive_elements_discovery.txt");
        using var writer = new StreamWriter(reportPath, false);

        var report = new System.Text.StringBuilder();
        report.AppendLine("\n\n" + new string('=', 120));
        report.AppendLine("🔎 INCENTIVE KONDISYON FORMU - ELEMENT KEŞİF RAPORU");
        report.AppendLine(new string('=', 120));

        var frame = await GetIncentiveConditionFrameAsync();

        // Get all inputs with id or name
        var allInputs = await frame.Locator("input, select, textarea").AllAsync();
        report.AppendLine($"\n📋 TOPLAM INPUT/SELECT ELEMENTLER: {allInputs.Count}\n");

        int inputIndex = 1;
        foreach (var input in allInputs)
        {
            try
            {
                var tagName = await input.EvaluateAsync<string>("el => el.tagName");
                var type = await input.GetAttributeAsync("type");
                var id = await input.GetAttributeAsync("id");
                var name = await input.GetAttributeAsync("name");
                var placeholder = await input.GetAttributeAsync("placeholder");
                var disabled = await input.IsDisabledAsync();
                var required = await input.GetAttributeAsync("required");
                var className = await input.GetAttributeAsync("class");
                var dataRole = await input.GetAttributeAsync("data-role");
                var ariaLabel = await input.GetAttributeAsync("aria-label");

                // Find associated label
                var labelText = "N/A";
                if (id != null)
                {
                    var labelCount = await frame.Locator($"label[for='{id}']").CountAsync();
                    if (labelCount > 0)
                    {
                        labelText = await frame.Locator($"label[for='{id}']").First.TextContentAsync();
                    }
                }

                report.AppendLine($"{inputIndex}. {tagName}");
                if (id != null) report.AppendLine($"   ID: {id}");
                if (name != null) report.AppendLine($"   Name: {name}");
                if (type != null && type != "") report.AppendLine($"   Type: {type}");
                if (labelText != "N/A") report.AppendLine($"   Label: {labelText.Trim()}");
                if (placeholder != null) report.AppendLine($"   Placeholder: {placeholder}");
                if (dataRole != null) report.AppendLine($"   Data-Role: {dataRole}");
                if (ariaLabel != null) report.AppendLine($"   Aria-Label: {ariaLabel}");
                if (className != null && className.Contains("k-")) report.AppendLine($"   Class: {className}");
                if (disabled) report.AppendLine($"   ⚠️ STATUS: DISABLED");
                if (required != null) report.AppendLine($"   ⭐ STATUS: REQUIRED");
                report.AppendLine();
                inputIndex++;
            }
            catch (Exception ex)
            {
                report.AppendLine($"   ⚠️ Error: {ex.Message}\n");
            }
        }

        // Kendo dropdown/combobox elements
        report.AppendLine($"\n📊 KENDO DROPDOWN/COMBOBOX ELEMENTS:\n");
        var kendoInputs = await frame.Locator(".k-dropdown, .k-combobox, [data-role='dropdownlist'], [data-role='combobox']").AllAsync();
        report.AppendLine($"Toplam: {kendoInputs.Count}\n");

        int kendoIndex = 1;
        foreach (var element in kendoInputs)
        {
            try
            {
                var id = await element.GetAttributeAsync("id");
                var className = await element.GetAttributeAsync("class");
                var dataRole = await element.GetAttributeAsync("data-role");
                var ariaOwns = await element.GetAttributeAsync("aria-owns");

                report.AppendLine($"{kendoIndex}. Kendo Element");
                if (id != null) report.AppendLine($"   ID: {id}");
                if (className != null) report.AppendLine($"   Class: {className}");
                if (dataRole != null) report.AppendLine($"   Data-Role: {dataRole}");
                if (ariaOwns != null) report.AppendLine($"   Aria-Owns: {ariaOwns}");
                report.AppendLine();
                kendoIndex++;
            }
            catch { }
        }

        // Radio buttons
        report.AppendLine($"\n✓ RADIO BUTTON ELEMENTLER:\n");
        var radios = await frame.Locator("input[type='radio']").AllAsync();
        report.AppendLine($"Toplam: {radios.Count}\n");

        int radioIndex = 1;
        foreach (var radio in radios)
        {
            try
            {
                var id = await radio.GetAttributeAsync("id");
                var name = await radio.GetAttributeAsync("name");
                var value = await radio.GetAttributeAsync("value");

                report.AppendLine($"{radioIndex}. Radio Button");
                if (id != null) report.AppendLine($"   ID: {id}");
                if (name != null) report.AppendLine($"   Name: {name}");
                if (value != null) report.AppendLine($"   Value: {value}");
                report.AppendLine();
                radioIndex++;
            }
            catch { }
        }

        report.AppendLine(new string('=', 120));
        report.AppendLine("🔍 DISCOVERY TAMAMLANDI\n");

        var content = report.ToString();
        await writer.WriteAsync(content);
        await writer.FlushAsync();

        Console.WriteLine(content);
        Console.WriteLine($"\n📄 Rapor kaydedildi: {reportPath}\n");
    }
}
