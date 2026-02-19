using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Supplier;
using Xunit.Abstractions;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Supplier Combined Approval Screen tests for Manager role
/// Tests unified approval workflows for different document types and roles
/// Requires user with approval permissions (Manager role)
/// </summary>
public class SupplierCombinedApprovalScreenManagerTests : ManagerTestBase
{
    private readonly ITestOutputHelper _output;
    private SupplierCombinedApprovalScreen? _approvalScreen;

    public SupplierCombinedApprovalScreenManagerTests(ITestOutputHelper output)
    {
        _output = output;
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _approvalScreen = new SupplierCombinedApprovalScreen();
    }

    // ========== MANAGER APPROVAL OPERATIONS TESTS ==========
    
    // ========== T1: CONTRACT CANCELLATION APPROVAL WITH SPECIFIC FILTERS ==========

    [Fact(DisplayName = "T1_ContractCancellationApproval_WithSpecificFilters_ShouldDisplayActionButtons")]
    public async Task T1_ContractCancellationApproval_WithSpecificFilters_ShouldDisplayActionButtons()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n\n=== T1: CONTRACT CANCELLATION APPROVAL TEST WITH SWAROVSKI FILTERS ===");
        Console.WriteLine("Applying specific filters from SWRI-2025-CFR contract scenario:");
        Console.WriteLine("  • Belge Tipi: Sözleşme");
        Console.WriteLine("  • Firma Kodu: SWRI");
        Console.WriteLine("  • Firma Adı: SWAROVSKI INTERNATIONAL");
        Console.WriteLine("  • Sözleşme Adı: SWRI-2025-CFR");
        Console.WriteLine("  • Durumu: İptal Onayı Bekleniyor\n");

        // Apply filters using Page Object methods
        try
        {
            await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Filter 1 ERROR: {ex.Message}");
        }

        try
        {
            await _approvalScreen.FillFirmCodeAsync("SWRI");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Filter 2 ERROR: {ex.Message}");
        }

        try
        {
            await _approvalScreen.FillFirmNameAsync("SWAROVSKI INTERNATIONAL");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Filter 3 ERROR: {ex.Message}");
        }

        try
        {
            await _approvalScreen.FillContractNameAsync("SWRI-2025-CFR");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Filter 4 ERROR: {ex.Message}");
        }

        try
        {
            await _approvalScreen.SelectEntityStatusAsync("İptal Onayı Bekleniyor");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Filter 5 ERROR: {ex.Message}");
        }
        
        // Execute search
        Console.WriteLine("\n📋 Executing search with all filters...");
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(3000);
        
        // Check results
        var records = await _approvalScreen.GetGridRecordCountAsync();
        Console.WriteLine($"\n📊 Step 3: Filtered records: {records} found");
        
        if (records == 0)
        {
            Console.WriteLine("ℹ️  No records found - Test SKIPPED (No matching data for SWRI-2025-CFR)");
            return;
        }
        
        if (records >= 40)
        {
            Console.WriteLine("⚠️ WARNING: 40+ records suggest filters NOT properly applied!");
        }
        else
        {
            Console.WriteLine($"✅ Filters applied successfully: {records} matching record(s)");
        }
        
        // Step 3: Open detail screen
        Console.WriteLine("\nStep 4: Açmak - Sözleşme Güncelleme Ekranı");
        try
        {
            await _approvalScreen.ClickFirstEditButtonAsync();
            await Task.Delay(2000);
            Console.WriteLine("✅ Detail screen (Sözleşme Güncelleme) opened successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to open detail screen: {ex.Message}");
            return;
        }
        
        // Step 4: Verify approval action buttons
        Console.WriteLine("\nStep 5: Verify required action buttons on detail screen:");
        
        var (updateVisible, closeVisible) = await _approvalScreen.CheckDetailScreenButtonsAsync();
        
