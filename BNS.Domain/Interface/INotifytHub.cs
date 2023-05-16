using BNS.Domain.Responses;

namespace BNS.Domain
{
    public interface INotifytHub
    {
        void SendNotify(string accountId, NotifyResponse notifyResponse);
    }
}
