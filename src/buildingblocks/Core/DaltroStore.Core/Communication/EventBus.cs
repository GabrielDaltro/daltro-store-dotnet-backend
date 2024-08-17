using MediatR;

namespace DaltroStore.Core.Communication
{
    public class EventBus : IEventBus 
    {
        private readonly IMediator mediator;

        public EventBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Publish<TEvent>(TEvent e, CancellationToken cancellationToken = default) where TEvent : IDomainEvent
        {
            await mediator.Send(e, cancellationToken);
        }
    }
}