using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;
using RetailTRUI.Tests.Pages.Supplier;

namespace RetailTRUI.Tests.Tests;

public class RebateInvoiceCreateTests : TestBase
{
    private ReceivablePoolSearchPage _receivablePoolPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        Driver.SetPage(Page);
        
        _receivablePoolPage = new ReceivablePoolSearchPage();
        
        // Verify we're authenticated and on dashboard
        Console.WriteLine($"[RebateInvoiceCreateTests] Current URL after login: {Page.Url}");
        
        // Wait for page to be fully ready
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(1000); // Give page time to settle
        
        // Navigate directly to receivable pool search page
        var config = ConfigurationManager.Instance;
        var receivablePoolUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/ContractReceivableInvoice/Index";
        
        Console.WriteLine($"[RebateInvoiceCreateTests] Navigating to: {receivablePoolUrl}");
        
        int retryCount = 0;
        const int maxRetries = 3;
        
        while (retryCount < maxRetries)
        {
            try
            {
                Console.WriteLine($"[RebateInvoiceCreateTests] Navigation attempt {retryCount + 1}/{maxRetries}");
                
                await Page.GotoAsync(receivablePoolUrl, new PageGotoOptions 
                { 
                    WaitUntil = WaitUntilState.NetworkIdle,
                    Timeout = 30000
                });
                
                Console.WriteLine($"[RebateInvoiceCreateTests] Navigation completed. Current URL: {Page.Url}");
                
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
                    Console.WriteLine($"[RebateInvoiceCreateTests] Redirected to login. Session might have expired.");
                    retryCount++;
                    
                    if (retryCount < maxRetries)
                    {
                        Console.WriteLine($"[RebateInvoiceCreateTests] Re-authenticating...");
                        await AuthenticateAndWaitAsync();
                        await Task.Delay(2000);
                        continue;
                    }
                    else
                    {
                        throw new Exception($"Failed to navigate to ContractReceivableInvoice page - redirected to login after {maxRetries} attempts");
                    }
                }
                
                break;
            }
            catch (PlaywrightException ex) when (ex.Message.Contains("ERR_ABORTED") || ex.Message.Contains("interrupted"))
            {
                Console.WriteLine($"[RebateInvoiceCreateTests] Navigation interrupted (attempt {retryCount + 1}): {ex.Message}");
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
                Console.WriteLine($"[RebateInvoiceCreateTests] Navigation error (attempt {retryCount + 1}): {ex.GetType().Name} - {ex.Message}");
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
        
        if (!Page.Url.Contains("ContractReceivableInvoice/Index"))
        {
            throw new Exception($"Navigation to ContractReceivableInvoice page failed. Current URL: {Page.Url}");
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
    public async Task TEST1_RebateInvoiceCreateAndReverse()
    {
        Driver.SetPage(Page);
        
        // Alacak Havuzu ekranında arama
        await _receivablePoolPage.FillCalculationDateAsync("31.05.2026");
        await _receivablePoolPage.FillContractNameAsync("PMI-2026-FCA");
        await _receivablePoolPage.ClickSearchButtonAsync();
        await Page.WaitForTimeoutAsync(2000);
        
        // İlk kaydı seç ve Rebate Faturası Oluştur
        await _receivablePoolPage.ClickFirstRowCheckboxAsync();
        await _receivablePoolPage.ClickCreateRebateInvoiceButtonAsync();
        bool frameOpened = await _receivablePoolPage.VerifyCreateRebateInvoiceFrameOpenedAsync();
        Assert.True(frameOpened, "Rebate Faturası Oluştur frame'i açılmalı");
        
        // Frame içinde açıklama yaz ve kaydet
        await _receivablePoolPage.FillDescriptionInFrameAsync("TEST AUTOMATION");
        await _receivablePoolPage.ClickSaveButtonInFrameAsync();
        await Page.WaitForTimeoutAsync(3000);
        
        // Alacak Havuzu ekranına geri dön ve fatura linkine tıkla
        await _receivablePoolPage.ClickInvoiceNumberLinkAsync();
        bool updateFrameOpened = await _receivablePoolPage.VerifyUpdateRebateInvoiceFrameOpenedAsync();
        Assert.True(updateFrameOpened, "Rebate Fatura Güncelleme frame'i açılmalı");
        
        // Geri Çek butonuna tıkla
        await _receivablePoolPage.ClickReverseButtonAsync();
        await Page.WaitForTimeoutAsync(5000);
        
        // Pop-up'ta geri çekme nedenini yaz ve onayla
        await _receivablePoolPage.FillReverseReasonInPopupAsync("TEST REVERSE");
        await _receivablePoolPage.ClickConfirmButtonInPopupAsync();
        await Page.WaitForTimeoutAsync(2000);
        
        // Başarı mesajını doğrula
        bool successMessage = await _receivablePoolPage.VerifySuccessMessageAsync();
        Assert.True(successMessage, "İşlem başarılı mesajı görünmeli");
    }

    [Fact]
    public async Task TEST2_DifferentCompanyConditionsError()
    {
        Driver.SetPage(Page);
        
        // Alacak Havuzu ekranında TEST ile arama
        await _receivablePoolPage.FillDescriptionAsync("TEST");
        await _receivablePoolPage.ClickSearchButtonAsync();
        await Page.WaitForTimeoutAsync(2000);
        
        // Alacak No 219 ve 207 olan kayıtları seç
        await _receivablePoolPage.SelectCheckboxForReceivableNumberAsync("219");
        await _receivablePoolPage.SelectCheckboxForReceivableNumberAsync("207");
        
        // Rebate Faturası Oluştur butonuna tıkla
        await _receivablePoolPage.ClickCreateRebateInvoiceButtonAsync();
        await Page.WaitForTimeoutAsync(2000);
        
        // Hata mesajını doğrula
        bool errorDisplayed = await _receivablePoolPage.VerifyErrorMessageContainsAsync(
            "Kondisyonlarin ait olduğu sözleşmeler aynı firmaya ait olmadığı için birleştiremezsiniz."
        );
        
        Assert.True(errorDisplayed, "Farklı firmaya ait kondisyonlar hata mesajı görünmeli");
    }

    [Fact]
    public async Task TEST3_SameCompanyConditionsModalOpen()
    {
        Driver.SetPage(Page);
        
        // Alacak Havuzu ekranında TEST ile arama
        await _receivablePoolPage.FillDescriptionAsync("TEST");
        await _receivablePoolPage.ClickSearchButtonAsync();
        await Page.WaitForTimeoutAsync(2000);
        
        // Alacak No 222 ve 223 olan kayıtları seç
        await _receivablePoolPage.SelectCheckboxForReceivableNumberAsync("222");
        await _receivablePoolPage.SelectCheckboxForReceivableNumberAsync("223");
        
        // Rebate Faturası Oluştur butonuna tıkla
        await _receivablePoolPage.ClickCreateRebateInvoiceButtonAsync();
        await Page.WaitForTimeoutAsync(2000);
        
        // Rebate Faturası Oluştur modal'ının açıldığını doğrula
        bool modalOpened = await _receivablePoolPage.VerifyRebateInvoiceCreateModalOpenedAsync();
        Assert.True(modalOpened, "Aynı firmaya ait kondisyonlar seçildiğinde modal açılmalı");
    }
}
