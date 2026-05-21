namespace DemoQa.Cucumber.Tests.Models;

public sealed record FormData(
    string FullName,
    string Email,
    string CurrentAddress,
    string PermanentAddress);
