using MediatR;

namespace DaltroStore.Core.Communication.Query
{
    public interface IQueryHandler<TQuery, TViewModel> : IRequestHandler<TQuery, TViewModel> where TQuery : IQuery<TViewModel>
    {
    }
}