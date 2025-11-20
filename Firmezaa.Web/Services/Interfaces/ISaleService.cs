using Firmezaa.Web.Models.Entities;

namespace Firmezaa.Web.Services.Interfaces;

public interface ISaleService
{
    Task<List<Sale>> GetAllSales();
    Task<Sale?> GetSaleById(int id);
    Task AddSale(Sale sale);
    Task UpdateSale(Sale sale);
    Task DeleteSale(int id);
}
