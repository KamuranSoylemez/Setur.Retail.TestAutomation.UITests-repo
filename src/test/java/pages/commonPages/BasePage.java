package pages.commonPages;

import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import org.junit.Assert;
import utils.Driver;
import utils.GlobalVariables;

public class BasePage {

    protected Page page;

    public BasePage()
    {
        page = Driver.get();
    }

    public void clickElement(Locator locator){
        locator.click();
    }

    public void verifyTextElement(String value, Locator locator){
        Assert.assertEquals(value, locator.textContent());
    }
    public void verifyTextElementUseTrim(String value, Locator locator){
        Assert.assertEquals(value.trim(), locator.textContent().trim());
    }
    public void verifyIsVisible(Locator locator){
        Assert.assertTrue(locator.isVisible());
    }

    public void addString(String key, String value){
        GlobalVariables.getInstance().addString(key,value);
    }
    public String getString(String key){
        return GlobalVariables.getInstance().getString(key);
    }

    public void pageScroll(){
        page.waitForTimeout(500);
        page.mouse().wheel(0, 10000); // Sayfayı aşağı kaydırır
    }
}


