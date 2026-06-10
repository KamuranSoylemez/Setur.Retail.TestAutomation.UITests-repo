namespace RetailTRUI.Tests.Pages.Supplier;

public partial class IncentiveConditionPage
{
    private static readonly string[] ConditionTypeDropdownCandidateIds =
    {
        "ContractRepresentativeTypeId",
        "ContractIncentiveTypeId",
        "IncentiveTypeId",
        "ConditionTypeId"
    };

    private static readonly string[] TargetTypeDropdownCandidateIds =
    {
        "ReckoningSourceId",
        "ContractRepresentativeIncentiveTargetTypeId",
        "ContractIncentiveTargetTypeId",
        "TargetTypeId",
        "HedefTipiId",
        "ReckoningTargetId"
    };

    private static readonly string[] IsGradientYesSelectorCandidates =
    {
        "#yes_IsGradual",
        "#yes_IsStair",
        "#yes_IsKademeli",
        "input[name='IsGradual'][value='true']",
        "input[name='IsStair'][value='true']"
    };

    private static readonly string[] IsTargetedYesSelectorCandidates =
    {
        "#yes_HasTarget",
        "#yes_IsTargeted",
        "#yes_IsTarget",
        "#yes_IsHedefli",
        "input[name='HasTarget'][value='true']",
        "input[name='IsTargeted'][value='true']"
    };

    private static readonly string[] IsMultipleRewardYesSelectorCandidates =
    {
        "#yes_HasMultipleReward",
        "#yes_IsMultiReward",
        "#yes_IsCokluOdul",
        "input[name='HasMultipleReward'][value='true']",
        "input[name='IsMultiReward'][value='true']"
    };

    private string GetDropdownId(string fieldLabel)
    {
        return fieldLabel switch
        {
            "Periyot" => "ContractRepresentativePeriodTypeId",
            "Faturalama Para Birimi" => "InvoiceCurrencyCode",
            _ => throw new NotImplementedException($"Dropdown id mapping not implemented for: {fieldLabel}")
        };
    }

    private string GetFieldId(string fieldName)
    {
        return fieldName switch
        {
            "Başlangıç Tarihi" => "#StartDate",
            "Bitiş Tarihi" => "#EndDate",
            "Hesaplama Periyodu" => "span[aria-owns='ContractRepresentativePeriodTypeId_listbox']",
            "Periyot" => "span[aria-owns='ContractRepresentativePeriodTypeId_listbox']",
            "Temel Ölçü Birimi" => "span[aria-owns='MainMeasureUnitId_listbox']",
            "Hesaplama Tutar Para Birimi" => "span[aria-owns='CalculationAmountCurrencyCode_listbox']",
            "İşlem Para Birimi" => "span[aria-owns='TargetRevenueCurrencyCode_listbox']",
            "Faturalama Para Birimi" => "span[aria-owns='InvoiceCurrencyCode_listbox']",
            "Net/Brüt" => "label:has-text('Net/Brüt')",
            "Kademeli mi?" => "#yes_IsGradual",
            "Hedefli mi?" => "#yes_HasTarget",
            "Çoklu Ödül mü?" => "#yes_HasMultipleReward",
            "Tutar Çarpanlı" => "#yes_HasMultiplier",
            "Kişi Başı mı?" => "#yes_IsPerPerson",
            "Sadece Barkodlu Satışlar mı?" => "#yes_IsOnlyBarcodeSales",
            "Firmaya Fatura Edilsin mi?" => "#yes_IsInvoicable",
            "Tutara Kdv Dahil" => "#yes_IsVatInclude",
            "Fatura Kdv'li mi" => "#yes_IsInvoiceVatInclude",
            "Fatura Tutarına Kdv Dahil" => "#yes_IsInvoiceVatInclude",
            "Hedef Ciro" => "span.k-numerictextbox:has(input#TargetRevenue)",
            "Hedef Miktar" => "span.k-numerictextbox:has(input#TargetUnit)",
            "Tutar" => "span.k-numerictextbox:has(input#RebateValue)",
            "Hesaplama Tutar" => "span.k-numerictextbox:has(input#RebateValue)",
            "Oran" => "span.k-numerictextbox:has(input#RebateRatio)",
            "Hesaplama Oran" => "span.k-numerictextbox:has(input#RebateRatio)",
            "Birim Çarpanı" => "span.k-numerictextbox:has(input#UnitMultiplier)",
            "Maksimum Kişi Sayısı" => "span.k-numerictextbox:has(input#MaxPersonCount)",
            "Marka" => "div.k-multiselect-wrap:has(ul#BrandIdArray_taglist)",
            "Açıklama" => "textarea[name='Description']",
            _ => $"label:has-text('{fieldName}')"
        };
    }
}