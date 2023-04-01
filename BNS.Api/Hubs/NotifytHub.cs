using BNS.Api;
using BNS.Api.Auth;
using BNS.Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BNS.Hubs
{
    [BNSAuthorization]
    public class NotifytHub : Hub, INotifytHub
    {
        protected readonly IHubContext<NotifytHub> _notifyHub;
        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();
        //private readonly ClaimsPrincipal _caller;
        public NotifytHub([NotNull] IHubContext<NotifytHub> notifyHub)
        {
            _notifyHub = notifyHub;
        }

        public async void SendChatMessage(string who, string message)
        {

            foreach (var connectionId in _connections.GetConnections(who))
            {
                await _notifyHub.Clients.Client(connectionId).SendAsync("notify", who, message);
            }
        }

        public override Task OnConnectedAsync()
        {
            //var x= _caller;
            string name = Context.User.Identity.Name;

            _connections.Add(name, Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
