using Microsoft.Playwright;

namespace RetailTRUI.Tests.Pages.Supplier;

public partial class ContractDefinitionPage
{
    public async Task<string> GetGeneralConditionNoFromDetailAsync()
    {
        var detailFrame = await GetTopMostPopupFrameAsync();

        var noValue = await detailFrame.EvaluateAsync<string>(@"
            () => {
                const tryGetValue = (el) => {
                    if (!el) return '';
                    if ('value' in el && typeof el.value === 'string') return el.value.trim();
                    return (el.textContent || '').trim();
                };

                const candidates = [];

                document.querySelectorAll(`input[id*='No'], input[name*='No'], span[id*='No'], span[name*='No'], div[id*='No']`)
                    .forEach(el => candidates.push(tryGetValue(el)));

                document.querySelectorAll('label').forEach(label => {
                    const txt = (label.textContent || '').toLocaleLowerCase('tr-TR');
                    if (!txt.includes('no')) return;

                    const forId = label.getAttribute('for');
                    if (forId) {
                        const linked = document.getElementById(forId);
                        candidates.push(tryGetValue(linked));
                    }

                    const wrapperValue = label.parentElement?.querySelector('input, span, div');
                    candidates.push(tryGetValue(wrapperValue));
                });

                for (const raw of candidates) {
                    const value = (raw || '').trim();
                    if (!value) continue;
                    if (value.length < 2) continue;
                    if (value.toLocaleLowerCase('tr-TR').includes('hazırlanıyor')) continue;
                    if (value.toLocaleLowerCase('tr-TR').includes('onay')) continue;
                    return value;
                }

                return '';
            }
        ");

        if (string.IsNullOrWhiteSpace(noValue))
        {
            throw new Exception("General condition number not found on detail popup");
        }

        Console.WriteLine($"✅ Captured general condition no from detail: {noValue}");
        return noValue;
    }

    public async Task VerifyApprovalSuccessMessageIsDisplayedAsync(int timeoutMs = 10000)
    {
        var deadline = DateTime.UtcNow.AddMilliseconds(timeoutMs);

        while (DateTime.UtcNow < deadline)
        {
            var successInPage = await Page.Locator(".ajs-message.ajs-success.ajs-visible, div.alertify-success, .alert-success, .toast-success, .k-notification-success").CountAsync();
            if (successInPage > 0)
            {
                Console.WriteLine("✅ Approval success message is displayed (page)");
                return;
            }

            try
            {
                var detailFrame = await GetTopMostPopupFrameAsync();
                var successInDetail = await detailFrame.Locator(".ajs-message.ajs-success.ajs-visible, div.alertify-success, .alert-success, .toast-success, .k-notification-success").CountAsync();
                if (successInDetail > 0)
                {
                    Console.WriteLine("✅ Approval success message is displayed (detail frame)");
                    return;
                }
            }
            catch
            {
                // Popup may be transitioning; keep polling.
            }

            await Page.WaitForTimeoutAsync(250);
        }

        throw new Exception("Approval success message not found after 'Onaya Gönder'");
    }

    public async Task VerifyGeneralConditionApprovalButtonsAreNotVisibleAsync()
    {
        var detailFrame = await GetTopMostPopupFrameAsync();

        var sendForApprovalButton = detailFrame.Locator(
            "#BtnSendApproval, #btnSendApproval, #ContractApprovalSend, button:has-text('Onaya Gönder'), a:has-text('Onaya Gönder'), button:has-text('Onaya Gonder'), a:has-text('Onaya Gonder')");
        var approveButton = detailFrame.Locator("#BtnApprove, #btnApprove, button:has-text('Onayla'), a:has-text('Onayla')");

        var isSendForApprovalVisible = await IsAnyVisibleAsync(sendForApprovalButton);
        var isApproveVisible = await IsAnyVisibleAsync(approveButton);

        isSendForApprovalVisible.Should().BeFalse("Genel kondisyon detayında 'Onaya Gönder' butonu görünmemelidir");
        isApproveVisible.Should().BeFalse("Genel kondisyon detayında 'Onayla' butonu görünmemelidir");

        Console.WriteLine("✅ General condition detail has no 'Onaya Gönder' or 'Onayla' button");
    }

    public async Task CloseGeneralConditionDetailAsync()
    {
        var detailFrame = await GetTopMostPopupFrameAsync();
        var closeButton = detailFrame.Locator("#ClosePopupBtn, button:has-text('Kapat'), a:has-text('Kapat')");

        if (await closeButton.CountAsync() > 0)
        {
            await closeButton.First.ClickAsync();
            await Page.WaitForTimeoutAsync(1000);
            Console.WriteLine("✅ Closed general condition detail popup");
            return;
        }

        await Page.Keyboard.PressAsync("Escape");
        await Page.WaitForTimeoutAsync(1000);
        Console.WriteLine("✅ Closed detail popup with Escape");
    }

    private async Task<IFrame> GetTopMostPopupFrameAsync()
    {
        var iframeElements = await GetSeturPopupIframes().ElementHandlesAsync();
        if (iframeElements.Count < 2)
        {
            throw new Exception("General condition detail popup frame not found (still on contract frame)");
        }

        var topFrame = await iframeElements[iframeElements.Count - 1].ContentFrameAsync();
        if (topFrame == null)
        {
            throw new Exception("Top popup frame content is null");
        }

        return topFrame;
    }

    private async Task<int> GetSeturPopupFrameCountAsync()
    {
        var iframeElements = await GetSeturPopupIframes().ElementHandlesAsync();
        return iframeElements.Count;
    }

    private async Task<bool> WaitForPopupFrameIncreaseAsync(int previousCount, int timeoutMs)
    {
        var deadline = DateTime.UtcNow.AddMilliseconds(timeoutMs);

        while (DateTime.UtcNow < deadline)
        {
            var currentCount = await GetSeturPopupFrameCountAsync();
            if (currentCount > previousCount)
            {
                return true;
            }

            await Page.WaitForTimeoutAsync(250);
        }

        return false;
    }

    private async Task DismissAlertifyDialogAsync()
    {
        await DismissAlertifyDialogViaJsAsync(3000);
    }

    private async Task<bool> DismissAlertifyDialogViaJsAsync(int timeoutMs)
    {
        var deadline = DateTime.UtcNow.AddMilliseconds(timeoutMs);

        while (DateTime.UtcNow < deadline)
        {
            var clicked = await Page.EvaluateAsync<bool>(@"
                () => {
                    var buttons = document.querySelectorAll('.ajs-button');
                    for (var btn of buttons) {
                        var text = btn.textContent.trim();
                        if (text === 'Onay' || text === 'Evet' || text === 'OK') {
                            btn.click();
                            return true;
                        }
                    }
                    var ok = document.querySelector('.ajs-ok');
                    if (ok) { ok.click(); return true; }
                    return false;
                }
            ");

            if (clicked)
            {
                await Page.WaitForTimeoutAsync(500);
                return true;
            }

            await Page.WaitForTimeoutAsync(250);
        }

        return false;
    }

    public async Task VerifyGeneralConditionStatusInDetailIsAsync(string expectedStatus)
    {
        var detailFrame = await GetTopMostPopupFrameAsync();
        var statusInput = detailFrame.Locator("#ContractRebateStatus");

        await statusInput.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        var deadline = DateTime.UtcNow.AddSeconds(20);
        var actualStatus = await statusInput.InputValueAsync();

        while (DateTime.UtcNow < deadline)
        {
            if (string.Equals(actualStatus?.Trim(), expectedStatus, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"✅ General condition status in detail verified: {actualStatus}");
                return;
            }

            await Page.WaitForTimeoutAsync(1000);
            actualStatus = await statusInput.InputValueAsync();
        }

        actualStatus.Should().Be(expectedStatus,
            $"Genel Kondisyon Durumu '{expectedStatus}' olmalıdır fakat '{actualStatus}' görüldü");
    }

    private async Task WaitForAlertifyToDisappearAsync(int timeoutMs)
    {
        var deadline = DateTime.UtcNow.AddMilliseconds(timeoutMs);

        while (DateTime.UtcNow < deadline)
        {
            var hasModal = await Page.EvaluateAsync<bool>(@"
                () => {
                    var modal = document.querySelector('.ajs-modal');
                    if (!modal) return false;
                    var style = window.getComputedStyle(modal);
                    return style.display !== 'none' && style.visibility !== 'hidden' && modal.offsetParent !== null;
                }
            ");

            if (!hasModal)
            {
                return;
            }

            await Page.EvaluateAsync(@"
                () => {
                    var btn = document.querySelector('.ajs-ok');
                    if (btn) btn.click();
                }
            ");

            await Page.WaitForTimeoutAsync(250);
        }

        Console.WriteLine("⚠️ Alertify modal still visible after timeout - continuing anyway");
    }

    private static async Task<bool> IsAnyVisibleAsync(ILocator locator)
    {
        var count = await locator.CountAsync();
        for (int i = 0; i < count; i++)
        {
            if (await locator.Nth(i).IsVisibleAsync())
            {
                return true;
            }
        }

        return false;
    }
}
