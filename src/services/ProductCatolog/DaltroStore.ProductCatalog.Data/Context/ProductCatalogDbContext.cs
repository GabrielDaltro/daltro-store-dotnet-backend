using Microsoft.EntityFrameworkCore;

namespace DaltroStore.ProductCatalog.Infrastructure.Context
{
    internal class ProductCatalogDbContext : DbContext
    {
        public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductCatalogDbContext).Assembly);
        }
    }
}