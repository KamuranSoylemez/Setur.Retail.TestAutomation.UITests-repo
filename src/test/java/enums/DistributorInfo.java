package enums;

public enum DistributorInfo {
    PARFUM_KOZMETIK("CHN", "CHANEL PARFUMS"),
    GIDA("MIGRS", "MİGROS TİCARET A.Ş."),
    TOBACCO_PRODUCTS("JTI", "JAPAN TOBACCO INTERNATIONAL"),
    BUTIK_AKSESUAR("FOS", "FOSSIL"),
    SPIRITS("TUBORG", "TUBORG"),
    OYUNCAK("LEG", "LEGO"),
    BAZAAR("MPAZ", "MALATYA PAZARI"),
    ELEKTRONIK("CAPI", "CAPI"),
    POSET("20002560", "ARCE PLASTİK İÇ VE DIŞ TİC"),
    ESANTIYON("ÇNRTX", "ÇINARTEKS TAAHHÜT TURİZM VE AMBALAJ SAN.DIŞ TİC.LTD.ŞTİ.");

    private final String firmCode;
    private final String firmName;

    DistributorInfo(String firmCode, String firmName) {
        this.firmCode = firmCode;
        this.firmName = firmName;
    }

    public String getFirmCode() {
        return firmCode;
    }

    public String getFirmName() {
        return firmName;
    }

    public static DistributorInfo fromCategoryLabel(String label) {
        for (DistributorInfo info : values()) {
            if (info.name().equalsIgnoreCase(label.replace(" ", "_"))) {
                return info;
            }
        }
        return null;
    }
}

