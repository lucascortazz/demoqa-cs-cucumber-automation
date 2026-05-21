using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Playwright;
using Reqnroll;
using DemoQa.Cucumber.Tests.Models;
using DemoQa.Cucumber.Tests.Pages;

namespace DemoQa.Cucumber.Tests.StepDefinitions;

[Binding]
public sealed class FormAndApiSteps
{
    private readonly TextBoxPage _textBoxPage;
    private readonly HttpClient _httpClient;
    private HttpResponseMessage? _response;
    private PostResponse? _postResponse;

    private readonly FormData _formData = new(
        "John Doe",
        "john.doe@example.com",
        "123 Main St",
        "456 Secondary St");

    public FormAndApiSteps(ScenarioContext scenarioContext)
    {
        var page = scenarioContext.Get<IPage>("Page");
        _textBoxPage = new TextBoxPage(page);
        _httpClient = new HttpClient();
    }

    [Given("I open the DemoQA text box page")]
    public async Task OpenDemoQaTextBoxPageAsync()
    {
        await _textBoxPage.OpenAsync();
    }

    [When("I submit the text box form with valid data")]
    public async Task SubmitTextBoxFormWithValidDataAsync()
    {
        await _textBoxPage.SubmitFormAsync(_formData);
    }

    [Then("the submitted form values should be displayed correctly")]
    public async Task SubmittedFormValuesShouldBeDisplayedCorrectlyAsync()
    {
        await _textBoxPage.WaitForOutputAsync();

        var outputName = await _textBoxPage.GetOutputNameAsync();
        var outputEmail = await _textBoxPage.GetOutputEmailAsync();
        var outputCurrentAddress = await _textBoxPage.GetOutputCurrentAddressAsync();
        var outputPermanentAddress = await _textBoxPage.GetOutputPermanentAddressAsync();

        outputName.Should().Contain(_formData.FullName, "the output name should match the submitted full name");
        outputEmail.Should().Contain(_formData.Email, "the output email should match the submitted email");
        outputCurrentAddress.Should().Contain(_formData.CurrentAddress, "the output current address should match the submitted current address");
        outputPermanentAddress.Should().Contain(_formData.PermanentAddress, "the output permanent address should match the submitted permanent address");
    }

    [When("I request post 1 from the public API")]
    public async Task RequestPostOneFromThePublicApiAsync()
    {
        _response = await _httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts/1");
        _postResponse = await _response.Content.ReadFromJsonAsync<PostResponse>();
    }

    [Then("the API response should be valid")]
    public void ApiResponseShouldBeValid()
    {
        _response.Should().NotBeNull();
        _response!.StatusCode.Should().Be(HttpStatusCode.OK);

        _postResponse.Should().NotBeNull();
        _postResponse!.UserId.Should().BeGreaterThan(0);
        _postResponse.Id.Should().Be(1);
        _postResponse.Title.Should().NotBeNullOrWhiteSpace();
        _postResponse.Body.Should().NotBeNullOrWhiteSpace();
    }

    [Then("I print the success message")]
    public void PrintTheSuccessMessage()
    {
        Console.WriteLine("All tests passed.");
    }
}
