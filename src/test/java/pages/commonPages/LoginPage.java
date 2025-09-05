package pages.commonPages;

import com.microsoft.playwright.Locator;
import org.junit.Assert;
import utils.UserDataReader;

public class LoginPage extends BasePage {

    Locator userNameLocator = page.locator("#UserName");
    Locator passwordLocator = page.locator("#Password");
    Locator loginButton = page.locator("#submit");
    Locator warningMessage = page.locator(".ajs-message.ajs-error.ajs-visible");
    Locator errorMessage = page.locator(".ajs-message.ajs-error.ajs-visible");

    String usernameClass = userNameLocator.getAttribute("class");
    String passwordClass = passwordLocator.getAttribute("class");

    /**
     * login sayfasında kullanıcı bilgilerini girer
     */
    public void fillUserNameAndPassword() {
        userNameLocator.fill(UserDataReader.getUsername());
        passwordLocator.fill(UserDataReader.getPassword());
    }

    /**
     * Giriş Yap butonuna tıklar
     */
    public void clickLoginButton() {
        clickElement(loginButton);
    }

    /**
     * Farklı kullanıcı ve şifre ile giriş yapar
     */
    public void tryLoginWithUserAndPass(String user, String pass) {
        userNameLocator.fill(user);
        passwordLocator.fill(pass);
    }


    /**
     * Role özel kullanıcı ile giriş yapar
     */    public void loginAsSpecialUser(String user, String pass) {
        userNameLocator.fill(user);
        passwordLocator.fill(pass);
    }

    /**
     * Hatalı girişleri verify eder
     */
    public void verifyUnsuccessfulLogin(){
        if (!warningMessage.isVisible()){
            verifyValidationErrors();
        }else {
            verifyWarningMessageVisible();
        }
    }
    /**
     * Kullanıcı adı veya şifre alanlarından biri boş ise durumlarını kontrol eder
     */
    private void verifyValidationErrors() {

        if (hasValidationError(usernameClass)){
            Assert.assertTrue("Kullanıcı adı zorunlu alan!", true);
        }if (hasValidationError(passwordClass)) {
            Assert.assertTrue("Şifre zorunlu alan!", true);
        }
    }
    /**
     * Kullanıcı adı ve şifre ikisi de hatalı ise
     */
    private void verifyWarningMessageVisible() {
        Assert.assertTrue("Hatalı kullanıcı adı veya şifre!", warningMessage.isVisible());
        verifyTextElement("InvalidUserCodeOrPassword",errorMessage);
    }
    /**
     * Utility: sınıf adında hata var mı?
     */
    private boolean hasValidationError(String className) {
        return className != null && className.contains("input-validation-error");
    }

}

