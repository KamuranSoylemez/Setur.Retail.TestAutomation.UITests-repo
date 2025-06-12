package pages.purchasePages;

import com.microsoft.playwright.Locator;
import com.microsoft.playwright.Page;
import pages.commonPages.BasePage;


public class WorkflowInboxPage extends BasePage {

    Locator pageTitle = page.locator("#PageTitle");
    Locator purchaseDropdown = page.locator(".glyphicon.glyphicon-tags");
    Locator orderLink = page.locator("//a[@href='/ApplicationManagement/PurchaseOrder/Index']");

    public void verifySuccessfulLogin() {

        page.waitForSelector("#PageTitle", new Page.WaitForSelectorOptions().setTimeout(60000));
        verifyTextElementUseTrim(pageTitle,"Akış Gelen Kutusu");
    }

    public void clickPurchaseDropdownToggle() {

        page.waitForSelector(".glyphicon.glyphicon-refresh",
                new Page.WaitForSelectorOptions().setTimeout(60000));

        clickElement(purchaseDropdown);
    }

    public void clickOrderLink() {

        page.waitForSelector(".glyphicon.glyphicon-refresh",
                new Page.WaitForSelectorOptions().setTimeout(60000));

        clickElement(orderLink);
        page.waitForURL("https://dfs-retail-ui-staging.azurewebsites.net/ApplicationManagement/PurchaseOrder/Index",
                new Page.WaitForURLOptions().setTimeout(60000));
    }
}
