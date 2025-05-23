package enums;

public enum Categories {

        PARFUM_KOZMETIK("PARFÜM-KOZMETİK"),
        GIDA("GIDA"),
        TOBACCO_PRODUCTS("TOBACCO PRODUCTS"),
        BUTIK_AKSESUAR("BUTİK-AKSESUAR"),
        SPIRITS("SPIRITS"),
        OYUNCAK("OYUNCAK"),
        BAZAAR("BAZAAR"),
        ELEKTRONIK("ELEKTRONİK"),
        POSET("POŞET"),
        ESANTIYON("EŞANTİYON");

        private final String label;

    Categories(String label) {
            this.label = label;
        }

        public String getLabel() {
            return label;
        }
    }
