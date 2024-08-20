namespace DaltroStore.Core.Communication.Query
{
    internal interface IQueryBus 
    {
        Task<TViewModel> Send<TQuery, TViewModel>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TViewModel>;
    }
}