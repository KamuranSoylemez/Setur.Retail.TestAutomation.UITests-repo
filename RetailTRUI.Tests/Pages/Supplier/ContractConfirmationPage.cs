using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.Supplier;

/// <summary>
/// Contract Confirmation page object
/// Handles contract approval workflows for different roles
/// </summary>
public class ContractConfirmationPage : BasePage
{
    private ILocator PageTitle => Page.Locator("#PageTitle");
    private ILocator ContractNameInput => Page.Locator("#FilterContractName");
    private ILocator StartDateInput => Page.Locator("#FilterStartDate");
    private ILocator EndDateInput => Page.Locator("#FilterEndDate");
    private ILocator IncotermDropdown => Page.Locator("span[aria-owns='FilterIncotermsId_listbox']");
    private ILocator ContractStatusDropdown => Page.Locator("span[aria-owns='FilterContractStatusId_listbox']");
    private ILocator FirmCodeInput => Page.Locator("#FilterFirmCode");
    private ILocator FirmNameInput => Page.Locator("#FilterFirmName");
    private ILocator SearchButton => Page.Locator("#FilterButtonId");
    private ILocator FirstEditButton => Page.Locator(".gridCmdBtn.cmdLink.ContractWaitingForApprovalGridCmd").First;
    
    // Approval buttons
    private ILocator ContractApproveButton => Page.Locator("#ContractApprove");
    private ILocator ContractRejectButton => Page.Locator("#ContractReject");
    private ILocator ContractCancellationApproveButton => Page.Locator("#ContractApproveCancellation");
    private ILocator ContractCancellationRejectButton => Page.Locator("#ContractRejectCancellation");
    private ILocator ContractDirectorApproveButton => Page.Locator("#ContractDirectorApprove");
    private ILocator ContractDirectorRejectButton => Page.Locator("#ContractDirectorReject");
    private ILocator CallBackButton => Page.Locator("#ContractWithdraw");

    public async Task VerifyContractConfirmationPageIsDisplayedAsync(string expectedTitle)
    {
        var title = await PageTitle.TextContentAsync();
        title?.Trim().Should().Be(expectedTitle);
    }

    public async Task FillContractNameAsync(string contractName)
    {
        await ContractNameInput.FillAsync(contractName);
    }

    public async Task SelectStartDateAsync(string startDate)
    {
        await StartDateInput.FillAsync(startDate);
    }

    public async Task SelectEndDateAsync(string endDate)
    {
        await EndDateInput.FillAsync(endDate);
    }

    public async Task SelectIncotermAsync()
    {
        await IncotermDropdown.ClickAsync();
        await Page.Locator("li[role='option']:has-text('CFR - Cost and Freight')").ClickAsync();
    }

    public async Task SelectContractStatusAsync(string status)
    {
        await ContractStatusDropdown.ClickAsync();
        await Page.WaitForSelectorAsync("ul#FilterContractStatusId_listbox >> li[role='option']");
        
        var options = await Page.Locator("ul#FilterContractStatusId_listbox >> li[role='option']").AllAsync();
        foreach (var option in options)
        {
            var text = await option.TextContentAsync();
            if (text?.Trim().Contains(status.Trim()) == true)
            {
                await option.ClickAsync();
                break;
            }
        }
    }

    public async Task FillFirmCodeAsync(string firmCode)
    {
        await FirmCodeInput.FillAsync(firmCode);
    }

    public async Task FillFirmNameAsync(string firmName)
    {
        await FirmNameInput.FillAsync(firmName);
    }

    public async Task ClickSearchButtonAsync()
    {
        await SearchButton.ClickAsync();
        await WaitForLoadingAsync();
        // Wait for grid to finish loading (don't require edit button to exist)
        await Task.Delay(2000);
    }

    public async Task ClickFirstEditButtonAsync()
    {
        await FirstEditButton.ClickAsync();
        // Wait a moment for iframe to appear
        await Task.Delay(2000);
    }

