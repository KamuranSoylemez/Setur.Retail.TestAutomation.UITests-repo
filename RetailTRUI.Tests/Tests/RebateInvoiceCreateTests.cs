using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;
using RetailTRUI.Tests.Pages.Purchasing;

namespace RetailTRUI.Tests.Tests;

public class RebateInvoiceCreateTests : TestBase
{
    private ReceivablePoolSearchPage _receivablePoolPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        Driver.SetPage(Page);
        
        _receivablePoolPage = new ReceivablePoolSearchPage();
        
        // Navigate directly to receivable pool search page
        var config = ConfigurationManager.Instance;
        var receivablePoolUrl = config.BaseUrl.TrimEnd('/') + "/ApplicationManagement/ContractReceivableInvoice/Index";
        
        try
        {
            await Page.GotoAsync(receivablePoolUrl, new PageGotoOptions 
            { 
                WaitUntil = WaitUntilState.DOMContentLoaded,
                Timeout = 30000
            });
        }
        catch (PlaywrightException ex) when (ex.Message.Contains("ERR_ABORTED") || ex.Message.Contains("interrupted"))
        {
            await Task.Delay(2000);
            if (!Page.Url.Contains("ContractReceivableInvoice/Index"))
            {
                await Page.GotoAsync(receivablePoolUrl, new PageGotoOptions { WaitUntil = WaitUntilState.DOMContentLoaded });
            }
        }
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
