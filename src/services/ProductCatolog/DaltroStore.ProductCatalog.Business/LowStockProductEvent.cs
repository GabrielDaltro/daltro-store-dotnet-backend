using DaltroStore.Core.DomainObjects;

namespace DaltroStore.ProductCatalog.Domain
{
    internal class LowStockProductEvent : DomainEvent
    {
        public int QuantityRemain { get; init; }

        public LowStockProductEvent(Guid AggregateId, int quantityRemain) : base(AggregateId)
        {
            QuantityRemain = quantityRemain;
        }
    }
}