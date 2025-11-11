using System.ComponentModel.DataAnnotations;

namespace Firmezaa.Web.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0.")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [MaxLength(50)]
        public string? Category { get; set; }

        [MaxLength(50)]
        public string? ProductType { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

        public int Quantity { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}