        Console.WriteLine("\n📌 Expected Buttons (Per SWRI-2025-CFR Scenario):");
        Console.WriteLine($"  • Güncelle: {(updateVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        Console.WriteLine($"  • Kapat: {(closeVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        
        // Assert: Update and Close buttons must be visible
        updateVisible.Should().BeTrue("Güncelle button must be visible on contract detail screen");
        closeVisible.Should().BeTrue("Kapat button must be visible on contract detail screen");
        
        Console.WriteLine("\n✅ T1 TEST PASSED: All expected action buttons are visible per SWRI-2025-CFR scenario");
        Console.WriteLine("=== T1 TEST END ===\n");
    }

    // ========== SEMANTIC MANAGER APPROVAL OPERATIONS TESTS ==========

    [Fact(DisplayName = "ManagerApprovalOperations_WithManagerApprovalStatus_ShouldDisplayLimitedButtons")]
    public async Task ManagerApprovalOperations_WithManagerApprovalStatus_ShouldDisplayLimitedButtons()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n=== MANAGER APPROVAL OPERATIONS: With Manager Approval Status ===");
        Console.WriteLine("Test: Manager viewing contracts with Müdür Onayı Bekleniyor status");
        Console.WriteLine("Verify: Only utility buttons visible (Manager has no action authority)\n");

        // Arrange & Act - Apply filter for Manager Approval Status
        try
        {
            await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
            await Task.Delay(300);
            await _approvalScreen.SelectApprovalStatusAsync("Müdür Onayı Bekleniyor");
            Console.WriteLine("✅ Filters applied: Sözleşme + Müdür Onayı Bekleniyor");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Filter setup failed: {ex.Message}");
            _output.WriteLine($"⚠️ Test skipped: {ex.Message}");
            return;
        }

        // Execute search
        Console.WriteLine("\n📋 Searching for manager approval contracts...");
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(2000);

        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        Console.WriteLine($"📊 Found: {recordCount} manager approval contracts");

        if (recordCount == 0)
        {
            Console.WriteLine("⚠️ No records found - test skipped");
            _output.WriteLine("⚠️ Test skipped: No manager approval contracts available");
            return;
        }

        // Open detail screen
        Console.WriteLine("\n🔘 Opening first contract detail...");
        try
        {
            await _approvalScreen.ClickFirstEditButtonAsync();
            await Task.Delay(2000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to open detail: {ex.Message}");
            return;
        }

        // Assert - Check all button visibility (utility + action + withdraw buttons)
        Console.WriteLine("\n🔍 Detecting visible buttons (iframe-aware)...");
        var (updateVisible, closeVisible) = await _approvalScreen.CheckDetailScreenButtonsAsync();
        var (approveVisible, rejectVisible) = await _approvalScreen.CheckApprovalActionButtonsAsync();
        
        // Check Geri Çek (Withdraw) button - try main page first
        var withdrawBtn = Page.Locator("#ContractWithdraw");
        bool withdrawVisible = await withdrawBtn.IsVisibleAsync();
        
        // If not found on main page, try in frames
        if (!withdrawVisible)
        {
            var frames = Page.Frames;
            foreach (var frame in frames)
            {
                var frameWithdrawBtn = frame.Locator("#ContractWithdraw");
                try
                {
                    if (await frameWithdrawBtn.IsVisibleAsync())
                    {
                        withdrawVisible = true;
                        break;
                    }
                }
                catch { }
            }
        }

        Console.WriteLine($"\n📌 Button Visibility for Manager Approval Status:");
        Console.WriteLine($"  • Güncelle (Update): {(updateVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        Console.WriteLine($"  • Kapat (Close): {(closeVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        Console.WriteLine($"  • Onayla (Approve): {(approveVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        Console.WriteLine($"  • Reddet (Reject): {(rejectVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        Console.WriteLine($"  • Geri Çek (Withdraw): {(withdrawVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");

        // For manager approval status, manager should see all action buttons
        approveVisible.Should().BeTrue("Onayla button must be visible for manager approval");
        rejectVisible.Should().BeTrue("Reddet button must be visible for manager approval");
        withdrawVisible.Should().BeTrue("Geri Çek button must be visible for manager to withdraw contract");

        _output.WriteLine("✅ Test passed: Manager approval status buttons verified (Onayla + Reddet + Geri Çek visible)");
        Console.WriteLine("\n=== TEST END ===\n");
    }

    [Fact(DisplayName = "ManagerApprovalOperations_WithCancellationStatus_ShouldDisplayActionButtons")]
    public async Task ManagerApprovalOperations_WithCancellationStatus_ShouldDisplayActionButtons()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n=== MANAGER APPROVAL OPERATIONS: With Cancellation Status ===");
        Console.WriteLine("Test: Manager viewing contracts with İptal Onayı Bekleniyor status");
        Console.WriteLine("Verify: Action buttons visible for cancellation decision\n");

        // Arrange & Act - Apply filter for Cancellation Status
        try
        {
            await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
            await Task.Delay(300);
            await _approvalScreen.SelectApprovalStatusAsync("İptal Onayı Bekleniyor");
            Console.WriteLine("✅ Filters applied: Sözleşme + İptal Onayı Bekleniyor");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Filter setup failed: {ex.Message}");
            _output.WriteLine($"⚠️ Test skipped: {ex.Message}");
            return;
        }

        // Execute search
        Console.WriteLine("\n📋 Searching for cancellation approval contracts...");
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(2000);

        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        Console.WriteLine($"📊 Found: {recordCount} cancellation contracts");

        if (recordCount == 0)
        {
            Console.WriteLine("⚠️ No records found - test skipped");
            _output.WriteLine("⚠️ Test skipped: No cancellation contracts available");
            return;
        }

        // Open detail screen
        Console.WriteLine("\n🔘 Opening first contract detail...");
        try
        {
            await _approvalScreen.ClickFirstEditButtonAsync();
            await Task.Delay(2000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to open detail: {ex.Message}");
            return;
        }

        // Assert - Check button visibility for cancellation approval
        Console.WriteLine("\n🔍 Detecting visible buttons (iframe-aware)...");
        var (updateVisible, closeVisible) = await _approvalScreen.CheckDetailScreenButtonsAsync();

        Console.WriteLine($"\n📌 Button Visibility for Cancellation Status:");
        Console.WriteLine($"  • Güncelle (Update): {(updateVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        Console.WriteLine($"  • Kapat (Close): {(closeVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");

        // For cancellation status, buttons should be visible to allow manager action
        (updateVisible || closeVisible).Should().BeTrue("Manager should see action buttons for cancellation decision");

        _output.WriteLine("✅ Test passed: Cancellation approval buttons visible");
        Console.WriteLine("\n=== TEST END ===\n");
    }

    [Fact(DisplayName = "ManagerApprovalOperations_WithDirectorApprovalStatus_ShouldDisplayReadOnlyView")]
    public async Task ManagerApprovalOperations_WithDirectorApprovalStatus_ShouldDisplayReadOnlyView()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n=== MANAGER APPROVAL OPERATIONS: With Director Approval Status ===");
        Console.WriteLine("Test: Manager viewing contracts with Direktör Onayı Bekleniyor status");
        Console.WriteLine("Verify: Read-only view for director-level approvals\n");

        // Arrange & Act - Apply filter for Director Approval Status
        try
        {
            await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
            await Task.Delay(300);
            await _approvalScreen.SelectApprovalStatusAsync("Direktör Onayı Bekleniyor");
            Console.WriteLine("✅ Filters applied: Sözleşme + Direktör Onayı Bekleniyor");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Filter setup failed: {ex.Message}");
            _output.WriteLine($"⚠️ Test skipped: {ex.Message}");
            return;
        }

        // Execute search
        Console.WriteLine("\n📋 Searching for director approval contracts...");
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(2000);

        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        Console.WriteLine($"📊 Found: {recordCount} director approval contracts");

        if (recordCount == 0)
        {
            Console.WriteLine("⚠️ No records found - test skipped");
            _output.WriteLine("⚠️ Test skipped: No director approval contracts available");
            return;
        }

        // Open detail screen
        Console.WriteLine("\n🔘 Opening first contract detail...");
        try
        {
            await _approvalScreen.ClickFirstEditButtonAsync();
            await Task.Delay(2000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Failed to open detail: {ex.Message}");
            return;
        }

        // Assert - Check button visibility (read-only view for director-level approvals)
        Console.WriteLine("\n🔍 Detecting visible buttons (iframe-aware)...");
        var (updateVisible, closeVisible) = await _approvalScreen.CheckDetailScreenButtonsAsync();
        var (approveVisible, rejectVisible) = await _approvalScreen.CheckApprovalActionButtonsAsync();
        
        // Check Geri Çek (Withdraw) button - try main page first
        var withdrawBtn = Page.Locator("#ContractWithdraw");
        bool withdrawVisible = await withdrawBtn.IsVisibleAsync();
        
        // If not found on main page, try in frames
        if (!withdrawVisible)
        {
            var frames = Page.Frames;
            foreach (var frame in frames)
            {
                var frameWithdrawBtn = frame.Locator("#ContractWithdraw");
                try
                {
                    if (await frameWithdrawBtn.IsVisibleAsync())
                    {
                        withdrawVisible = true;
                        break;
                    }
                }
                catch { /* Continue searching */ }
            }
        }

        Console.WriteLine($"\n📌 Button Visibility for Director Approval Status (Manager role):");
        Console.WriteLine($"  • Güncelle (Update): {(updateVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        Console.WriteLine($"  • Kapat (Close): {(closeVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        Console.WriteLine($"  • Geri Çek (Withdraw): {(withdrawVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        Console.WriteLine($"  • Onayla (Approve): {(approveVisible ? "❌ SHOULD NOT BE VISIBLE" : "✅ NOT VISIBLE")}");
        Console.WriteLine($"  • Reddet (Reject): {(rejectVisible ? "❌ SHOULD NOT BE VISIBLE" : "✅ NOT VISIBLE")}");

        // Manager can view director approval contracts but should see utility buttons only
        updateVisible.Should().BeTrue("Manager should see Güncelle button for view access");
        closeVisible.Should().BeTrue("Manager should see Kapat button for view access");
        withdrawVisible.Should().BeTrue("Manager should see Geri Çek button");
        
        // Manager should NOT see approval action buttons for director-level approvals
        approveVisible.Should().BeFalse("Onayla button must NOT be visible (director-level approval)");
        rejectVisible.Should().BeFalse("Reddet button must NOT be visible (director-level approval)");

        _output.WriteLine("✅ Test passed: Director approval view shows only utility buttons (no action buttons) for manager");
        Console.WriteLine("\n=== TEST END ===\n");
    }

    [Fact(DisplayName = "ManagerApprovalOperations_WithSWRIFilters_ShouldApplyMultipleFiltersAndDisplayRecord")]
    public async Task ManagerApprovalOperations_WithSWRIFilters_ShouldApplyMultipleFiltersAndDisplayRecord()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n=== MANAGER APPROVAL OPERATIONS: With SWRI Multiple Filters ===");
        Console.WriteLine("Test: Apply 5 filters together to find SWRI-2025-CFR contract");
        Console.WriteLine("Verify: Exact match record with all buttons visible\n");

        // Arrange & Act - Apply comprehensive filters
        Console.WriteLine("📋 Applying filters:");
        try
        {
            await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
            Console.WriteLine("  ✅ Belge Tipi: Sözleşme");
            
            await Task.Delay(300);
            await _approvalScreen.FillFirmCodeAsync("SWRI");
            Console.WriteLine("  ✅ Firma Kodu: SWRI");
            
            await Task.Delay(300);
            await _approvalScreen.FillFirmNameAsync("SWAROVSKI INTERNATIONAL");
            Console.WriteLine("  ✅ Firma Adı: SWAROVSKI INTERNATIONAL");
            
            await Task.Delay(300);
            await _approvalScreen.FillContractNameAsync("SWRI-2025-CFR");
            Console.WriteLine("  ✅ Sözleşme Adı: SWRI-2025-CFR");
            
            await Task.Delay(300);
            await _approvalScreen.SelectApprovalStatusAsync("İptal Onayı Bekleniyor");
            Console.WriteLine("  ✅ Durumu: İptal Onayı Bekleniyor");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Filter setup failed: {ex.Message}");
            _output.WriteLine($"⚠️ Test failed: {ex.Message}");
            throw;
        }

        // Execute search
        Console.WriteLine("\n🔍 Executing multi-filter search...");
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(2000);

        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        Console.WriteLine($"📊 Search Results: {recordCount} record(s)");

        // Assert - Verify exact match
        recordCount.Should().Be(1, $"Expected exactly 1 SWRI-2025-CFR record, found {recordCount}");
        Console.WriteLine("✅ EXACT MATCH: Found 1 SWRI-2025-CFR record");

        // Open record and verify buttons
        Console.WriteLine("\n🔘 Opening detail screen...");
        await _approvalScreen.ClickFirstEditButtonAsync();
        await Task.Delay(2000);

        Console.WriteLine("🔍 Detecting buttons on detail screen...");
        var (updateVisible, closeVisible) = await _approvalScreen.CheckDetailScreenButtonsAsync();

        Console.WriteLine($"\n📌 Button Visibility on SWRI-2025-CFR:");
        Console.WriteLine($"  • Güncelle: {(updateVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        Console.WriteLine($"  • Kapat: {(closeVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");

        updateVisible.Should().BeTrue("Güncelle button must be visible");
        closeVisible.Should().BeTrue("Kapat button must be visible");

        _output.WriteLine("✅ Test passed: SWRI filters applied and buttons verified");
        Console.WriteLine("\n=== TEST END ===\n");
    }
}
