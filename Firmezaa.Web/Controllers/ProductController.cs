using Firmezaa.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Firmezaa.Web.Models.Entities;

namespace Firmezaa.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            return View(products);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetProductById(id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Price,Stock,Category")] Product product)
        {
            if (!ModelState.IsValid) return View(product);

            product.CreatedAt = DateTime.UtcNow;

            try
            {
                await _productService.AddProduct(product);
                TempData["Success"] = "Product created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error creating product: {ex.Message}");
                return View(product);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetProductById(id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Stock,Category")] Product product)
        {
            if (id != product.Id) return NotFound();
            if (!ModelState.IsValid) return View(product);

            try
            {
                await _productService.UpdateProduct(product);
                TempData["Success"] = "Product updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(product.Id))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating product: {ex.Message}");
                return View(product);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _productService.GetProductById(id.Value);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                TempData["Success"] = "Product deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error deleting product: {ex.Message}");
                var product = await _productService.GetProductById(id);
                return View("Delete", product);
            }
        }

        private async Task<bool> ProductExists(int id)
        {
            var product = await _productService.GetProductById(id);
            return product != null;
        }
    }
}
