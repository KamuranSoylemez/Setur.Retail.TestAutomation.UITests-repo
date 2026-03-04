using Microsoft.Playwright;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Supplier;

/// <summary>
/// Contract Approval Screen page object
/// Handles approval operations for various supplier-related documents
/// Consolidated approval workflow for different roles
/// </summary>
public class ContractApprovalScreen : BasePage
{
    // Page locators
    private ILocator PageTitle => Page.Locator("#PageTitle");
    
    // Filter section locators
    private ILocator FilterDocumentTypeDropdown => Page.Locator("span[aria-owns='FilterEntityType_listbox']");
    private ILocator FilterStatusDropdown => Page.Locator("span[aria-owns='FilterEntityStatusId_listbox']");
    private ILocator FilterEntityStatusDropdown => Page.Locator("span[aria-owns='FilterEntityStatusId_listbox']");
    private ILocator FilterSupplierNameInput => Page.Locator("#FilterSupplierName, #FilterFirmName");
    private ILocator FilterFirmCodeInput => Page.Locator("#FilterFirmCode");
    private ILocator FilterFirmNameInput => Page.Locator("#FilterFirmName");
    private ILocator FilterContractNameInput => Page.Locator("#FilterContractName");
    private ILocator FilterDocumentNoInput => Page.Locator("#FilterDocumentNo");
    private ILocator FilterStartDateInput => Page.Locator("#FilterStartDate");
    private ILocator FilterEndDateInput => Page.Locator("#FilterEndDate");
    private ILocator FilterApprovalStatusDropdown => Page.Locator("span[aria-owns='FilterEntityStatusId_listbox']");
    private ILocator SearchButton => Page.Locator("#FilterButtonId");
    private ILocator ResetButton => Page.Locator("#ResetButtonId");
    
    // Grid locators
    private ILocator ApprovalGrid => Page.Locator("#ApprovalGridId, #ApprovalOperationsGridId, .k-grid");
    private ILocator GridRows => Page.Locator("table tbody tr, div[role='row']");
    private ILocator FirstEditButton => Page.Locator("a.k-grid-edit.gridCmdBtn, a.cmdLink").First;
    private ILocator FirstViewButton => Page.Locator("a.k-grid-view.gridCmdBtn, a.gridCmdBtn.k-success").First;
    
    // Approval action buttons
    private ILocator ApproveButton => Page.Locator("#BtnApprove, button:has-text('Onayla')");
    private ILocator RejectButton => Page.Locator("#BtnReject, button:has-text('Reddet')");
    private ILocator CancellationApproveButton => Page.Locator("#BtnApproveCancellation, button:has-text('İptal Talebini Onayla'), button:has-text('İptali Onayla')");
    private ILocator CancellationRejectButton => Page.Locator("#BtnRejectCancellation, button:has-text('İptal Talebini Reddet'), button:has-text('İptali Reddet')");
    private ILocator DirectorApproveButton => Page.Locator("#BtnDirectorApprove, button:has-text('Direktör Onayı')");
    private ILocator DirectorRejectButton => Page.Locator("#BtnDirectorReject, button:has-text('Direktör Reddi')");
    private ILocator ManagerApproveButton => Page.Locator("#BtnManagerApprove, button:has-text('Müdür Onayı')");
    private ILocator ManagerRejectButton => Page.Locator("#BtnManagerReject, button:has-text('Müdür Reddi')");
    private ILocator WithdrawButton => Page.Locator("#BtnWithdraw, button:has-text('Geri Çek')");
    
    // Comment/Reason field
    private ILocator RejectReasonTextarea => Page.Locator("#RejectReason, textarea[name*='Reason']");
    
