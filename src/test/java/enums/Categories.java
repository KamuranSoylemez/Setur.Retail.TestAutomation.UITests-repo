package enums;

public enum Categories {
    PARFUM_KOZMETIK("PARFÜM-KOZMETİK", DistributorInfo.PARFUM_KOZMETIK),
    GIDA("GIDA", DistributorInfo.GIDA),
    TUTUN_URUNLERI("TÜTÜN ÜRÜNLERİ", DistributorInfo.TUTUN_URUNLERI),
    BUTIK_AKSESUAR("BUTİK-AKSESUAR", DistributorInfo.BUTIK_AKSESUAR),
    ICKI("İÇKİ", DistributorInfo.ICKI),
    OYUNCAK("OYUNCAK", DistributorInfo.OYUNCAK),
    BAZAAR("BAZAAR", DistributorInfo.BAZAAR),
    ELEKTRONIK("ELEKTRONİK", DistributorInfo.ELEKTRONIK),
    POSET("POŞET", DistributorInfo.POSET),
    ESANTIYON("EŞANTİYON", DistributorInfo.ESANTIYON);

    private final String label;
    private final DistributorInfo distributorInfo;

    Categories(String label, DistributorInfo distributorInfo) {
        this.label = label;
        this.distributorInfo = distributorInfo;
    }

    public String getLabel() {
        return label;
    }

    public DistributorInfo getDistributorInfo() {
        return distributorInfo;
    }

    /**
     * Verilen etiket (label) değerine karşılık gelen {@link Categories} enum sabitini döner.
     * Bu metod, bir string etiket ile eşleşen enum sabitini bulmak için
     * {@link Categories} enum sabitleri arasında case-insensitive (büyük/küçük harf duyarsız) karşılaştırma yapar.
     * @param label Aranan kategori etiketi (örneğin: "Elektronik", "Giyim" vb.)
     * @return Etiketle eşleşen {@link Categories} enum sabiti; eşleşme bulunamazsa {@code null} döner.
     */
    public static Categories fromLabel(String label) {
        for (Categories category : values()) {
            if (category.getLabel().equalsIgnoreCase(label)) {
                return category;
            }
        }
        return null;
    }
}

