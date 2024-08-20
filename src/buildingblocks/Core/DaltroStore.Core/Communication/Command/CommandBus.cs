using MediatR;

namespace DaltroStore.Core.Communication.Command
{
    public class CommandBus : ICommandBus
    {
        private readonly IMediator mediator;

        public CommandBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<TResponse> Send<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>
        {
            return await mediator.Send(command, cancellationToken);
        }
    }
}