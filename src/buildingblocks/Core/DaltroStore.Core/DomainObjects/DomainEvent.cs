using DaltroStore.Core.Communication.DomainEvent;

namespace DaltroStore.Core.DomainObjects
{
    public class DomainEvent : IDomainEvent
    {
        public Guid AggregateId { get; init; }

        public DateTime DateTime { get; init; }

        public DomainEvent(Guid AggregateId)
        {
            DateTime = DateTime.UtcNow;
            this.AggregateId = AggregateId;
        }
    }
}
