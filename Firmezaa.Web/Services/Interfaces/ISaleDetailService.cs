using Firmezaa.Web.Models.Entities;

namespace Firmezaa.Web.Services.Interfaces;

public interface ISaleDetailService
{
    Task<List<SaleDetail>> GetAllDetails();
    Task<SaleDetail?> GetDetailById(int id);
    Task AddDetail(SaleDetail detail);
    Task UpdateDetail(SaleDetail detail);
    Task DeleteDetail(int id);
}
