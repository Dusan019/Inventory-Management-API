using InventoryManagementAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagementAPI.Configuration
{
    public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            // Seed data for stock movements (example data)
            var stockMovement1 = new StockMovement
            {
                Id = 1,
                ProductId = 1,
                QuantityChanged = 10,
                Action = "Restock",
                Date = new DateTime(2024, 1, 1)
            };

            var stockMovement2 = new StockMovement
            {
                Id = 2,
                ProductId = 1,
                QuantityChanged = 5,
                Action = "Sell",
                Date = new DateTime(2024, 1, 10)
            };

            builder.HasData(stockMovement1, stockMovement2);

            // Configure table columns
            builder.Property(sm => sm.QuantityChanged)
                .IsRequired(); // QuantityChanged is required

            builder.Property(sm => sm.Action)
                .IsRequired() // Action is required
                .HasMaxLength(20); // Set maximum length for Action (either "Restock" or "Sell")

            builder.Property(sm => sm.Date)
                .IsRequired(); // Date is required

            // Configure the relationship with Product (Many-to-One relationship)
            builder.HasOne(sm => sm.Product)
                .WithMany(p => p.StockMovements)
                .HasForeignKey(sm => sm.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete: delete stock movements if product is deleted
        }
    }
}
