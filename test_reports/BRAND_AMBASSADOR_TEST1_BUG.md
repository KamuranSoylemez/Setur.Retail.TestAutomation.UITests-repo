# Test Bug Report - BrandAmbassadorConditionTests

## Test: TEST1_Salary_WithoutCalculation_ShouldShowCorrectFieldValidation

**Status:** ❌ FAILED  
**Duration:** 23 seconds  
**Date:** 2026-03-05

---

## Bug Description

When testing Brand Ambassador condition field validation for "Salary" calculation type without additional fields, the field **'Kdv Dahil mi?' (Include VAT?)** is **optional** but the test expects it to be **mandatory**.

### Error Message

```
Expected status to be "mandatory" with a length of 9 because Field 'Kdv Dahil mi?' 
should be mandatory, but "optional" has a length of 8, differs near "opt" (index 0).
```

### Stack Trace Location

- **Test File:** `BrandAmbassadorConditionTests.cs` (line 137)
- **Page Method:** `VerifyFieldIsMandatoryAsync("Kdv Dahil mi?")` at `BrandAmbassadorConditionPage.cs` (line 314)

---

## Steps to Reproduce

1. Navigate to Contract Definition page
2. Open Brand Ambassador Condition form
3. Select Calculation Type: **"Salary"** (Maaş)
4. Verify field validation
5. Check field **'Kdv Dahil mi?'** (Include VAT?) status
6. **Observe:** Field status is "optional" instead of "mandatory"

---

## Expected Behavior

The field 'Kdv Dahil mi?' (Include VAT?) should be marked as **mandatory** when Salary calculation type is selected without additional calculation type.

---

## Actual Behavior

The field 'Kdv Dahil mi?' (Include VAT?) is marked as **optional**.

---

## Impact

- **Severity:** Medium
- **Affected Module:** Brand Ambassador Condition / Marka Elçi Koşulu
- **Test Count:** 1 test affected
- **Blocking:** Yes - Test cannot pass until this is resolved

---

## Recommendations

1. **Application Team:** Review field validation rules for "Salary" calculation type
2. **QA:** Verify if this is design behavior or a bug
3. **Test:** Consider updating test expectations if field should remain optional

---

**Detected By:** Automated Test Suite  
**Test Framework:** Playwright / xUnit  
**Browser:** Chrome (headless: false)
