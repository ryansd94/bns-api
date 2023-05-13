
using BNS.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BNS.Domain.Interface
{
    public interface INotifyService
    {
        Task SendNotify(List<NotifyResponse> notifyResponses);
    }
}
