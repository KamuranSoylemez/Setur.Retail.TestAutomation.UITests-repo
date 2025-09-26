package pages.SupplierPages;
import com.microsoft.playwright.FrameLocator;
import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import pages.commonPages.BasePage;


public class GeneralConditionPage extends BasePage {

    Locator descInput = page.locator("#Description");
    Locator brandInput = page.locator("div.k-widget.k-multiselect input.k-input.k-readonly");

    public void fillOutTheGeneralConditionFormAndSave(String brand, String desc) {
        // Açıklama alanını doldurur.
        descInput.fill(desc);
        // Marka alanını doldurur.
        brandInput.click();
        Locator brandOption = page.locator("li.k-item", new Page.LocatorOptions().setHasText(brand));
        brandOption.click();
    }

    public void clickToSaveButton() {
        // Kaydet butonuna tıklar.
        page.waitForSelector("k-content-frame");
        FrameLocator iframe = page.frameLocator("iframe[title='Setur']");
        iframe.locator("#SaveBtn").click();
    }







}
