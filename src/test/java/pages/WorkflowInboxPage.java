package pages;

import com.microsoft.playwright.Locator;
import org.junit.Assert;


public class WorkflowInboxPage extends BasePage{

    Locator pageTitle = page.locator("#PageTitle");
    Locator purchaseDropdown = page.locator(".glyphicon.glyphicon-tags");
    Locator orderLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrder/Index']");

    public void verifySuccessfulLogin() {

        Assert.assertEquals("Akış Gelen Kutusu",pageTitle.textContent().trim());
    }

    public void clickPurchaseDropdownToggle() {

        purchaseDropdown.click();
    }

    public void clickOrderLink() {

        orderLink.click();
    }


}
