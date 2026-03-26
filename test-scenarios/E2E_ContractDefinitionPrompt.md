# E2E Contract Definition Prompt

## Test Scenario: Create Contract Definition with FNR Firma and Aksesuar Category

### Objective
Create a complete contract definition record with specific parameters for FNR firma and Aksesuar category.

### Prerequisites
- User is logged in to RetailTRUI staging environment
- Contract Definition page is displayed

### Test Data
| Field | Value |
|-------|-------|
| **Firma (Company)** | FNR |
| **Kategori (Category)** | BUTİK-AKSESUAR |
| **Başlangıç Tarihi (Start Date)** | 01.04.2026 |
| **Mali Ay (Fiscal Month)** | Nisan |
| **Teslimat Şekli (Incoterms)** | CFR - Cost and Freight |
| **Cins (Type/Descriptor)** | Aksesuar |
| **Vade (Payment Terms - Days)** | %= (Special condition) |

### Step-by-Step Execution

#### Step 1: Fill Firma Field
- Locate Firma multiselect field (k-multiselect)
- Enter text: "FNR"
- Wait for dropdown suggestions to appear (800ms delay)
- Click the FNR option from dropdown

#### Step 2: Select Kategori
- Click Kategori field
- Wait for options to load (800ms delay)
- Select "BUTİK-AKSESUAR" from options

#### Step 3: Set Başlangıç Tarihi (Start Date)
- Click date picker field
- Enter or select: 01.04.2026

#### Step 4: Select Mali Ay (Fiscal Month)
- Locate Fiscal Month dropdown
- Select "Nisan" (April)

#### Step 5: Select Incoterms
- Locate Incoterms dropdown
- Select "CFR - Cost and Freight"

#### Step 6: Select Cins (Type)
- Locate Type/Descriptor field
- Select "Aksesuar"

#### Step 7: Fill Vade (Payment Terms)
- Locate Vade/Payment Terms field
- Enter: "%=" (or equivalent special condition value)

#### Step 8: Save
- Click Kaydet (Save) button
- Wait for save operation to complete (2000ms delay)
- Verify record saved successfully

### Expected Results
- ✅ Contract definition record created successfully
- ✅ All fields populated with correct values
- ✅ Record appears in main contract definition list
- ✅ Status shows "Hazırlanıyor" (In Preparation)

### Error Handling
- If duplicate record exists: Log warning, test should pass
- If element not found: Take screenshot, increase wait time, retry
- If save fails: Capture error message, validate form data

---

**Last Updated:** March 26, 2026  
**Test Class:** ContractDefinitionTests  
**Related Tests:** TEST0 (Component Inspection), TEST1 (Basic Creation)
