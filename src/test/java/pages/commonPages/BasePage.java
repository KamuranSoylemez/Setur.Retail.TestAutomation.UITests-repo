package pages.commonPages;

import com.microsoft.playwright.*;
import com.microsoft.playwright.options.AriaRole;
import com.microsoft.playwright.options.WaitForSelectorState;
import org.junit.Assert;
import utils.Driver;
import utils.GlobalVariables;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.Arrays;
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

    /**
     * text elementi doğrular.
     * @param value string değeri
     * @param locator locator değeri
     */
    public void verifyTextElement(String value, Locator locator){
        Assert.assertEquals(value, locator.textContent());
    }

    /**
     * trim kullanarak text elementi doğrular.
     * @param value text değeri
     * @param locator locator içindeki text değeri
     */
    public void verifyTextElementUseTrim(String value, Locator locator){
        Assert.assertEquals(value.trim(), locator.textContent().trim());
    }

    /**
     * elementin görünür olmasını kontrol eder.
     * @param locator elementi
     */
    public void verifyIsVisible(Locator locator){
        Assert.assertTrue(locator.isVisible());
    }

    /**
     * gereken değeri başka yerde kullanmak için saklar.
     * @param key string değer kendimiz veririz.
     * @param value locator değerini alır.
     */
    public void addString(String key, String value){
        GlobalVariables.getInstance().addString(key,value);
    }

    /**
     * saklanan string değeri kullanmak için getirir.
     * @param key addString metodunda kendimizin yazdığı değeri karşılar
     * @return değeri döner
     */
    public String getString(String key){
        return GlobalVariables.getInstance().getString(key);
    }

    /**
     * frame içinde işlem yapmaya yarar.
     * @param dialogTitle frame ismi
     * @return frame değerini döner
     */
    public FrameLocator getFrameByDialogTitle(String dialogTitle) {
        return page
                .getByRole(AriaRole.DIALOG, new Page.GetByRoleOptions().setName(dialogTitle))
                .frameLocator("iframe[title='Setur']");
    }

    /**
     * Kendo komponenti kullanıldığı için gereken yerlerde kullanılır.
     * @param frameLocator hangi frame ise
     * @param inputSelector hangi locator ise
     * @param value locatora girilecek değer
     */
    public void setKendoNumericTextBoxValue(FrameLocator frameLocator, String inputSelector, String value) {
        Locator input = frameLocator.locator(inputSelector);
        input.evaluate("(el, val) => {" +
                "  const widget = $(el).data('kendoNumericTextBox');" +
                "  if (widget) {" +
                "    widget.value(val);" +
                "    widget.trigger('change');" +  // veya widget.trigger('input');
                "  }" +
                "}", value);
    }

    public void setKendoNumericTextBoxValue1(Locator input, String value) {
        input.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.ATTACHED));

        ElementHandle inputHandle = input.elementHandle();
        if (inputHandle != null) {
            input.page().evaluate("(element, val) => {" +
                    "  const widget = $(element).data('kendoNumericTextBox');" +
                    "  if (widget) {" +
                    "    console.log('Widget bulundu, değer atanıyor');" +
                    "    widget.value(val);" +
                    "    widget.trigger('change');" +
                    "  } else {" +
                    "    console.warn('Kendo NumericTextBox bulunamadı');" +
                    "  }" +
                    "}", Arrays.asList(inputHandle, value));
        } else {
            System.out.println("Element bulunamadı veya görünmüyor.");
        }
    }



    public int generateRandomNumber(){
        Random random = new Random();
        return random.nextInt(10000);
    }
    /**
     * random tarih değeri girer
     * @return tarih değerini döner
     */
    public String generateRandomDate(){
        Random random = new Random();
        int daysToAdd = random.nextInt(30);
        LocalDate randomDate = LocalDate.now().plusDays(daysToAdd);
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd.MM.yyyy");
        return randomDate.format(formatter);
    }
    /**
     * barkod numarası üretir
     * @return string bir barkod no döner
     */
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
    /**
     * Gümrük Beyanname No random üretir.
     * @return Gümrük Beyanname No
     */
    public String generateCustomHouseNo() {
        Random random = new Random();
        String part1 = String.format("%08d", random.nextInt(100_000_000));
        String part2 = String.format("%08d", random.nextInt(100_000_000));
        return part1 + "AN" + part2;
    }
    /**
     * pop-up onaylama yapar. (frame içi hariç)
     */
    public void popUpConfirmationProcess(){
        Locator popup = page.locator(".ajs-dialog");
        popup.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));

        Locator okButton = popup.locator(".ajs-button.ajs-ok");
        okButton.click();
    }

    /**
     * pop-up vazgeçme işlemi yapar.
     */
    private void orderCancellationProcess(){
        Locator popup = page.locator(".ajs-dialog");
        popup.waitFor(new Locator.WaitForOptions().setState(WaitForSelectorState.VISIBLE));

        Locator cancelButton = popup.locator(".ajs-button.ajs-cancel");
        cancelButton.click();
    }
}


