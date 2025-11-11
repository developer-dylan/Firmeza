using Firmezaa.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Firmezaa.Web.DTOs;

namespace Firmezaa.Web.Controllers
{
    [Route("Excel/[action]")]
    public class ExcelController(IExcelService excelService) : Controller
    {
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile? file, string? localTime)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Por favor selecciona un archivo Excel válido.");
                return View();
            }

            var extension = Path.GetExtension(file.FileName);
            if (extension != ".xlsx")
            {
                ModelState.AddModelError(string.Empty, "Solo se permiten archivos .xlsx.");
                return View();
            }

            try
            {
                // ✅ Capturamos la hora local enviada desde el navegador (si existe)
                DateTime? userCreatedAt = null;
                if (!string.IsNullOrEmpty(localTime))
                {
                    // Parseamos la hora local recibida
                    userCreatedAt = DateTime.Parse(localTime).ToUniversalTime(); 
                }

                // ✅ Llamamos al servicio con la hora local del usuario
                ExcelImportResult result = await excelService.ProcessExcelAsync(file, userCreatedAt);

                if (result.Success)
                {
                    TempData["Success"] = $"✅ Se importaron {result.Imported} registros correctamente.";
                    if (result.Messages.Any())
                        TempData["Info"] = string.Join("<br>", result.Messages.Take(5));
                }
                else
                {
                    TempData["Error"] = $"❌ Se encontraron errores. {result.Imported} importados, {result.Errors} errores.";
                    if (result.Messages.Any())
                        TempData["Details"] = string.Join("<br>", result.Messages.Take(10));
                }

                return RedirectToAction("Upload");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al procesar el archivo: {ex.Message}");
                return View();
            }
        }
    }
}
