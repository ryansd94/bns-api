using MediatR;
using System.Threading.Tasks;

namespace BNS.Domain.Messaging
{
    public interface IBusSubscriber
    {
        Task<IBusSubscriber> SubscribeEvent<TEvent>() where TEvent : IEvent, IRequest;
    }
}
