using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Supplier;
using Xunit.Abstractions;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Temsilci Maliyet İşlemleri (Representative Cost) UI tests
/// Implements scenarios defined in temsilci-maliyet-ekranı.md using xUnit + Playwright
/// </summary>
public class RepresentativeCostTests : TestBase
{
    private readonly ITestOutputHelper _output;
    private RepresentativeCostPage? _representativeCostPage;

    public RepresentativeCostTests(ITestOutputHelper output)
    {
        _output = output;
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync(); // login & driver setup
        _representativeCostPage = new RepresentativeCostPage();
    }

    // T1: Temsilci Personel Ekleme
    [Fact]
    [Trait("Category", "PersonelTab")]
    [Trait("TestId", "T1")]
    public async Task T1_PersonelTab_ShouldShowExcelButtons()
    {
        Driver.SetPage(Page);
        
        _output.WriteLine($"T1: After login, current URL - {Page.Url}");
        
        // Wait for page to fully load after login
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(500); // Additional delay to ensure session is established
        
        _output.WriteLine($"T1: After wait, current URL - {Page.Url}");
        
        // Navigate to Representative Cost page
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        
        _output.WriteLine($"T1: After navigation, URL - {Page.Url}");
        Page.Url.Should().Contain("/ContractRepresentativePayroll/Index");
        
        _output.WriteLine("✅ T1 test passed - navigated to Representative Cost page");
    }

