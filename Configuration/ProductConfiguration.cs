using InventoryManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementAPI.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Seed data for products (example data)
            var product1 = new Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "High performance laptop",
                Price = 999.99m,
                QuantityInStock = 50,
                CategoryId = 1
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Phone",
                Description = "Smartphone with great features",
                Price = 499.99m,
                QuantityInStock = 100,
                CategoryId = 1
            };

            builder.HasData(product1, product2);

            // Configure table columns
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200); // Set maximum length for Name

            builder.Property(p => p.Description)
                .HasMaxLength(500); // Set maximum length for Description

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)") // Set the price column to have decimal precision
                .IsRequired(); // Price is required

            builder.Property(p => p.QuantityInStock)
                .IsRequired(); // QuantityInStock is required

            // Configure relationship with Category (Many-to-One relationship)
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete: delete products if category is deleted

            // Configure the relationship with StockMovement (One-to-Many relationship)
            builder.HasMany(p => p.StockMovements)
                .WithOne(sm => sm.Product)
                .HasForeignKey(sm => sm.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete for stock movements if product is deleted
        }
    }
}
