using MediatR;

namespace DaltroStore.Core.Communication.Query
{
    public class QueryBus : IQueryBus
    {
        private readonly IMediator mediator;

        public QueryBus(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<TViewModel> Send<TQuery, TViewModel>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TViewModel>
        {
            return await mediator.Send(query, cancellationToken);
        }
    }
}
