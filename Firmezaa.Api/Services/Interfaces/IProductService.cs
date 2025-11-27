using Firmezaa.Api.DTOs.Product;
using Firmezaa.Api.Responses;
 
namespace Firmezaa.Api.Services.Interfaces;

public interface IProductService
{
    Task<ApiResponse<object>> GetAllAsync();
    Task<ApiResponse<object>> GetByIdAsync(int id);
    Task<ApiResponse<object>> CreateAsync(ProductCreateDto dto);
    Task<ApiResponse<object>> UpdateAsync(int id, ProductUpdateDto dto);
    Task<ApiResponse<object>> DeleteAsync(int id);
}