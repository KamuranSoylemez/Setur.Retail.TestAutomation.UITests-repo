package pages.commonPages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import com.microsoft.playwright.options.AriaRole;
import org.junit.Assert;
import utils.Driver;
import utils.GlobalVariables;

import java.util.Random;

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

    public FrameLocator getFrameByDialogTitle(String dialogTitle) {
        return page
                .getByRole(AriaRole.DIALOG, new Page.GetByRoleOptions().setName(dialogTitle))
                .frameLocator("iframe[title='Setur']");
    }
    //kendo component özelliğinden dolayı input girişi için kod
    public void setKendoNumericTextBoxValue(FrameLocator frame, String inputSelector, String value) {
        Locator input = frame.locator(inputSelector);
        input.evaluate("(el, val) => {" +
                "  const widget = $(el).data('kendoNumericTextBox');" +
                "  if (widget) {" +
                "    widget.value(val);" +
                "    widget.trigger('change');" +  // veya widget.trigger('input');
                "  }" +
                "}", value);
    }

    public int generateRandomNumber(){
        Random random = new Random();
        return random.nextInt(1000);
    }
}


