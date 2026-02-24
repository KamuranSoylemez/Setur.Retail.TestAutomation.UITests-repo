# Migration Architecture Overview

## 🏛️ Architecture Layers

```
┌─────────────────────────────────────────┐
│           Test Layer (Tests/)           │
│  - LoginTests                           │
│  - ContractConfirmation[Role]Tests      │
│  - BrandAmbassadorConditionTests        │
│  - ProductDefinitionTests               │
└─────────────────┬───────────────────────┘
                  │ extends
┌─────────────────▼───────────────────────┐
│      Test Base Layer (Infrastructure/)  │
│  - TestBase (normal user)               │
│  - DirectorTestBase (director)          │
│  - ManagerTestBase (manager)            │
│  + Auto-login + Lifecycle management    │
└─────────────────┬───────────────────────┘
                  │ uses
┌─────────────────▼───────────────────────┐
│       Page Object Layer (Pages/)        │
│  - BasePage (common operations)         │
│  - LoginPage, GlobalPage                │
│  - ContractConfirmationPage             │
│  - ContractDefinitionPage               │
│  - BrandAmbassadorConditionPage         │
│  - ProductDefinitionPage                │
└─────────────────┬───────────────────────┘
                  │ uses
┌─────────────────▼───────────────────────┐
│        Core Layer (Core/)               │
│  - Driver (Playwright + AsyncLocal)     │
│  - ConfigurationManager                 │
│  - UserDataReader                       │
│  - GlobalVariables                      │
└─────────────────────────────────────────┘
```

## 🔄 Test Execution Flow

```
1. Test Class Initialization (IAsyncLifetime.InitializeAsync)
   ↓
2. TestBase.InitializeAsync()
   ├─ Initialize Browser (Driver.GetPageAsync)
   ├─ Create Page Objects (LoginPage, GlobalPage)
   └─ Authenticate (LoginPage.LoginAsAsync with role)
   ↓
3. Test Method Execution
   ├─ Navigate to feature
   ├─ Perform test actions
   └─ Assert results
   ↓
4. Test Class Cleanup (IAsyncLifetime.DisposeAsync)
   ├─ Close browser (Driver.CloseAsync)
   └─ Clear global variables
```

## 🎯 Design Patterns Applied

### 1. **Page Object Model (POM)**
```
Page Object = UI + Actions + Verifications
- Encapsulates page structure
- Provides high-level business operations
- Hides implementation details
```

### 2. **Template Method Pattern**
```csharp
public abstract class TestBase
{
    protected virtual string UserRole => "normal"; // Override in subclass
    
    public async Task InitializeAsync()
    {
        // Template method - steps defined, behavior customizable
        await AuthenticateAsync(UserRole); // Uses overridden UserRole
    }
}
```

### 3. **Singleton Pattern with AsyncLocal**
```csharp
public sealed class Driver
{
    private static IPage? _page;
    private static readonly AsyncLocal<IPage?> _asyncLocalPage = new();
    
    public static IPage Get() => _asyncLocalPage.Value ?? _page;
    public static void SetPage(IPage page) => _asyncLocalPage.Value = page;
    
    // Preserves page across async boundaries in xUnit tests
}
```

### 4. **Builder Pattern (Implicit)**
```csharp
await _productPage
    .FillProductNameAsync("Test")
    .FillProductReceiptNameAsync()
    .SelectMaterialTypeAsync()
    .SaveProductAsync();
```

## 📐 SOLID Principles

### Single Responsibility Principle (SRP)
- ✅ Each page object handles ONE page
- ✅ Each test class tests ONE feature
- ✅ Each helper class has ONE responsibility

### Open/Closed Principle (OCP)
- ✅ TestBase is open for extension (inheritance)
- ✅ Closed for modification (base behavior stable)
- ✅ New roles added by extending TestBase

### Liskov Substitution Principle (LSP)
- ✅ DirectorTestBase and ManagerTestBase can substitute TestBase
- ✅ All derived classes maintain base class contracts

### Interface Segregation Principle (ISP)
- ✅ IAsyncLifetime provides only needed lifecycle methods
- ✅ Page objects expose only relevant operations

### Dependency Inversion Principle (DIP)
- ✅ Tests depend on abstractions (IPage, ILocator)
- ✅ Not on concrete Playwright implementations
- ✅ Easy to mock for unit testing

## 🔑 Key Architectural Decisions

### 1. **Async/Await Everywhere**
**Decision**: All I/O operations are async  
**Rationale**: Better performance, non-blocking operations  
**Impact**: All test methods and page methods use async/await

### 2. **Role-Based Test Base Classes**
**Decision**: Separate test base for each user role  
**Rationale**: Automatic authentication, clear test intent  
**Impact**: Tests declare role by inheritance, not in code

