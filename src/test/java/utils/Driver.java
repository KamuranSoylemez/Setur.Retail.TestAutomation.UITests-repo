package utils;

import com.microsoft.playwright.*;

import java.util.Arrays;
import java.util.Objects;

public class Driver {

    private static Playwright playwright;
    private static Browser browser;
    private static BrowserContext context;
    private static Page page;

    public static Page get() {

        if (page == null) {

            String browserType = ConfigDataReader.getConfig("browser");
            double slowMoValue = Double.valueOf(ConfigDataReader.getConfig("slow_mo"));

            if(Objects.equals(browserType, "chrome"))
            {


            playwright = Playwright.create();
            browser = playwright.chromium().launch(new BrowserType.LaunchOptions()
                    .setHeadless(false)
                    .setArgs(Arrays.asList("--start-maximized"))
                    .setSlowMo(slowMoValue));

            context = browser.newContext(new Browser.NewContextOptions().setViewportSize(null));
            page = context.newPage();
            }
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
