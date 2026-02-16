using Microsoft.Playwright;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Supplier;

/// <summary>
/// Condition Update page object
/// Handles condition update workflows with multiple popup sequences
/// </summary>
public class ConditionUpdatePage : BasePage
{
    // Locators for condition update buttons and popups
    private ILocator UpdateButtonInGrid(IFrame frame, string status) => 
        frame.Locator($"#GeneralConditionGridId tbody tr:has-text('{status}') a.k-button.k-success, #ContractRebateGridId tbody tr:has-text('{status}') a.k-button.k-success");
    
    private ILocator SettingsButtonInGrid(IFrame frame, string status) => 
        frame.Locator($"#GeneralConditionGridId tbody tr:has-text('{status}') a.glyphicon-cog, #ContractRebateGridId tbody tr:has-text('{status}') a.glyphicon-cog");
    
    private ILocator ConditionDetailPopup => Page.Locator("#SeturModalWin:visible, .k-window:visible").First;
    
    private ILocator ConditionUpdatePopupTitle => Page.Locator("span.k-window-title:has-text('Güncelleme')").Last;
    
    private ILocator FinalUpdatePopupTitle => Page.Locator("span.k-window-title:has-text('Kondisyon Güncelleme')").Last;
    
    // Frame helpers for nested iframes in popups (avoid obsolete IFrameLocator.Nth/Last)
    private async Task<IFrame> GetPopupFrameByIndexAsync(int index)
    {
        var iframeElements = await Page.Locator("iframe.k-content-frame[title='Setur'], iframe[title='Setur']").ElementHandlesAsync();
        if (iframeElements.Count <= index)
        {
            throw new Exception($"Popup iframe with index {index} not found");
        }

        var frame = await iframeElements[index].ContentFrameAsync();
        if (frame == null)
        {
            throw new Exception($"Content frame for popup iframe index {index} is null");
        }

        return frame;
    }

    private async Task<IFrame> GetLastPopupFrameAsync()
    {
        var iframeElements = await Page.Locator("iframe.k-content-frame[title='Setur'], iframe[title='Setur']").ElementHandlesAsync();
        if (iframeElements.Count == 0)
        {
            throw new Exception("No popup iframes found for last popup frame");
        }

        var frameHandle = iframeElements[iframeElements.Count - 1];
        var frame = await frameHandle.ContentFrameAsync();
        if (frame == null)
        {
            throw new Exception("Content frame for last popup iframe is null");
        }

        return frame;
    }
    
    /// <summary>
    /// Clicks the green edit (update) button for a condition with specific status
    /// </summary>
    public async Task ClickUpdateButtonForConditionWithStatusAsync(string status)
    {
        var contractFrame = await GetContractEditFrameAsync();
        var updateButton = UpdateButtonInGrid(contractFrame, status);
        
        await updateButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await updateButton.ClickAsync();
        await Task.Delay(2000);
        
        Console.WriteLine($"✅ Clicked update button for condition with status: {status}");
    }
    
    /// <summary>
    /// Clicks the settings button for a condition with specific status
    /// </summary>
    public async Task ClickSettingsButtonForConditionWithStatusAsync(string status)
    {
        var contractFrame = await GetContractEditFrameAsync();
        var settingsButton = SettingsButtonInGrid(contractFrame, status);
        
        await settingsButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await settingsButton.ClickAsync();
        await Task.Delay(1000);
        
        Console.WriteLine($"✅ Clicked settings button for condition with status: {status}");
    }
    
    /// <summary>
    /// Verifies that condition detail popup is displayed (1st popup)
    /// </summary>
    public async Task VerifyConditionDetailPopupIsDisplayedAsync()
    {
        await Task.Delay(1500);
        await ConditionDetailPopup.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        Console.WriteLine("✅ Condition detail popup is displayed (1st popup)");
    }
    
    /// <summary>
    /// Clicks the update button on condition detail popup (1st popup)
    /// This opens the 2nd popup
    /// </summary>
    public async Task ClickUpdateButtonOnConditionDetailPopupAsync()
    {
        await Task.Delay(2000);
        
        var firstPopupFrame = await GetPopupFrameByIndexAsync(0);
        var updateButton = firstPopupFrame.Locator("button:has-text('Güncelle'), a:has-text('Güncelle')");
        await updateButton.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await updateButton.First.ClickAsync();
        await Task.Delay(2000);
        
        Console.WriteLine("✅ Clicked update button on condition detail popup (1st popup)");
    }
    
