package utils;

import com.microsoft.playwright.*;

import java.nio.file.Paths;
import java.util.List;
import java.util.Objects;

public class Driver {

    private static Playwright playwright;
    private static Browser browser;
    private static BrowserContext context;
    private static Page page;

    public static Page get() {

        if (page == null) {

            String browserType = ConfigDataReader.getConfig("browser");
            double slowMoValue = Double.parseDouble(ConfigDataReader.getConfig("slow_mo"));

            String executablePath = null;

            if (Objects.equals(browserType, "chrome")) {
                executablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
            } else if (Objects.equals(browserType, "edge")) {
                executablePath = "C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe";
            }

            if (executablePath != null) {
                playwright = Playwright.create();
                browser = playwright.chromium().launch(new BrowserType.LaunchOptions()
                        .setHeadless(false)
                        .setExecutablePath(Paths.get(executablePath))
                        .setArgs(List.of("--start-maximized"))
                        .setSlowMo(slowMoValue));

                context = browser.newContext(new Browser.NewContextOptions().setViewportSize(null));
                page = context.newPage();
            } else {
                System.out.println("Desteklenmeyen tarayıcı türü: " + browserType);
            }


            /*
            if(Objects.equals(browserType, "chrome")) {
            playwright = Playwright.create();
            browser = playwright.chromium().launch(new BrowserType.LaunchOptions()
                    .setHeadless(false)
                    .setArgs(List.of("--start-maximized"))
                    .setSlowMo(slowMoValue));

            context = browser.newContext(new Browser.NewContextOptions().setViewportSize(null));
            page = context.newPage();
            }
            */
        }
        return page;
    }

    public static void closeDriver() {
        if (page != null) {
            page.close();
            page = null;
        }
        if (context != null) {
            context.close();
            context = null;
        }
        if (browser != null) {
            browser.close();
            browser = null;
        }
        if (playwright != null) {
            playwright.close();
            playwright = null;
        }
    }
}