    private Task<IFrame?> GetContractEditFrameAsync()
    {
        // Find iframe containing /ApplicationManagement/Contract/Edit
        var frames = Page.Frames;
        foreach (var frame in frames)
        {
            if (frame.Url.Contains("/ApplicationManagement/Contract/Edit"))
            {
                return Task.FromResult<IFrame?>(frame);
            }
        }
        return Task.FromResult<IFrame?>(null);
    }

    public async Task VerifyContractApproveButtonIsVisibleAsync()
    {
        var frame = await GetContractEditFrameAsync();
        if (frame != null)
        {
            var isVisible = await frame.Locator("#ContractApprove").IsVisibleAsync();
            isVisible.Should().BeTrue("Contract approve button should be visible");
        }
        else
        {
            var isVisible = await ContractApproveButton.IsVisibleAsync();
            isVisible.Should().BeTrue("Contract approve button should be visible");
        }
    }

    public async Task VerifyContractRejectButtonIsVisibleAsync()
    {
        var frame = await GetContractEditFrameAsync();
        if (frame != null)
        {
            var isVisible = await frame.Locator("#ContractReject").IsVisibleAsync();
            isVisible.Should().BeTrue("Contract reject button should be visible");
        }
        else
        {
            var isVisible = await ContractRejectButton.IsVisibleAsync();
            isVisible.Should().BeTrue("Contract reject button should be visible");
        }
    }

    public async Task VerifyContractCancellationApproveButtonIsVisibleAsync()
    {
        var frame = await GetContractEditFrameAsync();
        if (frame != null)
        {
            var isVisible = await frame.Locator("#ContractApproveCancellation").IsVisibleAsync();
            isVisible.Should().BeTrue("Contract cancellation approve button should be visible");
        }
        else
        {
            var isVisible = await ContractCancellationApproveButton.IsVisibleAsync();
            isVisible.Should().BeTrue("Contract cancellation approve button should be visible");
        }
    }

    public async Task VerifyContractCancellationRejectButtonIsVisibleAsync()
    {
        var frame = await GetContractEditFrameAsync();
        if (frame != null)
        {
            var isVisible = await frame.Locator("#ContractRejectCancellation").IsVisibleAsync();
            isVisible.Should().BeTrue("Contract cancellation reject button should be visible");
        }
        else
        {
            var isVisible = await ContractCancellationRejectButton.IsVisibleAsync();
            isVisible.Should().BeTrue("Contract cancellation reject button should be visible");
        }
    }

    public async Task VerifyContractDirectorApproveButtonIsVisibleAsync()
    {
        var frame = await GetContractEditFrameAsync();
        if (frame != null)
        {
            var isVisible = await frame.Locator("#ContractDirectorApprove").IsVisibleAsync();
            isVisible.Should().BeTrue("Contract director approve button should be visible in iframe");
        }
        else
        {
            var isVisible = await ContractDirectorApproveButton.IsVisibleAsync();
            isVisible.Should().BeTrue("Contract director approve button should be visible");
        }
    }

    public async Task VerifyContractDirectorRejectButtonIsVisibleAsync()
    {
        var frame = await GetContractEditFrameAsync();
        if (frame != null)
        {
            var isVisible = await frame.Locator("#ContractDirectorReject").IsVisibleAsync();
            isVisible.Should().BeTrue("Contract director reject button should be visible in iframe");
        }
        else
        {
            var isVisible = await ContractDirectorRejectButton.IsVisibleAsync();
            isVisible.Should().BeTrue("Contract director reject button should be visible");
        }
    }

    public async Task VerifyCallBackButtonIsVisibleAsync()
    {
        var isVisible = await CallBackButton.IsVisibleAsync();
        isVisible.Should().BeTrue("Callback button should be visible");
    }

    public async Task<int> CountVisibleButtonsAsync()
    {
        var buttons = await Page.Locator("button[id$='Btn']:visible").CountAsync();
        return buttons;
    }
}
