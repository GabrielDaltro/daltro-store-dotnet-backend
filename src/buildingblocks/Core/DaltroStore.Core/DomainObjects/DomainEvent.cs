namespace DaltroStore.Core.DomainObjects
{
    public class DomainEvent
    {
        public Guid AggregateId { get; init; }

        public DateTime DateTime { get; init; }

        public DomainEvent(Guid AggregateId)
        {
            DateTime = DateTime.UtcNow;
        }
    }
}
