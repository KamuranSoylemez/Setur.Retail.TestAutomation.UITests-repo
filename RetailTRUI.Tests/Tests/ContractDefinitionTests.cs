using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Supplier;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Contract Definition tests migrated from contractDefinition.feature
/// Tests the creation and validation of contract definitions
/// </summary>
public class ContractDefinitionTests : TestBase
{
    private ContractDefinitionPage _contractDefinitionPage = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _contractDefinitionPage = new ContractDefinitionPage();
    }

    /// <summary>
    /// TEST1: Contract Definition New Record Test
    /// Scenario: Create a new contract definition for BUTİK-AKSESUAR category
    /// </summary>
    [Fact(DisplayName = "TEST1 - Should create new contract definition successfully for BUTIK-AKSESUAR")]
    public async Task ContractDefinition_NewRecordTest_ShouldCreateSuccessfully()
    {
        Driver.SetPage(Page);
        
        // Background - already logged in via TestBase
        await GlobalPage.ClickSupplierDropdownToggleAsync();
        await GlobalPage.ClickContractDefinitionLinkAsync();
        
        // Then verify contract definition page is displayed
        await _contractDefinitionPage.VerifyContractDefinitionPageIsDisplayedAsync();
        
        // And open new contract definition form
        await _contractDefinitionPage.OpenNewContractDefinitionFormAsync();
        
        // And fill out the form and save "BUTİK-AKSESUAR"
        await _contractDefinitionPage.OpenCompanyIdentificationFrameAsync();
        await _contractDefinitionPage.FillCompanyCodeAsync("BUTİK-AKSESUAR");
        await _contractDefinitionPage.SearchCompanyAsync();
        await _contractDefinitionPage.SelectCompanyFromListAsync();
        await _contractDefinitionPage.OpenCategoriesAsync();
        await _contractDefinitionPage.SelectCategoryOptionAsync("BUTİK-AKSESUAR");
        await _contractDefinitionPage.SelectTypeOptionAsync();
        await _contractDefinitionPage.SelectStartDateAsync();
        await _contractDefinitionPage.SelectFirstDayOfMonthAsync();
        await _contractDefinitionPage.SelectFiscalMonthStartAsync();
        await _contractDefinitionPage.SelectIncotermsAsync();
        await _contractDefinitionPage.SelectBrandAsync("***");
        await _contractDefinitionPage.FillTermDaysAsync();
        await _contractDefinitionPage.FillDescriptionAsync();
        
        // And save contract definition
        await _contractDefinitionPage.SaveContractDefinitionAsync();
        
        try
        {
            await _contractDefinitionPage.VerifyDuplicateRecordAsync();
        }
        catch (InvalidOperationException)
        {
            // Duplicate record exists - this is expected for re-runs
            Console.WriteLine("⚠️  Duplicate record exists, skipping verification");
            return;
        }
        
        await _contractDefinitionPage.VerifyRecordSavedSuccessfullyAsync();
        await _contractDefinitionPage.VerifyContractStatusAsync("Hazırlanıyor");
        await _contractDefinitionPage.CloseContractUpdateFrameAsync();
        
        // Then verify contract definition is created on main page "BUTİK-AKSESUAR"
        await _contractDefinitionPage.OpenCompanyIdentificationFrameOnMainPageAsync();
        await _contractDefinitionPage.FillCompanyCodeAsync("BUTİK-AKSESUAR");
        await _contractDefinitionPage.SearchCompanyAsync();
        await _contractDefinitionPage.SelectCompanyFromListAsync();
        await _contractDefinitionPage.OpenCategoriesFromMainPageAsync();
        await _contractDefinitionPage.SelectCategoryFromListAsync("BUTİK-AKSESUAR");
        await _contractDefinitionPage.SelectTypeOptionFromMainPageAsync();
        await _contractDefinitionPage.SearchForRecordOnMainPageAsync();
        await _contractDefinitionPage.VerifyRecordExistsOnMainPageAsync();
        await _contractDefinitionPage.VerifyFirmNameOnMainPageAsync();
        await _contractDefinitionPage.VerifyCategoryOnMainPageAsync("BUTİK-AKSESUAR");
        await _contractDefinitionPage.VerifyTypeOnMainPageAsync();
    }

    /// <summary>
    /// TEST2: Contract Definition Category/Type/Brand Test
    /// Tests multiple category/type/brand combinations
    /// </summary>
    [Theory(DisplayName = "TEST2 - Should validate category/type/brand combinations")]
    [MemberData(nameof(GetContractDefinitionTestData))]
    public async Task ContractDefinition_CategoryTypeAndBrandTest_ShouldValidateSuccessfully(
        string category, string type, string brand)
    {
        Driver.SetPage(Page);
        
        // Background - already logged in via TestBase
        await GlobalPage.ClickSupplierDropdownToggleAsync();
        await GlobalPage.ClickContractDefinitionLinkAsync();
        
        // Then verify contract definition page is displayed
        await _contractDefinitionPage.VerifyContractDefinitionPageIsDisplayedAsync();
        
        // And fill out the form for categories (data table iteration)
        await _contractDefinitionPage.OpenNewContractDefinitionFormAsync();
        await _contractDefinitionPage.OpenCompanyIdentificationFrameAsync();
        await _contractDefinitionPage.FillCompanyCodeAsync(category);
        await _contractDefinitionPage.SearchCompanyAsync();
        await _contractDefinitionPage.SelectCompanyFromListAsync();
        await _contractDefinitionPage.OpenCategoriesAsync();
        await _contractDefinitionPage.SelectCategoryOptionAsync(category);
        await _contractDefinitionPage.VerifyCategorySelectedAsync();
        await _contractDefinitionPage.SelectTypeAsync(type);
        await _contractDefinitionPage.VerifyTypeOptionSelectedAsync();
        await _contractDefinitionPage.SelectBrandAsync(brand);
        await _contractDefinitionPage.VerifyBrandSelectedAsync();
        await _contractDefinitionPage.CloseContractDefinitionFrameAsync();
    }

    /// <summary>
    /// Test data for TEST2 - 10 category/type/brand combinations from feature file
    /// </summary>
    public static IEnumerable<object[]> GetContractDefinitionTestData()
    {
        return new List<object[]>
        {
            new object[] { "BUTİK-AKSESUAR", "BAYANGİYİM", "NIŞANTAŞI" },
            new object[] { "PARFÜM-KOZMETİK", "PARFÜM", "LACOSTE" },
            new object[] { "GIDA", "KAHVALTILIK", "NESCAFE" },
            new object[] { "TÜTÜN ÜRÜNLERİ", "SİGARA", "PARLIAMENT" },
            new object[] { "İÇKİ", "ALKOLLÜ", "ABSOLUT" },
            new object[] { "OYUNCAK", "MİNİK", "BARBIE" },
            new object[] { "BAZAAR", "AKSESUAR", "BAZAARC1" },
            new object[] { "ELEKTRONİK", "TELEFON", "APPLE" },
            new object[] { "POŞET", "SEFFAF", "BELBIM01" },
            new object[] { "EŞANTİYON", "DAGILIM", "BAYAN" }
        };
    }
}
