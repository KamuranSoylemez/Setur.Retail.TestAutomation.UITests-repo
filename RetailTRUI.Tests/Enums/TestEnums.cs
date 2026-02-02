namespace RetailTRUI.Tests.Enums;

public enum ProductExcelType
{
    ProductDefinition,
    ProductUpdate
}

public enum Categories
{
    PARFUM_KOZMETIK,
    GIDA,
    TUTUN_URUNLERI,
    BUTIK_AKSESUAR,
    ICKI,
    OYUNCAK,
    BAZAAR,
    ELEKTRONIK,
    POSET,
    ESANTIYON
}

public static class CategoriesExtensions
{
    private static readonly Dictionary<Categories, (string Label, DistributorInfo Info)> CategoryData = new()
    {
        { Categories.PARFUM_KOZMETIK, ("PARFÜM-KOZMETİK", DistributorInfo.PARFUM_KOZMETIK) },
        { Categories.GIDA, ("GIDA", DistributorInfo.GIDA) },
        { Categories.TUTUN_URUNLERI, ("TÜTÜN ÜRÜNLERİ", DistributorInfo.TUTUN_URUNLERI) },
        { Categories.BUTIK_AKSESUAR, ("BUTİK-AKSESUAR", DistributorInfo.BUTIK_AKSESUAR) },
        { Categories.ICKI, ("İÇKİ", DistributorInfo.ICKI) },
        { Categories.OYUNCAK, ("OYUNCAK", DistributorInfo.OYUNCAK) },
        { Categories.BAZAAR, ("BAZAAR", DistributorInfo.BAZAAR) },
        { Categories.ELEKTRONIK, ("ELEKTRONİK", DistributorInfo.ELEKTRONIK) },
        { Categories.POSET, ("POŞET", DistributorInfo.POSET) },
        { Categories.ESANTIYON, ("EŞANTİYON", DistributorInfo.ESANTIYON) }
    };

    public static string GetLabel(this Categories category) => CategoryData[category].Label;
    
    public static DistributorInfo GetDistributorInfo(this Categories category) => CategoryData[category].Info;
    
    public static Categories? FromLabel(string label)
    {
        foreach (var kvp in CategoryData)
        {
            if (string.Equals(kvp.Value.Label, label, StringComparison.OrdinalIgnoreCase))
                return kvp.Key;
        }
        return null;
    }
}

public enum DistributorInfo
{
    PARFUM_KOZMETIK,
    GIDA,
    TUTUN_URUNLERI,
    BUTIK_AKSESUAR,
    ICKI,
    OYUNCAK,
    BAZAAR,
    ELEKTRONIK,
    POSET,
    ESANTIYON
}

public static class DistributorInfoExtensions
{
    private static readonly Dictionary<DistributorInfo, (string FirmCode, string FirmName)> DistributorData = new()
    {
        { DistributorInfo.PARFUM_KOZMETIK, ("CDR", "CHRISTIAN DIOR") },
        { DistributorInfo.GIDA, ("MIGRS", "MİGROS TİCARET A.Ş.") },
        { DistributorInfo.TUTUN_URUNLERI, ("PMI", "PHILIP MORRIS INTERNATIONAL") },
        { DistributorInfo.BUTIK_AKSESUAR, ("FOS", "FOSSIL") },
        { DistributorInfo.ICKI, ("BACARDI", "BACARDI GMBH") },
        { DistributorInfo.OYUNCAK, ("SUNMAN", "SUNMAN") },
        { DistributorInfo.BAZAAR, ("DİVAN", "DİVAN") },
        { DistributorInfo.ELEKTRONIK, ("ARZUM", "ARZUM") },
        { DistributorInfo.POSET, ("20018055", "ARCE PLASTİK") },
        { DistributorInfo.ESANTIYON, ("MUMA", "MUMAY ÇANTA") }
    };

    public static string GetFirmCode(this DistributorInfo info) => DistributorData[info].FirmCode;
    
    public static string GetFirmName(this DistributorInfo info) => DistributorData[info].FirmName;
}
