using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firmezaa.Web.Models.Entities;

public class Sale
{
    [Key]
    public int Id { get; set; }

    // Relación con Client
    [Required]
    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal IVA { get; set; }

    [MaxLength(50)]
    public string? SaleType { get; set; } // Ejemplo: "Venta", "Renta"

    // Relación con SaleDetails
    public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}