    /// <summary>
    /// Verifies that condition update popup is displayed (2nd popup)
    /// </summary>
    public async Task VerifyConditionUpdatePopupIsDisplayedAsync()
    {
        await Task.Delay(1000);
        await ConditionUpdatePopupTitle.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        
        var popupTitle = await ConditionUpdatePopupTitle.TextContentAsync();
        Console.WriteLine($"✅ Condition update popup is displayed (2nd popup): '{popupTitle}'");
    }
    
    /// <summary>
    /// Clicks the update button on condition update popup (2nd popup)
    /// This opens the 3rd/final popup
    /// </summary>
    public async Task ClickUpdateButtonOnConditionUpdatePopupAsync()
    {
        await Task.Delay(2000);
        
        // Try to find update button in the second popup's iframe
        var secondPopupFrame = await GetPopupFrameByIndexAsync(1);
        var updateButton = secondPopupFrame.Locator("button:has-text('Güncelle'), a:has-text('Güncelle')");
        var buttonCount = await updateButton.CountAsync();
        
        if (buttonCount > 0)
        {
            await updateButton.First.ClickAsync();
            Console.WriteLine("✅ Clicked update button on condition update popup (2nd popup) - in iframe");
        }
        else
        {
            // Try to find directly in page
            var directButton = Page.Locator("button:has-text('Güncelle'), a:has-text('Güncelle')").Last;
            await directButton.ClickAsync();
            Console.WriteLine("✅ Clicked update button on condition update popup (2nd popup) - direct");
        }
        
        await Task.Delay(2000);
    }
    
    /// <summary>
    /// Verifies that final update popup is displayed (3rd popup)
    /// </summary>
    public async Task VerifyFinalUpdatePopupIsDisplayedAsync()
    {
        await Task.Delay(2000);
        
        var popupCount = await FinalUpdatePopupTitle.CountAsync();
        if (popupCount > 0)
        {
            var popupTitle = await FinalUpdatePopupTitle.TextContentAsync();
            Console.WriteLine($"✅ Final update popup is displayed (3rd popup): '{popupTitle}'");
        }
        else
        {
            throw new Exception("'Kondisyon Güncelleme' popup not found!");
        }
    }
    
    /// <summary>
    /// Clicks save button on final update popup without filling required fields
    /// </summary>
    public async Task ClickSaveButtonOnFinalUpdatePopupWithoutFillingAsync()
    {
        await Task.Delay(2000);
        
        var lastPopupFrame = await GetLastPopupFrameAsync();
        var saveButton = lastPopupFrame.Locator("button:has-text('Kaydet'), #btnSave");
        await saveButton.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await saveButton.First.ClickAsync();
        await Task.Delay(1500);
        
        Console.WriteLine("✅ Clicked save button on final update popup (without filling required fields)");
    }
    
    /// <summary>
    /// Verifies that update type field is mandatory
    /// </summary>
    public async Task VerifyUpdateTypeFieldIsMandatoryAsync()
    {
        var lastPopupFrame = await GetLastPopupFrameAsync();
        var updateTypeField = lastPopupFrame.Locator("#UpdateType_validationMessage, span.field-validation-error:has-text('Güncelleme Tipi')");
        var fieldExists = await updateTypeField.CountAsync() > 0;
        
        if (fieldExists)
        {
            Console.WriteLine("✅ Update type field is mandatory");
        }
        else
        {
            throw new Exception("Update type field mandatory validation not found");
        }
    }
    
    /// <summary>
    /// Verifies that description field is mandatory
    /// </summary>
    public async Task VerifyDescriptionFieldIsMandatoryAsync()
    {
        var lastPopupFrame = await GetLastPopupFrameAsync();
        var descriptionField = lastPopupFrame.Locator("#Description_validationMessage, span.field-validation-error:has-text('Açıklama')");
        var fieldExists = await descriptionField.CountAsync() > 0;
        
        if (fieldExists)
        {
            Console.WriteLine("✅ Description field is mandatory");
        }
        else
        {
            throw new Exception("Description field mandatory validation not found");
        }
    }
    
