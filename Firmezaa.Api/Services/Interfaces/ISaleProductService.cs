using Firmezaa.Api.Data.Entities;
using Firmezaa.Api.Responses;
using Firmezaa.Api.DTOs.SaleProduct;

namespace Firmezaa.Api.Services.Interfaces;

public interface ISaleProductService
{
    Task<ApiResponse<List<SaleProduct>>> GetAllAsync();
    Task<ApiResponse<SaleProduct?>> GetByIdAsync(int id);
    Task<ApiResponse<SaleProduct?>> CreateAsync(SaleProductCreateDto request);
    Task<ApiResponse<SaleProduct?>> UpdateAsync(int id, SaleProductUpdateDto request);
    Task<ApiResponse<string>> DeleteAsync(int id);
}