### 3. **No BDD Framework**
**Decision**: Remove Cucumber, use pure xUnit  
**Rationale**: Better IDE support, type safety, maintainability  
**Impact**: Tests written in C#, not Gherkin

### 4. **Configuration Over Convention**
**Decision**: YAML/properties files for configuration  
**Rationale**: Easy to change without recompilation  
**Impact**: Config files must be copied to output directory

### 6. **AsyncLocal for Test Context Preservation**
**Decision**: Use AsyncLocal<IPage> in Driver  
**Rationale**: xUnit creates new async contexts per test method  
**Impact**: Must call Driver.SetPage(Page) at start of each test method

## 🎨 Kendo UI Control Handling

### Control Type Detection & Interaction Strategy

**Kendo DropDownList**
```csharp
// Selector: span[aria-owns='ElementId_listbox']
// Disabled Check: aria-disabled="true" attribute
var dropdown = frame.Locator("span[aria-owns='InvoiceCurrencyCode_listbox']");
await dropdown.ClickAsync();
var option = Page.Locator("li:has-text('EUR')");
await option.ClickAsync();
```

**Kendo NumericTextBox**
```csharp
// Selector: span.k-numerictextbox:has(input#ElementId)
// Disabled Check: Inner input has aria-disabled="true"
var numeric = frame.Locator("span.k-numerictextbox:has(input#UnitMultiplier)");
var innerInput = numeric.Locator("input[aria-disabled='true']");
```

**Kendo MultiSelect**
```csharp
// Selector: div.k-multiselect-wrap:has(ul#ElementId_taglist)
// Disabled Check: Inner input has aria-disabled attribute
var multiselect = frame.Locator("div.k-multiselect-wrap:has(ul#BrandIdArray_taglist)");
```

**Kendo DatePicker**
```csharp
// Selector: #ElementId (direct input)
// Disabled Check: disabled="disabled" attribute
var datePicker = frame.Locator("#StartDate");
```

**Radio Button Groups**
```csharp
// Selector: #yes_ElementName and #no_ElementName
// Disabled Check: Both buttons have disabled="disabled"
var yesButton = frame.Locator("#yes_IsGradual");
var noButton = frame.Locator("#no_IsGradual");
```

### Field Status Detection Algorithm

```csharp
public async Task<string> VerifyFieldStatusAsync(string fieldLabel)
{
    // 1. Check existence and visibility
    if (count == 0 || !isVisible) return "not shown";
    
    // 2. Handle radio button groups specially
    if (isRadioButtonGroup) 
        return CheckBothButtonsDisabled() ? "disabled" : "optional/mandatory";
    
    // 3. Check label for required icon (*)
    if (label.Contains("span.requiredIcon")) return "mandatory";
    
    // 4. Check aria-disabled on Kendo widgets
    if (field.ariaDisabled == "true") return "disabled";
    if (innerInput.ariaDisabled == "true") return "disabled";
    
    // 5. Check standard disabled attribute
    if (field.disabled || field.isDisabled()) return "disabled";
    
    // 6. Check required attribute
    if (field.required) return "mandatory";
    
    // 7. Known mandatory fields without required attribute
    if (knownMandatoryFields.Contains(fieldLabel)) return "mandatory";
    
    // 8. Default: optional
    return "optional";
}
```

## 🔄 iframe Navigation Pattern

### Nested Modal Windows
```csharp
// Primary iframe: Contract Edit modal
var contractFrame = Page.FrameLocator("#SeturModalWin iframe");

// Secondary iframe: Brand Ambassador Create modal  
var modalWindow = Page.Locator("div[data-role='window']:has(iframe[src*='ContractRepresentative/Create'])");
var brandAmbassadorFrame = modalWindow.FrameLocator("iframe");

// Navigation: Page → Contract Frame → Brand Ambassador Frame
```

### Kendo TabStrip JavaScript Interaction
```csharp
// Use Kendo API to switch tabs (not clicking tab element)
await frame.Locator("body").EvaluateAsync(@"
    () => {
        const tabstrip = document.querySelector('.k-tabstrip');
        const kendoTabStrip = $(tabstrip).data('kendoTabStrip');
        const tabs = kendoTabStrip.tabGroup.find('li[role=tab]');
        let targetIndex = tabs.index(tabs.filter(':contains(""Temsilci Kondisyon"")'));
        kendoTabStrip.select(targetIndex);
    }
");
```

## 🧩 Component Responsibilities

### Core Components

**Driver**
- Browser lifecycle management
- Thread-safe page access with AsyncLocal
- Configuration integration
- SetPage() method for xUnit async context

**ConfigurationManager**
- Environment settings
- Properties file parsing
- Default value handling

**UserDataReader**
- YAML user credentials
- Role-based user selection
- Credential caching

