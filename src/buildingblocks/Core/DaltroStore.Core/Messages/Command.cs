namespace DaltroStore.Core.Messages
{
    public abstract class Command
    {
        public Guid AggregateId { get; init; }

        public DateTime DateTime { get; init; }

        protected Command()
        {
            DateTime = DateTime.UtcNow;
        }
    }
}
