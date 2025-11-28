namespace Firmezaa.Api.DTOs.Product;

public class UpdateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Price { get; set; }
}