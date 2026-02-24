using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Infrastructure;

/// <summary>
/// Browser fixture to share browser instance across tests in a collection
/// Significantly improves test performance by avoiding repeated browser initialization
/// </summary>
public class BrowserFixture : IAsyncLifetime
{
    private IPage? _page;
    private LoginPage? _loginPage;
    
    public async Task InitializeAsync()
    {
        // Initialize browser and login once for all tests in collection
        _page = await Driver.GetPageAsync();
        Driver.SetPage(_page);
        
        _loginPage = new LoginPage();
        await _loginPage.NavigateToLoginPageAsync();
        await _loginPage.LoginAsAsync("normal");
        await _loginPage.VerifyLoginSuccessAsync();
    }

    public IPage Page => _page ?? throw new InvalidOperationException("Browser not initialized");

    public async Task DisposeAsync()
    {
        await Driver.CloseAsync();
        GlobalVariables.Instance.Clear();
    }
}

/// <summary>
/// Collection definition to group tests that share browser instance
/// </summary>
[CollectionDefinition("Browser Collection")]
public class BrowserCollectionFixture : ICollectionFixture<BrowserFixture>
{
    // This class is never instantiated
}
