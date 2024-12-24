using Inventory_Management_API.Dto.CategoryDto;

public class CategoryWithProductsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<CategorySpecificProductDto> Products { get; set; }
}
