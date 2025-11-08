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
        public async Task<IActionResult> Create([Bind("Name,Price,Quantity,Category")] Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            try
            {
                product.CreatedAt = DateTime.UtcNow;

                await _productService.AddProduct(product);

                TempData["Success"] = "Product created successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the product.");
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
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the product.");
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

                TempData["Success"] = "Product deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the product.");
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
