package pages;

import com.microsoft.playwright.Locator;
import org.junit.Assert;


public class WorkflowInboxPage extends BasePage{

    Locator pageTitle = page.locator("#PageTitle");
    Locator purchaseDropdown = page.locator(".glyphicon.glyphicon-tags");
    Locator orderLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrder/Index']");

    public void verifySuccessfulLogin() {

        //Assert.assertEquals("Akış Gelen Kutusu",pageTitle.textContent().trim());
        verifyTextElementUseTrim(pageTitle,"Akış Gelen Kutusu");
    }

    public void clickPurchaseDropdownToggle() {

        //purchaseDropdown.click();
        clickElement(purchaseDropdown);
    }

    public void clickOrderLink() {

        //orderLink.click();
        clickElement(orderLink);
    }


}
