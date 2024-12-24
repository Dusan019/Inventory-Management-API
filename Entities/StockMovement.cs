namespace InventoryManagementAPI.Entities
{
    public class StockMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int QuantityChanged { get; set; }
        public string Action { get; set; } // "Restock" or "Sell"
        public DateTime Date { get; set; }
    }
}
