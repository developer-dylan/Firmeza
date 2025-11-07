using Firmezaa.Web.Data;
using Firmezaa.Web.DTOs;
using Firmezaa.Web.Repositories.Interfaces;
using Firmezaa.Web.Models.Entities;

namespace Firmezaa.Web.Repositories.Implementations;

public class ExcelRepository(AppDbContext context) : IExcelRepository
{
    public async Task SaveProductsFromExcelAsync(IEnumerable<ExcelProductDto> excelProducts)
    {
        var products = excelProducts.Select(dto => new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Quantity = dto.Quantity,
            CreatedAt = DateTime.UtcNow
        }).ToList();

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}