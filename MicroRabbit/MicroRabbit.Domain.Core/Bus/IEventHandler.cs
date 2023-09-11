using MicroRabbit.Domain.Core.Events;

namespace MicroRabbit.Domain.Core.Bus
{
    public interface IEventHandler<in TEvent> : IEventHadler where TEvent : Event
    {
        Task Handle(TEvent @event);
    }

    public interface IEventHadler
    {
    }
}