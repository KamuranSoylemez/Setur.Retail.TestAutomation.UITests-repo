package utils;

import java.util.HashMap;
import java.util.Map;

public class GlobalVariables {

    // Her yerden erişilebilecek tek bir instance
    private static final GlobalVariables instance = new GlobalVariables();

    // Saklanan string'ler
    private final Map<String, String> stringMap;

    // Constructor sadece bir kere çalışır
    private GlobalVariables() {
        stringMap = new HashMap<>();
    }

    // Singleton instance getter
    public static GlobalVariables getInstance() {
        return instance;
    }

    // Değer ekle
    public void addString(String key, String value) {
        System.out.println("SET: " + key + " = " + value); // Debug için
        stringMap.put(key, value);
    }

    // Değer al
    public String getString(String key) {
        String value = stringMap.get(key);
        System.out.println("GET: " + key + " = " + value); // Debug için
        return value;
    }
}
