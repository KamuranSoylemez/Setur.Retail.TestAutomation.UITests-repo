namespace RetailTRUI.Tests.Pages.Supplier;

public partial class IncentiveConditionPage
{
    /// <summary>
    /// Select condition type (Incentive, Satış Adedi, Satış Cirosu, etc)
    /// </summary>
    public async Task SelectConditionTypeAsync(string conditionType)
    {
        await Task.Delay(2000);
        var frame = await GetIncentiveConditionFrameAsync();

        ILocator? dropdown = null;
        string? foundId = null;

        foreach (var id in ConditionTypeDropdownCandidateIds)
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
            var allDropdowns = await frame.Locator("span[aria-owns$='_listbox']").AllAsync();
            Console.WriteLine($"⚠️ Condition Type dropdown not found. Available dropdowns: {allDropdowns.Count}");
            foreach (var dd in allDropdowns)
            {
                var ariaOwns = await dd.GetAttributeAsync("aria-owns");
                Console.WriteLine($"  - aria-owns: {ariaOwns}");
            }
            throw new Exception($"Condition Type dropdown not found. Tried IDs: {string.Join(", ", ConditionTypeDropdownCandidateIds)}");
        }

        await dropdown.ClickAsync();
        await Task.Delay(1500);

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

        ILocator? dropdown = null;
        string? foundId = null;

