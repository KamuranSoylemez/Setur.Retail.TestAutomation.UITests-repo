package utils;

import java.util.HashMap;
import java.util.Map;

public class GlobalVariables {
    private static GlobalVariables instance;
    private final Map<String, String> stringMap;

    private GlobalVariables() {
        stringMap = new HashMap<>();
    }


    public static GlobalVariables getInstance() {
        if (instance == null) {
            instance = new GlobalVariables();
        }
        return instance;
    }

    public void addString(String key, String value){
        stringMap.put(key,value);
    }

    public String getString(String key){
        return stringMap.get(key);
    }

}
