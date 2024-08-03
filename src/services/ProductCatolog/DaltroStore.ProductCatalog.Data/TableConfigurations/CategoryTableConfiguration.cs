using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DaltroStore.ProductCatalog.Domain.Models;

namespace DaltroStore.ProductCatalog.Infrastructure.TableConfigurations
{
    internal class CategoryTableConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Code)
                .IsRequired();

            builder.HasIndex(c => c.Name)
                .IsUnique();

            builder.HasIndex(c => c.Code)
                .IsUnique();
        }
    }
}
