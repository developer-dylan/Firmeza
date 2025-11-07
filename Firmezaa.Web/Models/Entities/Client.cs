using System.ComponentModel.DataAnnotations;

namespace Firmezaa.Web.Models.Entities;

public class Client : Person
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    // Relación con ventas
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
