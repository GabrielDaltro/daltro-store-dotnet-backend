using MediatR;

namespace DaltroStore.Core.Communication.Command
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}