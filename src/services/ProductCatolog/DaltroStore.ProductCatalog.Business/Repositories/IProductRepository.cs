using DaltroStore.ProductCatalog.Domain.Models;

namespace DaltroStore.ProductCatalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();

        Task<Product> GetById(Guid id);

        Task<IEnumerable<Product>> GetByCategory(Guid categoryId);

        Task Add(Product category);

        Task Update(Product category);

        Task DeleteById(Guid id);
    }
}