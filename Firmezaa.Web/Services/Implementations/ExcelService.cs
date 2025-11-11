using Firmezaa.Web.DTOs;
using System.Globalization;
using Firmezaa.Web.Services.Interfaces;
using Firmezaa.Web.Repositories.Interfaces;
using OfficeOpenXml;

namespace Firmezaa.Web.Services.Implementations
{
    public class ExcelService(IExcelRepository excelRepository) : IExcelService
    {
        private readonly IExcelRepository _excelRepository = excelRepository ?? throw new ArgumentNullException(nameof(excelRepository));

        public async Task<ExcelImportResult> ProcessExcelAsync(IFormFile? file, DateTime? userCreatedAt = null)
        {
            var result = new ExcelImportResult();

            if (file == null || file.Length == 0)
            {
                result.Errors++;
                result.Messages.Add("El archivo está vacío o no se proporcionó.");
                return result;
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                using var package = new ExcelPackage(stream);

                foreach (var worksheet in package.Workbook.Worksheets)
                {
                    if (worksheet?.Dimension == null) continue;

                    int totalRows = worksheet.Dimension.End.Row;
                    int totalCols = worksheet.Dimension.End.Column;
                    result.TotalRows += totalRows - 1;

                    // Detectar encabezados
                    var headers = new Dictionary<int, string>();
                    for (int c = 1; c <= totalCols; c++)
                    {
                        var header = worksheet.Cells[1, c].Text?.Trim().ToLowerInvariant();
                        if (!string.IsNullOrEmpty(header))
                            headers[c] = header;
                    }

                    var products = new List<ExcelProductDto>();

                    for (int row = 2; row <= totalRows; row++)
                    {
                        try
                        {
                            string? name = FindValue(worksheet, headers, row, new[] { "nombre", "producto", "name" });
                            string? priceText = FindValue(worksheet, headers, row, new[] { "precio", "price" });
                            string? qtyText = FindValue(worksheet, headers, row, new[] { "cantidad", "qty", "quantity" });

                            if (string.IsNullOrWhiteSpace(name))
                            {
                                result.Errors++;
                                result.Messages.Add($"Hoja {worksheet.Name}, fila {row}: nombre vacío o inválido.");
                                continue;
                            }

                            if (!decimal.TryParse(priceText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
                            {
                                result.Errors++;
                                result.Messages.Add($"Hoja {worksheet.Name}, fila {row}: precio inválido '{priceText}'.");
                                continue;
                            }

                            int.TryParse(qtyText, out int quantity);

                            products.Add(new ExcelProductDto
                            {
                                Name = name,
                                Price = price,
                                Quantity = quantity
                            });
                        }
                        catch (Exception exRow)
                        {
                            result.Errors++;
                            result.Messages.Add($"Hoja {worksheet.Name}, fila {row}: {exRow.Message}");
                        }
                    }

                    if (products.Any())
                    {
                        // ✅ Pasamos la hora local del usuario (si existe)
                        await _excelRepository.SaveProductsFromExcelAsync(products, userCreatedAt);
                        result.Imported += products.Count;
                    }
                }

                if (result.Imported == 0)
                {
                    result.Errors++;
                    result.Messages.Add("No se importaron productos válidos del archivo Excel.");
                }
            }
            catch (Exception ex)
            {
                result.Errors++;
                result.Messages.Add($"Error procesando Excel: {ex.Message}");
            }

            return result;
        }

        private static string? FindValue(ExcelWorksheet sheet, Dictionary<int, string> headers, int row, string[] candidates)
        {
            foreach (var h in headers)
            {
                if (candidates.Any(c => h.Value.Contains(c)))
                    return sheet.Cells[row, h.Key].Text?.Trim();
            }
            return null;
        }
    }
}
