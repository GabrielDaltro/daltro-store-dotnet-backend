namespace DaltroStore.Core.Communication
{
    public interface ICommandBus
    {
        Task<TResponse> Send<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>;
    }
}