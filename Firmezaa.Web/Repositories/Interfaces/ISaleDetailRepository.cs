using Firmezaa.Web.Models.Entities;

namespace Firmezaa.Web.Repositories.Interfaces;

public interface ISaleDetailRepository
{
    Task<List<SaleDetail>> GetAllSaleDetails();
    Task<SaleDetail?> GetSaleDetailById(int id);
    Task AddSaleDetail(SaleDetail detail);
    Task UpdateSaleDetail(SaleDetail detail);
    Task DeleteSaleDetail(int id);
}
