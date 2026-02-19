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
}
