package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.When;
import pages.LoginPage;

public class LoginStepdefs {

    LoginPage loginPage = new LoginPage();

    @When("fill {string} and {string}")
    public void fillUserNameAndPassword(String userName, String password) {
        loginPage.fillUserNameAndPassword(userName,password);
    }

    @And("click login")
    public void clickLogin() {
        loginPage.clickLoginButton();
    }

}
