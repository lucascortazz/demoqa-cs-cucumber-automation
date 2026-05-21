namespace DemoQa.Cucumber.Tests.Support;

public static class TestSettings
{
    public static bool Headless
    {
        get
        {
            var value = Environment.GetEnvironmentVariable("HEADLESS");
            return !string.Equals(value, "false", StringComparison.OrdinalIgnoreCase);
        }
    }
}
