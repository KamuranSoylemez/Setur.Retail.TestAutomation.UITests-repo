<<<<<<< HEAD
# Setur.Retail.TestAutomation.UITests
=======
# Retail TRUI Test Automation Suite

## Introduction

This project is a comprehensive test automation suite for the **Retail TRUI (Tedarikçi Onay İşlemleri - Supplier Approval Operations)** application. It provides automated testing for supplier contract approval workflows with support for multiple user roles (Director and Manager).

### Key Features
- **Role-based Testing**: Separate test suites for Director and Manager approval operations
- **Multi-scenario Coverage**: Tests for different approval statuses (Manager Approval, Director Approval, Cancellation)
- **Advanced Button Detection**: Iframe-aware detection of approval action buttons across different workflows
- **Comprehensive Filtering**: Support for multi-filter scenarios (Document Type, Firm Code, Firm Name, Contract Name, Status)
- **Page Object Model**: Clean separation of test logic and page interactions

## Getting Started

### Prerequisites
- **.NET SDK 8.0.22** or later
- **Playwright** dependencies (auto-installed via NuGet)
- Chrome browser (for Headless: False testing)
- Configuration files for test environment

### Installation

1. Clone the repository:
```bash
git clone https://github.com/Setur-Yazilim/Setur.Retail.TestAutomation.UITests.git
cd RetailTRUI_Modernize_2
```

2. Install dependencies:
```bash
dotnet restore RetailTRUI.Tests/RetailTRUI.Tests.csproj
```

3. Ensure configuration files are in place:
```
RetailTRUI.Tests/Config/Env/staging.properties
RetailTRUI.Tests/Config/Env/staging.users.yml
```

### Configuration

Update `staging.properties` with:
```properties
base_url=https://dfs-retail-ui-staging.azurewebsites.net
headless=false
slowmo=500
```

## Build and Test

### Build the Project
```bash
dotnet build RetailTRUI.Tests/RetailTRUI.Tests.csproj
```

### Run All Tests
```bash
dotnet test RetailTRUI.Tests/RetailTRUI.Tests.csproj -v normal
```

### Run Specific Test Classes

**Director Approval Tests** (4 tests):
```bash
dotnet test RetailTRUI.Tests/RetailTRUI.Tests.csproj \
  --filter "FullyQualifiedName~SupplierCombinedApprovalScreenDirectorTests" -v normal
```

**Manager Approval Tests** (5 tests):
```bash
dotnet test RetailTRUI.Tests/RetailTRUI.Tests.csproj \
  --filter "FullyQualifiedName~SupplierCombinedApprovalScreenManagerTests" -v normal
```

### Run Individual Tests

**Run a specific test by name**:
```bash
dotnet test RetailTRUI.Tests/RetailTRUI.Tests.csproj \
  --filter "FullyQualifiedName~DirectorApprovalOperations_WithDirectorApprovalStatus_ShouldDisplayDirectorButtons" \
  -v normal
```

## Test Structure

### Director Test Suite (SupplierCombinedApprovalScreenDirectorTests)

| Test Name | Description | Status |
|-----------|-------------|--------|
| DirectorApprovalOperations_WithDirectorApprovalStatus_ShouldDisplayDirectorButtons | Verifies director sees approval/rejection buttons | ✅ PASS |
| DirectorApprovalOperations_WithCancellationStatus_ShouldDisplayCancellationButtons | Verifies director sees cancellation decision buttons | ✅ PASS |
| DirectorApprovalOperations_WithManagerApprovalStatus_ShouldDisplayLimitedButtons | Verifies director sees limited buttons for manager-level approvals | ✅ PASS |
| DirectorApprovalOperations_WithSWRIContract_ShouldApplySWRIFiltersAndDisplayActionButtons | Verifies multi-filter SWRI scenario with 5 filters | ✅ PASS |

### Manager Test Suite (SupplierCombinedApprovalScreenManagerTests)

