using MediatR;

namespace DaltroStore.Core.Communication.Command
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse> { }
}