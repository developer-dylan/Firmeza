namespace Firmezaa.Web.DTOs;

public class ExcelProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public string? Category { get; set; }
}