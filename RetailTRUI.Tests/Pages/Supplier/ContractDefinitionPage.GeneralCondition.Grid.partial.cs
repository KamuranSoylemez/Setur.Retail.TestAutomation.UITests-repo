using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace RetailTRUI.Tests.Pages.Supplier;

public partial class ContractDefinitionPage
{
    public async Task<bool> HasAnyGeneralConditionRecordAsync()
    {
        var frame = await GetContractEditFrameAsync();
        await Page.WaitForTimeoutAsync(1000);

        var rows = GetGeneralConditionRows(frame);
        var count = await rows.CountAsync();
        Console.WriteLine($"📊 General condition record count: {count}");
        return count > 0;
    }

    public async Task OpenFirstGeneralConditionDetailAsync()
    {
        await OpenGeneralConditionDetailByRowIndexAsync(0);
    }

    public async Task<int> FindFirstGeneralConditionRowIndexByStatusAsync(string status)
    {
        var frame = await GetContractEditFrameAsync();
        var rows = GetGeneralConditionRows(frame);
        var rowCount = await rows.CountAsync();

        for (int i = 0; i < rowCount; i++)
        {
            var rowText = await rows.Nth(i).TextContentAsync();
            if (!string.IsNullOrWhiteSpace(rowText) && rowText.Contains(status, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }

            if (status.Equals("Hazırlanıyor", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(rowText)
                && rowText.Contains("Hazirlaniyor", StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        return -1;
    }

    public async Task OpenGeneralConditionDetailByRowIndexAsync(int rowIndex)
    {
        var frame = await GetContractEditFrameAsync();
        var rows = GetGeneralConditionRows(frame);
        var rowCount = await rows.CountAsync();

        if (rowCount == 0)
        {
            throw new Exception("General condition grid is empty");
        }

        if (rowIndex < 0 || rowIndex >= rowCount)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex), $"General condition row index {rowIndex} is out of range. Total rows: {rowCount}");
        }

        var targetRow = rows.Nth(rowIndex);
        var popupFrameCountBefore = await GetSeturPopupFrameCountAsync();

        var detailButton = targetRow.Locator("a.k-button.k-success, a.k-success, a.k-grid-edit, a:has-text('Detay'), button:has-text('Detay'), a.k-grid-view, a[title*='Detay']");
        if (await detailButton.CountAsync() > 0)
        {
            await detailButton.First.ClickAsync();
        }
        else
        {
            throw new Exception($"General condition detail opener button not found on row index {rowIndex}");
        }

        var popupOpened = await WaitForPopupFrameIncreaseAsync(popupFrameCountBefore, 10000);
        popupOpened.Should().BeTrue("Genel kondisyon detay popup'ı açılmalıdır");

        await Page.WaitForTimeoutAsync(1500);
        Console.WriteLine($"✅ Opened general condition detail for row index: {rowIndex}");
    }

    public async Task VerifyGeneralConditionRowIsPendingApprovalAsync(int rowIndex)
    {
        var frame = await GetContractEditFrameAsync();
        var rows = GetGeneralConditionRows(frame);
        var rowCount = await rows.CountAsync();

        if (rowIndex < 0 || rowIndex >= rowCount)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex), $"General condition row index {rowIndex} is out of range. Total rows: {rowCount}");
        }

        var rowText = await rows.Nth(rowIndex).TextContentAsync();
        var normalized = (rowText ?? string.Empty).ToLowerInvariant();
        var isPendingApproval = normalized.Contains("onay") && normalized.Contains("bekleniyor");

        isPendingApproval.Should().BeTrue($"General condition row at index {rowIndex} should be 'Onay Bekleniyor'. Actual row text: {rowText}");
        Console.WriteLine($"✅ General condition row {rowIndex} status is 'Onay Bekleniyor'");
    }

    public async Task<string> GetGeneralConditionNoByRowIndexAsync(int rowIndex)
    {
        var frame = await GetContractEditFrameAsync();
        var rows = GetGeneralConditionRows(frame);
        var rowCount = await rows.CountAsync();

        if (rowIndex < 0 || rowIndex >= rowCount)
        {
            throw new ArgumentOutOfRangeException(nameof(rowIndex), $"General condition row index {rowIndex} is out of range. Total rows: {rowCount}");
        }

        var row = rows.Nth(rowIndex);
        var noCellCandidates = row.Locator("td[data-field*='No'], td[data-field-name*='No'], td[aria-describedby*='No']");

        string? generalConditionNo = null;
        var candidateCount = await noCellCandidates.CountAsync();
        for (int i = 0; i < candidateCount; i++)
        {
            var text = (await noCellCandidates.Nth(i).InnerTextAsync())?.Trim();
            if (!string.IsNullOrWhiteSpace(text))
            {
                generalConditionNo = text;
                break;
            }
        }

        if (string.IsNullOrWhiteSpace(generalConditionNo))
        {
            var rowText = (await row.InnerTextAsync())?.Trim();
            if (!string.IsNullOrWhiteSpace(rowText))
            {
                var match = Regex.Match(rowText, @"\b\d{4,}\b");
                if (match.Success)
                {
                    generalConditionNo = match.Value;
                }
            }
        }

        if (string.IsNullOrWhiteSpace(generalConditionNo))
        {
            var rowDataUid = await row.GetAttributeAsync("data-uid");
            generalConditionNo = rowDataUid;
        }

        if (string.IsNullOrWhiteSpace(generalConditionNo))
        {
            throw new Exception($"General condition number not found for row index {rowIndex}");
        }

        Console.WriteLine($"✅ Captured general condition no: {generalConditionNo}");
        return generalConditionNo;
    }

    public async Task VerifyGeneralConditionStatusByNoOnGridAsync(string generalConditionNo, string expectedStatus, int timeoutMs = 20000)
    {
        var deadline = DateTime.UtcNow.AddMilliseconds(timeoutMs);

        while (DateTime.UtcNow < deadline)
        {
            var frame = await GetContractEditFrameAsync();
            var rows = GetGeneralConditionRows(frame);
            var rowCount = await rows.CountAsync();

            for (int i = 0; i < rowCount; i++)
            {
                var row = rows.Nth(i);
                var rowText = (await row.InnerTextAsync()) ?? string.Empty;
                var rowDataUid = await row.GetAttributeAsync("data-uid");
                var matchesIdentifier = rowText.Contains(generalConditionNo, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(rowDataUid, generalConditionNo, StringComparison.OrdinalIgnoreCase);

                if (!matchesIdentifier)
                {
                    continue;
                }

                var hasExpectedStatus = rowText.Contains(expectedStatus, StringComparison.OrdinalIgnoreCase)
                    || (expectedStatus.Equals("Onay Bekleniyor", StringComparison.OrdinalIgnoreCase)
                        && rowText.Contains("Onay Bekleniyor", StringComparison.OrdinalIgnoreCase));

                if (hasExpectedStatus)
                {
                    Console.WriteLine($"✅ General condition '{generalConditionNo}' status on grid verified: {expectedStatus}");
                    return;
                }

                throw new Exception($"General condition '{generalConditionNo}' found but status is not '{expectedStatus}'. Row text: {rowText}");
            }

            await Page.WaitForTimeoutAsync(1000);
        }

        throw new Exception($"General condition '{generalConditionNo}' with status '{expectedStatus}' not found on grid within timeout");
    }
}
