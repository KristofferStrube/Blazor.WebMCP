namespace KristofferStrube.Blazor.WebMCP;

/// <summary>
/// The service provides methods for web applications to register and manage tools that can be invoked by agents.
/// </summary>
public interface IModelContextService
{
    /// <summary>
    /// Registers a tool that agents can invoke.
    /// Throws an exception if a tool with the same name is already registered, if the given name or description are empty strings, or if the inputSchema is invalid.
    /// </summary>
    /// <param name="tool">The tool to register.</param>
    /// <param name="options">Options for how the tool is registered.</param>
    public Task RegisterToolAsync<TInput, TOutput>(ModelContextTool<TInput, TOutput> tool, ModelContextRegisterToolOptions? options = null);
    
    /// <summary>
    /// Checks whether the current browser supports the WebMCP API.
    /// </summary>
    public Task<SupportStatus> CheckForSupportStatusAsync();
}
