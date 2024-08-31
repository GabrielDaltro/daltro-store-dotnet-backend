using Microsoft.EntityFrameworkCore;
using DaltroStore.ProductCatalog.Domain.Models;
using DaltroStore.ProductCatalog.Domain.Repositories;
using DaltroStore.ProductCatalog.Infrastructure.Context;

namespace DaltroStore.ProductCatalog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductCatalogDbContext dbContext;

        public CategoryRepository(ProductCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Category category)
        {
            dbContext.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            dbContext.Categories.Remove(category);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetByCode(int code)
        {
            return await dbContext.Categories.SingleOrDefaultAsync(c => c.Code == code);
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> GetByName(string name)
        {
            return await dbContext.Categories.SingleOrDefaultAsync(c => c.Name.Equals(name));
        }

        public void Update(Category category)
        {
            dbContext.Categories.Update(category);
        }
    }
}