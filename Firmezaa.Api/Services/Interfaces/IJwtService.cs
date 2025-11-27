using Microsoft.AspNetCore.Identity;
 
namespace Firmezaa.Api.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(IdentityUser user, IList<string> roles);
}