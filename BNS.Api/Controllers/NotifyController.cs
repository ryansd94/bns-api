using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BNS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        protected readonly INotifytHub _notifyHub;
        public NotifyController(INotifytHub notifyHub)
        {
            _notifyHub = notifyHub;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MessagePost messagePost)
        {
            _notifyHub.SendChatMessage("ryansd994@gmail.com", "The message '" + messagePost.Message + "' has been received");

            return Ok();
        }
        public class MessagePost
        {
            public virtual string Message { get; set; }
        }
    }
}
