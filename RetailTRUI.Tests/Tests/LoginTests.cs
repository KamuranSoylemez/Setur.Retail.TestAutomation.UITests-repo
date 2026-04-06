using RetailTRUI.Tests.Infrastructure;
using RetailTRUI.Tests.Pages.Common;

namespace RetailTRUI.Tests.Tests;

/// <summary>
/// Login functionality tests
/// Covers successful and unsuccessful login scenarios
/// </summary>
public class LoginTests : IAsyncLifetime
{
    private LoginPage _loginPage = null!;

    public async Task InitializeAsync()
    {
        await Driver.GetPageAsync();
        _loginPage = new LoginPage();
    }

    public async Task DisposeAsync()
    {
        await Driver.CloseAsync();
    }

    [Fact]
    public async Task SuccessfulLogin_WithValidCredentials_ShouldAuthenticate()
    {
        // CRITICAL: Set page in async context for xUnit
        var page = await Driver.GetPageAsync();
        Driver.SetPage(page);
        
        // Arrange
        await _loginPage.NavigateToLoginPageAsync();

        // Act
        await _loginPage.LoginAsAsync("normal");

        // Assert
        await _loginPage.VerifyLoginSuccessAsync();
    }

    [Theory]
    [InlineData("USERNAME", "PASSWORD")]
    [InlineData("", "")]
    [InlineData("ADMINUSER", "1234567")]
    [InlineData("KAMURAN_SOYLEMEZ", "")]
    [InlineData("", "correctPassword")]
    [InlineData("KAMURAN_SOYLEMEZ", "xxx")]
    [InlineData("xxxxxx", "correctPassword")]
    public async Task UnsuccessfulLogin_WithInvalidCredentials_ShouldShowError(string username, string password)
    {
        // CRITICAL: Set page in async context for xUnit
        var page = await Driver.GetPageAsync();
        Driver.SetPage(page);
        
        // Arrange
        await _loginPage.NavigateToLoginPageAsync();

        // Act
        await _loginPage.FillCredentialsAsync(username, password);
        await _loginPage.ClickLoginButtonAsync();

        // Assert
        await _loginPage.VerifyUnsuccessfulLoginAsync();
    }
}
