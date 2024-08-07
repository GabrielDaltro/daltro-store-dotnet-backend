using DaltroStore.Core.Communication;

namespace DaltroStore.Core.Messages
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