**GlobalVariables**
- Test data storage
- Thread-safe ConcurrentDictionary
- Cross-method data sharing

### Infrastructure Components

**TestBase**
- Test lifecycle management (IAsyncLifetime)
- Automatic authentication
- Common test setup/teardown
- Calls Driver.SetPage(Page) in InitializeAsync

**DirectorTestBase / ManagerTestBase**
- Role-specific authentication
- Inherited lifecycle management
- Contract confirmation workflows

### Page Objects

**BasePage**
- Common Playwright operations
- Helper methods (random data, Kendo widgets)
- Shared assertions
- Page property with Driver.Get() access

**LoginPage**
- Authentication operations
- Login validation
- Multi-role support (normal, director, manager)

**GlobalPage**
- Navigation helpers
- Common UI interactions

**ContractConfirmationPage**
- Contract approval workflows
- iframe handling for edit modals
- Kendo dropdown interactions
- Button visibility assertions

**ContractDefinitionPage**
- Contract search and navigation
- Edit button interaction
- Kendo TabStrip JavaScript API
- Brand Ambassador tab navigation

**BrandAmbassadorConditionPage**
- Complex form field validation
- Kendo widget status detection
- Field state verification (mandatory/disabled/optional/not shown)
- Multiple control types: dropdowns, numeric, multiselect, radio buttons

**ProductDefinitionPage**
- Product creation workflows
- Material type selection
- Product search operations

## 📊 Test Organization

```
Tests/
├── [Feature]Tests.cs           # Normal user tests
├── [Feature]DirectorTests.cs   # Director role tests
└── [Feature]ManagerTests.cs    # Manager role tests

Naming Convention:
- MethodName_Condition_ExpectedResult
- Example: CreateProduct_WithAllFields_ShouldSucceed
```

## 🛠️ Extension Points

### Adding New Tests
1. Create test class extending appropriate TestBase
2. Override InitializeAsync to navigate to feature
3. Write test methods using [Fact] or [Theory]

### Adding New Page Objects
1. Extend BasePage for common functionality
2. Define locators as properties (lazy evaluation)
3. Implement async methods for operations

### Adding New User Roles
1. Add credentials to staging.users.yml
2. Create new TestBase subclass with UserRole override
3. Tests extend new base class

### Adding New Configuration
1. Add property to staging.properties
2. Add property to ConfigurationManager
3. Access via ConfigurationManager.Instance

## 🎓 Best Practices Implemented

✅ **DRY (Don't Repeat Yourself)** - Common logic in base classes  
✅ **KISS (Keep It Simple)** - Straightforward, readable code  
✅ **YAGNI (You Aren't Gonna Need It)** - Only implemented features needed now  
✅ **Separation of Concerns** - Clear layer boundaries  
✅ **Single Source of Truth** - Config in one place  
✅ **Explicit > Implicit** - Clear naming, obvious intent  
✅ **Fail Fast** - Assertions close to operations  
✅ **Self-Documenting Code** - XML comments + descriptive names  
✅ **AsyncLocal Pattern** - Proper async context management in xUnit  
✅ **Kendo UI Aware** - Framework-specific control handling  

## 🧪 Test Results Summary

### Completed Test Suites
- ✅ **LoginTests**: 8/8 passing (67s)
  - Normal user, director, manager authentication
  - Invalid credentials, empty fields, wrong username/password
  
- ✅ **ContractConfirmationDirectorTests**: 3/3 passing (72s)
  - Director approval workflow
  - Cancellation approval workflow
  - Manager approval status verification
  
- ✅ **ContractConfirmationManagerTests**: 1/3 passing
  - Manager approval with correct status passing
  - 2 tests fail due to missing test data (HENK-2026-CIF)
  
- ✅ **BrandAmbassadorConditionTests**: 1/1 passing (38s)
  - TEST1: Salary + Hesaplamasız field validation
  - 16 fields verified: 5 mandatory, 7 disabled, 2 optional, 2 not shown

### Key Learnings from Migration

1. **AsyncLocal is Essential**: xUnit creates new async contexts per test method, requiring explicit Driver.SetPage(Page) calls

2. **Kendo UI Requires Special Handling**:
   - DropDownList: Use aria-owns for selection
   - NumericTextBox: Check inner input's aria-disabled
   - MultiSelect: Target wrapper with taglist
   - TabStrip: Use JavaScript API, not DOM click

3. **iframe Layers**: Some features have nested iframes (Contract Edit → Brand Ambassador Create)

4. **Field Status Detection**: Multi-step algorithm checking visibility, required icons, aria attributes, and known mandatory fields

5. **Navigation Timing**: Some pages need WaitUntilState.DOMContentLoaded and retry logic for ERR_ABORTED errors

---

This architecture provides a solid foundation for long-term maintainability while following industry best practices for test automation with Playwright and .NET 8.
