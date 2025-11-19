using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firmezaa.Web.Models.Entities;

public class Sale
{
    [Key]
    public int Id { get; set; }

    // Relación con el usuario de Identity
    [Required]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal IVA { get; set; }

    [MaxLength(50)]
    public string? SaleType { get; set; } // Venta, Renta

    public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
