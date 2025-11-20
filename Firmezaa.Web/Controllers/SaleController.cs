using Firmezaa.Web.Data;
using Firmezaa.Web.Models.Entities;
using Firmezaa.Web.ViewModels.Sales;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firmezaa.Web.Controllers
{
    public class SalesController : Controller
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Sales
        public async Task<IActionResult> Index()
        {
            var sales = await _context.Sales
                .Include(s => s.User)
                .Include(s => s.SaleDetails)
                .ThenInclude(d => d.Product)
                .ToListAsync();

            return View(sales);
        }

        // GET: /Sales/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var sale = await _context.Sales
                .Include(s => s.User)
                .Include(s => s.SaleDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return NotFound();

            return View(sale);
        }

        // GET: /Sales/Create
        public async Task<IActionResult> Create()
        {
            var vm = new SaleCreateViewModel
            {
                Users = await _context.Users.ToListAsync(),
                Products = await _context.Products.ToListAsync()
            };

            return View(vm);
        }

        // POST: /Sales/Create
        [HttpPost]
        public async Task<IActionResult> Create(SaleCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Users = await _context.Users.ToListAsync();
                model.Products = await _context.Products.ToListAsync();
                return View(model);
            }

            var sale = new Sale
            {
                UserId = model.UserId,
                Date = DateTime.Now,
                IVA = model.IVA,
                Total = model.Total
            };

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            foreach (var item in model.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);

                if (product == null) continue;

                var detail = new SaleDetail
                {
                    SaleId = sale.Id,
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    Subtotal = product.Price * item.Quantity
                };

                _context.SaleDetails.Add(detail);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
