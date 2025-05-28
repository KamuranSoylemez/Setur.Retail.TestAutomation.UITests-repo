package pages.purchasePages;

import com.microsoft.playwright.Locator;
import pages.commonPages.BasePage;


public class WorkflowInboxPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator purchaseDropdown = page.locator(".glyphicon.glyphicon-tags");
    Locator orderLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrder/Index']");

    public void verifySuccessfulLogin() {

        verifyTextElementUseTrim(pageTitle,"Akış Gelen Kutusu");
    }

    public void clickPurchaseDropdownToggle() {

        clickElement(purchaseDropdown);
    }

    public void clickOrderLink() {

        clickElement(orderLink);
    }


}
