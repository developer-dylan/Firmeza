using Firmezaa.Web.Models.Entities;
namespace Firmezaa.Web.Services.Interfaces;

public interface IProductService
{
    Task<List<Product>> GetAllProducts();
    Task<Product?> GetProductById(int id);
    Task AddProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(int id);
}
