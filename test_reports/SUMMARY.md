# Bug Report Summary

## Brand Ambassador Condition Tests

**Total Tests:** 11  
**Status:** ❌ 0 PASSED / 11 FAILED  
**Duration:** ~8 minutes 30 seconds  
**Report:** BRAND_AMBASSADOR_ALL_TESTS_REPORT.md  

### Critical Issues Found

| Test # | Test Name | Field Issue | Expected | Actual | Status |
|--------|-----------|-------------|----------|--------|--------|
| 1 | Salary_WithoutCalculation | Kdv Dahil mi? | mandatory | optional | ❌ FAIL |
| 2 | Bonus_WithoutCalculation | Hesaplama Tutar Para Birimi | mandatory | disabled | ❌ FAIL |
| 3 | Commission_SalesQuantity_WithTarget | Hesaplama Para Birimi | mandatory | disabled | ❌ FAIL |
| 4 | Commission_SalesQuantity_NoTarget | Hesaplama Para Birimi | mandatory | disabled | ❌ FAIL |
| 5 | Commission_SalesQuantity_WithGradient | Hesaplama Para Birimi | mandatory | disabled | ❌ FAIL |
| 6 | Commission_SalesRevenue_WithTarget | Hesaplama Para Birimi | mandatory | disabled | ❌ FAIL |
| 7 | Commission_SalesRevenue_NoTarget | Hesaplama Para Birimi | mandatory | disabled | ❌ FAIL |
| 8 | Commission_SalesRevenue_WithGradient | Hesaplama Para Birimi | mandatory | disabled | ❌ FAIL |
| 9 | Commission_WithoutCalculation | Hesaplama Para Birimi | mandatory | disabled | ❌ FAIL |
| **10** | **PromotionRentalFee_WithoutCalculation** | **Hesaplama Periyodu** | **mandatory** | **not shown** | **❌ FAIL** |
| **11** | **PromotionMarketingActivity_WithoutCalculation** | **Hesaplama Periyodu** | **mandatory** | **not shown** | **❌ FAIL** |

---

## Navigation Fix Applied

✅ Updated BrandAmbassadorConditionTests.InitializeAsync():
- Changed WaitUntil from DOMContentLoaded to **NetworkIdle**
- Added **3-attempt retry logic** with session detection
- Implemented **re-authentication on session loss** (LoginPage integration)
- Applied **CreditNote/ReceivablePoolSearch pattern**

**Impact:** Tests now run successfully without "Target page, context or browser has been closed" errors

---

## Issue Analysis

### Primary Issue: Field Validation Status Mismatches
- **Pattern 1:** Currency fields expected "mandatory" but got "disabled" (7 tests)
- **Pattern 2:** Amount currency field expected "mandatory" but got "disabled" (1 test)
- **Pattern 3:** Tax inclusion field expected "mandatory" but got "optional" (1 test)
- **Pattern 4:** Calculation Period field completely hidden/not shown for Promotion types (2 tests NEW)

### Root Cause
Application's field validation rules for Brand Ambassador conditions do not match test expectations. 

For Promotion types specifically: The 'Hesaplama Periyodu' field is completely hidden/not shown, suggesting either:
  1. Design change: Field should not appear for Promotion condition types
  2. Bug: Field should be shown and mandatory
  3. Test expectation mismatch: Test needs updating

---

## Test Coverage Update

✅ **Added TEST10 & TEST11** to cover Promotion condition types:
- TEST10: Promotion Rental Fee (Promosyon Kira Bedeli)
- TEST11: Promotion Marketing Activity (Promosyon Marketing Aktivitesi)

Both new tests validate field visibility and status for Promotion types.

## Severity: HIGH
- **Blocking:** Yes
- **Scope:** 100% of Brand Ambassador condition tests
- **Impact:** All field validation tests failing

---

## Next Steps

1. [ ] Review application field validation rules for all condition types
2. [ ] Investigate why 'Hesaplama Periyodu' is hidden for Promotion types
3. [ ] Determine if changes are intentional (design) or bugs
4. [ ] Update test expectations or fix application logic
5. [ ] Re-run all tests after fixes

---

## Test Files Generated

- `BRAND_AMBASSADOR_TEST1_BUG.md` - Initial single test report
- `BRAND_AMBASSADOR_ALL_TESTS_REPORT.md` - Comprehensive all tests report (11/11)
- `SUMMARY.md` - This summary with all findings

**Test Framework:** Playwright / xUnit  
**Date:** 2026-03-05  
**New Tests Added:** TEST10, TEST11 from scenario specifications

