using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading.Tasks;
using HelloBots.Controllers;
using Microsoft.Bot.Connector;


namespace HelloBots.Dialogs
{
    [Serializable]
    public class MyDialog : IDialog<string>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }
        private async Task MessageReceivedAsync(IDialogContext context,IAwaitable<object> result)
        {
            var ActivityMessage = await result as Activity;
            var Message = ActivityMessage.Text;

            await context.PostAsync($"Escribiste: {Message}");
            context.Wait(MessageReceivedAsync);
        }
    }
}