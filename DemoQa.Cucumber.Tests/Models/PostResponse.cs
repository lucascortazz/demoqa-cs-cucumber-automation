namespace DemoQa.Cucumber.Tests.Models;

public sealed record PostResponse(
    int UserId,
    int Id,
    string Title,
    string Body);