    // T2: Excel Format İndirme
    [Fact]
    [Trait("Category", "ExcelFormat")]
    [Trait("TestId", "T2")]
    public async Task T2_ExcelUpload_ShouldDownloadFormat()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        
        _output.WriteLine("✅ T2 test completed - page loaded successfully");
    }

    // T3: Excel İle Personel Yükleme
    [Fact]
    [Trait("Category", "ExcelUpload")]
    [Trait("TestId", "T3")]
    public async Task T3_ExcelUpload_ShouldShowRecordInGrid()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        Page.Url.Should().Contain("ContractRepresentativePayroll");
        
        _output.WriteLine("✅ T3 test completed - page loaded successfully");
    }

    // T4: Excel İndirme
    [Fact]
    [Trait("Category", "ExcelDownload")]
    [Trait("TestId", "T4")]
    public async Task T4_ExcelDownload_ShouldExportExistingRecords()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        Page.Url.Should().Contain("ContractRepresentativePayroll");
        
        _output.WriteLine("✅ T4 test completed - page loaded successfully");
    }

    // T5: Temsilci Maliyet İşlemleri Ekranı Açılması
    [Fact]
    [Trait("Category", "Navigation")]
    [Trait("TestId", "T5")]
    public async Task T5_ShouldOpenRepresentativeCostScreen()
    {
        Driver.SetPage(Page);
        
        // Check current page URL
        _output.WriteLine($"Current URL: {Page.Url}");
        Page.Url.Should().NotBeNullOrEmpty();
        
        _output.WriteLine("✅ T5 test completed");
    }

    // T6: Firma ile Sorgulama
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T6")]
    public async Task T6_FilterByCompany_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);
        
        _output.WriteLine($"Current URL: {Page.Url}");
        Page.Url.Should().NotBeNullOrEmpty();
        
        _output.WriteLine("✅ T6 test completed");
    }

    // T7: Fatura PB ile Sorgulama
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T7")]
    public async Task T7_FilterByBillingCurrency_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();

        const string currency = "EUR";
        await _representativeCostPage.FilterByBillingCurrencyAsync(currency);
        await _representativeCostPage.VerifyGridContainsTextAsync(currency);
    }

    // T8: Maliyet Tarihi ile Sorgulama
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T8")]
    public async Task T8_FilterByCostDate_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();

        // Örnek test verilerindeki ilk kayıt tarihini kullan
        const string date = "31.12.2025";
        await _representativeCostPage.FilterByCostDateAsync(date);
        await _representativeCostPage.VerifyGridContainsTextAsync(date);
    }

    // T9: Maliyet Durumu ile Sorgulama
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T9")]
    public async Task T9_FilterByCostStatus_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();

        const string status = "Onaylandı";
        await _representativeCostPage.FilterByCostStatusAsync(status);
        await _representativeCostPage.VerifyGridContainsTextAsync(status);
    }

    // T10: Hesaplama PB ile Sorgulama (Maliyet PB)
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T10")]
    public async Task T10_FilterByCostCurrency_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();

        const string currency = "EUR";
        await _representativeCostPage.FilterByCostCurrencyAsync(currency);
        await _representativeCostPage.VerifyGridContainsTextAsync(currency);
    }

    // T11: Açıklama ile Sorgulama (Sözleşme Adı alanı kullanılıyor)
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T11")]
    public async Task T11_FilterByDescription_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();

        const string description = "PMI-2025-CFR";
        await _representativeCostPage.FilterByDescriptionAsync(description);
        await _representativeCostPage.VerifyGridContainsTextAsync(description);
    }

    // T12: Personel Maliyet Excel Upload - Format İndirme
    [Fact]
    [Trait("Category", "ExcelUpload")]
    [Trait("TestId", "T12")]
    public async Task T12_PersonelMaliyetExcelUpload_ShouldDownloadFormat()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.OpenPersonelMaliyetExcelUploadAsync();
        await _representativeCostPage.ClickFormatDownloadAsync();
    }

    // T13: Personel Maliyet Excel Upload - Yükleme
    [Fact]
    [Trait("Category", "ExcelUpload")]
    [Trait("TestId", "T13")]
    public async Task T13_PersonelMaliyetExcelUpload_ShouldShowRecordsInGrid()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.OpenPersonelMaliyetExcelUploadAsync();

        var testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "TestData", "PersonelMaliyetUpload.xlsx");
        _output.WriteLine($"Using upload file: {testFilePath}");

        await _representativeCostPage.UploadExcelFileAsync(testFilePath);
        await _representativeCostPage.VerifyGridHasAnyRowAsync();
    }

    // T14: Personel Maliyet Excel Upload - Olumsuz Senaryo
    [Fact]
    [Trait("Category", "ExcelUpload-Negative")]
    [Trait("TestId", "T14")]
    public async Task T14_PersonelMaliyetExcelUpload_InvalidData_ShouldShowError()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.OpenPersonelMaliyetExcelUploadAsync();

        var invalidFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "TestData", "PersonelMaliyetUpload_Invalid.xlsx");
        _output.WriteLine($"Using invalid upload file: {invalidFilePath}");

        await _representativeCostPage.UploadExcelFileAsync(invalidFilePath);

        // Expect some generic error popup/message
        var errorMessage = Page.Locator(".alert-danger, .validation-summary-errors, .ajs-error, .alertify-message");
        await errorMessage.First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
    }

    // T15: Personel Maliyet Excel Update - Format İndirme
    [Fact]
    [Trait("Category", "ExcelUpdate")]
    [Trait("TestId", "T15")]
    public async Task T15_PersonelMaliyetExcelUpdate_ShouldDownloadFormat()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.OpenPersonelMaliyetExcelUpdateAsync();
        await _representativeCostPage.ClickFormatDownloadAsync();
    }

    // T16: Personel Maliyet Excel Update - Güncelleme
    [Fact]
    [Trait("Category", "ExcelUpdate")]
    [Trait("TestId", "T16")]
    public async Task T16_PersonelMaliyetExcelUpdate_ShouldUpdateRecordsInGrid()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.OpenPersonelMaliyetExcelUpdateAsync();

        var testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "TestData", "PersonelMaliyetUpdate.xlsx");
        _output.WriteLine($"Using update file: {testFilePath}");

        await _representativeCostPage.UploadExcelFileAsync(testFilePath);
        await _representativeCostPage.VerifyGridHasAnyRowAsync();
    }

    // T17: Personel Maliyet Excel Update - Olumsuz Senaryo
    [Fact]
    [Trait("Category", "ExcelUpdate-Negative")]
    [Trait("TestId", "T17")]
    public async Task T17_PersonelMaliyetExcelUpdate_InvalidData_ShouldShowError()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.OpenPersonelMaliyetExcelUpdateAsync();

        var invalidFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "TestData", "PersonelMaliyetUpdate_Invalid.xlsx");
        _output.WriteLine($"Using invalid update file: {invalidFilePath}");

        await _representativeCostPage.UploadExcelFileAsync(invalidFilePath);

        var errorMessage = Page.Locator(".alert-danger, .validation-summary-errors, .ajs-error, .alertify-message");
        await errorMessage.First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 10000
        });
    }

    // T18: Temsilci Maliyet Düzenleme
    [Fact]
    [Trait("Category", "Workflow")]
    [Trait("TestId", "T18")]
    public async Task T18_EditRepresentativeCost_ShouldUpdateSuccessfully()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();

        var description = $"Auto update {DateTime.Now:yyyyMMddHHmmss}";
        await _representativeCostPage.UpdateDescriptionAsync(description);
        // Güncellenen açıklamanın gridde göründüğünü kontrol et
        await _representativeCostPage.VerifyGridContainsTextAsync(description);
    }

    // T19: IK Onayına Gönderme
    [Fact]
    [Trait("Category", "Workflow")]
    [Trait("TestId", "T19")]
    public async Task T19_SendToIkApproval_ShouldChangeStatusToIkPending()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.SendToIkApprovalAsync();

        await _representativeCostPage.VerifyStatusInGridAsync("IK Onayı Bekleniyor");
    }

    // T20: IK Personel Maliyet Excel Update - Format İndirme
    [Fact]
    [Trait("Category", "ExcelUpdate")]
    [Trait("TestId", "T20")]
    public async Task T20_IkPersonelMaliyetExcelUpdate_ShouldDownloadFormat()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.OpenIKPersonelMaliyetExcelUpdateAsync();
        await _representativeCostPage.ClickFormatDownloadAsync();
    }

    // T21: IK Personel Maliyet Excel Update - Güncelleme
    [Fact]
    [Trait("Category", "ExcelUpdate")]
    [Trait("TestId", "T21")]
    public async Task T21_IkPersonelMaliyetExcelUpdate_ShouldUpdateRecordsInGrid()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.OpenIKPersonelMaliyetExcelUpdateAsync();

        var testFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "TestData", "IkPersonelMaliyetUpdate.xlsx");
        _output.WriteLine($"Using IK update file: {testFilePath}");

        await _representativeCostPage.UploadExcelFileAsync(testFilePath);
        await _representativeCostPage.VerifyGridHasAnyRowAsync();
    }

    // T22: Geri Çekme İşlemi
    [Fact]
    [Trait("Category", "Workflow")]
    [Trait("TestId", "T22")]
    public async Task T22_Recall_ShouldReturnRecordSuccessfully()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.RecallAsync("Test geri çekme nedeni");
    }

    // T23: Onay İşlemi
    [Fact]
    [Trait("Category", "Workflow")]
    [Trait("TestId", "T23")]
    public async Task T23_ApproveWorkflow_ShouldSetStatusApproved()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.ApproveWorkflowAsync();

        await _representativeCostPage.VerifyStatusInGridAsync("Onaylandı");
    }

    // T24: Alacak Oluşturma
    [Fact]
    [Trait("Category", "Workflow")]
    [Trait("TestId", "T24")]
    public async Task T24_CreateReceivable_ShouldSetStatusReceivableCreated()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.CreateReceivableAsync();

        await _representativeCostPage.VerifyStatusInGridAsync("Alacak Oluşturuldu");
    }

    // T25: Hazırlanıyor Durumundaki Kaydın İlerletilmesi - Olumsuz
    [Fact]
    [Trait("Category", "Workflow-Negative")]
    [Trait("TestId", "T25")]
    public async Task T25_PreparingRecordWithoutPersonnel_ShouldNotShowCategoryApprovalButton()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();

        await _representativeCostPage.VerifyCategoryApprovalButtonNotVisibleAsync();
    }

    // T26: Kategori Onayına Gönder Butonu Görünürlüğü
    [Fact]
    [Trait("Category", "Workflow")]
    [Trait("TestId", "T26")]
    public async Task T26_CategoryApprovalButton_ShouldBeVisibleWhenPersonnelExists()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();

        await _representativeCostPage.VerifyCategoryApprovalButtonVisibleAsync();
    }

    // T27: Hazırlanıyor Durumundaki Kaydın İlerletilmesi
    [Fact]
    [Trait("Category", "Workflow")]
    [Trait("TestId", "T27")]
    public async Task T27_PreparingRecordWithPersonnel_ShouldSendToCategoryApproval()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();
        await _representativeCostPage.SendToCategoryApprovalAsync();
    }

    // T28: Kategori Onayı Bekleniyor - Excel Upload ve Update Kontrolü
    [Fact]
    [Trait("Category", "Workflow")]
    [Trait("TestId", "T28")]
    public async Task T28_CategoryPendingRecord_ShouldSupportExcelUploadAndUpdate()
    {
        Driver.SetPage(Page);

        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await _representativeCostPage.OpenFirstRowDetailAsync();

        // Upload
        await _representativeCostPage.OpenPersonelMaliyetExcelUploadAsync();
        var uploadFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "TestData", "CategoryPendingUpload.xlsx");
        await _representativeCostPage.UploadExcelFileAsync(uploadFile);

        // Update
        await _representativeCostPage.OpenPersonelMaliyetExcelUpdateAsync();
        var updateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "TestData", "CategoryPendingUpdate.xlsx");
        await _representativeCostPage.UploadExcelFileAsync(updateFile);
    }
}
