package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.commonPages.LoginPage;

public class LoginStepdefs {

    LoginPage loginPage = new LoginPage();

    @When("fill username and password")
    public void fillUserNameAndPassword() {
        loginPage.fillUserNameAndPassword();
    }

    @And("click login button")
    public void clickLoginButton() {
        loginPage.clickLoginButton();
    }

    @When("try login with incorrect {string} or {string}")
    public void tryLoginWithIncorrectUserAndPass(String user, String pass) {
        loginPage.tryLoginWithUserAndPass(user,pass);
    }

    @Then("verify unsuccessful login")
    public void verifyUnsuccessfulLogin() {
        loginPage.verifyUnsuccessfulLogin();
    }


    @When("login as special user {string} {string}")
    public void loginAsSpecialUser(String user, String pass) {
    loginPage.loginAsSpecialUser(user,pass);
}

}
