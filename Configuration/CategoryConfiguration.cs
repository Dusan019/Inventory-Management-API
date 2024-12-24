using InventoryManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementAPI.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Seed data for categories (example data)
            var category1 = new Category
            {
                Id = 1,
                Name = "Electronics"
            };

            var category2 = new Category
            {
                Id = 2,
                Name = "Clothing"
            };

            builder.HasData(category1, category2);

            // Configure table columns
            builder.Property(c => c.Name)
                .IsRequired() // Name is required
                .HasMaxLength(100); // Set a max length for Name column

            // Configure the relationship with Products (One-to-Many relationship)
            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete: delete products if category is deleted
        }
    }
}
