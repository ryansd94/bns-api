using BNS.Api.Route;
using BNS.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static BNS.Utilities.Enums;

namespace BNS.Api.Controllers
{
    [AppRouteControllerAttribute]
    [ApiController]
    public class NotifyController : ControllerBase
    {
        protected readonly INotifytHub _notifyHub;
        private readonly ClaimsPrincipal _caller;
        public NotifyController(INotifytHub notifyHub, IHttpContextAccessor httpContextAccessor)
        {
            _notifyHub = notifyHub;
            _caller = httpContextAccessor.HttpContext.User;
        }

        public class MessagePost
        {
            public virtual string Message { get; set; }
        }
    }
}
