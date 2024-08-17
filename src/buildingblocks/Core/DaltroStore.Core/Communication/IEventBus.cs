namespace DaltroStore.Core.Communication
{
    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent e, CancellationToken cancellationToken = default) where TEvent : IDomainEvent;
    }
}