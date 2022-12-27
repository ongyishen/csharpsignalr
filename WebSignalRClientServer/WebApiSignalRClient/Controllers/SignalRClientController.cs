using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using WebApiSignalRClient.Core;
using WebApiSignalRClient.Models;

namespace WebApiSignalRClient.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SignalRClientController : ControllerBase
	{
		[HttpPost("SendMessage")]
		public async Task<IActionResult> SendMessage(ChatForm param)
		{
			var connection = new HubConnectionBuilder()
				.WithUrl(GlobalContext.SystemConfig.ChatHubUrl)
				.WithAutomaticReconnect()
				.Build();

			try
			{

				await connection.StartAsync();
				await connection.InvokeAsync("SendMessage", param.User, param.Message);
				await connection.StopAsync();
			}
			catch (Exception ex)
			{
				return Ok(ex.Message);
			}

			return Ok();
		}

	}
}
