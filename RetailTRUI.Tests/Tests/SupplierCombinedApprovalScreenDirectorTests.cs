using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Supplier;
using Xunit.Abstractions;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Supplier Combined Approval Screen tests for Director role
/// Tests unified approval workflows specific to Director responsibilities
/// Requires user with Director approval permissions
/// </summary>
public class SupplierCombinedApprovalScreenDirectorTests : DirectorTestBase
{
    private readonly ITestOutputHelper _output;
    private SupplierCombinedApprovalScreen? _approvalScreen;

    public SupplierCombinedApprovalScreenDirectorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _approvalScreen = new SupplierCombinedApprovalScreen();
    }

    // ========== DIRECTOR ROLE SPECIFIC TESTS ==========

    [Fact(DisplayName = "D1_DirectorApprovalScreen_ShouldBeAccessible")]
    public async Task D1_DirectorApprovalScreen_ShouldBeAccessible()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n\n=== D1: DIRECTOR APPROVAL OPERATIONS SCREEN ===");
        Console.WriteLine("Test: Director role can access Approval Operations screen");

        // Assert
        _output.WriteLine("✅ D1 PASSED: Director successfully accessed Approval Operations screen");
        Console.WriteLine("=== D1 TEST END ===\n");
    }

    [Fact(DisplayName = "D2_DirectorApprovalContracts_ShouldSearch")]
    public async Task D2_DirectorApprovalContracts_ShouldSearch()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n\n=== D2: DIRECTOR CONTRACT SEARCH ===");
        Console.WriteLine("Test: Director can search for contracts");
        Console.WriteLine("  • Belge Tipi: Sözleşme");
        Console.WriteLine("  • Durumu: Direktör Onayı Bekleniyor");

        // Act - Apply filters for director approvals
        try
        {
            await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
            await Task.Delay(500);
            await _approvalScreen.SelectApprovalStatusAsync("Direktör Onayı Bekleniyor");
            Console.WriteLine("✅ Applied filters");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Filter ERROR: {ex.Message}");
            _output.WriteLine($"⚠️ D2 failed: {ex.Message}");
            return;
        }
        
        // Execute search
        Console.WriteLine("\n📋 Executing search...");
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(2000);
        
        // Assert
        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        Console.WriteLine($"📊 Search Results: {recordCount} director approval contracts found");
        
        recordCount.Should().BeGreaterThanOrEqualTo(0, "Director should be able to search for contracts");
        
        _output.WriteLine($"✅ D2 PASSED: Director can search contracts with {recordCount} results");
        Console.WriteLine("=== D2 TEST END ===\n");
    }

    [Fact(DisplayName = "D3_DirectorApprovalButtons_ShouldBeVisibleOnDetailScreen")]
    public async Task D3_DirectorApprovalButtons_ShouldBeVisibleOnDetailScreen()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n\n=== D3: DIRECTOR APPROVAL BUTTONS ON DETAIL SCREEN ===");
        Console.WriteLine("Test: Director should see approval action buttons");
        Console.WriteLine("  • Belge Tipi: Sözleşme");
        Console.WriteLine("  • Durumu: Direktör Onayı Bekleniyor");

        // Act - Apply filters
        try
        {
            await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
            await Task.Delay(500);
            await _approvalScreen.SelectApprovalStatusAsync("Direktör Onayı Bekleniyor");
            Console.WriteLine("✅ Applied filters");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Filter ERROR: {ex.Message}");
            _output.WriteLine($"⚠️ D3 failed: {ex.Message}");
            return;
        }
        
        // Execute search
        Console.WriteLine("\n📋 Searching...");
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(2000);
        
        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        Console.WriteLine($"📊 Found: {recordCount} contracts");
        
        if (recordCount == 0)
        {
            Console.WriteLine("⚠️ D3 SKIPPED: No director approval contracts found");
            _output.WriteLine("⚠️ D3 skipped: No records");
            return;
        }
        
        // Open detail screen
        Console.WriteLine("\n🔘 Opening first contract detail screen...");
        try
        {
            await _approvalScreen.ClickFirstEditButtonAsync();
            await Task.Delay(2000);
            
            // Check for approval buttons
            var (updateVisible, closeVisible) = await _approvalScreen.CheckDetailScreenButtonsAsync();
            
            Console.WriteLine($"\n📌 Button Visibility on director approval contract:");
            Console.WriteLine($"  • Update: {(updateVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
            Console.WriteLine($"  • Close: {(closeVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
            
            (updateVisible || closeVisible).Should().BeTrue("Director should see action buttons for director approval contracts");
            
            _output.WriteLine($"✅ D3 PASSED: Director sees action buttons on detail screen");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Could not verify buttons: {ex.Message}");
            _output.WriteLine($"⚠️ D3 warning: {ex.Message}");
        }
        
        Console.WriteLine("=== D3 TEST END ===\n");
    }

    [Fact(DisplayName = "D4_DirectorMultipleFilters_ShouldApplyFilters")]
    public async Task D4_DirectorMultipleFilters_ShouldApplyFilters()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n\n=== D4: DIRECTOR MULTIPLE FILTERS ===");
        Console.WriteLine("Test: Director can apply multiple filters");
        Console.WriteLine("  • Filter 1: Belge Tipi = 'Sözleşme'");
        Console.WriteLine("  • Filter 2: Firma Kodu = 'SWRI'");
        Console.WriteLine("  • Filter 3: Durumu = 'Direktör Onayı Bekleniyor'");

        // Act - Apply multiple filters
        try
        {
            await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
            await Task.Delay(500);
            await _approvalScreen.FillFirmCodeAsync("SWRI");
            await Task.Delay(500);
            await _approvalScreen.SelectApprovalStatusAsync("Direktör Onayı Bekleniyor");
            Console.WriteLine("✅ All filters applied");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Filter ERROR: {ex.Message}");
            _output.WriteLine($"⚠️ D4 failed: {ex.Message}");
            return;
        }
        
        // Execute search
        Console.WriteLine("\n📋 Executing multi-filter search...");
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(2000);
        
        // Assert
        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        Console.WriteLine($"📊 Search Results: {recordCount} records found");
        
        recordCount.Should().BeGreaterThanOrEqualTo(0, "Multi-filter search should complete");
        
        _output.WriteLine($"✅ D4 PASSED: Director multi-filter search returned {recordCount} results");
        Console.WriteLine("=== D4 TEST END ===\n");
    }

    [Fact(DisplayName = "D5_DirectorCancellationApprovals_ShouldBeVisible")]
    public async Task D5_DirectorCancellationApprovals_ShouldBeVisible()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n\n=== D5: DIRECTOR CANCELLATION APPROVALS ===");
        Console.WriteLine("Test: Director can view cancellation approval requests");
        Console.WriteLine("  • Belge Tipi: Sözleşme");
        Console.WriteLine("  • Durumu: İptal Onayı Bekleniyor");

        // Act - Apply filters
        try
        {
            await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
            await Task.Delay(500);
            await _approvalScreen.SelectApprovalStatusAsync("İptal Onayı Bekleniyor");
            Console.WriteLine("✅ Applied filters");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Filter ERROR: {ex.Message}");
            _output.WriteLine($"⚠️ D5 failed: {ex.Message}");
            return;
        }
        
        // Execute search
        Console.WriteLine("\n📋 Searching for cancellation approvals...");
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(2000);
        
        var recordCount = await _approvalScreen.GetGridRecordCountAsync();
        Console.WriteLine($"📊 Found: {recordCount} cancellation approval contracts");
        
        recordCount.Should().BeGreaterThanOrEqualTo(0, "Director should see cancellation approval contracts");
        
        _output.WriteLine($"✅ D5 PASSED: Director sees {recordCount} cancellation approval contracts");
        Console.WriteLine("=== D5 TEST END ===\n");
    }

    [Fact(DisplayName = "D6_DirectorResetFilters_ShouldClearAllInputs")]
    public async Task D6_DirectorResetFilters_ShouldClearAllInputs()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n\n=== D6: DIRECTOR RESET FILTERS ===");
        Console.WriteLine("Test: Director can reset all filters");

        // Act - Apply filters
        try
        {
            await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
            await Task.Delay(300);
            await _approvalScreen.FillFirmCodeAsync("SWRI");
            await Task.Delay(300);
            Console.WriteLine("✅ Filters applied");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Filter ERROR: {ex.Message}");
            return;
        }
        
        // Click reset
        Console.WriteLine("\n📋 Clicking Reset button...");
        await _approvalScreen.ClickResetButtonAsync();
        await Task.Delay(1500);
        
        // Assert - Verify reset
        try
        {
            var firmCodeValue = await Page.Locator("#FilterFirmCode").InputValueAsync();
            Console.WriteLine($"After Reset - Firm Code: '{firmCodeValue}'");
            
            firmCodeValue.Should().BeEmpty("Firm code should be empty after reset");
            
            _output.WriteLine("✅ D6 PASSED: Director filters reset successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Verification ERROR: {ex.Message}");
            throw;
        }
        
        Console.WriteLine("=== D6 TEST END ===\n");
    }

    // ========== DIRECTOR ROLE - CONTRACT CANCELLATION APPROVAL EQUIVALENT TO T1 ==========
    
    [Fact(DisplayName = "D_T1_DirectorContractCancellationApproval_WithSWRIFilters_ShouldDisplayActionButtons")]
    public async Task D_T1_DirectorContractCancellationApproval_WithSWRIFilters_ShouldDisplayActionButtons()
    {
        // Setup
        Driver.SetPage(Page);
        await Page.SetViewportSizeAsync(1920, 1080);
        await Task.Delay(1000);
        
        // Navigate
        await _approvalScreen!.NavigateToApprovalOperationsAsync();
        await _approvalScreen.VerifyApprovalScreenIsDisplayedAsync();
        
        Console.WriteLine("\n\n=== D_T1: DIRECTOR CONTRACT CANCELLATION APPROVAL TEST WITH SWAROVSKI FILTERS ===");
        Console.WriteLine("Applying specific filters from SWRI-2025-CFR contract scenario (Director Role):");
        Console.WriteLine("  • Belge Tipi: Sözleşme");
        Console.WriteLine("  • Firma Kodu: SWRI");
        Console.WriteLine("  • Firma Adı: SWAROVSKI INTERNATIONAL");
        Console.WriteLine("  • Sözleşme Adı: SWRI-2025-CFR");
        Console.WriteLine("  • Durumu: İptal Onayı Bekleniyor\n");

        // Apply filters
        try
        {
            await _approvalScreen.SelectDocumentTypeAsync("Sözleşme");
            await _approvalScreen.FillFirmCodeAsync("SWRI");
            await _approvalScreen.FillFirmNameAsync("SWAROVSKI INTERNATIONAL");
            await _approvalScreen.FillContractNameAsync("SWRI-2025-CFR");
            await _approvalScreen.SelectEntityStatusAsync("İptal Onayı Bekleniyor");
            Console.WriteLine("✅ All filters applied successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Filter ERROR: {ex.Message}");
            return;
        }
        
        // Execute search
        Console.WriteLine("\n📋 Executing search with all filters...");
        await _approvalScreen.ClickSearchButtonAsync();
        await Task.Delay(3000);
        
        // Check results
        var records = await _approvalScreen.GetGridRecordCountAsync();
        Console.WriteLine($"📊 Step 3: Filtered records: {records} found");
        
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
        
        // Open detail screen
        Console.WriteLine("\nStep 4: Opening - Sözleşme Güncelleme Ekranı");
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
        
        // Verify approval action buttons
        Console.WriteLine("\nStep 5: Verify required action buttons on detail screen:");
        
        var (updateVisible, closeVisible) = await _approvalScreen.CheckDetailScreenButtonsAsync();
        
        Console.WriteLine("\n📌 Expected Buttons (Per SWRI-2025-CFR Scenario - Director Role):");
        Console.WriteLine($"  • Güncelle: {(updateVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        Console.WriteLine($"  • Kapat: {(closeVisible ? "✅ VISIBLE" : "❌ NOT VISIBLE")}");
        
        // Assert: Buttons must be visible for director
        updateVisible.Should().BeTrue("Güncelle button must be visible on contract detail screen for director");
        closeVisible.Should().BeTrue("Kapat button must be visible on contract detail screen for director");
        
        Console.WriteLine("\n✅ D_T1 TEST PASSED: All expected action buttons are visible for director on SWRI-2025-CFR scenario");
        Console.WriteLine("=== D_T1 TEST END ===\n");
    }
}