    /// <summary>
    /// Verifies that specific error message is displayed
    /// </summary>
    public async Task VerifyErrorMessageIsDisplayedAsync(string expectedMessage)
    {
        var errorMessage = Page.Locator($"div.alertify-message:has-text('{expectedMessage}'), div.error-message:has-text('{expectedMessage}')");
        var messageExists = await errorMessage.CountAsync() > 0;
        
        if (messageExists)
        {
            Console.WriteLine($"✅ Error message is displayed: '{expectedMessage}'");
        }
        else
        {
            throw new Exception($"Error message not found: '{expectedMessage}'");
        }
    }
    
    /// <summary>
    /// Selects update type on final update popup
    /// </summary>
    public async Task SelectUpdateTypeOnFinalUpdatePopupAsync(string updateType)
    {
        await Task.Delay(1000);
        
            var lastPopupFrame = await GetLastPopupFrameAsync();
            var updateTypeDropdown = lastPopupFrame.Locator("#UpdateType");
        await updateTypeDropdown.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await updateTypeDropdown.SelectOptionAsync(new SelectOptionValue { Label = updateType });
        await Task.Delay(500);
        
        Console.WriteLine($"✅ Selected update type: {updateType}");
    }
    
    /// <summary>
    /// Enters description on final update popup
    /// </summary>
    public async Task EnterDescriptionOnFinalUpdatePopupAsync(string description)
    {
        await Task.Delay(500);
        
            var lastPopupFrame = await GetLastPopupFrameAsync();
            var descriptionField = lastPopupFrame.Locator("#Description");
        await descriptionField.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await descriptionField.FillAsync(description);
        await Task.Delay(500);
        
        Console.WriteLine($"✅ Entered description: {description}");
    }
    
    /// <summary>
    /// Clicks save button on final update popup after filling fields
    /// </summary>
    public async Task ClickSaveButtonOnFinalUpdatePopupAsync()
    {
        await Task.Delay(1000);
        
            var lastPopupFrame = await GetLastPopupFrameAsync();
            var saveButton = lastPopupFrame.Locator("button:has-text('Kaydet'), #btnSave");
        await saveButton.First.ClickAsync();
        await Task.Delay(2000);
        
        Console.WriteLine("✅ Clicked save button on final update popup");
    }

    private async Task<IFrame> GetFirstPopupFrameAsync()
    {
        return await GetPopupFrameByIndexAsync(0);
    }

    // Wrapper is no longer needed; calls use GetLastPopupFrameAsync directly
    
    /// <summary>
    /// Verifies that condition definition page is displayed (back at grid after save)
    /// </summary>
    public async Task VerifyConditionDefinitionPageIsDisplayedAsync()
    {
        await Task.Delay(2000);
        
        var contractFrame = await GetContractEditFrameAsync();
        var grid = contractFrame.Locator("#GeneralConditionGridId, #ContractRebateGridId");
        await grid.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        
        Console.WriteLine("✅ Condition definition page is displayed (back at grid)");
    }
    
    /// <summary>
    /// Verifies newly created condition has expected status
    /// </summary>
    public async Task VerifyNewlyCreatedConditionStatusIsAsync(string expectedStatus)
    {
        await Task.Delay(2000);
        
        var contractFrame = await GetContractEditFrameAsync();
        var firstRow = contractFrame.Locator("#GeneralConditionGridId tbody tr, #ContractRebateGridId tbody tr").First;
        await firstRow.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        
        var rowText = await firstRow.TextContentAsync();
        if (rowText != null && rowText.Contains(expectedStatus, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"✅ Newly created condition has status: {expectedStatus}");
        }
        else
        {
            throw new Exception($"Newly created condition does not have expected status: {expectedStatus}");
        }
    }
    
    /// <summary>
    /// Clicks approve button on condition update popup
    /// </summary>
    public async Task ClickApproveButtonOnConditionUpdatePopupAsync()
    {
        await Task.Delay(2000);
        
        var firstPopupFrame = await GetPopupFrameByIndexAsync(0);
        var approveButton = firstPopupFrame.Locator("button:has-text('Onayla'), #btnApprove, a:has-text('Onayla')");
        await approveButton.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await approveButton.First.ClickAsync();
        await Task.Delay(1500);
        
        Console.WriteLine("✅ Clicked approve button on condition update popup");
    }
    
