using MediatR;

namespace DaltroStore.Core.Communication
{
    public interface ICommand<out TResponse> : IRequest<TResponse> { }
}