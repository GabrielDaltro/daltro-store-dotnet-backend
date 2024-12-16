using Microsoft.EntityFrameworkCore;
using DaltroStore.Customers.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DaltroStore.Customers.Infrastructure.TableConfig
{
    internal class CustomerTableConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Value)
                    .IsRequired()
                    .HasMaxLength(Cpf.CpfMaxLength)
                    .HasColumnName("Cpf")
                    .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Address)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.AddressMaxLength})");
            });

            builder.HasOne<Address>(c => c.Address)
                .WithOne()
                .HasForeignKey<Address>(address => address.CustomerId);

            builder.ToTable("Customer");
        }
    }
}
