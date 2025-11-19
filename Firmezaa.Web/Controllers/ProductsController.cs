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

        // LIST ALL PRODUCTS
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            return View(products);
        }

        // DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _productService.GetProductById(id.Value);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Quantity,Category")] Product product, string? localTime)
        {
            if (!ModelState.IsValid)
                return View(product);

            try
            {
                // Hora del dispositivo si viene
                if (!string.IsNullOrEmpty(localTime))
                {
                    product.CreatedAt = DateTime.Parse(localTime).ToUniversalTime();
                }
                else
                {
                    product.CreatedAt = DateTime.UtcNow;
                }

                // Primera actualización = creación
                product.UpdatedAt = product.CreatedAt;

                await _productService.AddProduct(product);

                TempData["Success"] = "Producto creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al crear el producto.");
                return View(product);
            }
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _productService.GetProductById(id.Value);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // EDIT (POST) — ACTUALIZADO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Quantity,Category")] Product updatedProduct)
        {
            if (id != updatedProduct.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(updatedProduct);

            try
            {
                var existingProduct = await _productService.GetProductById(id);
                if (existingProduct == null)
                    return NotFound();

                // Campos editables
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Quantity = updatedProduct.Quantity;
                existingProduct.Category = updatedProduct.Category;

                // NO tocar CreatedAt
                // Actualizar UpdatedAt
                existingProduct.UpdatedAt = DateTime.UtcNow;

                await _productService.UpdateProduct(existingProduct);

                TempData["Success"] = "Producto actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id))
                    return NotFound();

                throw;
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al actualizar el producto.");
                return View(updatedProduct);
            }
        }

        // DELETE (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _productService.GetProductById(id.Value);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);

                TempData["Success"] = "Producto eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al eliminar el producto.");

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
