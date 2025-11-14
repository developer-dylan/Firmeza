using Firmezaa.Web.Models.Entities;

namespace Firmezaa.Web.Repositories.Interfaces
{
    public interface IUserRepository
    {
       Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<bool> ExistsAsync(string id); 
    }
}