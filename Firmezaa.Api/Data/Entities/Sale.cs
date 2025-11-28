using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace Firmezaa.Api.Data.Entities;

public class Sale
{
    [Key]
    public int Id { get; set; } 

    [Required(ErrorMessage = "La fecha de venta es obligatoria")]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "El Id del cliente es obligatorio")]
    public string ClientId { get; set; }
    public IdentityUser Client { get; set; }

    public ICollection<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();
}