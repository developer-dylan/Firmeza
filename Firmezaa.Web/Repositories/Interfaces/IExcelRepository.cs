using Firmezaa.Web.DTOs;

namespace Firmezaa.Web.Repositories.Interfaces
{
    public interface IExcelRepository
    {
        Task SaveProductsFromExcelAsync(IEnumerable<ExcelProductDto> excelProducts, DateTime? userCreatedAt = null);
    }
}