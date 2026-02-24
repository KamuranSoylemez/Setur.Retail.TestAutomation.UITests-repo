using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Infrastructure;

/// <summary>
/// Base test class providing common test infrastructure
/// Handles browser initialization, cleanup, and authentication
/// </summary>
public abstract class TestBase : IAsyncLifetime
{
    protected IPage Page { get; private set; } = null!;
    protected LoginPage LoginPage { get; private set; } = null!;
    protected GlobalPage GlobalPage { get; private set; } = null!;
    
    /// <summary>
    /// Override this property in derived classes to specify different user roles
    /// Default is "normal" user
    /// </summary>
    protected virtual string UserRole => "normal";

    public virtual async Task InitializeAsync()
    {
        // Initialize browser and page FIRST
        Page = await Driver.GetPageAsync();
        
        // Set Page for Driver so BasePage can access it
        Driver.SetPage(Page);
        
        // THEN initialize page objects
        LoginPage = new LoginPage();
        GlobalPage = new GlobalPage();
        
        // Perform login before each test
        await AuthenticateAsync();
    }

    private async Task AuthenticateAsync()
    {
        await LoginPage.NavigateToLoginPageAsync();
        await LoginPage.LoginAsAsync(UserRole);
        await LoginPage.VerifyLoginSuccessAsync();
    }

    public async Task DisposeAsync()
    {
        await Driver.CloseAsync();
        GlobalVariables.Instance.Clear();
    }
}

/// <summary>
/// Test base for tests requiring Director role authentication
/// </summary>
public abstract class DirectorTestBase : TestBase
{
    protected override string UserRole => "RETAIL_DIRECTOR";
}

/// <summary>
/// Test base for tests requiring Manager role authentication
/// </summary>
public abstract class ManagerTestBase : TestBase
{
    protected override string UserRole => "RETAIL_MANAGER";
}
