using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Firmezaa.Web.Models.Entities;

public class User : IdentityUser
{
    [MaxLength(100)]
    public string? FullName { get; set; }

    [MaxLength(50)]
    public string? DocumentNumber { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }
}
