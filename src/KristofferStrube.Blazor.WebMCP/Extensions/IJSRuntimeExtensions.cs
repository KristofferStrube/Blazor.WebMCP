using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebMCP.Extensions;

internal static class IJSRuntimeExtensions
{
    internal static async Task<IJSObjectReference> GetHelperAsync(this IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/KristofferStrube.Blazor.WebMCP/KristofferStrube.Blazor.WebMCP.js");
    }
    internal static async Task<ErrorHandlingJSObjectReference> GetErrorHandlingHelperAsync(this IJSRuntime jSRuntime)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        return new ErrorHandlingJSObjectReference(jSRuntime, helper);
    }
}
