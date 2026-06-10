using System.IO;
using System.Text;

namespace RetailTRUI.Tests.Pages.Supplier;

public partial class IncentiveConditionPage
{
    public async Task DiscoverAllElementsAsync()
    {
        var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "incentive_elements_discovery.txt");
        using var writer = new StreamWriter(reportPath, false);

        var report = new StringBuilder();
        report.AppendLine("\n\n" + new string('=', 120));
        report.AppendLine("🔎 INCENTIVE KONDISYON FORMU - ELEMENT KEŞİF RAPORU");
        report.AppendLine(new string('=', 120));

        var frame = await GetIncentiveConditionFrameAsync();

        var allInputs = await frame.Locator("input, select, textarea").AllAsync();
        report.AppendLine($"\n📋 TOPLAM INPUT/SELECT ELEMENTLER: {allInputs.Count}\n");

        int inputIndex = 1;
        foreach (var input in allInputs)
        {
            try
            {
                var tagName = await input.EvaluateAsync<string>("el => el.tagName");
                var type = await input.GetAttributeAsync("type");
                var id = await input.GetAttributeAsync("id");
                var name = await input.GetAttributeAsync("name");
                var placeholder = await input.GetAttributeAsync("placeholder");
                var disabled = await input.IsDisabledAsync();
                var required = await input.GetAttributeAsync("required");
                var className = await input.GetAttributeAsync("class");
                var dataRole = await input.GetAttributeAsync("data-role");
                var ariaLabel = await input.GetAttributeAsync("aria-label");

                var labelText = "N/A";
                if (id != null)
                {
                    var labelCount = await frame.Locator($"label[for='{id}']").CountAsync();
                    if (labelCount > 0)
                    labelText = (await frame.Locator($"label[for='{id}']").First.TextContentAsync()) ?? "N/A";
                }

                report.AppendLine($"{inputIndex}. {tagName}");
                if (id != null) report.AppendLine($"   ID: {id}");
                if (name != null) report.AppendLine($"   Name: {name}");
                if (!string.IsNullOrEmpty(type)) report.AppendLine($"   Type: {type}");
                if (labelText != "N/A") report.AppendLine($"   Label: {labelText.Trim()}");
                if (placeholder != null) report.AppendLine($"   Placeholder: {placeholder}");
                if (dataRole != null) report.AppendLine($"   Data-Role: {dataRole}");
                if (ariaLabel != null) report.AppendLine($"   Aria-Label: {ariaLabel}");
                if (className != null && className.Contains("k-")) report.AppendLine($"   Class: {className}");
                if (disabled) report.AppendLine("   ⚠️ STATUS: DISABLED");
                if (required != null) report.AppendLine("   ⭐ STATUS: REQUIRED");
                report.AppendLine();
                inputIndex++;
            }
            catch (Exception ex)
            {
                report.AppendLine($"   ⚠️ Error: {ex.Message}\n");
            }
        }

        report.AppendLine("\n📊 KENDO DROPDOWN/COMBOBOX ELEMENTS:\n");
        var kendoInputs = await frame.Locator(".k-dropdown, .k-combobox, [data-role='dropdownlist'], [data-role='combobox']").AllAsync();
        report.AppendLine($"Toplam: {kendoInputs.Count}\n");

        int kendoIndex = 1;
        foreach (var element in kendoInputs)
        {
            try
            {
                var id = await element.GetAttributeAsync("id");
                var className = await element.GetAttributeAsync("class");
                var dataRole = await element.GetAttributeAsync("data-role");
                var ariaOwns = await element.GetAttributeAsync("aria-owns");

                report.AppendLine($"{kendoIndex}. Kendo Element");
                if (id != null) report.AppendLine($"   ID: {id}");
                if (className != null) report.AppendLine($"   Class: {className}");
                if (dataRole != null) report.AppendLine($"   Data-Role: {dataRole}");
                if (ariaOwns != null) report.AppendLine($"   Aria-Owns: {ariaOwns}");
                report.AppendLine();
                kendoIndex++;
            }
            catch
            {
            }
        }

        report.AppendLine("\n✓ RADIO BUTTON ELEMENTLER:\n");
        var radios = await frame.Locator("input[type='radio']").AllAsync();
        report.AppendLine($"Toplam: {radios.Count}\n");

        int radioIndex = 1;
        foreach (var radio in radios)
        {
            try
            {
                var id = await radio.GetAttributeAsync("id");
                var name = await radio.GetAttributeAsync("name");
                var value = await radio.GetAttributeAsync("value");

                report.AppendLine($"{radioIndex}. Radio Button");
                if (id != null) report.AppendLine($"   ID: {id}");
                if (name != null) report.AppendLine($"   Name: {name}");
                if (value != null) report.AppendLine($"   Value: {value}");
                report.AppendLine();
                radioIndex++;
            }
            catch
            {
            }
        }

        report.AppendLine(new string('=', 120));
        report.AppendLine("🔍 DISCOVERY TAMAMLANDI\n");

        var content = report.ToString();
        await writer.WriteAsync(content);
        await writer.FlushAsync();

        Console.WriteLine(content);
        Console.WriteLine($"\n📄 Rapor kaydedildi: {reportPath}\n");
    }
}
