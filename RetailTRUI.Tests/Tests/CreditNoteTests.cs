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
        
        // Verify we're authenticated and on dashboard
        Console.WriteLine($"[CreditNoteTests] Current URL after login: {Page.Url}");
        
        // Wait for page to be fully ready
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(1000); // Give page time to settle
        
        // Navigate to credit note page directly via URL
        var config = ConfigurationManager.Instance;
        var creditNoteUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/CreditNote/Index";
        
        Console.WriteLine($"[CreditNoteTests] Navigating to: {creditNoteUrl}");
        
        int retryCount = 0;
        const int maxRetries = 3;
        
        while (retryCount < maxRetries)
        {
            try
            {
                Console.WriteLine($"[CreditNoteTests] Navigation attempt {retryCount + 1}/{maxRetries}");
                
                await Page.GotoAsync(creditNoteUrl, new PageGotoOptions 
                { 
                    WaitUntil = WaitUntilState.NetworkIdle,
                    Timeout = 30000
                });
                
                Console.WriteLine($"[CreditNoteTests] Navigation completed. Current URL: {Page.Url}");
                
                // Verify page is still active
                if (Page.IsClosed)
                {
                    throw new Exception("Page closed after navigation");
                }
                
                // Wait for page load to complete
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
                
                // Check if we got redirected to login (session might have expired)
                if (Page.Url.Contains("/Login/Index"))
                {
                    Console.WriteLine($"[CreditNoteTests] Redirected to login. Session might have expired.");
                    retryCount++;
                    
                    if (retryCount < maxRetries)
                    {
                        Console.WriteLine($"[CreditNoteTests] Re-authenticating...");
                        await AuthenticateAndWaitAsync();
                        await Task.Delay(2000);
                        continue;
                    }
                    else
                    {
                        throw new Exception($"Failed to navigate to CreditNote page - redirected to login after {maxRetries} attempts");
                    }
                }
                
                break;
            }
            catch (PlaywrightException ex) when (ex.Message.Contains("ERR_ABORTED") || ex.Message.Contains("interrupted"))
            {
                Console.WriteLine($"[CreditNoteTests] Navigation interrupted (attempt {retryCount + 1}): {ex.Message}");
                retryCount++;
                
                if (retryCount < maxRetries)
                {
                    await Task.Delay(2000);
                    continue;
                }
                else
                {
                    throw new Exception($"Navigation failed after {maxRetries} attempts", ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CreditNoteTests] Navigation error (attempt {retryCount + 1}): {ex.GetType().Name} - {ex.Message}");
                retryCount++;
                
                if (retryCount < maxRetries)
                {
                    await Task.Delay(2000);
                    continue;
                }
                else
                {
                    throw new Exception($"Navigation failed after {maxRetries} attempts with error: {ex.Message}", ex);
                }
            }
        }
        
        if (!Page.Url.Contains("CreditNote/Index"))
        {
            throw new Exception($"Navigation to CreditNote page failed. Current URL: {Page.Url}");
        }
    }
    
    private async Task AuthenticateAndWaitAsync()
    {
        var loginPage = new LoginPage();
        await loginPage.NavigateToLoginPageAsync();
        await loginPage.LoginAsAsync("normal");
        await loginPage.VerifyLoginSuccessAsync();
        
        // Wait for dashboard to load
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
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
