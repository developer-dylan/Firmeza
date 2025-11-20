using Firmezaa.Web.Models.Entities;
using Firmezaa.Web.Repositories.Interfaces;
using Firmezaa.Web.Services.Interfaces;

namespace Firmezaa.Web.Services.Implementations;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _repo;

    public SaleService(ISaleRepository repo)
    {
        _repo = repo;
    }

    public async Task AddSale(Sale sale) => await _repo.AddSale(sale);

    public async Task DeleteSale(int id) => await _repo.DeleteSale(id);

    public async Task<List<Sale>> GetAllSales() => await _repo.GetAllSales();

    public async Task<Sale?> GetSaleById(int id) => await _repo.GetSaleById(id);

    public async Task UpdateSale(Sale sale) => await _repo.UpdateSale(sale);
}
