using Microsoft.Playwright;

namespace RetailTRUI.Tests.Pages.Supplier;

public partial class ContractDefinitionPage
{
    public async Task ClickGeneralConditionTabAsync()
    {
        var frame = await GetContractEditFrameAsync();

        // Click on "Genel Kondisyon" tab - use the first exact match
        var generalConditionTab = GetContractEditTab(frame, "Genel Kondisyon");
        await generalConditionTab.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await generalConditionTab.ClickAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine("✅ Clicked General Condition tab");
    }

    public async Task ClickNewGeneralConditionButtonAsync()
    {
        var frame = await GetContractEditFrameAsync();

        // Find and click "Yeni Kayıt" button for GeneralCondition
        var newButton = GetNewGeneralConditionButton(frame);
        await newButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
        await newButton.ClickAsync();
        await Page.WaitForTimeoutAsync(2000);
        Console.WriteLine("✅ Clicked New General Condition button");
    }

    public async Task VerifyNewGeneralConditionButtonIsActiveAsync()
    {
        var frame = await GetContractEditFrameAsync();

        var newButton = GetNewGeneralConditionButton(frame);
        await newButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });

        string? classList = await newButton.First.GetAttributeAsync("class");
        bool isDisabled = classList != null && classList.Contains("k-state-disabled", StringComparison.OrdinalIgnoreCase);

        isDisabled.Should().BeFalse("Genel kondisyon 'Yeni Kayıt' butonu aktif olmalıdır");
        Console.WriteLine("✅ New General Condition button is active");
    }

    public async Task VerifyNewGeneralConditionButtonIsInactiveAsync()
    {
        var frame = await GetContractEditFrameAsync();

        var newButton = GetNewGeneralConditionButton(frame);
        var buttonCount = await newButton.CountAsync();

        if (buttonCount == 0)
        {
            Console.WriteLine("✅ New General Condition button is not available");
            return;
        }

        await newButton.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached, Timeout = 10000 });
        var isVisible = await newButton.First.IsVisibleAsync();

        isVisible.Should().BeFalse("Genel kondisyon 'Yeni Kayıt' butonu görünmemelidir");
        Console.WriteLine("✅ New General Condition button is not visible");
    }


    public async Task ClickSendForApprovalOnGeneralConditionDetailAsync()
    {
        var detailFrame = await GetTopMostPopupFrameAsync();
        var clickedSendForApproval = await detailFrame.EvaluateAsync<bool>(@"
            () => {
                const isVisible = (el) => {
                    if (!el) return false;
                    const style = window.getComputedStyle(el);
                    return style.display !== 'none' && style.visibility !== 'hidden' && el.offsetParent !== null;
                };

                const bySelectors = [
                    '#BtnSendApproval',
                    '#btnSendApproval',
                    '#ContractApprovalSend',
                    '#btnSendToApproval'
                ];

                for (const selector of bySelectors) {
                    const el = document.querySelector(selector);
                    if (isVisible(el)) {
                        el.click();
                        return true;
                    }
                }

                const candidates = Array.from(document.querySelectorAll('button, a'));
                for (const el of candidates) {
                    if (!isVisible(el)) continue;
                    const text = (el.textContent || '').trim().toLocaleLowerCase('tr-TR');
                    if (text === 'onaya gönder' || text === 'onaya gonder') {
                        el.click();
                        return true;
                    }
                }

                return false;
            }
        ");

        clickedSendForApproval.Should().BeTrue("Genel kondisyon detayında görünür bir 'Onaya Gönder' butonu bulunup tıklanmalıdır");
        await Page.WaitForTimeoutAsync(300);

        var confirmed = await ConfirmSendForApprovalDialogAsync(detailFrame, 8000);
        if (confirmed)
        {
            Console.WriteLine("✅ Confirmed 'Onaya Gönder' dialog (Onay clicked)");
        }
        else
        {
            Console.WriteLine("ℹ️ No confirmation dialog appeared after Onaya Gönder");
        }

        // Alertify tamamen kaybolana kadar bekle.
        await WaitForAlertifyToDisappearAsync(8000);

        Console.WriteLine("✅ Clicked 'Onaya Gönder' on general condition detail");
    }

    private async Task<bool> ConfirmSendForApprovalDialogAsync(IFrame detailFrame, int timeoutMs)
    {
        var deadline = DateTime.UtcNow.AddMilliseconds(timeoutMs);

        while (DateTime.UtcNow < deadline)
        {
            var clickedInDetailFrame = await detailFrame.EvaluateAsync<bool>(@"
                () => {
                    const isVisible = (el) => {
                        if (!el) return false;
                        const style = window.getComputedStyle(el);
                        return style.display !== 'none' && style.visibility !== 'hidden' && el.offsetParent !== null;
                    };

                    const dialogs = Array.from(document.querySelectorAll('.ajs-dialog')).filter(isVisible);
                    for (const dialog of dialogs) {
                        const content = ((dialog.querySelector('.ajs-content')?.textContent || '').trim()).toLocaleLowerCase('tr-TR');
                        const isSendForApprovalDialog =
                            (content.includes('onaya') && content.includes('emin')) ||
                            content.includes('kondisyonu onaya göndermek') ||
                            content.includes('kondisyonu onaya gondermek');

                        if (!isSendForApprovalDialog) continue;

                        const buttons = Array.from(dialog.querySelectorAll('button.ajs-button'));
                        const onayButton = buttons.find(btn => {
                            const text = (btn.textContent || '').trim().toLocaleLowerCase('tr-TR');
                            return text === 'onay' || text === 'evet' || text === 'ok';
                        });

                        if (onayButton) {
                            onayButton.click();
                            return true;
                        }
                    }

                    return false;
                }
            ");

            if (clickedInDetailFrame)
            {
                await Page.WaitForTimeoutAsync(500);
                return true;
            }

            var clickedInPage = await Page.EvaluateAsync<bool>(@"
                () => {
                    const isVisible = (el) => {
                        if (!el) return false;
                        const style = window.getComputedStyle(el);
                        return style.display !== 'none' && style.visibility !== 'hidden' && el.offsetParent !== null;
                    };

                    const dialogs = Array.from(document.querySelectorAll('.ajs-dialog')).filter(isVisible);
                    for (const dialog of dialogs) {
                        const content = ((dialog.querySelector('.ajs-content')?.textContent || '').trim()).toLocaleLowerCase('tr-TR');
                        const isSendForApprovalDialog =
                            (content.includes('onaya') && content.includes('emin')) ||
                            content.includes('kondisyonu onaya göndermek') ||
                            content.includes('kondisyonu onaya gondermek');

                        if (!isSendForApprovalDialog) continue;

                        const buttons = Array.from(dialog.querySelectorAll('button.ajs-button'));
                        const onayButton = buttons.find(btn => {
                            const text = (btn.textContent || '').trim().toLocaleLowerCase('tr-TR');
                            return text === 'onay' || text === 'evet' || text === 'ok';
                        });

                        if (onayButton) {
                            onayButton.click();
                            return true;
                        }
                    }

                    return false;
                }
            ");

            if (clickedInPage)
            {
                await Page.WaitForTimeoutAsync(500);
                return true;
            }

            await Page.WaitForTimeoutAsync(250);
        }

        return false;
    }

    public async Task CloseGeneralConditionDetailIfOpenAsync()
    {
        // Ensure any lingering alertify dialogs are dismissed before trying to close the popup.
        await DismissAlertifyDialogAsync();
        await WaitForAlertifyToDisappearAsync(3000);

        var popupFrameCount = await GetSeturPopupFrameCountAsync();
        if (popupFrameCount >= 2)
        {
            await CloseGeneralConditionDetailAsync();
        }
    }

}
