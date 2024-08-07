using MediatR;

namespace DaltroStore.Core.Communication
{
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse> { }
}