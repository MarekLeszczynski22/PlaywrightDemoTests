using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Playwright;

namespace PlaywrightDemoTests
{
    internal class InventoryPage
    {
        private readonly IPage _page;
        public InventoryPage(IPage page)
        {
            _page = page;
        }
        
        private ILocator InventoryContainer => _page.Locator("[data-test='inventory-container']");
        private ILocator ShoppingCartIcon => _page.Locator("[data-test='shopping-cart-link']");

        public async Task WaitForPageAsync()
        {
            await InventoryContainer.WaitForAsync();
		}

        public async Task<bool> IsLoadedAsync()
        {
            return await InventoryContainer.IsVisibleAsync();
		}
	}
}
