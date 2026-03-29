using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebMCP;

/// <summary>
/// Describes a tool that can be invoked by agents.
/// </summary>
/// <remarks><see href="https://webmachinelearning.github.io/webmcp/#dictdef-modelcontexttool">See the API definition here</see>.</remarks>
public class ModelContextTool<TInput, TOutput> : IDisposable
{
    internal readonly DotNetObjectReference<ModelContextTool<TInput, TOutput>> ObjRef;
    internal IJSRuntime? JSRuntime;
    private bool disposedValue;

    /// <summary>
    /// A unique identifier for the tool. This is used by agents to reference the tool when making tool calls.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// A natural language description of the tool's functionality. This helps agents understand when and how to use the tool.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// A JSON Schema object describing the expected input parameters for the tool.
    /// </summary>
    /// <remarks>
    /// This gets populated automatically with the JSON schema of <typeparamref name="TInput"/> if not set, but you can override it.
    /// </remarks>
    public object? InputSchema { get; set; } = null;

    /// <summary>
    /// A callback function that is invoked when an agent calls the tool.
    /// </summary>
    /// <remarks>
    /// It doesn't look like the client parameter has been adopted by any versions of Chrome yet, so expect it to be null.
    /// </remarks>
    public required Func<TInput, ModelContextClient?, Task<TOutput>> Execute { get; set; }

    /// <summary>
    /// Optional annotations providing additional metadata about the tool’s behavior.
    /// </summary>
    public ToolAnnotations? Annotations { get; set; }

    /// <summary>
    /// Creates a new <see cref="ModelContextTool{TInput, TOutput}"/>.
    /// </summary>
    public ModelContextTool()
    {
        ObjRef = DotNetObjectReference.Create(this);
    }

    /// <summary>
    /// Invokes <see cref="Execute"/> with the <paramref name="input"/>.
    /// </summary>
    /// <param name="input">The input for the tool.</param>
    /// <param name="jSClient">The client as a JS object.</param>
    [JSInvokable]
    public async Task<TOutput> InvokeExecuteAsync(TInput input, IJSObjectReference? jSClient)
    {
        if (JSRuntime is null)
        {
            throw new InvalidOperationException("The tool was invoked before it was registered.");
        }

        if (jSClient is null)
        {
            return await Execute(input, null);
        }
        else
        {
            await using ModelContextClient client = await ModelContextClient.CreateAsync(JSRuntime, jSClient, new() { DisposesJSReference = true });

            return await Execute(input, client);
        }
    }

    /// <inheritdoc/>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                ObjRef.Dispose();
            }
            disposedValue = true;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
