
using System.Threading.Tasks;
using Microsoft.JSInterop;

public static class JsRuntimeExtensions
{

    public static ValueTask<bool> Confirm(this IJSRuntime jSRuntime, string message)
    {
        return jSRuntime.InvokeAsync<bool>("confirm", message);
    }

}