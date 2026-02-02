using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PlaywrightDemoTests;
using PlaywrightDemoTests.Pages;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginTest : BaseTest
{

	[Test]
	public async Task InvalidLoginShowsError()
	{
		var loginPage = new LoginPage(Page);

		const string invalidUsername = "wrong_username";
		const string invalidPassword = "wrong_password";

		await Page.GotoAsync(BaseUrl);
		await Expect(Page).ToHaveURLAsync("https://www.saucedemo.com/");

		await loginPage.LoginAsync(invalidUsername, invalidPassword);
        await Expect(loginPage.ErrorMessage).ToBeVisibleAsync();
		await Expect(loginPage.ErrorMessage).ToContainTextAsync("Username and password do not match");
	}

	[Test]
	public async Task ValidLoginNaviagtesToInventory()
	{
		var loginPage = new LoginPage(Page);
		var inventoryPage = new InventoryPage(Page);

		await Page.GotoAsync(BaseUrl);
		await loginPage.LoginAsync("standard_user", "secret_sauce");

		await inventoryPage.WaitForPageAsync();

		await Expect(Page).ToHaveURLAsync(new Regex(".*inventory.html"));
		Assert.That(await inventoryPage.IsLoadedAsync(), Is.True);
	}

	[TestCase("", "secret_sauce", "Username is required")]
	[TestCase("standard_user", "", "Password is required")]
	public async Task InvalidLogin_ShowsCorrectError(string username, string password, string expectedMessage)
	{
		var loginPage = new LoginPage(Page);

		await Page.GotoAsync(BaseUrl);
		await loginPage.LoginAsync(username, password);

		await Expect(loginPage.ErrorMessage).ToContainTextAsync(expectedMessage);
	}
}