        foreach (var id in TargetTypeDropdownCandidateIds)
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
            var allDropdowns = await frame.Locator("span[aria-owns$='_listbox']").AllAsync();
            Console.WriteLine($"⚠️ Target Type dropdown not found. Available dropdowns: {allDropdowns.Count}");
            foreach (var dd in allDropdowns)
            {
                var ariaOwns = await dd.GetAttributeAsync("aria-owns");
                Console.WriteLine($"  - aria-owns: {ariaOwns}");
            }
            throw new Exception($"Target Type dropdown not found. Tried IDs: {string.Join(", ", TargetTypeDropdownCandidateIds)}");
        }

        Console.WriteLine($"🔍 Found Target Type dropdown with ID: {foundId}");

        await dropdown.ClickAsync();
        await Task.Delay(1500);

        var listboxInFrame = await frame.Locator($"#{foundId}_listbox").CountAsync();
        var listboxInPage = await Page.Locator($"#{foundId}_listbox").CountAsync();

        ILocator listbox;
        if (listboxInFrame > 0)
            listbox = frame.Locator($"#{foundId}_listbox");
        else if (listboxInPage > 0)
            listbox = Page.Locator($"#{foundId}_listbox");
        else
            throw new Exception($"{foundId}_listbox not found");

        await listbox.Locator($"li:has-text('{targetType}')").First.ClickAsync();
        await Task.Delay(2000);
        Console.WriteLine($"✅ Target type selected: {targetType}");
    }

    public async Task SelectIsGradientAsync(string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();

        ILocator? radioButton = null;
        foreach (var id in IsGradientYesSelectorCandidates)
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
            throw new Exception("Kademeli mi? radio button not found");

        ILocator selectRadio;
        if (value.ToLower() == "evet")
        {
            selectRadio = radioButton;
        }
        else
        {
            var yesId = await radioButton.GetAttributeAsync("id");
            var noId = yesId?.Replace("yes_", "no_") ?? "no_IsGradual";
            selectRadio = frame.Locator($"#{noId}");
        }

        try
        {
            await selectRadio.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        }
        catch
        {
            Console.WriteLine("⚠️ Radio button visibility wait timed out, proceeding anyway");
        }

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

    public async Task SelectIsTargetedAsync(string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();

        ILocator? radioButton = null;
        string? foundBaseId = null;

        foreach (var id in IsTargetedYesSelectorCandidates)
        {
            var count = await frame.Locator(id).CountAsync();
            if (count > 0)
            {
                radioButton = frame.Locator(id);
                if (id.StartsWith("#yes_"))
                    foundBaseId = id.Replace("#yes_", "");
                else if (id.Contains("name='"))
                    foundBaseId = id.Split("name='")[1].Split("'")[0];

                Console.WriteLine($"✅ Found 'Hedefli mi?' radio with selector: {id}");
                break;
            }
        }

        if (radioButton == null)
            throw new Exception("Hedefli mi? radio button not found");

        try
        {
            await radioButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        }
        catch
        {
            Console.WriteLine("⚠️ Radio button visibility wait timed out, proceeding anyway");
        }

        ILocator selectRadio = radioButton;
        if (value.ToLower() == "hayır" && foundBaseId != null)
            selectRadio = frame.Locator($"#no_{foundBaseId}");

        if (value.ToLower() == "hayır")
        {
            var altNo = frame.Locator("#no_HasTarget");
            var altCount = await altNo.CountAsync();
            if (altCount > 0)
                selectRadio = altNo;
        }

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

    public async Task SelectIsMultipleRewardAsync(string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();

        ILocator? radioButton = null;
        string? foundBaseId = null;

        foreach (var id in IsMultipleRewardYesSelectorCandidates)
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
            throw new Exception("Çoklu Ödül mü? radio button not found");

        try
        {
            await radioButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        }
        catch
        {
            Console.WriteLine("⚠️ Radio button visibility wait timed out, proceeding anyway");
        }

        ILocator selectRadio = radioButton;
        if (value.ToLower() == "hayır" && foundBaseId != null)
            selectRadio = frame.Locator($"#no_{foundBaseId}");

        if (value.ToLower() == "hayır")
        {
            var altNo = frame.Locator("#no_HasMultipleReward");
            var altCount = await altNo.CountAsync();
            if (altCount > 0)
                selectRadio = altNo;
        }

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

    public async Task SelectDropdownAsync(string dropdownId, string value)
    {
        await Task.Delay(1000);
        var frame = await GetIncentiveConditionFrameAsync();

        var dropdown = frame.Locator($"span[aria-owns='{dropdownId}_listbox']");
        var count = await dropdown.CountAsync();
        if (count == 0)
            throw new Exception($"Dropdown with ID '{dropdownId}' not found");

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

        var listboxInFrame = await frame.Locator($"#{dropdownId}_listbox").CountAsync();
        var listboxInPage = await Page.Locator($"#{dropdownId}_listbox").CountAsync();

        ILocator listbox;
        if (listboxInFrame > 0)
            listbox = frame.Locator($"#{dropdownId}_listbox");
        else if (listboxInPage > 0)
            listbox = Page.Locator($"#{dropdownId}_listbox");
        else
            throw new Exception($"{dropdownId}_listbox not found");

        try
        {
            await listbox.Locator($"li:has-text('{value}')").First.ClickAsync(new LocatorClickOptions { Timeout = 5000 });
        }
        catch (TimeoutException)
        {
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

    public async Task FillNumericFieldAsync(string fieldId, string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();

        var field = frame.Locator(fieldId);
        var count = await field.CountAsync();
        if (count == 0)
            throw new Exception($"Numeric field '{fieldId}' not found");

        var input = field.Locator("input").First;
        await input.ClearAsync();
        await input.FillAsync(value);
        await Task.Delay(500);

        Console.WriteLine($"✅ Numeric field filled: {value}");
    }

    public async Task FillTextFieldAsync(string fieldId, string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();

        var field = frame.Locator(fieldId);
        var count = await field.CountAsync();
        if (count == 0)
            throw new Exception($"Text field '{fieldId}' not found");

        await field.First.ClearAsync();
        await field.First.FillAsync(value);
        await Task.Delay(500);

        Console.WriteLine($"✅ Text field filled: {value}");
    }

    public async Task FillDateFieldAsync(string fieldId, string value)
    {
        var frame = await GetIncentiveConditionFrameAsync();

        var field = frame.Locator(fieldId);
        var count = await field.CountAsync();
        if (count == 0)
            throw new Exception($"Date field '{fieldId}' not found");

        await field.First.FillAsync(value);
        await Task.Delay(500);

        Console.WriteLine($"✅ Date field filled: {value}");
    }
}
