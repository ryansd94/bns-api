using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.CodeAnalysis;
using WebSocket.Hubs;

namespace WebSocket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        protected readonly IHubContext<NotifytHub> _notifyHub;
        public NotifyController([NotNull] IHubContext<NotifytHub> notifyHub)
        {
            _notifyHub = notifyHub;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MessagePost messagePost)
        {
            await _notifyHub.Clients.All.SendAsync("notify", "The message '" + messagePost.Message + "' has been received");

            return Ok();
        }
        public class MessagePost
        {
            public virtual string Message { get; set; }
        }
    }
}
