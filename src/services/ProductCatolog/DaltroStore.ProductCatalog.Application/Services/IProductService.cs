using DaltroStore.ProductCatalog.Domain.Models;

namespace DaltroStore.ProductCatalog.Application.Services
{
    public interface IProductService
    {
        Task RegisterProduct(Product product);

        Task UnregisterProduct(Guid productId);

        Task UpdateProduct(Product product);

        Task<IEnumerable<Product>> GetProductsByCategory(Guid categotyId);

        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product?> GetProductById(Guid productId);

        Task IncreaseProductStockQuantity(Guid productId, uint quantity);

        Task DecreaseProductStockQuantity(Guid productId, uint quantity);
    }
}
