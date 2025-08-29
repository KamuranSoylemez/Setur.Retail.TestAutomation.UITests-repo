package enums;

public enum DistributorInfo {
    PARFUM_KOZMETIK("DPL", "LOREAL DE LUXE INT"),
    GIDA("MIGRS", "MİGROS TİCARET A.Ş."),
    TUTUN_URUNLERI("JTI", "JAPAN TOBACCO INTERNATIONAL"),
    BUTIK_AKSESUAR("FNR", "FENERIUM"),
    ICKI("TUBORG", "TUBORG"),
    OYUNCAK("LEG", "LEGO"),
    BAZAAR("MPAZ", "MALATYA PAZARI"),
    ELEKTRONIK("GENÇ", "GENÇ ELEKTRONİK"),
    POSET("20018055", "ARCE PLASTİK"),
    ESANTIYON("MUMA", "MUMAY ÇANTA");

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

