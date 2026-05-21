using Microsoft.Playwright;
using Reqnroll;
using Reqnroll.BoDi;

namespace DemoQa.Cucumber.Tests.Hooks;

[Binding]
public class PlaywrightHooks
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    private IBrowserContext? _context;

    [BeforeScenario]
    public async Task BeforeScenario(IObjectContainer container)
    {
        _playwright = await Playwright.CreateAsync();

        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        _context = await _browser.NewContextAsync();
        var page = await _context.NewPageAsync();

        container.RegisterInstanceAs<IPage>(page);
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        if (_context is not null)
        {
            await _context.CloseAsync();
        }

        if (_browser is not null)
        {
            await _browser.CloseAsync();
        }

        _playwright?.Dispose();
    }
}