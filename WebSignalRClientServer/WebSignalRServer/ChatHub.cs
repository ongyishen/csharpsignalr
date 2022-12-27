using Microsoft.AspNetCore.SignalR;

namespace WebSignalRServer
{
	public class ChatHub : Hub
	{
		public async Task SendMessage(string user, string message)
		{

			await Clients.All.SendAsync("ReceiveMessage", DateTime.Now.ToString("[hh:mm:ss tt]") + ":" + user, message);
		}
	}
}
