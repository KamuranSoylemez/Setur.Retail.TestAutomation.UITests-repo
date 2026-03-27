# E2E Contract Definition Cancel Prompt

## Test Scenario: Cancel Contract Definition Record

### Objective
Cancel the contract definition record created in TEST0 using the Cancel workflow.

### Prerequisites
- TEST0 has successfully created a contract definition (FENERIUM / BUTİK-AKSESUAR)
- User is logged in to RetailTRUI staging environment
- Contract Definition page is accessible

### Test Data
| Field | Value |
|-------|-------|
| **Firma (Company)** | FENERIUM |
| **Kategori (Category)** | BUTİK-AKSESUAR |
| **Tip (Type)** | Aksesuar |
| **Arama Kriteri (Search Criteria)** | FENERIUM |

### Step-by-Step Execution

#### Step 1: Navigate to Contract Definition Page
- Go to URL: https://dfs-retail-ui-staging.azurewebsites.net/ApplicationManagement/Contract/Index
- Wait for page load (NetworkIdle)
- Verify page displayed

#### Step 2: Search for Contract Record
- Click company identification frame button
- Enter company code: FENERIUM
- Click Search button
- Wait for search results to load (1200ms delay)
- Select FENERIUM from results list

#### Step 3: Apply Category Filter
- Open categories filter dropdown (800ms delay)
- Select category: BUTİK-AKSESUAR
- Select type filter option

#### Step 4: Search on Main Page
- Click search/filter button on main page
- Wait for results to load (1500ms delay)
- Verify contract record appears in grid

#### Step 5: Open Contract Record
- Click first contract record in the grid
- Wait for edit page to fully load (2000ms delay)
- Verify record details are displayed

#### Step 6: Cancel Contract
- Locate "İptal Et" (Cancel) button
- Click the button
- Confirm cancellation if prompted
- Wait for operation to complete (2000ms delay)

#### Step 7: Verify Cancellation Status
- Contract status should change to "İptal Edilmiş" (Cancelled)
- Verify status update on page
- Close edit frame

### Expected Results
- ✅ Contract definition successfully cancelled
- ✅ Status shows "İptal Edilmiş" (Cancelled)
- ✅ Record no longer appears in active contracts list
- ✅ Operation completes without errors

### Error Handling
- If cancel button not found: Take screenshot, check page element
- If confirmation dialog appears: Accept/confirm the cancellation
- If status doesn't update: Refresh page and verify again

### Related Tests
- **TEST0:** E2E_ContractDefinitionPrompt.md - Create contract definition
- **Related Methods:** ContractDefinitionPage.CancelContractAsync() (to be implemented)

---

**Last Updated:** March 26, 2026  
**Test Class:** ContractDefinitionTests  
**Test Method:** TEST1 (ContractDefinition_CancelTest)
