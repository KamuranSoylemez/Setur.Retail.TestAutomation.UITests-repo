# RETAIL TRUI TEST AUTOMATION - DEVELOPMENT PROTOCOL

## ⛔ FORBIDDEN ZONES - DO NOT TOUCH
Never, ever modify these folders:
- RetailTRUI.Tests/Pages/Common/ (GlobalPage.cs, BasePage.cs)
- RetailTRUI.Tests/Infrastructure/ (TestBase.cs, BrowserFixture.cs, Driver.cs)
- RetailTRUI.Tests/Core/ (ConfigurationManager.cs, GlobalVariables.cs, UserDataReader.cs, Driver.cs)
- RetailTRUI.Tests/Config/ (all configuration files)
- RetailTRUI.Tests/Enums/ (TestEnums.cs)

If any change in these folders is required, STOP and ASK USER FIRST.

## ✅ ALLOWED ZONES - YOU CAN EDIT
- RetailTRUI.Tests/Tests/*.cs (Test files: RepresentativeCostTests.cs, LoginTests.cs, etc.)
- RetailTRUI.Tests/Pages/[Supplier/Purchasing/RetailDefinition/etc]/*.cs (Page Objects)

## 📍 GLOBALPAGE.CS - ONLY FOR NAVIGATION LINKS
When creating a new test feature, ONLY add navigation links to GlobalPage.cs if external navigation is needed.

Add pattern:
```csharp
private ILocator NewFeatureLink => Page.Locator("a[href*='YourPath']");

public async Task ClickNewFeatureLinkAsync()
{
    await NewFeatureLink.ClickAsync();
    await Page.WaitForTimeoutAsync(1000);
}
```

ONLY add link if: Your test navigates FROM home/menu TO the feature page.
DON'T add if: Test navigates directly via URL in test code.

## 🔧 TEST CREATION WORKFLOW

### Step 1: Create/Update Page Object
File: `RetailTRUI.Tests/Pages/[Category]/YourPage.cs`

```csharp
using Microsoft.Playwright;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.[Category];

public class YourPage : BasePage
{
    public async Task NavigateToYourPageAsync()
    {
        var baseUrl = ConfigurationManager.Instance.BaseUrl;
        await Page.GotoAsync($"{baseUrl}/ApplicationManagement/YourPath/Index");
    }

    public async Task FilterByXAsync(string value)
    {
        var filterField = Page.Locator("selector-here");
        await filterField.FillAsync(value);
    }

    public async Task ClickSearchAsync()
    {
        var searchBtn = Page.Locator("button[name='search'], button:has-text('Sorgula')");
        await searchBtn.ClickAsync();
    }

    public async Task VerifyGridContainsTextAsync(string expected)
    {
        var grid = Page.Locator("table, div.grid, [role='grid']");
        await grid.Locator($"text={expected}").WaitForAsync();
    }

    public async Task VerifyGridHasAnyRowAsync()
    {
        var rows = Page.Locator("table tbody tr, div[role='row']");
        await Expect(rows.First).ToBeVisibleAsync();
    }
}
```

### Step 2: Create Test File
File: `RetailTRUI.Tests/Tests/YourTests.cs`

```csharp
using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.[Category];
using Xunit.Abstractions;

namespace RetailTRUI.Tests.Tests;

public class YourTests : TestBase
{
    private readonly ITestOutputHelper _output;
    private YourPage? _yourPage;

    public YourTests(ITestOutputHelper output)
    {
        _output = output;
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        _yourPage = new YourPage();
    }

    [Fact]
    [Trait("Category", "YourCategory")]
    [Trait("TestId", "T1")]
    public async Task T1_TestName_ShouldVerifyBehavior()
    {
        // REQUIRED: Setup page context after login
        Driver.SetPage(Page);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000); // Session establishment delay
        
        // Navigate to feature
        await _yourPage!.NavigateToYourPageAsync();
        await Task.Delay(2000);
        
        // Test logic
        const string testValue = "TEST_DATA";
        await _yourPage.FilterByXAsync(testValue);
        await _yourPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        // Verify
        await _yourPage.VerifyGridContainsTextAsync(testValue);
        _output.WriteLine("✅ T1 passed");
    }

    [Fact]
    [Trait("Category", "YourCategory")]
    [Trait("TestId", "T2")]
    public async Task T2_AnotherTest_ShouldWork()
    {
        Driver.SetPage(Page);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Task.Delay(2000);
        
        await _yourPage!.NavigateToYourPageAsync();
        await Task.Delay(2000);
        
        // Test logic here
        await _yourPage.ClickSearchAsync();
        await Task.Delay(1500);
        
        await _yourPage.VerifyGridHasAnyRowAsync();
        _output.WriteLine("✅ T2 passed");
    }
}
```

### Step 3: Update GlobalPage.cs (IF needed)
File: `RetailTRUI.Tests/Pages/Common/GlobalPage.cs`

ONLY if your test navigates from home menu:
```csharp
private ILocator YourFeatureLink => Page.Locator("a[href*='YourPath']");

public async Task ClickYourFeatureLinkAsync()
{
    await YourFeatureLink.ClickAsync();
    await Page.WaitForTimeoutAsync(1000);
}
```

### Step 4: Commit & Push
```bash
git add -A
git commit -m "test: Add YourTests with T1-T5 filter tests"
git push
```

## ⚙️ MANDATORY TEST SETUP (Copy for every test)
```csharp
Driver.SetPage(Page);
await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
await Task.Delay(2000); // REQUIRED: Session establishment
```

## 🧪 TEST PATTERN (Filter tests example)
For each filter:
1. Setup: Driver.SetPage + NetworkIdle + delay
2. Navigate: _page.NavigateToXAsync()
3. Filter: _page.FilterByXAsync()
4. Search: _page.ClickSearchAsync()
5. Wait: Task.Delay(1500)
6. Verify: _page.VerifyGridContains/HasAnyRow()

## 📋 CHECKLIST BEFORE COMMIT
- [ ] Tests follow exact pattern (SetPage → NetworkIdle → Navigate → Filter → Search → Verify)
- [ ] Page object methods are async and in separate file
- [ ] No changes to Common/, Infrastructure/, Config/, Core/, Enums/
- [ ] GlobalPage.cs only has navigation links added (if needed)
- [ ] Test file is in Tests/ folder
- [ ] Page object file is in Pages/[Category]/ folder
- [ ] Build passes: `dotnet build`
- [ ] At least one test passes: `dotnet test --filter "T1_YourTest"`
- [ ] Git commit with descriptive message

## 🚫 ASK USER IF:
1. Any folder in Common/, Infrastructure/, Config/, Core/, Enums/ needs modification
2. Need to change existing test methods (T1, T2, T3 that already exist)
3. Need to modify TestBase, BasePage, Driver, ConfigurationManager
4. Need to modify GlobalPage beyond simple link additions
5. Framework dependencies need to change (Playwright version, xUnit version)
6. Need to change a file undder test-scenarios

## 🎯 DEFAULT VALUES
- Headless: false (browser visible)
- SlowMo: 500ms
- Browser: Chrome
- Resolution: 1920x1080
- Environment: Staging (https://dfs-retail-ui-staging.azurewebsites.net/)

## 📝 NOTES
- All page navigation URLs must start with ConfigurationManager.Instance.BaseUrl
- All filter methods must be in Page Object, not in Test class
- All verification must check actual grid/table content
- Parallel execution is disabled (sequential tests only)
- Each test is isolated (login happens automatically in TestBase.InitializeAsync)
