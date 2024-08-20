namespace DaltroStore.Core.Communication.Command
{
    public abstract class Command<TResponse> : ICommand<TResponse>
    {
        public Guid AggregateId { get; init; }

        public DateTime DateTime { get; init; }

        protected Command()
        {
            DateTime = DateTime.UtcNow;
        }
    }
}
