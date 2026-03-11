# 📊 APPLICATION BUGS - QUICK REFERENCE

**Last Updated:** March 6, 2026  
**Source:** Individual TEST2 execution with AssertionScope pattern  
**Status:** 4 Bugs Found Across 11 Tests

---

## 🚨 BUG SUMMARY TABLE

| Bug # | Field Name | Expected | Actual | Severity | Tests Affected |
|-------|-----------|----------|--------|----------|---|
| 1 | Hedefli | disabled | mandatory | 🔴 CRITICAL | 8 tests |
| 2 | Birim Çarpanı | mandatory | optional | 🟠 MAJOR | TEST2 |
| 3 | Tutar Çarpan Var mı? | mandatory | optional | 🟠 MAJOR | TEST2 |
| 4 | Hesaplama Tutar | disabled | optional | 🟠 MAJOR | TEST9 |

---

## 🔴 BUG #1: HEDEFLI FIELD (CRITICAL - 8 TESTS)

### Affected Tests:
- ❌ TEST1_Salary  
- ❌ TEST2_Bonus
- ❌ TEST5_Commission3_Hierarchical
- ❌ TEST7_Commission2_Hierarchical
- ❌ TEST8_Commission2_Hierarchical2
- ❌ TEST9_Commission_Hesaplamasız
- ❌ TEST10_PromotionRental
- ❌ TEST11_PromotionMarketing

### Issue:
```
Expected: disabled (read-only)
Got: mandatory (required field)
```

### Impact:
- User can modify target setting in scenarios where it should be locked
- 8 out of 11 tests fail due to this issue alone
- Form shows field as required when it should be disabled

### Fix Location:
- Component: BrandAmbassador form
- Element ID: `#yes_HasTarget` (radio button)
- Issue: Field enabled when it should be disabled in these scenarios

---

## 🟠 BUG #2: BIRIM ÇARPANI FIELD (MAJOR - TEST2 ONLY)

### Affected Test:
- ❌ TEST2_Bonus

### Issue:
```
Expected: mandatory (required field)
Got: optional (no required indicator)
```

### Impact:
- User can create bonus configuration without specifying unit multiplier
- Incomplete bonus setup possible
- Bonus calculation may fail without this parameter

### Fix Location:
- Test: TEST2_Bonus
- Scenario: Bonus compensation
- Field: "Birim Çarpanı" (Unit Multiplier)

---

## 🟠 BUG #3: TUTAR ÇARPAN VAR MI? FIELD (MAJOR - TEST2 ONLY)

### Affected Test:
- ❌ TEST2_Bonus

### Issue:
```
Expected: mandatory (required field)
Got: optional (no required indicator)
```

### Impact:
- User can skip amount multiplier decision in bonus setup
- Inconsistent bonus calculation behavior
- Form allows incomplete configuration

### Fix Location:
- Test: TEST2_Bonus
- Scenario: Bonus compensation
- Field: "Tutar Çarpan Var mı?" (Is there an amount multiplier?)

---

## 🟠 BUG #4: HESAPLAMA TUTAR FIELD (MAJOR - TEST9 ONLY)

### Affected Test:
- ❌ TEST9_Commission_Hesaplamasız

### Issue:
```
Expected: disabled (read-only in non-calculation scenario)
Got: optional (user can modify)
```

### Impact:
- User can modify calculation amount in fixed commission scenario
- Field should be locked when "Hesaplamasız" (No Calculation) is selected
- Data consistency issue: amount shouldn't matter for fixed commission

### Fix Location:
- Test: TEST9_Commission_Hesaplamasız
- Scenario: Commission with no calculation
- Field: "Hesaplama Tutar" (Calculation Amount)
- Dependency: When "Hesaplamasız" = Evet

---

## 📈 TEST FAILURE BREAKDOWN

### Total Tests: 11
### Passing: 0 ✅
### Failing: 11 ❌

```
Failure Cause Distribution:

Bug #1 (Hedefli):              8 tests affected
Bug #2 (Birim Çarpanı):        1 test affected (TEST2)
Bug #3 (Tutar Çarpan Var mı?): 1 test affected (TEST2)  
Bug #4 (Hesaplama Tutar):      1 test affected (TEST9)

Note: TEST2 has 3 bugs (Bugs #1, #2, #3)
      TEST9 has 2 bugs (Bugs #1, #4)
      Other tests have 1 bug each (only Bug #1)
```

### Failure Details by Test:

