namespace KristofferStrube.Blazor.WebMCP;

/// <summary>
/// Differentiates which kind of support a browser has for this API.
/// </summary>
public enum SupportStatus
{
    /// <summary>
    /// The browser does not support the WebMCP API.
    /// </summary>
    None,
    /// <summary>
    /// The browser supports an old version of the WebMCP API which this wrapper is not compatible with.
    /// </summary>
    OldVersion,
    /// <summary>
    /// The WebMCP API is supported in this browser.
    /// </summary>
    Supported
}
