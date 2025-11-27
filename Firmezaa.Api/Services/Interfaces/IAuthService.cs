using Firmezaa.Api.DTOs;
using Firmezaa.Api.Responses;

namespace Firmezaa.Api.Services.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<object>> LoginAsync(LoginDTO request);
    Task<ApiResponse<object>> RegisterAsync(RegisterDTO request);
}