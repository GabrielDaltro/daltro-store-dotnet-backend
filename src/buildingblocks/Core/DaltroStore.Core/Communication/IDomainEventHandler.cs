using MediatR;

namespace DaltroStore.Core.Communication
{
    public interface IDomainEventHandler<in TEvent> : IRequestHandler<TEvent> where TEvent : IDomainEvent { }
}