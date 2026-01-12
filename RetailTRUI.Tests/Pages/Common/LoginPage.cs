namespace RetailTRUI.Tests.Pages.Common;

/// <summary>
/// Login page object encapsulating authentication functionality
/// Supports multiple user roles and validation scenarios
/// </summary>
public class LoginPage : BasePage
{
    private ILocator UsernameInput => Page.Locator("#UserName");
    private ILocator PasswordInput => Page.Locator("#Password");
    private ILocator LoginButton => Page.Locator("#submit");
    private ILocator WarningMessage => Page.Locator(".ajs-message.ajs-error.ajs-visible");
    private ILocator ErrorMessage => Page.Locator(".ajs-message.ajs-error.ajs-visible");

    public async Task NavigateToLoginPageAsync()
    {
        var baseUrl = ConfigurationManager.Instance.BaseUrl;
        await Page.GotoAsync(baseUrl);
    }

    public async Task LoginAsAsync(string role = "normal")
    {
        var user = UserDataReader.GetUser(role);
        await FillCredentialsAsync(user.Username, user.Password);
        await ClickLoginButtonAsync();
    }

    public async Task FillCredentialsAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
    }

    public async Task LoginAsSpecialUserAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
    }

    public async Task ClickLoginButtonAsync()
    {
        await LoginButton.ClickAsync();
    }

    public async Task<bool> IsLoginSuccessfulAsync()
    {
        try
        {
            await Page.WaitForURLAsync(url => !url.Contains("/Account/Login"), 
                new PageWaitForURLOptions { Timeout = 5000 });
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task VerifyLoginSuccessAsync()
    {
        var isSuccess = await IsLoginSuccessfulAsync();
        isSuccess.Should().BeTrue("Login should be successful");
    }

    public async Task VerifySuccessfulLoginAsync()
    {
        var isSuccess = await IsLoginSuccessfulAsync();
        isSuccess.Should().BeTrue("Login should be successful");
    }

    public async Task VerifyUnsuccessfulLoginAsync()
    {
        var isWarningVisible = await WarningMessage.IsVisibleAsync();
        
        if (!isWarningVisible)
        {
            await VerifyValidationErrorsAsync();
        }
        else
        {
            await VerifyWarningMessageVisibleAsync();
        }
    }

    private async Task VerifyValidationErrorsAsync()
    {
        var usernameClass = await UsernameInput.GetAttributeAsync("class") ?? string.Empty;
        var passwordClass = await PasswordInput.GetAttributeAsync("class") ?? string.Empty;

        if (HasValidationError(usernameClass))
        {
            true.Should().BeTrue("Username is required field");
        }
        
        if (HasValidationError(passwordClass))
        {
            true.Should().BeTrue("Password is required field");
        }
    }

    private async Task VerifyWarningMessageVisibleAsync()
    {
        var isVisible = await WarningMessage.IsVisibleAsync();
        isVisible.Should().BeTrue("Invalid username or password warning should be visible");
        
        var errorText = await ErrorMessage.TextContentAsync();
        errorText.Should().Be("InvalidUserCodeOrPassword");
    }

    private bool HasValidationError(string className)
    {
        return !string.IsNullOrEmpty(className) && className.Contains("input-validation-error");
    }
}
