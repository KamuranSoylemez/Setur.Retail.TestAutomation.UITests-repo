package enums;

public enum DistributorInfo {
    PARFUM_KOZMETIK("CDR", "CHRISTIAN DIOR"),
    GIDA("MIGRS", "MİGROS TİCARET A.Ş."),
    TUTUN_URUNLERI("PMI", "PHILIP MORRIS INTERNATIONAL"),
    BUTIK_AKSESUAR("FOS", "FOSSIL"),
    ICKI("BACARDI", "BACARDI GMBH"),
    OYUNCAK("SUNMAN", "SUNMAN"),
    BAZAAR("DİVAN", "DİVAN"),
    ELEKTRONIK("ARZUM", "ARZUM"),
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

