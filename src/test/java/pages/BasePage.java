package pages;

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

    public void verifyTextElement(Locator locator, String value){
        Assert.assertEquals(locator.textContent(),value);
    }
    public void verifyTextElementUseTrim(Locator locator, String value){
        Assert.assertEquals(locator.textContent().trim(),value);
    }
    public void addString(String key, String value){
        GlobalVariables.getInstance().addString(key,value);
    }
    public String getString(String key){
        return GlobalVariables.getInstance().getString(key);
    }
}


