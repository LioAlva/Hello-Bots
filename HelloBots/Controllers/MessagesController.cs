using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace HelloBots.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private async Task MessageReceivedAsync(IDialogContext context,IAwaitable<object> result)
        {
            var ActivityMessage = await result as Activity;

            var Message = ActivityMessage.Text;

            await context.PostAsync($"Escribiste: {Message}");

            context.Wait(MessageReceivedAsync);
        }

        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new Dialogs.MyDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var Response = Request.CreateResponse(HttpStatusCode.OK);
            return Response;
        }        void HandleSystemMessage(Activity message)
        {
            switch (message.Type)
            {
                case ActivityTypes.ConversationUpdate:
                    break;
                case ActivityTypes.ContactRelationUpdate:
                    break;
                case ActivityTypes.Typing:
                    break;
                case ActivityTypes.Ping:
                    break;
                case ActivityTypes.DeleteUserData:
                    break;
            }
        }
    }
}
