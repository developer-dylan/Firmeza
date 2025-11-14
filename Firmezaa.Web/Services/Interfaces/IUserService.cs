using Firmezaa.Web.Models.Entities;

namespace Firmezaa.Web.Services.Interfaces
{
    public interface IUserService
    {
       Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
        Task CreateWithPasswordAsync(User user, string password, string role); 
    }
}