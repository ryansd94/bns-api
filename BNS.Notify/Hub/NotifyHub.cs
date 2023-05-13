using BNS.Domain;
using BNS.Domain.Responses;
using BNS.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.CodeAnalysis;

namespace BNS.Notify.Hubs
{
    public class NotifyHub : Hub, INotifytHub
    {
        protected readonly IHubContext<NotifyHub> _notifyHub;
        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        public NotifyHub([NotNull] IHubContext<NotifyHub> notifyHub)
        {
            _notifyHub = notifyHub;
        }

        //public override Task OnDisconnectedAsync(System.Exception exception)
        //{
        //    var connectionId = Context.ConnectionId;
        //    if (!string.IsNullOrEmpty(connectionId))
        //    {
        //        _connections.Remove(connectionId, Context.ConnectionId);
        //    }
        //    return base.OnDisconnectedAsync(exception);
        //}


        public async Task OnDisConnected(string accountCompanyId)
        {
            if (!string.IsNullOrEmpty(accountCompanyId))
            {
                _connections.Add(accountCompanyId, Context.ConnectionId);
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

        public async void SendNotify(string accountCompanyId, NotifyResponse notifyResponse)
        {
            var connections = _connections.GetConnections(accountCompanyId);
            foreach (var connectionId in connections)
            {
                await _notifyHub.Clients.Client(connectionId).SendAsync("notify", notifyResponse);
            }
        }

        //public override Task OnConnectedAsync()
        //{
        //    var x =  Context.User.Identities.SelectMany(s=>s.Claims).ToList();

        //    var accountCompanyId = Context.User.Claims.FirstOrDefault(c => c.Type == EClaimType.AccountCompanyId.ToString())?.Value;
        //    if (!string.IsNullOrEmpty(accountCompanyId))
        //    {
        //        _connections.Add(accountCompanyId, Context.ConnectionId);
        //        return base.OnConnectedAsync();
        //    }
        //    return null;
        //}
    }
}
