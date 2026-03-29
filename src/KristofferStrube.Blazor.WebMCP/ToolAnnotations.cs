using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebMCP;

/// <summary>
/// Provides optional metadata about a tool.
/// </summary>
/// <remarks><see href="https://webmachinelearning.github.io/webmcp/#dictdef-toolannotations">See the API definition here</see>.</remarks>
public class ToolAnnotations
{
    /// <summary>
    /// If <see langword="true"/>, indicates that the tool does not modify any state and only reads data.
    /// This hint can help agents make decisions about when it is safe to call the tool.
    /// </summary>
    [JsonPropertyName("readOnlyHint")]
    public bool ReadOnlyHint { get; set; }
}
