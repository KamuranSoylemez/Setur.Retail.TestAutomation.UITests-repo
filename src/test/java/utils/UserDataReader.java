package utils;

import org.yaml.snakeyaml.LoaderOptions;
import org.yaml.snakeyaml.Yaml;
import org.yaml.snakeyaml.constructor.Constructor;

import java.io.FileInputStream;
import java.io.InputStream;
import java.util.Map;

public class UserDataReader {
    private static Map<String, Map<String, String>> users;

    static {
        String env = ConfigDataReader.getConfig("env");

        try {
            if (env == null) {
                env = "staging";
            }

            String credDir = System.getProperty("credentialsDir");
            if (credDir == null || env == null) {
                throw new RuntimeException("credentialsDir veya env property’si eksik!");
            }
            String path = credDir + "/" + env + ".users.yml";

            LoaderOptions loaderOptions = new LoaderOptions();
            Yaml yaml = new Yaml(new Constructor(loaderOptions));
            try (InputStream input = new FileInputStream(path)) {
                users = yaml.load(input);
            } catch (Exception e) {
                e.printStackTrace();
            }
        } catch (Exception e) {
            e.printStackTrace();

        }
    }

    public static String getUsername(String userType) { //normal
        return users.get(userType).get("username");
    }

    public static String getPassword(String userType) {
        return users.get(userType).get("password");
    }
}
