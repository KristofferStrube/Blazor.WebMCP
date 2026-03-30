[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE)
[![GitHub issues](https://img.shields.io/github/issues/KristofferStrube/Blazor.WebMCP)](https://github.com/KristofferStrube/Blazor.WebMCP/issues)
[![GitHub forks](https://img.shields.io/github/forks/KristofferStrube/Blazor.WebMCP)](https://github.com/KristofferStrube/Blazor.WebMCP/network/members)
[![GitHub stars](https://img.shields.io/github/stars/KristofferStrube/Blazor.WebMCP)](https://github.com/KristofferStrube/Blazor.WebMCP/stargazers)
[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/KristofferStrube.Blazor.WebMCP?label=NuGet%20Downloads)](https://www.nuget.org/packages/KristofferStrube.Blazor.WebMCP/)

# Blazor.WebMCP
A Blazor wrapper for the [WebMCP browser API.](https://github.com/webmachinelearning/webmcp)
The Web API enables the browser to provide tools that can be accessed by AI agents and assistive technologies to create collaborative, human-in-the-loop workflows. This project implements a wrapper around the API for Blazor so that we can easily and safely expose our own tools in C# through the WebMCP API.

**The browser API is not stable yet, so this project might break in the future if the API changes**

# Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.WebMCP/

On each page, you can find the corresponding code for the example in the top right corner.

On the [API Coverage Status](https://kristofferstrube.github.io/Blazor.WebMCP/Status) page, you can see how much of the WebIDL specs this wrapper has covered.

# Getting started
## Prerequisites
You need to install .NET 7.0 or newer to use the library.

[Download .NET 7](https://dotnet.microsoft.com/download/dotnet/10.0)

## Installation
You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:
```bash
dotnet add package KristofferStrube.Blazor.WebMCP
```

## Usage
Using the library you can add your own WebMCP tools to your web page. First you need to register a service in your service collection using the following extension:
```csharp
builder.Services.AddModelContextService();
```

Then in some page you can inject the `IModelContextService` to register your own tool.

```razor
@using KristofferStrube.Blazor.DOM
@using KristofferStrube.Blazor.WebMCP
@implements IAsyncDisposable
@inject IModelContextService ModelContextService
@inject IJSRuntime JSRuntime

<PageTitle>Blazor WebMCP</PageTitle>

<h1>@contentFromAI</h1>

@code {
    string contentFromAI = "";
    SupportStatus browserSupport;
    AbortController? toolUnregisterController;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;

        ModelContextTool<ToolArguments, string> tool = new()
        {
            Name = "WriteContent",
            Description = "This tool can write some content to the website.",
            Execute = async (ToolArguments input, ModelContextClient? client) =>
            {
                contentFromAI = input.Message;
                await InvokeAsync(StateHasChanged);
                return "Content was written to the website.";
            }
        };

        toolUnregisterController = await AbortController.CreateAsync(JSRuntime);
        await using AbortSignal toolUnregisterSignal = await toolUnregisterController.GetSignalAsync();

        await ModelContextService.RegisterToolAsync(tool, new ModelContextRegisterToolOptions()
        {
            Signal = toolUnregisterSignal
        });
    }

    public record ToolArguments(string Message);

    public async ValueTask DisposeAsync()
    {
        if (toolUnregisterController is not null)
        {
            await toolUnregisterController.AbortAsync();
            await toolUnregisterController.DisposeAsync();
        }
    }
}
```

In the above example we also unregister our tool once the user navigated away from the specific page by implementing `AsyncDisposable`. This is especially important in Blazor WASM where navigation does not reload the page.

# Related repositories
The library uses the following other packages to support its features:
- https://github.com/KristofferStrube/Blazor.WebIDL (To make error handling JSInterop)

# Related articles
This repository was built with help from the following series of articles:

- [Typed exceptions for JSInterop in Blazor](https://kristoffer-strube.dk/post/typed-exceptions-for-jsinterop-in-blazor/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)
