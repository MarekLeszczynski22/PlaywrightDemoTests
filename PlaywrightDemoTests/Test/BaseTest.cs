using Microsoft.Extensions.Configuration;
using Microsoft.Playwright.NUnit;
using Microsoft.Testing.Platform.Configurations;
using NUnit.Framework.Interfaces;

public abstract class BaseTest : PageTest
{
	// Cleanup later
	protected Microsoft.Extensions.Configuration.IConfiguration Config { get; private set; }
	protected string BaseUrl => Config["BaseUrl"];

	[SetUp]
	public void BaseSetUp()
	{
		Config = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json")
			.Build();

		Page.SetDefaultTimeout(int.Parse(Config["Playwright:DefaultTimeout"] ?? "30000"));
	}

	[TearDown]
	public async Task BaseTearDown()
	{
		if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
		{
			var fileName = $"{TestContext.CurrentContext.Test.Name}.png";
			await Page.ScreenshotAsync(new() { Path = fileName });
		}
	}
}