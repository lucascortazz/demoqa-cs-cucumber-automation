using Microsoft.Playwright;
using Reqnroll;
using DemoQa.Cucumber.Tests.Support;

namespace DemoQa.Cucumber.Tests.Hooks;

[Binding]
public sealed class PlaywrightHooks
{
    private readonly ScenarioContext _scenarioContext;
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    private IBrowserContext? _context;

    public PlaywrightHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public async Task BeforeScenarioAsync()
    {
        _playwright = await Playwright.CreateAsync();

        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = TestSettings.Headless
        });

        _context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize
            {
                Width = 1280,
                Height = 720
            }
        });

        var page = await _context.NewPageAsync();
        _scenarioContext["Page"] = page;
    }

    [AfterScenario]
    public async Task AfterScenarioAsync()
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
