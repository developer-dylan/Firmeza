using Firmezaa.Web.Data;
using Firmezaa.Web.Models.Entities;
using Firmezaa.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Firmezaa.Web.Repositories.Implementations;

public class SaleDetailRepository : ISaleDetailRepository
{
    private readonly AppDbContext _context;

    public SaleDetailRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddSaleDetail(SaleDetail detail)
    {
        await _context.SaleDetails.AddAsync(detail);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSaleDetail(int id)
    {
        var detail = await _context.SaleDetails.FindAsync(id);
        if (detail != null)
        {
            _context.SaleDetails.Remove(detail);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<SaleDetail>> GetAllSaleDetails()
    {
        return await _context.SaleDetails
            .Include(x => x.Product)
            .Include(x => x.Sale)
            .ToListAsync();
    }

    public async Task<SaleDetail?> GetSaleDetailById(int id)
    {
        return await _context.SaleDetails
            .Include(x => x.Product)
            .Include(x => x.Sale)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateSaleDetail(SaleDetail detail)
    {
        _context.SaleDetails.Update(detail);
        await _context.SaveChangesAsync();
    }
}
