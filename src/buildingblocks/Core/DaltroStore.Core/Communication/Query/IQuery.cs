using MediatR;

namespace DaltroStore.Core.Communication.Query
{
    public interface IQuery<out TViewModel> : IRequest<TViewModel>
    {
    }
}
