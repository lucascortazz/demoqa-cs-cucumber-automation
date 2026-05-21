using DemoQa.Cucumber.Tests.Models;
using FluentAssertions;
using Microsoft.Playwright;
using Reqnroll;
using System.Net;
using System.Net.Http.Json;

namespace DemoQa.Cucumber.Tests.StepDefinitions;

[Binding]
public class FormAndApiSteps
{
    private readonly IPage _page;
    private readonly HttpClient _httpClient = new();
    private PostResponse? _postResponse;

    [Obsolete("Required by Reqnroll")]
    public FormAndApiSteps(IPage page)
    {
        _page = page;
    }

    [Given("I open the DemoQA text box page")]
    public async Task GivenIOpenTheDemoQATextBoxPage()
    {
        await _page.GotoAsync("https://demoqa.com/text-box");
    }

    [When("I submit the text box form with valid data")]
    public async Task WhenISubmitTheTextBoxFormWithValidData()
    {
        await _page.Locator("#userName").FillAsync("John Doe");
        await _page.Locator("#userEmail").FillAsync("john.doe@example.com");
        await _page.Locator("#currentAddress").FillAsync("123 Main St");
        await _page.Locator("#permanentAddress").FillAsync("456 Secondary St");
        await _page.Locator("#submit").ClickAsync();
    }

    [Then("the submitted form values should be displayed correctly")]
    public async Task ThenTheSubmittedFormValuesShouldBeDisplayedCorrectly()
    {
        await _page.Locator("#output").WaitForAsync();

        var output = await _page.Locator("#output").InnerTextAsync();

        output.Should().Contain("John Doe");
        output.Should().Contain("john.doe@example.com");
        output.Should().Contain("123 Main St");
        output.Should().Contain("456 Secondary St");
    }

    [When("I request post 1 from the public API")]
    public async Task WhenIRequestPost1FromThePublicApi()
    {
        var response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        _postResponse = await response.Content.ReadFromJsonAsync<PostResponse>();
    }

    [Then("the API response should be valid")]
    public void ThenTheApiResponseShouldBeValid()
    {
        _postResponse.Should().NotBeNull();
        _postResponse!.UserId.Should().BeGreaterThan(0);
        _postResponse.Id.Should().Be(1);
        _postResponse.Title.Should().NotBeNullOrWhiteSpace();
        _postResponse.Body.Should().NotBeNullOrWhiteSpace();
    }

    [Then("I print the success message")]
    public void ThenIPrintTheSuccessMessage()
    {
        Console.WriteLine("All tests passed.");
    }
}