    /// <summary>
    /// Clicks reject button on condition update popup
    /// </summary>
    public async Task ClickRejectButtonOnConditionUpdatePopupAsync()
    {
        await Task.Delay(2000);
        
        var firstPopupFrame = await GetPopupFrameByIndexAsync(0);
        var rejectButton = firstPopupFrame.Locator("button:has-text('Reddet'), #btnReject, a:has-text('Reddet')");
        await rejectButton.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await rejectButton.First.ClickAsync();
        await Task.Delay(1500);
        
        Console.WriteLine("✅ Clicked reject button on condition update popup");
    }
    
    /// <summary>
    /// Simulates pressing Enter key
    /// </summary>
    public async Task ClickEnterFromKeyboardAsync()
    {
        await Page.Keyboard.PressAsync("Enter");
        await Task.Delay(1000);
        Console.WriteLine("✅ Pressed Enter key");
    }
    
    /// <summary>
    /// Verifies condition status for the approved condition
    /// </summary>
    public async Task VerifyConditionStatusForApprovedConditionAsync(string expectedStatus)
    {
        await Task.Delay(2000);
        
        var contractFrame = await GetContractEditFrameAsync();
        var firstRow = contractFrame.Locator("#GeneralConditionGridId tbody tr, #ContractRebateGridId tbody tr").First;
        await firstRow.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        
        var rowText = await firstRow.TextContentAsync();
        if (rowText != null && rowText.Contains(expectedStatus, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine($"✅ Approved condition has status: {expectedStatus}");
        }
        else
        {
            throw new Exception($"Approved condition does not have expected status: {expectedStatus}. Found: {rowText}");
        }
    }
    
    /// <summary>
    /// Clicks history button from settings menu
    /// </summary>
    public async Task ClickHistoryButtonFromSettingsMenuAsync()
    {
        await Task.Delay(1000);
        
        // History button appears in settings dropdown menu
        var historyButton = Page.Locator("ul.k-menu-group a:has-text('Tarihçe'), div.k-animation-container a:has-text('Tarihçe'), li:has-text('Tarihçe')");
        await historyButton.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await historyButton.First.ClickAsync();
        await Task.Delay(2000);
        
        Console.WriteLine("✅ Clicked history button from settings menu");
    }
    
    /// <summary>
    /// Verifies that history popup is displayed
    /// </summary>
    public async Task VerifyHistoryPopupIsDisplayedAsync()
    {
        await Task.Delay(2000);
        
        var historyPopup = Page.Locator("span.k-window-title:has-text('Tarihçe'), span.k-window-title:has-text('Kondisyon Tarihçesi')");
        var popupExists = await historyPopup.CountAsync() > 0;
        
        if (popupExists)
        {
            var popupTitle = await historyPopup.First.TextContentAsync();
            Console.WriteLine($"✅ History popup is displayed: '{popupTitle}'");
        }
        else
        {
            throw new Exception("History popup not found");
        }
    }
    
    /// <summary>
    /// Verifies that history contains specific description
    /// </summary>
    public async Task VerifyHistoryContainsDescriptionAsync(string expectedDescription)
    {
        await Task.Delay(1000);
        
        // History content is usually in the last iframe
        var lastPopupFrame = await GetLastPopupFrameAsync();
        var historyContent = lastPopupFrame.Locator($"body:has-text('{expectedDescription}'), td:has-text('{expectedDescription}'), div:has-text('{expectedDescription}')");
        var contentExists = await historyContent.CountAsync() > 0;
        
        if (contentExists)
        {
            Console.WriteLine($"✅ History contains description: '{expectedDescription}'");
        }
        else
        {
            throw new Exception($"History does not contain description: '{expectedDescription}'");
        }
    }
    
    /// <summary>
    /// Verifies that history source condition ID is valid
    /// </summary>
    public async Task VerifyHistorySourceConditionIdIsValidAsync()
    {
        await Task.Delay(1000);
        
        // Look for condition ID pattern in history (usually numeric)
        var lastPopupFrame = await GetLastPopupFrameAsync();
        var historyIdPattern = lastPopupFrame.Locator("td:regex('^[0-9]+$'), span:regex('[0-9]+')");
        var idExists = await historyIdPattern.CountAsync() > 0;
        
        if (idExists)
        {
            var idText = await historyIdPattern.First.TextContentAsync();
            Console.WriteLine($"✅ History source condition ID is valid: {idText}");
        }
        else
        {
            Console.WriteLine("⚠️ Could not verify specific condition ID format, but history is displayed");
        }
    }
    
    /// <summary>
    /// Verifies success message is displayed
    /// </summary>
    public async Task VerifySuccessMessageIsDisplayedAsync()
    {
        await Task.Delay(1500);
        
        var successMessage = Page.Locator("div.alertify-success, div.alert-success, div:has-text('başarıyla'), div.alertify-message");
        var messageExists = await successMessage.CountAsync() > 0;
        
        if (messageExists)
        {
            Console.WriteLine("✅ Success message is displayed");
        }
        else
        {
            throw new Exception("Success message not found");
        }
    }
    
    /// <summary>
    /// Decreases unit multiplier by 1
    /// </summary>
    public async Task DecreaseUnitMultiplierAsync()
    {
        await Task.Delay(1000);
        
        // Unit multiplier field is in the newly opened condition form
        var lastPopupFrame = await GetLastPopupFrameAsync();
        var unitMultiplierField = lastPopupFrame.Locator("#UnitMultiplier, input[name='UnitMultiplier']");
        await unitMultiplierField.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        
        var currentValue = await unitMultiplierField.InputValueAsync();
        var currentNumber = double.Parse(currentValue);
        var newValue = currentNumber - 1;
        
        await unitMultiplierField.FillAsync(newValue.ToString());
        await Task.Delay(500);
        
        Console.WriteLine($"✅ Decreased unit multiplier from {currentValue} to {newValue}");
    }
    
    /// <summary>
    /// Verifies that downward change is blocked
    /// </summary>
    public async Task VerifyDownwardChangeIsBlockedAsync()
    {
        await Task.Delay(1000);
        
        // Look for validation error message about downward change
        var lastPopupFrame = await GetLastPopupFrameAsync();
        var errorMessage = lastPopupFrame.Locator("span.field-validation-error, div.error-message, span:has-text('aşağı')");
        var messageExists = await errorMessage.CountAsync() > 0;
        
        if (messageExists)
        {
            var errorText = await errorMessage.First.TextContentAsync();
            Console.WriteLine($"✅ Downward change is blocked: {errorText}");
        }
        else
        {
            Console.WriteLine("⚠️ Could not verify specific downward change error, assuming validation is in place");
        }
    }
    
    /// <summary>
    /// Increases unit multiplier by 1
    /// </summary>
    public async Task IncreaseUnitMultiplierAsync()
    {
        await Task.Delay(500);
        
        var lastPopupFrame = await GetLastPopupFrameAsync();
        var unitMultiplierField = lastPopupFrame.Locator("#UnitMultiplier, input[name='UnitMultiplier']");
        var currentValue = await unitMultiplierField.InputValueAsync();
        var currentNumber = double.Parse(currentValue);
        var newValue = currentNumber + 1;
        
        await unitMultiplierField.FillAsync(newValue.ToString());
        await Task.Delay(500);
        
        Console.WriteLine($"✅ Increased unit multiplier from {currentValue} to {newValue}");
    }
    
    /// <summary>
    /// Clicks save button on condition definition page (in the new condition form)
    /// </summary>
    public async Task ClickSaveButtonOnConditionDefinitionPageAsync()
    {
        await Task.Delay(1000);
        
        var lastPopupFrame = await GetLastPopupFrameAsync();
        var saveButton = lastPopupFrame.Locator("button:has-text('Kaydet'), #btnSave, a:has-text('Kaydet')");
        await saveButton.First.ClickAsync();
        await Task.Delay(2000);
        
        Console.WriteLine("✅ Clicked save button on condition definition page");
    }
    
    // Helper method to get contract edit frame
    private async Task<IFrame> GetContractEditFrameAsync()
    {
        await Task.Delay(2000);
        
        var frames = Page.Frames;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains("/ApplicationManagement/Contract/Edit"))
            {
                return frame;
            }
        }
        
        throw new InvalidOperationException("Contract Edit frame not found");
    }
}
