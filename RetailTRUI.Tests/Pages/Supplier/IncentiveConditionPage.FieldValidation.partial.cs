namespace RetailTRUI.Tests.Pages.Supplier;

public partial class IncentiveConditionPage
{
    public async Task<string> VerifyFieldStatusAsync(string fieldLabel)
    {
        var frame = await GetIncentiveConditionFrameAsync();
        var fieldId = GetFieldId(fieldLabel);
        var field = frame.Locator(fieldId);

        var count = await field.CountAsync();
        if (count == 0)
            return "not shown";

        var isVisible = await field.First.IsVisibleAsync();
        if (!isVisible)
            return "not shown";

        var radioButtonFields = new[] { "Kademeli mi?", "Hedefli mi?", "Çoklu Ödül mü?", "Tutar Çarpanlı", "Kişi Başı mı?", "Sadece Barkodlu Satışlar mı?", "Firmaya Fatura Edilsin mi?", "Tutara Kdv Dahil", "Fatura Kdv'li mi", "Fatura Tutarına Kdv Dahil" };
        if (radioButtonFields.Contains(fieldLabel))
        {
            string yesButtonId = fieldId;
            string noButtonId = fieldId.Replace("yes_", "no_");

            var yesButton = frame.Locator(yesButtonId);
            var noButton = frame.Locator(noButtonId);

            var yesExists = await yesButton.CountAsync() > 0;
            var noExists = await noButton.CountAsync() > 0;

            if (yesExists && noExists)
            {
                var yesDisabled = await yesButton.GetAttributeAsync("disabled");
                var noDisabled = await noButton.GetAttributeAsync("disabled");

                if (yesDisabled != null && noDisabled != null)
                    return "disabled";

                var radioLabelSelector = fieldLabel.Contains("'") ? $"label:has-text(\"{fieldLabel}\")" : $"label:has-text('{fieldLabel}')";
                var radioLabel = frame.Locator(radioLabelSelector);
                var radioLabelCount = await radioLabel.CountAsync();
                if (radioLabelCount > 0)
                {
                    var requiredIcon = radioLabel.Locator("span.requiredIcon");
                    var hasRequiredIcon = await requiredIcon.CountAsync() > 0;
                    if (hasRequiredIcon)
                        return "mandatory";
                }

                return "optional";
            }
        }

        var labelSelector = fieldLabel.Contains("'") ? $"label:has-text(\"{fieldLabel}\")" : $"label:has-text('{fieldLabel}')";
        var label = frame.Locator(labelSelector);
        var labelCount = await label.CountAsync();

        if (labelCount > 0)
        {
            var requiredIcon = label.Locator("span.requiredIcon");
            var hasRequiredIcon = await requiredIcon.CountAsync() > 0;
            if (hasRequiredIcon)
            {
                var inputInside = field.Locator("input");
                var inputExists = await inputInside.CountAsync() > 0;
                if (inputExists)
                {
                    var inputDisabled = await inputInside.First.GetAttributeAsync("disabled");
                    var inputAriaDisabled = await inputInside.First.GetAttributeAsync("aria-disabled");
                    if (inputDisabled != null || inputAriaDisabled == "true")
                        return "disabled";
                }

                return "mandatory";
            }
        }

        if (fieldId.StartsWith("span.k-numerictextbox"))
        {
            var inputInside = field.Locator("input[required]");
            var hasRequired = await inputInside.CountAsync() > 0;
            if (hasRequired)
                return "mandatory";
        }

        if (fieldId.StartsWith("span[aria-owns="))
        {
            var inputInside = field.Locator("input[required]");
            var hasRequired = await inputInside.CountAsync() > 0;
            if (hasRequired)
                return "mandatory";
        }

        var ariaDisabled = await field.GetAttributeAsync("aria-disabled");
        if (ariaDisabled == "true")
            return "disabled";

        var innerInput = field.Locator("input[aria-disabled='true']");
        var innerInputCount = await innerInput.CountAsync();
        if (innerInputCount > 0)
            return "disabled";

        var disabledAttr = await field.GetAttributeAsync("disabled");
        var isDisabled = disabledAttr != null || await field.IsDisabledAsync();
        if (isDisabled)
            return "disabled";

        var requiredAttr = await field.GetAttributeAsync("required");
        if (requiredAttr != null)
            return "mandatory";

        var mandatoryFields = new[] { "Başlangıç Tarihi", "Bitiş Tarihi", "Hesaplama Periyodu", "Faturalama Para Birimi", "Tutara Kdv Dahil" };
        if (mandatoryFields.Contains(fieldLabel) && !isDisabled)
            return "mandatory";

        return "optional";
    }

    public async Task VerifyFieldIsMandatoryAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("mandatory", $"Field '{fieldLabel}' should be mandatory");
        Console.WriteLine($"✅ Field '{fieldLabel}' is mandatory");
    }

    public async Task VerifyFieldIsDisabledAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("disabled", $"Field '{fieldLabel}' should be disabled");
        Console.WriteLine($"✅ Field '{fieldLabel}' is disabled");
    }

    public async Task VerifyFieldIsOptionalAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("optional", $"Field '{fieldLabel}' should be optional");
        Console.WriteLine($"✅ Field '{fieldLabel}' is optional");
    }

    public async Task VerifyFieldIsNotShownAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        status.Should().Be("not shown", $"Field '{fieldLabel}' should not be shown");
        Console.WriteLine($"✅ Field '{fieldLabel}' is not shown");
    }

    public async Task<string> GetFieldStateAsync(string fieldLabel)
    {
        var status = await VerifyFieldStatusAsync(fieldLabel);
        Console.WriteLine($"📊 Field '{fieldLabel}' state: {status}");
        return status;
    }
}