| Test Name | Description | Status |
|-----------|-------------|--------|
| T1_ContractCancellationApproval_WithSpecificFilters_ShouldDisplayActionButtons | Verifies manager sees action buttons for SWRI-2025-CFR contract | ✅ PASS |
| ManagerApprovalOperations_WithManagerApprovalStatus_ShouldDisplayLimitedButtons | Verifies manager sees 5 buttons (Güncelle, Kapat, Onayla, Reddet, Geri Çek) | ✅ PASS |
| ManagerApprovalOperations_WithCancellationStatus_ShouldDisplayActionButtons | Verifies manager sees buttons for cancellation approval | ✅ PASS |
| ManagerApprovalOperations_WithDirectorApprovalStatus_ShouldDisplayReadOnlyView | Verifies manager sees 3 utility buttons (Güncelle, Kapat, Geri Çek) for director-level items | ✅ PASS |
| ManagerApprovalOperations_WithSWRIFilters_ShouldApplyMultipleFiltersAndDisplayRecord | Verifies multi-filter SWRI scenario finds exact match with buttons visible | ✅ PASS |

## Architecture

### Page Objects
- **SupplierCombinedApprovalScreen**: Unified interface for approval operations
  - Navigation and filter methods
  - Button detection utilities (detail buttons, approval action buttons, cancellation buttons)
  - Grid and record management

### Test Base Classes
- **TestBase**: Abstract base class with browser initialization
- **DirectorTestBase**: Extends TestBase, auto-logs in with RETAIL_DIRECTOR role
- **ManagerTestBase**: Extends TestBase, auto-logs in with RETAIL_MANAGER role

### Button Detection Methods
- **CheckDetailScreenButtonsAsync()**: Detects utility buttons (Güncelle, Kapat)
- **CheckApprovalActionButtonsAsync()**: Detects approval buttons (Onayla, Reddet)
- **CheckCancellationButtonsAsync()**: Detects cancellation buttons (İptal Talebini Onayla, İptal Talebini Reddet)

## Key Implementation Details

### Iframe Detection
Most action buttons are located within iframe contexts. All button detection methods implement frame iteration:
```csharp
var frames = Page.Frames;
foreach (var frame in frames)
{
    // Check for buttons in each frame
}
```

### Button Selectors
- **Güncelle (Update)**: `#btnSave`
- **Kapat (Close)**: `#ClosePopupBtn`
- **Onayla (Approve)**: `#BtnApprove` / Contains "Onayla" text
- **Reddet (Reject)**: `#BtnReject` / Contains "Reddet" text
- **İptal Talebini Onayla**: `#BtnApproveCancellation`
- **İptal Talebini Reddet**: `#BtnRejectCancellation`
- **Geri Çek (Withdraw)**: `#ContractWithdraw`

## Development Workflow

1. Create a feature branch:
```bash
git checkout -b feature/test-name
```

2. Implement tests following the Page Object Model pattern

3. Run tests locally:
```bash
dotnet test RetailTRUI.Tests/RetailTRUI.Tests.csproj -v normal
```

4. Commit with semantic commit messages:
```bash
git commit -m "feat: add test for specific scenario"
```

5. Push to remote and create a pull request:
```bash
git push origin feature/test-name
```

## Continuous Integration

Tests run automatically on:
- Pull requests to `main` branch
- Merges to `main` branch
- Scheduled nightly runs

See `azure-pipelines.yml` for CI/CD configuration.

## Troubleshooting

### Common Issues

**Buttons not detected**:
- Verify iframe context is being checked
- Check browser console for element visibility
- Ensure viewport size is set (1920x1080)

**Filters not applying**:
- Wait for dropdown to render before selecting
- Check test data availability in staging environment
- Verify user permissions for the filter options

**Timeout errors**:
- Increase delays between actions
- Check network connectivity to staging environment
- Review browser console for JavaScript errors

## Contributing

1. Follow the existing test structure and naming conventions
2. Use semantic naming for test methods
3. Document test purpose with XML comments
4. Ensure all tests pass before submitting PR
5. Include button detection for approval workflows
6. Test both happy path and edge cases

## References

- [Playwright C# Documentation](https://playwright.dev/dotnet/)
- [xUnit Documentation](https://xunit.net/)
- [Page Object Model Pattern](https://www.selenium.dev/documentation/test_practices/encouraged/page_object_models/)
>>>>>>> testsCopilot
