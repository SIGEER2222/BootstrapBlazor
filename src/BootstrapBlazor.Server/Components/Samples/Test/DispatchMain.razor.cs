using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Server.Components.Samples.Test;

public class LotInfo
{
    [Required(ErrorMessage = "{0}不能为空")]
    public string LotName { get; set; }
    [Required(ErrorMessage = "{0}不能为空")]
    public string EqpName { get; set; }
}

public partial class DispatchMain
{
    private readonly AutoResetEvent _locker = new(true);

    private ConcurrentQueue<ConsoleMessageItem> Messages { get; set; } = new();
    private void OnClear()
    {
        _locker.WaitOne();
        while (!Messages.IsEmpty)
        {
            Messages.TryDequeue(out var _);
        }
        _locker.Set();
    }

}