# Test Bug Report - BrandAmbassadorConditionTests

## Summary: All 11 Tests Completed

**Total Tests:** 11  
**Status:** ❌ 2 NEW TESTS / 9 EXISTING TESTS  
**Total Duration:** ~8 minutes 30 seconds  
**Date:** 2026-03-05

---

## Individual Test Failures

### TEST1: Salary_WithoutCalculation_ShouldShowCorrectFieldValidation
- **Status:** ❌ FAILED (23s)
- **Field Issue:** 'Kdv Dahil mi?' (Include VAT?)
- **Expected:** mandatory
- **Actual:** optional
- **Line:** BrandAmbassadorConditionTests.cs:137

---

### TEST2: Bonus_WithoutCalculation_ShouldShowCorrectFieldValidation
- **Status:** ❌ FAILED (23s)
- **Field Issue:** 'Hesaplama Tutar Para Birimi' (Calculation Amount Currency)
- **Expected:** mandatory
- **Actual:** disabled
- **BrandAmbassadorConditionPage.cs:** line 314

---

### TEST3: Commission_SalesQuantity_WithTarget_NoGradient_ShouldShowCorrectFieldValidation
- **Status:** ❌ FAILED (28s)
- **Field Issue:** 'Hesaplama Para Birimi' (Calculation Currency)
- **Expected:** mandatory
- **Actual:** disabled

---

### TEST4: Commission_SalesQuantity_NoTarget_NoGradient_ShouldShowCorrectFieldValidation
- **Status:** ❌ FAILED (28s)
- **Field Issue:** 'Hesaplama Para Birimi' (Calculation Currency)
- **Expected:** mandatory
- **Actual:** disabled

---

### TEST5: Commission_SalesQuantity_WithGradient_ShouldShowCorrectFieldValidation
- **Status:** ❌ FAILED (26s)
- **Field Issue:** 'Hesaplama Para Birimi' (Calculation Currency)
- **Expected:** mandatory
- **Actual:** disabled

---

### TEST6: Commission_SalesRevenue_WithTarget_NoGradient_ShouldShowCorrectFieldValidation
- **Status:** ❌ FAILED (28s)
- **Field Issue:** 'Hesaplama Para Birimi' (Calculation Currency)
- **Expected:** mandatory
- **Actual:** disabled

---

### TEST7: Commission_SalesRevenue_NoTarget_NoGradient_ShouldShowCorrectFieldValidation
- **Status:** ❌ FAILED (28s)
- **Field Issue:** 'Hesaplama Para Birimi' (Calculation Currency)
- **Expected:** mandatory
- **Actual:** disabled

---

### TEST8: Commission_SalesRevenue_WithGradient_ShouldShowCorrectFieldValidation
- **Status:** ❌ FAILED (26s)
- **Field Issue:** 'Hesaplama Para Birimi' (Calculation Currency)
- **Expected:** mandatory
- **Actual:** disabled

---

### TEST9: Commission_WithoutCalculation_ShouldShowCorrectFieldValidation
- **Status:** ❌ FAILED (23s)
- **Field Issue:** 'Hesaplama Para Birimi' (Calculation Currency)
- **Expected:** mandatory
- **Actual:** disabled

---

### TEST10: PromotionRentalFee_WithoutCalculation_ShouldShowCorrectFieldValidation
- **Status:** ❌ FAILED (21s) - **NEW TEST**
- **Field Issue:** 'Hesaplama Periyodu' (Calculation Period)
- **Expected:** mandatory
- **Actual:** not shown
- **Line:** BrandAmbassadorConditionTests.cs:552
- **Note:** Field is completely hidden/not displayed for Promotion Rental Fee condition type

---

### TEST11: PromotionMarketingActivity_WithoutCalculation_ShouldShowCorrectFieldValidation
- **Status:** ❌ FAILED (21s) - **NEW TEST**
- **Field Issue:** 'Hesaplama Periyodu' (Calculation Period)
- **Expected:** mandatory
- **Actual:** not shown
- **Line:** BrandAmbassadorConditionTests.cs:601
- **Note:** Field is completely hidden/not displayed for Promotion Marketing Activity condition type

---

## Common Issues

### Pattern 1: Currency/Amount Field Status Issues (8 tests)
**Affected Tests:** TEST2, TEST3, TEST4, TEST5, TEST6, TEST7, TEST8, TEST9  
**Issue Type:** Field status mismatch (expected "mandatory" but got "disabled")  
**Fields Affected:**
- 'Hesaplama Para Birimi' (Calculation Currency) - 7 occurrences
- 'Hesaplama Tutar Para Birimi' (Calculation Amount Currency) - 1 occurrence

### Pattern 2: Inclusion Field Status Issues (1 test)
**Affected Tests:** TEST1  
**Issue Type:** Field status mismatch (expected "mandatory" but got "optional")  
**Fields Affected:**
- 'Kdv Dahil mi?' (Include VAT?) - 1 occurrence

### Pattern 3: Hidden Field Visibility Issues (2 tests) - **NEW**
**Affected Tests:** TEST10, TEST11  
**Issue Type:** Field is not shown/hidden (expected "mandatory" but got "not shown")  
**Fields Affected:**
- 'Hesaplama Periyodu' (Calculation Period) - 2 occurrences for Promotion condition types
- **Condition Types:** Promotion Rental Fee, Promotion Marketing Activity

---

## Root Cause Analysis

All test failures are due to **field validation status mismatches**. The application's field validation rules for Brand Ambassador conditions do not match the test expectations:

1. **Currency fields** are being set to "disabled" when tests expect them to be "mandatory" (8 tests)
2. **Tax inclusion field** is being set to "optional" when test expects it to be "mandatory" (1 test)
3. **Calculation Period field** is completely hidden for Promotion condition types when test expects it to be "mandatory" (2 tests - NEW)

This suggests:
- Either the application behavior changed and field validation rules were updated
- Or the test expectations need to be updated to match current behavior
- Or there's a bug in the application field validation logic
- For Promotion types: The field logic may be different (not shown by design or bug)

---

## Impact Assessment

- **Severity:** High
- **Affected Module:** Brand Ambassador Condition / Marka Elçi Koşulu
- **Blocking:** Yes - 100% test failure rate (11/11 tests)
- **Scope:** All field validation tests for Brand Ambassador module
- **New Issues:** TEST10 & TEST11 added, both failing with hidden field issue

---

## Next Steps

1. **Application Team:** Review field validation logic for Brand Ambassador conditions
2. **For Promotion Types:** Investigate why 'Hesaplama Periyodu' is hidden/not shown
3. **QA:** Validate whether field status changes are intentional or bugs
4. **Test Maintenance:** Update test expectations if behavior changes are permanent
5. **Design Review:** Confirm if current field status is per design

---

**Test Framework:** Playwright / xUnit  
**Browser:** Chrome (headless: false)  
**Navigation Fix Applied:** ✅ NetworkIdle + Retry Logic + Re-authentication
