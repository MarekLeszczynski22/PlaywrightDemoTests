using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ExampleTest : PageTest
{
	[Test]
	public async Task HasTitle()
	{
		await Page.GotoAsync("https://playwright.dev");

		// Expect a title "to contain" a substring.
		await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
	}

	[Test]
	public async Task GetStartedLink()
	{
		await Page.GotoAsync("https://playwright.dev");

		// Click the get started link.
		await Page.GetByRole(AriaRole.Link, new() { Name = "Get started" }).ClickAsync();

		// Expects page to have a heading with the name of Installation.
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Installation" })).ToBeVisibleAsync();
	}

	[Test]
	public async Task InvalidUsername()
	{
		await Page.GotoAsync("https://www.saucedemo.com/");

		ILocator usernameInput = Page.Locator("[data-test='username']");

		await usernameInput.FillAsync("standard_user");

		ILocator passwordInput = Page.Locator("[data-test='password']");

		await passwordInput.FillAsync("password");

		ILocator loginButton = Page.Locator("[data-test='login-button']");

		await loginButton.ClickAsync();

		ILocator errorMessage = Page.Locator("[data-test='error']");

		await Expect(errorMessage).ToContainTextAsync("Epic sadface: Username and password do not match any user in this service");




	}
}