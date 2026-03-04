using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Supplier;
using Xunit.Abstractions;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Contract Approval Screen tests for Director role
/// Tests unified approval workflows specific to Director responsibilities
/// Mirrors ContractConfirmationDirectorTests patterns for semantic naming
/// Requires user with Director approval permissions
/// </summary>
public class ContractApprovalScreenDirectorTests : DirectorTestBase
{
    private readonly ITestOutputHelper _output;
    private ContractApprovalScreen? _approvalScreen;

    public ContractApprovalScreenDirectorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _approvalScreen = new ContractApprovalScreen();
    }

    // ========== DIRECTOR ROLE - APPROVAL STATUS TESTS ==========

    [Fact(DisplayName = "DirectorApprovalOperations_WithDirectorApprovalStatus_ShouldDisplayDirectorButtons")]
    public async Task DirectorApprovalOperations_WithDirectorApprovalStatus_ShouldDisplayDirectorButtons()
    {
        // Ensure Page is available in this async context
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Arrange
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        // Act - Search for contracts with Director Approval status
        await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
        await Task.Delay(500);
        await _approvalScreen.SelectApprovalStatusAsync("Direktör Onayı Bekleniyor");
        await Task.Delay(1000);
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(2000);
        
        // Assert - Verify director approval structure
        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        if (recordCount > 0)
        {
            await _approvalScreen.ClickFirstEditButtonAsync();
            await Task.Delay(2000);
            
            var (updateVisible, closeVisible) = await _approvalScreen.CheckDetailScreenButtonsAsync();
            var (approveVisible, rejectVisible) = await _approvalScreen.CheckApprovalActionButtonsAsync();
            
            (updateVisible || closeVisible).Should().BeTrue("Director should see save/close buttons on detail screen");
            (approveVisible && rejectVisible).Should().BeTrue("Director should see Onayla (Approve) and Reddet (Reject) buttons for director approval");
            
            _output.WriteLine($"✅ PASSED: Director sees detail screen buttons and approval action buttons (Onayla, Reddet)");
        }
        else
        {
            _output.WriteLine("⚠️ SKIPPED: No contracts with director approval status found");
        }
    }

    [Fact(DisplayName = "DirectorApprovalOperations_WithCancellationStatus_ShouldDisplayCancellationButtons")]
    public async Task DirectorApprovalOperations_WithCancellationStatus_ShouldDisplayCancellationButtons()
    {
        // Ensure Page is available in this async context
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Arrange
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        // Act - Search for contracts with Cancellation Approval status
        await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
        await Task.Delay(500);
        await _approvalScreen.SelectApprovalStatusAsync("İptal Onayı Bekleniyor");
        await Task.Delay(1000);
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(2000);
        
        // Assert - Verify cancellation approval structure
        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        if (recordCount > 0)
        {
            await _approvalScreen.ClickFirstEditButtonAsync();
            await Task.Delay(2000);
            
            var (updateVisible, closeVisible, cancelApproveVisible, cancelRejectVisible) = await _approvalScreen.CheckCancellationButtonsAsync();
            
            updateVisible.Should().BeTrue("Director should see Güncelle (Update) button on detail screen");
            closeVisible.Should().BeTrue("Director should see Kapat (Close) button on detail screen");
            cancelApproveVisible.Should().BeTrue("Director should see İptal Talebini Onayla (Approve cancellation) button");
            cancelRejectVisible.Should().BeTrue("Director should see İptal Talebini Reddet (Reject cancellation) button");
            
            _output.WriteLine($"✅ PASSED: Director sees all cancellation buttons (Güncelle, Kapat, İptal Talebini Onayla, İptal Talebini Reddet)");
        }
        else
        {
            _output.WriteLine("⚠️ SKIPPED: No contracts with cancellation approval status found");
        }
    }

    [Fact(DisplayName = "DirectorApprovalOperations_WithManagerApprovalStatus_ShouldDisplayLimitedButtons")]
    public async Task DirectorApprovalOperations_WithManagerApprovalStatus_ShouldDisplayLimitedButtons()
    {
        // Ensure Page is available in this async context
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Arrange
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        // Act - Search for contracts with Manager Approval status (Director viewing manager-level approvals)
        await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
        await Task.Delay(500);
        await _approvalScreen.SelectApprovalStatusAsync("Müdür Onayı Bekleniyor");
        await Task.Delay(1000);
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(2000);
        
        // Assert - Manager approval status is read-only for director (limited view)
        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        if (recordCount > 0)
        {
            await _approvalScreen.ClickFirstEditButtonAsync();
            await Task.Delay(2000);
            
            // Director should only see Güncelle (Update) and Kapat (Close) buttons, no action buttons
            var (updateVisible, closeVisible) = await _approvalScreen.CheckDetailScreenButtonsAsync();
            var (approveVisible, rejectVisible) = await _approvalScreen.CheckApprovalActionButtonsAsync();
            
            updateVisible.Should().BeTrue("Director should see Güncelle (Update) button for read-only view");
            closeVisible.Should().BeTrue("Director should see Kapat (Close) button for read-only view");
            approveVisible.Should().BeFalse("Director should NOT see Onayla (Approve) button for manager approval status");
            rejectVisible.Should().BeFalse("Director should NOT see Reddet (Reject) button for manager approval status");
            
            _output.WriteLine($"✅ PASSED: Director has limited access to manager approval contracts (Güncelle, Kapat only - no action buttons)");
        }
        else
        {
            _output.WriteLine("⚠️ SKIPPED: No contracts with manager approval status found");
        }
    }

    // ========== DIRECTOR ROLE - CONTRACT CANCELLATION WITH SPECIFIC FILTERS ==========
    
    [Fact(DisplayName = "DirectorApprovalOperations_WithSWRIContract_ShouldApplySWRIFiltersAndDisplayActionButtons")]
    public async Task DirectorApprovalOperations_WithSWRIContract_ShouldApplySWRIFiltersAndDisplayActionButtons()
    {
        // Ensure Page is available in this async context
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Arrange
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        // Act - Apply SWRI contract filters (equivalent to Manager T1 test)
        await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
        await Task.Delay(500);
        await _approvalScreen.FillFirmCodeAsync("SWRI");
        await Task.Delay(500);
        await _approvalScreen.FillFirmNameAsync("SWAROVSKI INTERNATIONAL");
        await Task.Delay(500);
        await _approvalScreen.FillContractNameAsync("SWRI-2025-CFR");
        await Task.Delay(500);
        await _approvalScreen.SelectEntityStatusAsync("İptal Onayı Bekleniyor");
        await Task.Delay(1000);
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(3000);
        
        // Assert
        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        if (recordCount == 0)
        {
            _output.WriteLine("⚠️ SKIPPED: No SWRI-2025-CFR contracts with cancellation status found");
            return;
        }
        
        if (recordCount >= 40)
        {
            Assert.Fail("Filters not properly applied (40+ records returned)");
        }
        
        // Verify detail screen and buttons
        await _approvalScreen.ClickFirstEditButtonAsync();
        await Task.Delay(2000);
        
        var (updateVisible, closeVisible) = await _approvalScreen.CheckDetailScreenButtonsAsync();
        
        updateVisible.Should().BeTrue("Güncelle button must be visible on SWRI contract for director");
        closeVisible.Should().BeTrue("Kapat button must be visible on SWRI contract for director");
        
        _output.WriteLine($"✅ PASSED: Director can apply SWRI filters and sees action buttons ({recordCount} matching contracts)");
    }
}
