using BNS.Domain;
using BNS.Domain.Interface;
using BNS.Domain.Responses;
using BNS.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BNS.Service.Notify
{
    public class NotifyHub : Hub, INotifytHub
    {
        protected readonly IHubContext<NotifyHub> _notifyHub;
        protected readonly IConnectionMapping<string> _connections;

        public NotifyHub([NotNull] IHubContext<NotifyHub> notifyHub,
            IConnectionMapping<string> connections)
        {
            _notifyHub = notifyHub;
            _connections = connections;
        }

        public async Task OnDisConnected(string accountCompanyId)
        {
            if (!string.IsNullOrEmpty(accountCompanyId))
            {
                _connections.Remove(accountCompanyId, Context.ConnectionId);
                await base.OnDisconnectedAsync(null);
            }
        }

        public async Task OnConnected(string accountCompanyId)
        {
            if (!string.IsNullOrEmpty(accountCompanyId))
            {
                _connections.Add(accountCompanyId, Context.ConnectionId);
                await base.OnConnectedAsync();
            }
        }

        public override Task OnConnectedAsync()
        {
            var x = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public async void SendNotify(string accountCompanyId, NotifyResponse notifyResponse)
        {
            var connections = _connections.GetConnections(accountCompanyId);
            foreach (var connectionId in connections)
            {
                var isConnected = _notifyHub.Clients.Client(connectionId) != null;
                if (isConnected)
                {
                    await _notifyHub.Clients.Client(connectionId).SendAsync("notify", notifyResponse);
                }
            }
        }
    }
}
