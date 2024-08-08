namespace DaltroStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task Commit(CancellationToken cancellationToken = default);
    }
}
