using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.WebMCP.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebMCP;

/// <summary>
/// Rpresents an agent executing a tool provided by the site through the ModelContext API.
/// </summary>
/// <remarks><see href="https://webmachinelearning.github.io/webmcp/#modelcontextclient">See the API definition here</see>.</remarks>
public class ModelContextClient : IJSCreatable<ModelContextClient>
{
    private readonly Lazy<Task<IJSObjectReference>> helperTask;

    /// <inheritdoc/>
    public IJSRuntime JSRuntime { get; }

    /// <inheritdoc/>
    public IJSObjectReference JSReference { get; }

    /// <inheritdoc/>
    public bool DisposesJSReference { get; }

    /// <inheritdoc/>
    public static async Task<ModelContextClient> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<ModelContextClient> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new ModelContextClient(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected ModelContextClient(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        JSReference = jSReference;
        JSRuntime = jSRuntime;
        DisposesJSReference = options.DisposesJSReference;
        helperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// Asynchronously requests user input during the execution of a tool.
    /// The callback function is invoked to perform the user interaction(e.g., showing a confirmation dialog), and the promise resolves with the result of the callback.
    /// </summary>
    /// <typeparam name="T">The response type of the user interaction</typeparam>
    /// <param name="callback">A callback that works as an alternative to awaiting the method, though that is recommended.</param>
    public async Task<T> RequestUserInteractionAsync<T>(Func<Task<T>> callback)
    {
        using Callback<T> callbackWrapper = new(callback);
        IJSObjectReference helper = await helperTask.Value;
        T result = await helper.InvokeAsync<T>("requestUserInteraction", this, callbackWrapper);
        return result;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await IJSWrapper.DisposeJSReference(this);
        if (helperTask.IsValueCreated)
        {
            IJSObjectReference helper = await helperTask.Value;
            await helper.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}
