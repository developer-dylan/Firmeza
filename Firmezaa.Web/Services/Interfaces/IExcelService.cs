using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Firmezaa.Web.DTOs;

namespace Firmezaa.Web.Services.Interfaces
{
    public interface IExcelService
    { 
        Task<ExcelImportResult> ProcessExcelAsync(IFormFile file, DateTime? userCreatedAt = null);
    }
}