using Firmezaa.Api.Data.Entities;

namespace Firmezaa.Api.Repositories.Interfaces;

public interface ISaleProductRepository
{
    Task<List<SaleProduct>> GetAllAsync();
    Task<SaleProduct?> GetByIdAsync(int id);
    Task AddAsync(SaleProduct saleProduct);
    Task UpdateAsync(SaleProduct saleProduct);
    Task DeleteAsync(int id);
}