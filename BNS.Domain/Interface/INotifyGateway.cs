
using BNS.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BNS.Domain.Interface
{
    public interface INotifyGateway
    {
        Task SendNotify(List<NotifyResponse> notifyResponses);
    }
}
