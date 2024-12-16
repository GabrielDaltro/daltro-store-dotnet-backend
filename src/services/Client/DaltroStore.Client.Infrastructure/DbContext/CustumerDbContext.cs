using DaltroStore.Core.Data;
using DaltroStore.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DaltroStore.Customers.Infrastructure
{
    public class CustumerDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public CustumerDbContext(DbContextOptions<CustumerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustumerDbContext).Assembly);
        }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
        }
    }
}
