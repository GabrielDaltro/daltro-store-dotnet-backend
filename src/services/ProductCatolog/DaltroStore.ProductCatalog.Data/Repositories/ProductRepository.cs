using Microsoft.EntityFrameworkCore;
using DaltroStore.ProductCatalog.Domain.Models;
using DaltroStore.ProductCatalog.Domain.Repositories;
using DaltroStore.ProductCatalog.Infrastructure.Context;

namespace DaltroStore.ProductCatalog.Infrastructure.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private readonly ProductCatalogDbContext dbContext;

        public ProductRepository(ProductCatalogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Product product)
        {
            dbContext.Products.Add(product);
        }

        public void Delete(Product product)
        {
            dbContext.Products.Remove(product);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategory(Guid categoryId)
        {
            return await dbContext.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task<Product?> GetById(Guid id)
        {
            return await dbContext.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Product product)
        {
            dbContext.Products.Update(product);
        }
    }
}