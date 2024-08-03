using DaltroStore.ProductCatalog.Domain.Models;

namespace DaltroStore.ProductCatalog.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();

        Task<Category?> GetById(Guid id);
        
        Task<Category?> GetByName(string name);
        
        Task<Category?> GetByCode(int code);

        void Add(Category category);

        void Update(Category category);

        void Delete(Category category);
    }
}