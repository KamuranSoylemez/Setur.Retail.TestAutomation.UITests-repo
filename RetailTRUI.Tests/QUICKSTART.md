# Quick Start Guide

## 🚀 Running Your First Test

### 1. Open Terminal in Project Directory
```bash
cd RetailTRUI.Tests
```

### 2. Restore NuGet Packages
```bash
dotnet restore
```

### 3. Build the Project
```bash
dotnet build
```

### 4. Install Playwright Browsers (First Time Only)
```bash
# On macOS/Linux
pwsh bin/Debug/net8.0/playwright.ps1 install

# Or just run tests and Playwright will prompt you
```

### 5. Run Tests
```bash
# Run all tests
dotnet test

# Run specific test
dotnet test --filter "FullyQualifiedName~LoginTests"

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"
```

## 📝 Example: Adding a New Test

### 1. Create a Test Class
```csharp
using Xunit;
using RetailTRUI.Tests.Infrastructure;

namespace RetailTRUI.Tests.Tests;

public class MyNewFeatureTests : TestBase
{
    [Fact]
    public async Task MyTest_ShouldPass()
    {
        // Your test code here
        Assert.True(true);
    }
}
```

### 2. Run Your New Test
```bash
dotnet test --filter "FullyQualifiedName~MyNewFeatureTests"
```

## 🔑 Key Files to Know

- **Tests/**: Your test classes go here
- **Pages/**: Page object classes
- **Config/Env/staging.users.yml**: User credentials
- **Config/Env/staging.properties**: App settings

## 💡 Tips

- All tests automatically login before running
- Use `TestBase` for normal user tests
- Use `DirectorTestBase` for director tests
- Use `ManagerTestBase` for manager tests

## 🆘 Common Issues

**"Playwright not found"**
```bash
pwsh bin/Debug/net8.0/playwright.ps1 install chromium
```

**"Config file not found"**
- Check that config files are in `Config/Env/` folder
- Run `dotnet build` to copy them to output

**Tests timing out**
- Increase timeout in `Config/Env/staging.properties`

---

**That's it! You're ready to run tests.** 🎉
