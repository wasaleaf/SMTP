using Microsoft.JSInterop;

namespace SMTP.Net.Services;

/// <summary>
/// Service to handle browser window resize events using JavaScript interop
/// </summary>
/// <param name="runtime">JavaScript runtim for interop</param>
public class ResizeService(IJSRuntime runtime) : IAsyncDisposable
{
    /// <summary>
    /// JavaScript runtim for interop calls
    /// </summary>
    private readonly IJSRuntime jSRuntime = runtime;
    /// <summary>
    /// Reference for JS to call C# methods
    /// </summary>
    private DotNetObjectReference<ResizeService>? resizeService;

    /// <summary>
    /// Event triggered when the browser window is resized
    /// </summary>
    public event Action<int>? OnResize;

    /// <summary>
    /// Initializes the service and registers the resize event listener in JavaScript
    /// </summary>
    public async Task InitializeAsync()
    {
        resizeService = DotNetObjectReference.Create(this);

        await jSRuntime.InvokeVoidAsync("resizeService.addResizeListener", resizeService);
    }

    /// <summary>
    /// Called from JavaScript when the window is resized
    /// </summary>
    /// <param name="width">The new width of the browser window</param>
    [JSInvokable]
    public Task NotifyResize(int width)
    {
        OnResize?.Invoke(width);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Cleans up resources when the service is disposed
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (resizeService != null)
        {
            try
            {
                await jSRuntime.InvokeVoidAsync("resizeService.removeResizeListener");
            }
            catch (JSDisconnectedException) { }
            resizeService.Dispose();
        }
    }
}
