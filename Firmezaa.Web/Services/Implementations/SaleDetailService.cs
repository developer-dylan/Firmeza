using Firmezaa.Web.Models.Entities;
using Firmezaa.Web.Repositories.Interfaces;
using Firmezaa.Web.Services.Interfaces;

namespace Firmezaa.Web.Services.Implementations;

public class SaleDetailService : ISaleDetailService
{
    private readonly ISaleDetailRepository _repo;

    public SaleDetailService(ISaleDetailRepository repo)
    {
        _repo = repo;
    }

    public async Task AddDetail(SaleDetail detail) => await _repo.AddSaleDetail(detail);

    public async Task DeleteDetail(int id) => await _repo.DeleteSaleDetail(id);

    public async Task<List<SaleDetail>> GetAllDetails() => await _repo.GetAllSaleDetails();

    public async Task<SaleDetail?> GetDetailById(int id) => await _repo.GetSaleDetailById(id);

    public async Task UpdateDetail(SaleDetail detail) => await _repo.UpdateSaleDetail(detail);
}
