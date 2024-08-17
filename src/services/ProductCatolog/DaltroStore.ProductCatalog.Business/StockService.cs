using DaltroStore.Core.Communication;
using DaltroStore.Core.DomainObjects;
using DaltroStore.ProductCatalog.Domain.Models;
using DaltroStore.ProductCatalog.Domain.Repositories;

namespace DaltroStore.ProductCatalog.Domain.DomainServices
{
    public class StockService
    {
        private readonly IProductRepository productRepository;
        private readonly IEventBus eventBus;

        public StockService(IProductRepository productRepository, IEventBus eventBus)
        {
            this.productRepository = productRepository;
            this.eventBus = eventBus;
        }

        public async Task IncreaseStockQuantity(Guid productId, uint quantity)
        {
            Product? product = await productRepository.GetById(productId);
            if (product == null)
                throw new EntityNotFoundException($"Product of id {productId} does not exist");

            product.IncreaseStockQuantity(quantity);
            productRepository.Update(product);
        }

        public async Task DecreaseStockQuantity(Guid productId, uint quantity)
        {
            Product? product = await productRepository.GetById(productId);
            if (product == null)
                throw new EntityNotFoundException($"Product of id {productId} does not exist");

            product.DecreaseStockQuantity(quantity);

            productRepository.Update(product);

            if (product.StockQuantity < 10)
                await eventBus.Publish(new LowStockProductEvent(productId, product.StockQuantity));
        }
    }
}