using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Server.Components.Samples.Test
{
    public class User
    {
        [Required(ErrorMessage = "{0}不能为空")]
        public string Username { get; set; }
        [Required(ErrorMessage = "{0}不能为空")]
        public string Password { get; set; }
    }


    public partial class Login
    {
        [NotNull]
        private Foo? Model1 { get; set; }

        [NotNull]
        private ConsoleLogger? Logger1 { get; set; }

        private static Task ClickAsyncButton() => Task.Delay(1000);

        private async Task OnInvalidSubmit1(EditContext context)
        {
            await Task.Delay(1000);
            Logger1.Log(Localizer["OnInvalidSubmitLog"]);
        }

        private async Task OnValidSubmit1(EditContext context)
        {
            await Task.Delay(1000);
            Logger1.Log(Localizer["OnValidSubmitLog"]);
        }

        private void OnFiledChanged(string field, object? value)
        {
            Logger1.Log($"{field}:{value}");
        }

    }
}