package pages.commonPages;

import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import com.microsoft.playwright.options.AriaRole;
import com.microsoft.playwright.options.WaitForSelectorState;
import org.junit.Assert;
import utils.Driver;
import utils.GlobalVariables;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
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
        return random.nextInt(10000);
    }
    //random tarih üretme (aynı tarih için uyarı veriyor)
    public String generateRandomDate(){
        Random random = new Random();
        int daysToAdd = random.nextInt(30);
        LocalDate randomDate = LocalDate.now().plusDays(daysToAdd);
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd.MM.yyyy");
        return randomDate.format(formatter);
    }

    // 13 haneli barkod numarası üretme
    public String generateBarcodeNumber(){
        Random randomNum = new Random();
        StringBuilder sb = new StringBuilder();
        // İlk rakam 1–9 arasında olmalı (başta 0 olamaz)
        sb.append(randomNum.nextInt(9) + 1);
        // Geri kalan 12 rakam 0–9
        for (int i = 0; i < 12; i++) {
            sb.append(randomNum.nextInt(10));
        }
        return sb.toString();
    }
    // Random Gümrük Beyanname No üret (format: 8 rakam + AN + 8 rakam)
    public String generateCustomHouseNo() {
        Random random = new Random();
        String part1 = String.format("%08d", random.nextInt(100_000_000));
        String part2 = String.format("%08d", random.nextInt(100_000_000));
        return part1 + "AN" + part2;
    }
    // pop-up onayla işlemi (frame içindeki onaylamalar için geçerli değildir)
    public void popUpConfirmationProcess(){
        Locator popup = page.locator(".ajs-dialog");
        popup.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));

        Locator okButton = popup.locator(".ajs-button.ajs-ok");
        okButton.click();
    }
    // pop-up vazçgeç işlemi (frame içindeki onaylamalar için geçerli değildir)
    private void orderCancellationProcess(){
        Locator popup = page.locator(".ajs-dialog");
        popup.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));

        Locator cancelButton = popup.locator(".ajs-button.ajs-cancel");
        cancelButton.click();
    }
}


