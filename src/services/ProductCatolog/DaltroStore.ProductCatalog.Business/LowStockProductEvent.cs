using DaltroStore.Core.DomainObjects;

namespace DaltroStore.ProductCatalog.Domain
{
    internal class LowStockProductEvent : DomainEvent
    {
        public uint QuantityRemain { get; init; }

        public LowStockProductEvent(Guid AggregateId, uint quantityRemain) : base(AggregateId)
        {
            QuantityRemain = quantityRemain;
        }
    }
}