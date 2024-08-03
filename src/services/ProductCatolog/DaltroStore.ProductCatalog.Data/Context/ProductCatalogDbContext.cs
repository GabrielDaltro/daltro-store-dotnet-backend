using DaltroStore.Core.Data;
using DaltroStore.ProductCatalog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DaltroStore.ProductCatalog.Infrastructure.Context
{
    public class ProductCatalogDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductCatalogDbContext).Assembly);
        }

        public async Task Commit()
        {
           await SaveChangesAsync();
        }
    }
}