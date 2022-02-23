using MediatR;

namespace BNS.Domain.Messaging
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeEvent<TEvent>() where TEvent : IEvent, IRequest;
    }
}
