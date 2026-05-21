# DemoQA C# Cucumber Automation

Converted version of the original Playwright TypeScript challenge into C# using Playwright .NET and Reqnroll, a Cucumber style BDD framework for .NET.

## Objective

This project performs the same combined UI and API validation as the original project:

1. Opens `https://demoqa.com/text-box`
2. Fills the form with the required values
3. Submits the form
4. Validates that the output section displays the submitted values correctly
5. Sends a GET request to `https://jsonplaceholder.typicode.com/posts/1`
6. Validates that:
   * The response status is `200`
   * The JSON contains `userId`, `id`, `title`, and `body`
   * The `id` field equals `1`
7. Prints exactly `All tests passed.` when every validation passes

## Tech Stack

* C#
* .NET 8
* Reqnroll
* NUnit
* Playwright .NET
* FluentAssertions

## Prerequisites

Install .NET 8 SDK.

## Restore Dependencies

```bash
dotnet restore
```

## Install Playwright Browsers

From the solution folder:

```bash
dotnet build
pwsh DemoQa.Cucumber.Tests/bin/Debug/net8.0/playwright.ps1 install
```

On Windows PowerShell, use the same command. On bash based terminals, `pwsh` must be installed.

## Run the Tests

```bash
dotnet test
```

## Run in Headed Mode

```bash
dotnet test -- TestRunParameters.Parameter(name=Headless,value=false)
```

## Project Structure

```text
.
├── DemoQa.Cucumber.sln
├── README.md
└── DemoQa.Cucumber.Tests
    ├── DemoQa.Cucumber.Tests.csproj
    ├── Features
    │   └── FormAndApi.feature
    ├── Hooks
    │   └── PlaywrightHooks.cs
    ├── Models
    │   ├── FormData.cs
    │   └── PostResponse.cs
    ├── Pages
    │   └── TextBoxPage.cs
    ├── StepDefinitions
    │   └── FormAndApiSteps.cs
    └── Support
        └── TestSettings.cs
```

## Notes

The scenario intentionally keeps the UI and API validations in one BDD flow to preserve the original challenge behavior.
