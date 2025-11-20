using Firmezaa.Web.Data;
using Firmezaa.Web.Models.Entities;
using Firmezaa.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Firmezaa.Web.Repositories.Implementations;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddSale(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSale(int id)
    {
        var sale = await _context.Sales.FindAsync(id);
        if (sale != null)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Sale>> GetAllSales()
    {
        return await _context.Sales
            .Include(x => x.User)
            .Include(x => x.SaleDetails)
            .ToListAsync();
    }

    public async Task<Sale?> GetSaleById(int id)
    {
        return await _context.Sales
            .Include(x => x.User)
            .Include(x => x.SaleDetails)
            .ThenInclude(s => s.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateSale(Sale sale)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync();
    }
}
