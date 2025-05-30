package pages.commonPages;

import com.microsoft.playwright.Locator;
import org.junit.Assert;
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

    public void tryLoginWithUseAndPass(String user, String pass) {
        userNameLocator.fill(user);
        passwordLocator.fill(pass);
    }
    public void verifyUnsuccessfulLogin() {
        Locator warningMessage = page.locator(".ajs-message.ajs-error.ajs-visible");

        if (!warningMessage.isVisible()){
            // input-validation-error sınıfı kontrolü
            String usernameClass = userNameLocator.getAttribute("class");
            String passwordClass = passwordLocator.getAttribute("class");

            Assert.assertTrue("Kullanıcı adı alanı doğrulama hatası içermeli",
                    usernameClass.contains("input-validation-error"));
            Assert.assertTrue("Şifre alanı doğrulama hatası içermeli",
                    passwordClass.contains("input-validation-error"));
            }
        else {
            Assert.assertTrue(warningMessage.isVisible());
        }
    }
}

