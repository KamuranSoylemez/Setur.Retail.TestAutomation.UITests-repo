package utils;

import java.io.FileInputStream;
import java.util.Properties;

public class ConfigDataReader {
    public static Properties properties;

    static {
        String env = System.getProperty("env");
        try {
            if (env == null) {
                env = "staging";
            }
            String path = "config/env/" + env + ".properties";
            FileInputStream input = new FileInputStream(path);
            properties = new Properties();
            properties.load(input);

            input.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static String getConfig(String keyName) {
        return properties.getProperty(keyName);
    }

}
