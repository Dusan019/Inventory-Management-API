namespace InventoryManagementAPI.Dto
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryId { get; set; } // Optional: Allow updating the category
    }
}
