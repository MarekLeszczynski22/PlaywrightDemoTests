using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Playwright;

namespace PlaywrightDemoTests
{
    internal class LoginPage
    {
        private readonly IPage _page;

        public LoginPage(IPage page)
        {
            _page = page;
		}

		private ILocator UsernameInput => _page.Locator("[data-test='username']");
		private ILocator PasswordInput => _page.Locator("[data-test='password']");
		private ILocator LoginButton => _page.Locator("[data-test='login-button']");
		public ILocator ErrorMessage => _page.Locator("[data-test='error']");


		public async Task NavigateAsync()
		{
			await _page.GotoAsync("https://www.saucedemo.com/");
		}

		public async Task LoginAsync(string username, string password)
		{
			await UsernameInput.FillAsync(username);
			await PasswordInput.FillAsync(password);
			await LoginButton.ClickAsync();
		}

	}
}
