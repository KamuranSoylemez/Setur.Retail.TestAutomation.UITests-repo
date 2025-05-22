package pages;

import utils.ConfigDataReader;

public class GlobalPage extends BasePage{

    public void navigateToHomePage() {
        //page.navigate("https://dfs-retail-ui-staging.azurewebsites.net/CustomerManagement/Login");
        page.navigate(ConfigDataReader.getConfig("baseUrl"));
    }
}
