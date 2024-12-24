namespace Inventory_Management_API.Dto.CategoryDto
{
    public class CategorySpecificProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
    }
}
