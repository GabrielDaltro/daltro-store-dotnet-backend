using DaltroStore.ProductCatalog.Domain.Models;

namespace DaltroStore.ProductCatalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product?> GetById(Guid id);

        Task<IEnumerable<Product>> GetByCategory(Guid categoryId);

        void Add(Product product);

        void Update(Product product);

        void Delete(Product product);
    }
}