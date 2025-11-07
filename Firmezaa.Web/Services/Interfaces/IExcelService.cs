using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Firmezaa.Web.Services.Interfaces;

public interface IExcelService
{ 
    Task<bool> ProcessExcelAsync(IFormFile file);
}