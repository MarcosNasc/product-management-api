using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Price)
                .HasPrecision(10, 2) 
                .IsRequired();

            builder.HasOne(p => p.Category) 
                .WithMany(c => c.Products) 
                .HasForeignKey(p => p.CategoryId) 
                .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
