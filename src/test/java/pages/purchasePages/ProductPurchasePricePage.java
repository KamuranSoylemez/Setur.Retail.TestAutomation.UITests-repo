package pages.purchasePages;

import com.microsoft.playwright.Locator;
import pages.commonPages.BasePage;

public class ProductPurchasePricePage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator newRecord = page.locator(".glyphicon.glyphicon-plus");

    public void verifyPurchasePricePage() {
        verifyTextElementUseTrim("Satınalma Fiyatları",pageTitle);
    }

    public void newRecordPurchasePrice() {
        clickElement(newRecord);
    }
}
