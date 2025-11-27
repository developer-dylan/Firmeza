using Firmezaa.Api.Data.Entities;

namespace Firmezaa.Api.Repositories.Interfaces;

public interface ISaleRepository
{
    Task<List<Sale>> GetAllAsync();
    Task<Sale?> GetByIdAsync(int id);
    Task AddAsync(Sale sale);
    Task UpdateAsync(Sale sale);
    Task DeleteAsync(int id);
}