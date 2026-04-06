# Retail TRUI Test Automation - .NET 8

Modern test automation framework for Retail TRUI application, migrated from Java/Cucumber to .NET 8 with clean, maintainable architecture.

## 🏗️ Architecture Overview

This project follows **clean test architecture** principles with:

- ✅ **xUnit** testing framework
- ✅ **Page Object Model** (POM) pattern
- ✅ **Microsoft Playwright** for browser automation
- ✅ **FluentAssertions** for readable assertions
- ✅ **SOLID** principles and separation of concerns
- ✅ **Role-based authentication** management
- ✅ **Code-first** approach (no BDD framework)

## 📁 Project Structure

```
RetailTRUI.Tests/
├── Config/
│   └── Env/
│       ├── staging.properties      # Environment configuration
│       └── staging.users.yml       # User credentials by role
├── Core/
│   ├── ConfigurationManager.cs    # Config loader
│   ├── Driver.cs                  # Playwright driver manager
│   ├── GlobalVariables.cs         # Shared test data storage
│   └── UserDataReader.cs          # User credentials reader
├── Infrastructure/
│   └── TestBase.cs                # Base test classes with auth
├── Pages/
│   ├── Common/
│   │   ├── BasePage.cs            # Base page with common methods
│   │   ├── LoginPage.cs           # Login functionality
│   │   └── GlobalPage.cs          # Navigation helpers
│   ├── RetailDefinition/
│   │   └── ProductDefinitionPage.cs
│   └── Supplier/
│       └── ContractConfirmationPage.cs
├── Tests/
│   ├── LoginTests.cs
│   ├── ProductDefinitionTests.cs
│   ├── ContractConfirmationDirectorTests.cs
│   └── ContractConfirmationManagerTests.cs
└── Enums/
    └── TestEnums.cs               # Test enumerations
```

## 🚀 Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code
- Playwright browsers (installed automatically on first run)

### Installation

1. **Restore dependencies:**
   ```bash
   cd RetailTRUI.Tests
   dotnet restore
   ```

2. **Install Playwright browsers:**
   ```bash
   pwsh bin/Debug/net8.0/playwright.ps1 install
   ```

### Running Tests

**Run all tests:**
```bash
dotnet test
```

**Run specific test class:**
```bash
dotnet test --filter "FullyQualifiedName~LoginTests"
```

**Run with verbose output:**
```bash
dotnet test --logger "console;verbosity=detailed"
```

## 🔐 Authentication Strategy

The framework implements **role-based authentication** through test base classes:

### User Roles

- **Normal User** (default): `TestBase` - Used by most tests
- **Director**: `DirectorTestBase` - Contract director approval tests
- **Manager**: `ManagerTestBase` - Contract manager approval tests

### User Configuration

Users are defined in `Config/Env/staging.users.yml`:

```yaml
normal:
  username: "KAMURAN_SOYLEMEZ"
  password: "ks1221KS!!3"

RETAIL_DIRECTOR:
  username: "RETAIL_DIRECTOR"
  password: "rt1221RT!!"

RETAIL_MANAGER:
  username: "RETAIL_MANAGER"
  password: "rt1221RT!!"
```

### Test Example

```csharp
// Regular test - uses normal user
public class ProductDefinitionTests : TestBase
{
    [Fact]
    public async Task CreateProduct_ShouldSucceed()
    {
        // Already logged in as normal user
        await _productPage.CreateProductAsync();
    }
}

// Director test - uses RETAIL_DIRECTOR
public class ContractConfirmationDirectorTests : DirectorTestBase
{
    [Fact]
    public async Task ApproveContract_ShouldSucceed()
    {
        // Already logged in as director
        await _contractPage.ApproveContractAsync();
    }
}
```

## 📝 Writing Tests

### Test Class Template

