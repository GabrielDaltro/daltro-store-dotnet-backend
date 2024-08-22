using DaltroStore.Core.Communication.DomainEvent;
using DaltroStore.ProductCatalog.Domain;

namespace DaltroStore.ProductCatalog.Application.DomainEventHandlers
{
    internal class LowStockProductEventHandler : IDomainEventHandler<LowStockProductEvent>
    {
        public Task Handle(LowStockProductEvent request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
