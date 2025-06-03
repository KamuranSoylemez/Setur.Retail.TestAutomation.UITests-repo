package stepDefs;

import io.cucumber.java.en.And;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import pages.commonPages.LoginPage;

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
    @When("try login with {string} and {string}")
    public void tryLoginWithAnd(String user, String pass) {
        loginPage.tryLoginWithUserAndPass(user,pass);
    }

    @Then("verify unsuccessful login")
    public void verifyUnsuccessfulLogin() {
        loginPage.verifyUnsuccessfulLogin();
    }
}