    public async Task NavigateToApprovalOperationsAsync()
    {
        var baseUrl = ConfigurationManager.Instance.BaseUrl.TrimEnd('/');
        await Page.GotoAsync($"{baseUrl}/ApplicationManagement/ApprovalOperations/Index", new PageGotoOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded,
            Timeout = 30000
        });
        await Page.WaitForTimeoutAsync(2000);
        await WaitForLoadingAsync();
        Console.WriteLine("✅ Navigated to Approval Operations screen");
    }

    public async Task VerifyApprovalScreenIsDisplayedAsync(string expectedTitle = "Onay İşlemleri")
    {
        await PageTitle.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        var title = await PageTitle.TextContentAsync();
        title?.Should().Contain(expectedTitle, $"Page title should contain '{expectedTitle}'");
        Console.WriteLine($"✅ Approval Operations screen is displayed: {title}");
    }

    // Filter methods
    public async Task SelectDocumentTypeAsync(string documentType)
    {
        await FilterDocumentTypeDropdown.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        
        var option = Page.Locator($"li[role='option']:has-text('{documentType}')").First;
        await option.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await option.ClickAsync();
        
        Console.WriteLine($"✅ Selected document type: {documentType}");
    }

    public async Task SelectApprovalStatusAsync(string status)
    {
        await FilterApprovalStatusDropdown.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        
        var option = Page.Locator($"li[role='option']:has-text('{status}')").First;
        await option.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await option.ClickAsync();
        
        Console.WriteLine($"✅ Selected approval status: {status}");
    }

    public async Task SelectStatusAsync(string status)
    {
        await FilterStatusDropdown.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        
        var option = Page.Locator($"li[role='option']:has-text('{status}')").First;
        await option.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await option.ClickAsync();
        
        Console.WriteLine($"✅ Selected status: {status}");
    }

    public async Task FillSupplierNameAsync(string supplierName)
    {
        await FilterSupplierNameInput.FillAsync(supplierName);
        Console.WriteLine($"✅ Filled supplier name: {supplierName}");
    }

    public async Task FillFirmCodeAsync(string firmCode)
    {
        await FilterFirmCodeInput.FillAsync(firmCode);
        Console.WriteLine($"✅ Filled firm code: {firmCode}");
    }

    public async Task FillFirmNameAsync(string firmName)
    {
        await FilterFirmNameInput.FillAsync(firmName);
        Console.WriteLine($"✅ Filled firm name: {firmName}");
    }

    public async Task FillContractNameAsync(string contractName)
    {
        await FilterContractNameInput.FillAsync(contractName);
        Console.WriteLine($"✅ Filled contract name: {contractName}");
    }

    public async Task SelectEntityStatusAsync(string status)
    {
        await FilterEntityStatusDropdown.ClickAsync();
        await Page.WaitForTimeoutAsync(500);
        
        var option = Page.Locator($"li[role='option']:has-text('{status}')").First;
        await option.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await option.ClickAsync();
        
        Console.WriteLine($"✅ Selected entity status: {status}");
    }

    public async Task FillDocumentNoAsync(string documentNo)
    {
        await FilterDocumentNoInput.FillAsync(documentNo);
        Console.WriteLine($"✅ Filled document no: {documentNo}");
    }

    public async Task SelectStartDateAsync(string startDate)
    {
        await FilterStartDateInput.FillAsync(startDate);
        Console.WriteLine($"✅ Selected start date: {startDate}");
    }

    public async Task SelectEndDateAsync(string endDate)
    {
        await FilterEndDateInput.FillAsync(endDate);
        Console.WriteLine($"✅ Selected end date: {endDate}");
    }

    public async Task ClickSearchButtonAsync()
    {
        await SearchButton.ClickAsync();
        await WaitForLoadingAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine("✅ Clicked search button");
    }

    public async Task ClickResetButtonAsync()
    {
        await ResetButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Clicked reset button");
    }

    // Grid operation methods
    public async Task ClickFirstEditButtonAsync()
    {
        var editButton = await FirstEditButton.ElementHandleAsync();
        if (editButton == null)
        {
            throw new Exception("Edit button not found on grid");
        }
        
        // Wait for popup or new window to appear
        var popupTask = Page.WaitForPopupAsync();
        await FirstEditButton.ClickAsync();
        
        try
        {
            var popup = await popupTask.ConfigureAwait(false);
            // If popup opened, close it and switch context
            // For now, just log it
            Console.WriteLine("✅ Popup detected");
        }
        catch 
        {
            // No popup, might be inline modal or navigation
            Console.WriteLine("⚠️  No popup detected, continuing with current page");
        }
        
        await Page.WaitForTimeoutAsync(3000);
        Console.WriteLine("✅ Clicked first edit button");
    }

    public async Task ClickFirstViewButtonAsync()
    {
        var viewButton = await FirstViewButton.ElementHandleAsync();
        if (viewButton == null)
        {
            throw new Exception("View button not found on grid");
        }
        
        await FirstViewButton.ClickAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine("✅ Clicked first view button");
    }

    public async Task<int> GetGridRecordCountAsync()
    {
        var count = await GridRows.CountAsync();
        Console.WriteLine($"✅ Grid contains {count} records");
        return count;
    }

    public async Task VerifyRecordsExistAsync()
    {
        var count = await GetGridRecordCountAsync();
        count.Should().BeGreaterThan(0, "Grid should contain at least one record");
    }

    // Approval action methods
    public async Task ClickApproveButtonAsync()
    {
        await ApproveButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await ApproveButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Clicked approve button");
    }

    public async Task ClickRejectButtonAsync()
    {
        await RejectButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await RejectButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Clicked reject button");
    }

    public async Task FillRejectReasonAsync(string reason)
    {
        await RejectReasonTextarea.FillAsync(reason);
        Console.WriteLine($"✅ Filled reject reason: {reason}");
    }

    public async Task ClickDirectorApproveButtonAsync()
    {
        await DirectorApproveButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await DirectorApproveButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Clicked director approve button");
    }

    public async Task ClickDirectorRejectButtonAsync()
    {
        await DirectorRejectButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await DirectorRejectButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Clicked director reject button");
    }

    public async Task ClickManagerApproveButtonAsync()
    {
        await ManagerApproveButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await ManagerApproveButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Clicked manager approve button");
    }

    public async Task ClickManagerRejectButtonAsync()
    {
        await ManagerRejectButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await ManagerRejectButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Clicked manager reject button");
    }

    public async Task ClickWithdrawButtonAsync()
    {
        await WithdrawButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await WithdrawButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Clicked withdraw button");
    }

    public async Task ClickCancellationApproveButtonAsync()
    {
        await CancellationApproveButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await CancellationApproveButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Clicked cancellation approve button");
    }

    public async Task ClickCancellationRejectButtonAsync()
    {
        await CancellationRejectButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 5000 });
        await CancellationRejectButton.ClickAsync();
        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine("✅ Clicked cancellation reject button");
    }

    // Verification methods
    public async Task VerifyApproveButtonIsVisibleAsync()
    {
        var isVisible = await ApproveButton.IsVisibleAsync();
        isVisible.Should().BeTrue("Approve button should be visible");
        Console.WriteLine("✅ Approve button is visible");
    }

    public async Task VerifyRejectButtonIsVisibleAsync()
    {
        var isVisible = await RejectButton.IsVisibleAsync();
        isVisible.Should().BeTrue("Reject button should be visible");
        Console.WriteLine("✅ Reject button is visible");
    }

    public async Task VerifyDirectorApproveButtonIsVisibleAsync()
    {
        var isVisible = await DirectorApproveButton.IsVisibleAsync();
        isVisible.Should().BeTrue("Director approve button should be visible");
        Console.WriteLine("✅ Director approve button is visible");
    }

    public async Task VerifyManagerApproveButtonIsVisibleAsync()
    {
        var isVisible = await ManagerApproveButton.IsVisibleAsync();
        isVisible.Should().BeTrue("Manager approve button should be visible");
        Console.WriteLine("✅ Manager approve button is visible");
    }

    public async Task VerifyApproveButtonIsNotVisibleAsync()
    {
        var isVisible = await ApproveButton.IsVisibleAsync();
        isVisible.Should().BeFalse("Approve button should not be visible");
        Console.WriteLine("✅ Approve button is not visible (as expected)");
    }

    public async Task<int> CountVisibleActionButtonsAsync()
    {
        var buttons = new[]
        {
            ApproveButton,
            RejectButton,
            DirectorApproveButton,
            DirectorRejectButton,
            ManagerApproveButton,
            ManagerRejectButton,
            WithdrawButton,
            CancellationApproveButton,
            CancellationRejectButton
        };

        int count = 0;
        foreach (var button in buttons)
        {
            if (await button.IsVisibleAsync())
            {
                count++;
            }
        }

        Console.WriteLine($"✅ Found {count} visible action buttons");
        return count;
    }

    public async Task ConfirmApprovalDialogAsync()
    {
        await Task.Delay(1000);
        
        var confirmButton = Page.Locator("button.ajs-button.ajs-ok, button:has-text('Evet'), button:has-text('Onay'), button:has-text('OK')");
        if (await confirmButton.CountAsync() > 0)
        {
            await confirmButton.First.ClickAsync();
            await Page.WaitForTimeoutAsync(2000);
            Console.WriteLine("✅ Confirmed approval dialog");
        }
    }

    public async Task CancelApprovalDialogAsync()
    {
        await Task.Delay(1000);
        
        var cancelButton = Page.Locator("button.ajs-button.ajs-cancel, button:has-text('Hayır'), button:has-text('İptal')");
        if (await cancelButton.CountAsync() > 0)
        {
            await cancelButton.First.ClickAsync();
            await Page.WaitForTimeoutAsync(1500);
            Console.WriteLine("✅ Cancelled approval dialog");
        }
    }

    // Check if action buttons are visible on detail screen (inside iframe)
    public async Task<(bool UpdateVisible, bool CloseVisible)> CheckDetailScreenButtonsAsync()
    {
        // Wait for detail screen modal to fully load
        await Page.WaitForTimeoutAsync(3000);
        
        try
        {
            bool updateVisible = false;
            bool closeVisible = false;
            
            // Check all frames
            var frames = Page.Frames;
            Console.WriteLine($"DEBUG: Total frames on page: {frames.Count}");
            
            // First try main page
            var updateBtn = Page.Locator("#btnSave");
            var closeBtn = Page.Locator("#ClosePopupBtn");
            
            updateVisible = await updateBtn.IsVisibleAsync();
            closeVisible = await closeBtn.IsVisibleAsync();
            
            Console.WriteLine($"DEBUG: Main page - btnSave: {updateVisible}, ClosePopupBtn: {closeVisible}");
            
            // If not found on main page, check frames
            if (!updateVisible || !closeVisible)
            {
                foreach (var frame in frames)
                {
                    var frameUpdateBtn = frame.Locator("#btnSave");
                    var frameCloseBtn = frame.Locator("#ClosePopupBtn");
                    
                    int updateCount = await frameUpdateBtn.CountAsync();
                    int closeCount = await frameCloseBtn.CountAsync();
                    
                    Console.WriteLine($"DEBUG: Frame '{frame.Name}' - btnSave: {updateCount}, ClosePopupBtn: {closeCount}");
                    
                    if (updateCount > 0)
                    {
                        updateVisible = await frameUpdateBtn.IsVisibleAsync();
                        Console.WriteLine($"DEBUG:   btnSave visible in frame: {updateVisible}");
                    }
                    
                    if (closeCount > 0)
                    {
                        closeVisible = await frameCloseBtn.IsVisibleAsync();
                        Console.WriteLine($"DEBUG:   ClosePopupBtn visible in frame: {closeVisible}");
                    }
                    
                    if (updateVisible && closeVisible)
                        break;
                }
            }
            
            Console.WriteLine($"DEBUG: Final result - btnSave: {updateVisible}, ClosePopupBtn: {closeVisible}");
            
            return (updateVisible, closeVisible);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error checking buttons: {ex.Message}");
            return (false, false);
        }
    }

    // Check if approval action buttons (Onayla, Reddet) are visible on detail screen (inside iframe)
    public async Task<(bool ApproveVisible, bool RejectVisible)> CheckApprovalActionButtonsAsync()
    {
        try
        {
            bool approveVisible = false;
            bool rejectVisible = false;
            
            // Check all frames
            var frames = Page.Frames;
            Console.WriteLine($"DEBUG: Checking approval buttons in {frames.Count} frames");
            
            // First try main page
            approveVisible = await ApproveButton.IsVisibleAsync();
            rejectVisible = await RejectButton.IsVisibleAsync();
            
            Console.WriteLine($"DEBUG: Main page - Onayla: {approveVisible}, Reddet: {rejectVisible}");
            
            // If not found on main page, check frames
            if (!approveVisible || !rejectVisible)
            {
                foreach (var frame in frames)
                {
                    var frameApproveBtn = frame.Locator("#BtnApprove, button:has-text('Onayla')");
                    var frameRejectBtn = frame.Locator("#BtnReject, button:has-text('Reddet')");
                    
                    int approveCount = await frameApproveBtn.CountAsync();
                    int rejectCount = await frameRejectBtn.CountAsync();
                    
                    Console.WriteLine($"DEBUG: Frame '{frame.Name}' - Onayla: {approveCount}, Reddet: {rejectCount}");
                    
                    if (approveCount > 0)
                    {
                        approveVisible = await frameApproveBtn.IsVisibleAsync();
                        Console.WriteLine($"DEBUG:   Onayla visible in frame: {approveVisible}");
                    }
                    
                    if (rejectCount > 0)
                    {
                        rejectVisible = await frameRejectBtn.IsVisibleAsync();
                        Console.WriteLine($"DEBUG:   Reddet visible in frame: {rejectVisible}");
                    }
                    
                    if (approveVisible && rejectVisible)
                        break;
                }
            }
            
            Console.WriteLine($"DEBUG: Final result - Onayla: {approveVisible}, Reddet: {rejectVisible}");
            
            return (approveVisible, rejectVisible);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error checking approval action buttons: {ex.Message}");
            return (false, false);
        }
    }

    // Check if cancellation action buttons (İptal Talebini Onayla, İptal Talebini Reddet, Güncelle, Kapat) are visible on detail screen (inside iframe)
    public async Task<(bool UpdateVisible, bool CloseVisible, bool CancellationApproveVisible, bool CancellationRejectVisible)> CheckCancellationButtonsAsync()
    {
        try
        {
            bool updateVisible = false;
            bool closeVisible = false;
            bool cancellationApproveVisible = false;
            bool cancellationRejectVisible = false;
            
            // Wait for detail screen modal to fully load
            await Page.WaitForTimeoutAsync(2000);
            
            // Check all frames
            var frames = Page.Frames;
            Console.WriteLine($"DEBUG: Checking cancellation buttons in {frames.Count} frames");
            
            // First try main page
            updateVisible = await Page.Locator("#btnSave").IsVisibleAsync();
            closeVisible = await Page.Locator("#ClosePopupBtn").IsVisibleAsync();
            cancellationApproveVisible = await CancellationApproveButton.IsVisibleAsync();
            cancellationRejectVisible = await CancellationRejectButton.IsVisibleAsync();
            
            Console.WriteLine($"DEBUG: Main page - Güncelle: {updateVisible}, Kapat: {closeVisible}, İptal Onayla: {cancellationApproveVisible}, İptal Reddet: {cancellationRejectVisible}");
            
            // If not found on main page, check frames
            if (!updateVisible || !closeVisible || !cancellationApproveVisible || !cancellationRejectVisible)
            {
                foreach (var frame in frames)
                {
                    var frameUpdateBtn = frame.Locator("#btnSave");
                    var frameCloseBtn = frame.Locator("#ClosePopupBtn");
                    var frameCancelApproveBtn = frame.Locator("#BtnApproveCancellation, button:has-text('İptal Talebini Onayla'), button:has-text('İptali Onayla')");
                    var frameCancelRejectBtn = frame.Locator("#BtnRejectCancellation, button:has-text('İptal Talebini Reddet'), button:has-text('İptali Reddet')");
                    
                    int updateCount = await frameUpdateBtn.CountAsync();
                    int closeCount = await frameCloseBtn.CountAsync();
                    int cancelApproveCount = await frameCancelApproveBtn.CountAsync();
                    int cancelRejectCount = await frameCancelRejectBtn.CountAsync();
                    
                    Console.WriteLine($"DEBUG: Frame '{frame.Name}' - Güncelle: {updateCount}, Kapat: {closeCount}, İptal Onayla: {cancelApproveCount}, İptal Reddet: {cancelRejectCount}");
                    
                    if (updateCount > 0)
                        updateVisible = await frameUpdateBtn.IsVisibleAsync();
                    
                    if (closeCount > 0)
                        closeVisible = await frameCloseBtn.IsVisibleAsync();
                    
                    if (cancelApproveCount > 0)
                        cancellationApproveVisible = await frameCancelApproveBtn.IsVisibleAsync();
                    
                    if (cancelRejectCount > 0)
                        cancellationRejectVisible = await frameCancelRejectBtn.IsVisibleAsync();
                    
                    if (updateVisible && closeVisible && cancellationApproveVisible && cancellationRejectVisible)
                        break;
                }
            }
            
            Console.WriteLine($"DEBUG: Final result - Güncelle: {updateVisible}, Kapat: {closeVisible}, İptal Onayla: {cancellationApproveVisible}, İptal Reddet: {cancellationRejectVisible}");
            
            return (updateVisible, closeVisible, cancellationApproveVisible, cancellationRejectVisible);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error checking cancellation buttons: {ex.Message}");
            return (false, false, false, false);
        }
    }
}
