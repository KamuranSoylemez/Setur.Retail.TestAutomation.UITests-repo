package pages.commonPages;

import com.microsoft.playwright.Locator;
import utils.UserDataReader;

public class LoginPage extends BasePage {

    Locator userNameLocator = page.locator("#UserName");
    Locator passwordLocator = page.locator("#Password");
    Locator loginButton = page.locator("#submit");

    public void fillUserNameAndPassword(String userName, String password) {

        userNameLocator.fill(UserDataReader.getUsername(userName));
        passwordLocator.fill(UserDataReader.getPassword(password));

    }

    public void clickLoginButton() {
        clickElement(loginButton);
    }

}
