using KristofferStrube.Blazor.DOM;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.WebMCP;
/// <summary>
/// Carries information pertaining to a tool's registration, in contrast with the <see cref="ModelContextTool{TInput, TOutput}"/> which carries the tool definition itself.
/// </summary>
/// <remarks><see href="https://webmachinelearning.github.io/webmcp/#dictdef-modelcontextregistertooloptions">See the API definition here</see>.</remarks>
public class ModelContextRegisterToolOptions
{
    /// <summary>
    /// An <see cref="AbortSignal"/> that unregisters the tool when aborted.
    /// </summary>
    [JsonPropertyName("signal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AbortSignal? Signal { get; set; }
}
