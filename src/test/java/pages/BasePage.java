package pages;

import com.microsoft.playwright.Page;
import utils.Driver;

public class BasePage {

    protected Page page;

    public BasePage()
    {
        page = Driver.get();
    }
}
