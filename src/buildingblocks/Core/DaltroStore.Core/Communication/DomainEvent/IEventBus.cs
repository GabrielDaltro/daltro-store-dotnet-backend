namespace DaltroStore.Core.Communication.DomainEvent
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent e, CancellationToken cancellationToken = default) where TEvent : IDomainEvent;
    }
}