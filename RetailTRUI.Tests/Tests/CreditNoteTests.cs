using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;
using RetailTRUI.Tests.Pages.Purchasing;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Credit Note tests for search, create, edit, and delete operations
/// </summary>
public class CreditNoteTests : TestBase
{
    private GlobalPage _globalPage = null!;
    private CreditNotePage _creditNotePage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _globalPage = new GlobalPage();
        _creditNotePage = new CreditNotePage();
        
        // Navigate to credit note page directly via URL
        var config = ConfigurationManager.Instance;
        var creditNoteUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/CreditNote/Index";
        
        try
        {
            await Page.GotoAsync(creditNoteUrl, new PageGotoOptions 
            { 
                WaitUntil = WaitUntilState.DOMContentLoaded,
                Timeout = 30000
            });
        }
        catch (PlaywrightException ex) when (ex.Message.Contains("ERR_ABORTED") || ex.Message.Contains("interrupted"))
        {
            await Task.Delay(2000);
            if (!Page.Url.Contains("CreditNote/Index"))
            {
                await Page.GotoAsync(creditNoteUrl, new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
            }
        }
    }

    [Fact]
    public async Task SearchCreditNoteForDifferentStatus_ShouldDisplayResults()
    {
        Driver.SetPage(Page);
        
        // Verify page is displayed
        await _creditNotePage.VerifyCreditNotePageIsDisplayedAsync("Credit Note");
        
        // Search for credit notes with different statuses
        var statuses = new List<string>
        {
            "Hazırlanıyor",
            "Onaya Gönderildi",
            "Onaylandı",
            "Muhasebeleşti"
        };
        
        await _creditNotePage.SearchCreditNotesWithDifferentStatusAsync(statuses);
        
        // Sort the credit note list by all available columns
        await _creditNotePage.SortCreditNoteListByAllColumnsAsync();
        
        Console.WriteLine("✅ Search credit note for different status test completed successfully");
    }

    [Fact]
    public async Task SearchCreditNoteForOtherFields_ShouldDisplayResults()
    {
        Driver.SetPage(Page);
        
        // Verify page is displayed
        await _creditNotePage.VerifyCreditNotePageIsDisplayedAsync("Credit Note");
        
        // Fill search form with specific criteria
        await _creditNotePage.FillDocumentNoAsync("AUTOMATED TEST");
        await _creditNotePage.FillDocumentDateAsync("26.09.2025");
        await _creditNotePage.SelectFirmCodeAsync("DPL");
        await _creditNotePage.SelectPurchaseOrderAsync("1-2025-DPL-00000102");
        await _creditNotePage.SelectIsBrokenAsync("No");
        
        // Click search button
        await _creditNotePage.ClickSearchButtonAsync();
        
        Console.WriteLine("✅ Search credit note for other fields test completed successfully");
    }

    [Fact]
    public async Task CreateCreditNote_ShouldCreateSuccessfully()
    {
        Driver.SetPage(Page);
        
        // Verify page is displayed
        await _creditNotePage.VerifyCreditNotePageIsDisplayedAsync("Credit Note");
        
        // Click add new credit note button
        await _creditNotePage.ClickAddNewCreditNoteButtonAsync();
        
        // Create credit note
        await _creditNotePage.CreateCreditNoteAsync(
            documentNo: "TEST DOC 1",
            purchaseOrder: "1-2025-DPL-00000102",
            isBroken: "No",
            description: "AUTOMATED TEST 1"
        );
        
        // Save the credit note
        await _creditNotePage.ClickSaveButtonInPopupAsync();
        
        Console.WriteLine("✅ Create credit note test completed successfully");
    }

    [Fact]
    public async Task CreateCreditNoteForBroken_ShouldCreateSuccessfully()
    {
        Driver.SetPage(Page);
        
        // Verify page is displayed
        await _creditNotePage.VerifyCreditNotePageIsDisplayedAsync("Credit Note");
        
        // Click add new credit note button
        await _creditNotePage.ClickAddNewCreditNoteButtonAsync();
        
        // Create credit note for broken items
        await _creditNotePage.CreateCreditNoteForBrokenAsync(
            documentNo: "TEST DOC BROKEN",
            isBroken: "Yes",
            description: "AUTOMATED TEST BROKEN",
            firmCode: "DPL"
        );
        
        // Save the credit note
        await _creditNotePage.ClickSaveButtonInPopupAsync();
        
        Console.WriteLine("✅ Create credit note for broken test completed successfully");
    }

    [Fact]
    public async Task EditCreditNoteAndAddProduct_ShouldAddProductSuccessfully()
    {
        Driver.SetPage(Page);
        
        // Verify page is displayed
        await _creditNotePage.VerifyCreditNotePageIsDisplayedAsync("Credit Note");
        
        // Search for the credit note first
        await _creditNotePage.FillDocumentNoAsync("TEST DOC");
        await _creditNotePage.SelectFirmCodeAsync("DPL");
        await _creditNotePage.SelectPurchaseOrderAsync("1-2025-DPL-00000102");
        await _creditNotePage.SelectIsBrokenAsync("No");
        await _creditNotePage.ClickSearchButtonAsync();
        
        // Edit first credit note and add product
        await _creditNotePage.EditFirstCreditNoteAndAddProductAsync(
            invoiceNo: "TEST-61025",
            productCode: "1107",
            quantity: "10",
            profitCenter: "DFS GENEL",
            creditNoteType: "Hasarlı Ürün"
        );
        
        // Save the changes
        await _creditNotePage.ClickSaveButtonInDetailPageAsync();
        
        Console.WriteLine("✅ Edit credit note and add product test completed successfully");
    }

    [Fact]
    public async Task DeleteCreditNoteProduct_ShouldDeleteSuccessfully()
    {
        Driver.SetPage(Page);
        
        // Verify page is displayed
        await _creditNotePage.VerifyCreditNotePageIsDisplayedAsync("Credit Note");
        
        // Search for the credit note
        await _creditNotePage.FillDocumentNoAsync("TEST DOC");
        await _creditNotePage.SelectFirmCodeAsync("DPL");
        await _creditNotePage.SelectIsBrokenAsync("No");
        await _creditNotePage.ClickSearchButtonAsync();
        
        // Edit first credit note
        await _creditNotePage.ClickFirstEditButtonAsync();
        
        // Delete first product
        await _creditNotePage.ClickDeleteIconOnFirstProductRowAsync();
        
        // Confirm delete operation
        await _creditNotePage.ConfirmDeleteOperationAsync();
        
        // Verify product was deleted
        await _creditNotePage.VerifyProductWasDeletedAsync();
        
        Console.WriteLine("✅ Delete credit note product test completed successfully");
    }
}
