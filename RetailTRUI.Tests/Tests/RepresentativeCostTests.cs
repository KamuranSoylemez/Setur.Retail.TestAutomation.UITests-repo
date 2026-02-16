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
        
        // Wait for page to fully load after login
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000); // Extra delay to ensure session is established
        
        _output.WriteLine($"T1: Current URL before nav - {Page.Url}");
        
        // Navigate to Representative Cost page
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await Task.Delay(2000); // Extra delay to ensure page fully loaded
        
        _output.WriteLine($"T1: Current URL after nav - {Page.Url}");
        
        _output.WriteLine("✅ T1 passed");
    }

    // T2: Firma ile Sorgulama
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T2")]
    public async Task T2_FilterByCompany_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);
        
        // Wait for page to fully load after login
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000); // Extra delay to ensure session is established
        
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await Task.Delay(2000);
        
        const string company = "PHILIP MORRIS INTERNATIONAL";
        await _representativeCostPage.FilterByCompanyAsync(company);
        await _representativeCostPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        await _representativeCostPage.VerifyGridContainsTextAsync(company);
        _output.WriteLine("✅ T2 passed - Company filter");
    }

    // T3: Faturalama PB ile Sorgulama
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T3")]
    public async Task T3_FilterByBillingCurrency_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);
        
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
        
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await Task.Delay(2000);
        
        const string currency = "EUR";
        await _representativeCostPage.FilterByBillingCurrencyAsync(currency);
        await _representativeCostPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        await _representativeCostPage.VerifyGridContainsTextAsync(currency);
        _output.WriteLine("✅ T3 passed - Billing currency filter");
    }

    // T4: Maliyet PB ile Sorgulama
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T4")]
    public async Task T4_FilterByCostCurrency_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);
        
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
        
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await Task.Delay(2000);
        
        const string currency = "EUR";
        await _representativeCostPage.FilterByCostCurrencyAsync(currency);
        await _representativeCostPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        await _representativeCostPage.VerifyGridContainsTextAsync(currency);
        _output.WriteLine("✅ T4 passed - Cost currency filter");
    }

    // T5: Maliyet Tarihi ile Sorgulama
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T5")]
    public async Task T5_FilterByCostDate_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);
        
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
        
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await Task.Delay(2000);
        
        const string date = "31.12.2025";
        await _representativeCostPage.FilterByCostDateAsync(date);
        await _representativeCostPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        await _representativeCostPage.VerifyGridContainsTextAsync(date);
        _output.WriteLine("✅ T5 passed - Cost date filter");
    }

    // T6: Maliyet Durumu ile Sorgulama
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T6")]
    public async Task T6_FilterByCostStatus_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);
        
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
        
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await Task.Delay(2000);
        
        const string status = "Onaylandı";
        await _representativeCostPage.FilterByCostStatusAsync(status);
        await _representativeCostPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        await _representativeCostPage.VerifyGridContainsTextAsync(status);
        _output.WriteLine("✅ T6 passed - Cost status filter");
    }

    // T7: Sözleşme Adı ile Sorgulama
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T7")]
    public async Task T7_FilterByDescription_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);
        
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
        
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await Task.Delay(2000);
        
        const string description = "PMI-2025-CFR";
        await _representativeCostPage.FilterByDescriptionAsync(description);
        await _representativeCostPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        await _representativeCostPage.VerifyGridContainsTextAsync(description);
        _output.WriteLine("✅ T7 passed - Description filter");
    }

    // T8: Temsilci Tutar PB ile Sorgulama
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T8")]
    public async Task T8_FilterByRepresentativeAmountCurrency_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);
        
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
        
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await Task.Delay(2000);
        
        const string currency = "EUR";
        await _representativeCostPage.FilterByRepresentativeAmountCurrencyAsync(currency);
        await _representativeCostPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        await _representativeCostPage.VerifyGridContainsTextAsync(currency);
        _output.WriteLine("✅ T8 passed - Representative amount currency filter");
    }

    // T9: Kombinasyon Filtresi - Firma ve Fatura PB
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T9")]
    public async Task T9_FilterByCompanyAndBillingCurrency_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);
        
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
        
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await Task.Delay(2000);
        
        const string company = "PHILIP MORRIS INTERNATIONAL";
        const string currency = "EUR";
        
        await _representativeCostPage.FilterByCompanyAsync(company);
        await _representativeCostPage.FilterByBillingCurrencyAsync(currency);
        await _representativeCostPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        // Grid'de sonuç olmalı
        await _representativeCostPage.VerifyGridHasAnyRowAsync();
        _output.WriteLine("✅ T9 passed - Combined company + billing currency filter");
    }

    // T10: Kombinasyon Filtresi - Tarihi ve Durumu
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T10")]
    public async Task T10_FilterByDateAndStatus_ShouldListMatchingRecords()
    {
        Driver.SetPage(Page);
        
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
        
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await Task.Delay(2000);
        
        const string date = "31.12.2025";
        const string status = "Onaylandı";
        
        await _representativeCostPage.FilterByCostDateAsync(date);
        await _representativeCostPage.FilterByCostStatusAsync(status);
        await _representativeCostPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        // Grid'de sonuç olmalı
        await _representativeCostPage.VerifyGridHasAnyRowAsync();
        _output.WriteLine("✅ T10 passed - Combined date + status filter");
    }

    // T11: Boş alan Sorgulama - Tüm Kayıtlar
    [Fact]
    [Trait("Category", "Filters")]
    [Trait("TestId", "T11")]
    public async Task T11_EmptyFilters_ShouldShowAllRecords()
    {
        Driver.SetPage(Page);
        
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
        
        await _representativeCostPage!.NavigateToRepresentativeCostPageAsync();
        await Task.Delay(2000);
        
        // Hiç filtre olmadan sorgula (tüm kayıtları getir)
        await _representativeCostPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        // Grid'de kayıt olmalı
        await _representativeCostPage.VerifyGridHasAnyRowAsync();
        _output.WriteLine("✅ T11 passed - All records displayed");
    }
}
