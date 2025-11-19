using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Firmezaa.Web.Models.Entities;

public class User : IdentityUser
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string DocumentNumber { get; set; } = string.Empty;

    [StringLength(15)]
    public string Phone { get; set; } = string.Empty;

    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

    [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
    public int? Age { get; set; }

}
