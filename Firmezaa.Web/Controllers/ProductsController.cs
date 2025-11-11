using Firmezaa.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firmezaa.Web.Models.Entities;

namespace Firmezaa.Web.Controllers
{
    [Authorize(Roles = "Administrador,Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _productService.GetProductById(id.Value);
            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Quantity,Category")] Product product, string? localTime)
        {
            if (!ModelState.IsValid)
                return View(product);

            try
            {
                // ✅ Si el navegador envía la hora local del dispositivo, la usamos
                if (!string.IsNullOrEmpty(localTime))
                {
                    // Convertimos la hora local del dispositivo a UTC para guardar de forma coherente
                    product.CreatedAt = DateTime.Parse(localTime).ToUniversalTime();
                }
                else
                {
                    // Por compatibilidad: si no se envió, usamos la hora del servidor en UTC
                    product.CreatedAt = DateTime.UtcNow;
                }

                await _productService.AddProduct(product);

                TempData["Success"] = "✅ Producto creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "❌ Ocurrió un error al crear el producto.");
                return View(product);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _productService.GetProductById(id.Value);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Quantity,Category,CreatedAt")] Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(product);

            try
            {
                await _productService.UpdateProduct(product);

                TempData["Success"] = "✅ Producto actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(product.Id))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "❌ Ocurrió un error al actualizar el producto.");
                return View(product);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _productService.GetProductById(id.Value);
            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);

                TempData["Success"] = "✅ Producto eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "❌ Ocurrió un error al eliminar el producto.");
                var product = await _productService.GetProductById(id);
                return View(product);
            }
        }

        private async Task<bool> ProductExists(int id)
        {
            var product = await _productService.GetProductById(id);
            return product != null;
        }
    }
}
