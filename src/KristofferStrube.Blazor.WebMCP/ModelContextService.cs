using KristofferStrube.Blazor.WebMCP.Extensions;
using Microsoft.JSInterop;
using System.Text.Json;
using System.Text.Json.Schema;

namespace KristofferStrube.Blazor.WebMCP;

/// <inheritdoc cref="IModelContextService"/>
public class ModelContextService : IModelContextService, IAsyncDisposable
{
    private readonly IJSRuntime jSRuntime;
    private readonly Lazy<Task<IJSObjectReference>> helperTask;

    /// <summary>
    /// Creates a new instance of a <see cref="ModelContextService"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    public ModelContextService(IJSRuntime jSRuntime)
    {
        this.jSRuntime = jSRuntime;
        helperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <inheritdoc/>
    public async Task RegisterToolAsync<TInput, TOutput>(ModelContextTool<TInput, TOutput> tool, ModelContextRegisterToolOptions? options = null)
    {
        IJSObjectReference helper = await helperTask.Value;

        tool.JSRuntime = jSRuntime;

        await helper.InvokeVoidAsync("registerTool", new
        {
            name = tool.Name,
            description = tool.Description,
            inputSchema = tool.InputSchema ?? JsonSchemaExporter.GetJsonSchemaAsNode(JsonSerializerOptions.Web, typeof(TInput)),
            objRef = tool.ObjRef,
            annotations = tool.Annotations,
        }, options);
    }

    /// <inheritdoc/>
    public async Task<SupportStatus> CheckForSupportStatusAsync()
    {
        IJSObjectReference helper = await helperTask.Value;

        bool hasModelContext = await helper.InvokeAsync<bool>("hasModelContext");
        if (!hasModelContext)
        {
            return SupportStatus.None;
        }

        bool hasUnregisterToolFunction = await helper.InvokeAsync<bool>("hasUnregisterToolFunction");

        if (hasUnregisterToolFunction)
        {
            return SupportStatus.OldVersion;
        }

        return SupportStatus.Supported;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (helperTask.IsValueCreated)
        {
            IJSObjectReference helper = await helperTask.Value;
            await helper.DisposeAsync();
        }
    }
}
