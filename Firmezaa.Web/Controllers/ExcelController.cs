using Firmezaa.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Firmezaa.Web.Controllers
{
    [Route("Excel/[action]")]
    public class ExcelController : Controller
    {
        private readonly IExcelService _excelService;

        public ExcelController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Por favor selecciona un archivo Excel válido.");
                return View();
            }

            // Validar extensión
            var extension = Path.GetExtension(file.FileName);
            if (extension != ".xlsx")
            {
                ModelState.AddModelError(string.Empty, "Solo se permiten archivos .xlsx.");
                return View();
            }

            try
            {
                var success = await _excelService.ProcessExcelAsync(file);

                if (success)
                    TempData["Success"] = "Datos cargados correctamente a la base de datos.";
                else
                    TempData["Error"] = "Ocurrió un error al procesar el archivo.";

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