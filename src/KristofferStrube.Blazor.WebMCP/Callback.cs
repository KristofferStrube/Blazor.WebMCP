using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.WebMCP;

internal class Callback<TResult> : IDisposable
{
    internal readonly DotNetObjectReference<Callback<TResult>> ObjRef;

    private readonly Func<Task<TResult>> function;
    private bool disposedValue;

    public Callback(Func<Task<TResult>> function)
    {
        this.function = function;
        ObjRef = DotNetObjectReference.Create(this);
    }

    [JSInvokable]
    public async Task<TResult> InvokeCallback()
    {
        return await function.Invoke();
    }

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

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
