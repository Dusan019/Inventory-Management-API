﻿namespace InventoryManagementAPI.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<StockMovement> StockMovements { get; set; } // To track stock changes
    }
}
