using Microsoft.Extensions.Configuration;
using Microsoft.Playwright.NUnit;
using Microsoft.Testing.Platform.Configurations;

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
	}

	[TearDown]
	public async Task BaseTearDown()
	{
		await Task.CompletedTask;
	}
}