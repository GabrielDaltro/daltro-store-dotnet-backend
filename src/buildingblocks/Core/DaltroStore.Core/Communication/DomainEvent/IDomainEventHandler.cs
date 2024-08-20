using MediatR;

namespace DaltroStore.Core.Communication.DomainEvent
{
    public interface IDomainEventHandler<in TEvent> : IRequestHandler<TEvent> where TEvent : IDomainEvent { }
}