```csharp
using Xunit;
using RetailTRUI.Tests.Infrastructure;

namespace RetailTRUI.Tests.Tests;

public class MyFeatureTests : TestBase
{
    private MyFeaturePage _page = null!;

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync(); // Handles login
        _page = new MyFeaturePage();
        
        // Navigate to your feature
        await GlobalPage.NavigateToMyFeatureAsync();
    }

    [Fact]
    public async Task MyTest_WithCondition_ShouldSucceed()
    {
        // Arrange
        await _page.SetupDataAsync();
        
        // Act
        await _page.PerformActionAsync();
        
        // Assert
        await _page.VerifyResultAsync();
    }
}
```

### Page Object Template

```csharp
using Microsoft.Playwright;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Pages.MyFeature;

public class MyFeaturePage : BasePage
{
    private ILocator MyButton => Page.Locator("#myButton");
    
    public async Task ClickMyButtonAsync()
    {
        await MyButton.ClickAsync();
    }
    
    public async Task VerifyResultAsync()
    {
        var isVisible = await MyButton.IsVisibleAsync();
        isVisible.Should().BeTrue();
    }
}
```

## 🧪 Test Categories

### Login Tests (`LoginTests.cs`)
- ✅ Successful login with valid credentials
- ✅ Unsuccessful login scenarios (invalid credentials, empty fields)

### Product Definition Tests (`ProductDefinitionTests.cs`)
- ✅ Create product with all required fields
- ✅ Activate product after creation
- ✅ Copy existing product

### Contract Confirmation Tests
- **Director Tests** (`ContractConfirmationDirectorTests.cs`)
  - ✅ Director approval workflow
  - ✅ Cancellation approval
  - ✅ Manager approval status visibility

- **Manager Tests** (`ContractConfirmationManagerTests.cs`)
  - ✅ Manager approval workflow
  - ✅ Limited access to director approvals

## ⚙️ Configuration

### Environment Settings (`staging.properties`)

```properties
browser=chrome
baseUrl=https://dfs-retail-ui-staging.azurewebsites.net/
env=staging
default_timeout=30
default_assertion_timeout=30
slow_mo=1000
```

## 🎯 Key Design Principles

1. **No BDD Framework**: Tests are written in C# code, not Gherkin
2. **Single Responsibility**: Each page object handles one page
3. **DRY Principle**: Common logic in base classes
4. **Async/Await**: All I/O operations are asynchronous
5. **Explicit Waits**: Playwright handles waiting automatically
6. **Strong Typing**: Leverage C# type system for safety
7. **Fluent Assertions**: Readable and maintainable assertions

## 🔄 Migration Notes

### What Changed from Java/Cucumber

| Before (Java/Cucumber) | After (.NET/xUnit) |
|------------------------|---------------------|
| Feature files (.feature) | Test classes (.cs) |
| Step definitions | Test methods |
| Given/When/Then | Arrange/Act/Assert |
| Cucumber hooks | xUnit lifecycle (IAsyncLifetime) |
| JUnit runner | xUnit test runner |
| Maven (pom.xml) | NuGet (.csproj) |

### Benefits of Migration

✅ **Better IDE support** - Full IntelliSense, refactoring, debugging  
✅ **Type safety** - Compile-time error detection  
✅ **Faster execution** - No feature file parsing overhead  
✅ **Easier maintenance** - Direct code navigation  
✅ **Better testability** - Standard unit testing patterns  
✅ **Modern async/await** - Better performance  

## 🐛 Troubleshooting

### Playwright Browsers Not Found
```bash
pwsh bin/Debug/net8.0/playwright.ps1 install chromium
```

### Configuration File Not Found
Ensure `staging.properties` and `staging.users.yml` are marked as "Copy to Output Directory".

### Tests Failing Due to Timeout
Increase timeout in `staging.properties`:
```properties
default_timeout=60
```

## 📚 Additional Resources

- [Playwright .NET Documentation](https://playwright.dev/dotnet/)
- [xUnit Documentation](https://xunit.net/)
- [FluentAssertions Documentation](https://fluentassertions.com/)

## 👥 Contributors

Migrated from Java/Cucumber to .NET 8 - January 2026

---

**Note**: This framework prioritizes **long-term maintainability** and **clean code** over framework complexity. Tests are easy to read, write, and debug.
