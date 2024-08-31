using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DaltroStore.ProductCatalog.Domain.Models;

namespace DaltroStore.ProductCatalog.Infrastructure.TableConfigurations
{
    internal class ProductTableConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Active)
                .IsRequired();

            builder.Property(p => p.Image)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(p => p.CategoryId)
                .IsRequired();

            builder.Property(p => p.StockQuantity)
                .IsRequired();

            builder.Property(p => p.RegistrationDate)
                .IsRequired();

            builder.Property(p => p.Weight)
                .IsRequired()
                .HasColumnType("decimal(4,2)");

            builder.OwnsOne(p => p.Dimension, dimension =>
            {
                dimension.Property(d => d.Height)
                .HasColumnName("Height")
                .HasColumnType("decimal(6,3)")
                .IsRequired();

                dimension.Property(d => d.Width)
                .HasColumnName("Width")
                .HasColumnType("decimal(6,3)")
                .IsRequired();

                dimension.Property(d => d.Depth)
                .HasColumnName("Depth")
                .HasColumnType("decimal(6,3)")
                .IsRequired();
            });
        }
    }
}