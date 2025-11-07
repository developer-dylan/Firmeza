using Firmezaa.Web.DTOs;
using System.Globalization;
using Firmezaa.Web.Services.Interfaces;
using Firmezaa.Web.Repositories.Interfaces;
using OfficeOpenXml;


namespace Firmezaa.Web.Services.Implementations;

public class ExcelService : IExcelService
{
    private readonly IExcelRepository _excelRepository;

    public ExcelService(IExcelRepository excelRepository)
    {
        _excelRepository = excelRepository ?? throw new ArgumentNullException(nameof(excelRepository));
    }

    public async Task<bool> ProcessExcelAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return false;

        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);

            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null || worksheet.Dimension == null)
                return false;

            var products = new List<ExcelProductDto>();
            int totalRows = worksheet.Dimension.End.Row;

            for (int row = 2; row <= totalRows; row++)
            {
                var name = worksheet.Cells[row, 1].Text?.Trim();
                if (string.IsNullOrEmpty(name)) continue;

                // Parse Price
                decimal price = 0;
                if (!decimal.TryParse(worksheet.Cells[row, 2].Text?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out price))
                {
                    price = 0; // o lanzar excepción si quieres forzar que sea válido
                }

                // Parse Quantity
                int quantity = 0;
                if (!int.TryParse(worksheet.Cells[row, 3].Text?.Trim(), out quantity))
                {
                    quantity = 0;
                }

                var category = worksheet.Cells[row, 4].Text?.Trim() ?? string.Empty;

                products.Add(new ExcelProductDto
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                });
            }

            if (products.Count == 0) return false;

            await _excelRepository.SaveProductsFromExcelAsync(products);

            return true;
        }
        catch (Exception ex)
        {
            // Aquí puedes loguear la excepción si tienes ILogger
            Console.WriteLine($"Error procesando Excel: {ex.Message}");
            return false;
        }
    }
}
