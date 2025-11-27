using Firmeza.Api.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Firmezaa.Api.Services.Interfaces;

public interface IEmailService
{
    bool SendAccountCreated(IdentityUser user);
    bool SendPurcharseConfirmation(String email);
}