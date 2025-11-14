using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Firmezaa.Web.Models.Entities;

public class User : IdentityUser
{
    [MaxLength(100)]
    public string? FullName { get; set; }

    [MaxLength(50)]
    public string? DocumentNumber { get; set; }

    [Range(0, 150, ErrorMessage = "La edad debe estar entre 0 y 150 años.")]
    public int Age { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    
}