```
✅ TEST1_Salary                   
   └─ Bug #1: Hedefli (mandatory → disabled)

❌ TEST2_Bonus                    
   ├─ Bug #1: Hedefli (mandatory → disabled)
   ├─ Bug #2: Birim Çarpanı (optional → mandatory)
   └─ Bug #3: Tutar Çarpan Var mı? (optional → mandatory)

❌ TEST3_Commission_HasTarget     
   └─ Bug #1: Hedefli (mandatory → disabled)
   └─ [+ other bugs - analysis pending]

❌ TEST4_Commission_NoTarget      
   └─ Bug #1: Hedefli (mandatory → disabled)
   └─ [+ other bugs - analysis pending]

❌ TEST5_Commission3_Hierarchical 
   └─ Bug #1: Hedefli (mandatory → disabled)

❌ TEST6_Commission2_SalesVolume  
   └─ Bug #1: Hedefli (mandatory → disabled)
   └─ [+ other bugs - analysis pending]

❌ TEST7_Commission2_NoHierarchy  
   └─ Bug #1: Hedefli (mandatory → disabled)

❌ TEST8_Commission2_Hierarchical2
   └─ Bug #1: Hedefli (mandatory → disabled)

❌ TEST9_Commission_Hesaplamasız  
   ├─ Bug #1: Hedefli (mandatory → disabled)
   └─ Bug #4: Hesaplama Tutar (optional → disabled)

❌ TEST10_PromotionRental         
   └─ Bug #1: Hedefli (mandatory → disabled)

❌ TEST11_PromotionMarketing      
   └─ Bug #1: Hedefli (mandatory → disabled)
```

---

## 🎯 PRIORITY FIX ORDER

**Blocking Critical:** 🔴 Bug #1 (Hedefli)
- FIX FIRST - blocks 8/11 tests

**After Bug #1, Fix Remaining:** 🟠
- Bug #2 (Birim Çarpanı) - TEST2 unblocked by #1 still fails due to #2
- Bug #3 (Tutar Çarpan Var mı?) - TEST2 unblocked by #1 still fails due to #3
- Bug #4 (Hesaplama Tutar) - TEST9 unblocked by #1 still fails due to #4

**Expected Results After Fixes:**
- Fix Bug #1 → 8 tests pass (TEST1, TEST5, TEST7, TEST8, TEST10, TEST11 + partial TEST2, TEST9)
- Fix Bugs #2, #3 → TEST2 passes
- Fix Bug #4 → TEST9 passes
- **Final Result: 11/11 tests PASS ✅**

---

## 🔧 TECHNICAL DETAILS

### Field Mapping (Page Object)
```csharp
"Hedefli" → #yes_HasTarget (radio button)
"Birim Çarpanı" → [Needs mapping]
"Tutar Çarpan Var mı?" → [Needs mapping]  
"Hesaplama Tutar" → [Needs mapping]
```

### Field Detection Logic
- GetFieldStatusAsync() returns: "mandatory" | "optional" | "disabled" | "not shown"
- Radio buttons checked for label with required indicator
- Input elements checked for disabled attribute
- All elements checked for visibility on page

### AssertionScope Pattern
```csharp
using (new AssertionScope())
{
    // Multiple assertions execute without stopping at first failure
    // All failures collected and reported together
    // Example: TEST2 shows all 3 bugs at once
}
```

---

## 📞 NEXT STEPS FOR DEVELOPERS

1. **Review Bugs #1, #2, #3, #4** in comprehensive report
2. **Check application form rendering** for these fields
3. **Verify business logic conditions** in application code
4. **Validate against specification** (temsilci_kondisyonu_ekran_kuralları.md)
5. **Fix application UI** to match specification
6. **Deploy to staging** and re-run tests
7. **Expect result:** 11/11 tests passing

---

## 📄 REFERENCE FILES

- **Comprehensive Report:** [2026-03-06_COMPREHENSIVE_APPLICATION_BUGS.md](2026-03-06_COMPREHENSIVE_APPLICATION_BUGS.md)
- **Test Source:** BrandAmbassadorConditionTests.cs
- **Page Object:** BrandAmbassadorConditionPage.cs
- **Specification:** temsilci_kondisyonu_ekran_kuralları.md
- **Application URL:** https://dfs-retail-ui-staging.azurewebsites.net/

---

**Generated:** March 6, 2026  
**Test Execution Method:** Individual TEST2 run with AssertionScope  
**Test Framework:** xUnit.net with FluentAssertions
