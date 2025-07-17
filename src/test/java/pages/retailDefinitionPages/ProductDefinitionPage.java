package pages.retailDefinitionPages;

import com.microsoft.playwright.Locator;
import pages.commonPages.BasePage;

public class ProductDefinitionPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator newRecordButton = page.locator(".glyphicon.glyphicon-plus");

    public void verifyProductDefinitionPageIsDisplayed() {
        verifyTextElementUseTrim("Ürün Tanımlama", pageTitle);
    }
    /**
     * Yeni kayıt butonuna tıklar.
     */
    public void clickNewRecordButton() {
        clickElement(newRecordButton);
    }

}
