using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firmezaa.Web.Models.Entities;

public class SaleDetail
{
    [Key]
    public int Id { get; set; }

    // Relación con Sale
    [Required]
    public int SaleId { get; set; }
    public Sale Sale { get; set; } = null!;

    // Relación con Product
    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }
}