using Microsoft.Playwright;
using DemoQa.Cucumber.Tests.Models;

namespace DemoQa.Cucumber.Tests.Pages;

public sealed class TextBoxPage
{
    private readonly IPage _page;

    public TextBoxPage(IPage page)
    {
        _page = page;
    }

    public async Task OpenAsync()
    {
        await _page.GotoAsync("https://demoqa.com/text-box");
    }

    public async Task SubmitFormAsync(FormData formData)
    {
        await _page.Locator("#userName").FillAsync(formData.FullName);
        await _page.Locator("#userEmail").FillAsync(formData.Email);
        await _page.Locator("#currentAddress").FillAsync(formData.CurrentAddress);
        await _page.Locator("#permanentAddress").FillAsync(formData.PermanentAddress);
        await _page.Locator("#submit").ClickAsync();
    }

    public async Task WaitForOutputAsync()
    {
        await _page.Locator("#output").WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible
        });
    }

    public async Task<string> GetOutputNameAsync()
    {
        return await _page.Locator("#name").InnerTextAsync();
    }

    public async Task<string> GetOutputEmailAsync()
    {
        return await _page.Locator("#email").InnerTextAsync();
    }

    public async Task<string> GetOutputCurrentAddressAsync()
    {
        return await _page.Locator("p#currentAddress").InnerTextAsync();
    }

    public async Task<string> GetOutputPermanentAddressAsync()
    {
        return await _page.Locator("p#permanentAddress").InnerTextAsync();
    }
}
