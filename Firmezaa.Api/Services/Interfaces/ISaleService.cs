namespace Firmezaa.Api.Services.Interfaces;

public interface ISaleService
{
    Task<ApiResponse<object>> GetAllAsync();
    Task<ApiResponse<object>> GetByIdAsync(int id);
    Task<ApiResponse<object>> CreateAsync(SaleCreateDto request);
    Task<ApiResponse<object>> UpdateAsync(int id, SaleUpdateDto request);
    Task<ApiResponse<object>> DeleteAsync(int id);
}