# Automation Test - Bug Report

## Issue: Company Filter Selection Lost After Category Selection

### Test Scenario
**Test Case:** TEST3_MultipleFiltersSearch  
**Status:** ❌ Application Bug Detected

---

## Bug Description

When applying multiple filters on the Receivable Pool Search page, the **Company (Firma)** filter selection is lost after selecting a **Category (Kategori)**.

### Steps to Reproduce

1. Navigate to: `/ApplicationManagement/ContractReceivableInvoice/Index`
2. Select Company filter: **"BACARDI"**
3. Select Category filter: **"İçki"**
4. **Observe:** The Company field reverts to empty/default state
5. The Category selection remains intact

### Expected Behavior
- When a Company is selected and a Category is selected, both filters should remain selected
- The filter state should persist across multiple filter selections

### Actual Behavior
- Company filter is cleared/reset when selecting a Category
- Only Category filter remains selected

### Impact
- **Severity:** Medium
- **Affected Module:** Receivable Pool Search / Kondisyon Havuzu Arama
- **Test Automation:** Able to detect via TEST3_MultipleFiltersSearch

### Test Execution Log
```
Test: ReceivablePoolSearchTests.TEST3_MultipleFiltersSearch
Step 1: SelectCompanyAsync("BACARDI") ✓ Selected
Step 2: FillRebateDateAsync("31.05.2024") ✓ Filled
Step 3: SelectCategoryAsync("İçki") ✓ Selected
Result: Company filter value is lost
```

---

## Recommended Action
- **Developers:** Investigate the form's filter interaction logic
- **QA:** Verify if this is isolated to Receivable Pool or affects other search pages
- **UI:** Ensure filter selections persist independently

---

**Detected By:** Automated Test Suite  
**Date:** 2026-03-04  
**Test Framework:** Playwright / xUnit
