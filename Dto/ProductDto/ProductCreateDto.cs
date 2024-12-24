namespace InventoryManagementAPI.Dto
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; } // Link the product to a category
    }
}
