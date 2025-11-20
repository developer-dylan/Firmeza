using System.ComponentModel.DataAnnotations;
using Firmezaa.Web.Models.Entities;

namespace Firmezaa.Web.ViewModels.Sales
{
    public class SaleCreateViewModel
    {
        [Required(ErrorMessage = "You must select a user.")]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must add at least one product.")]
        [MinLength(1, ErrorMessage = "You must add at least one product to the sale.")]
        public List<SaleProductItem> Items { get; set; } = new();

        [Required(ErrorMessage = "IVA is required.")]
        [Range(0, 999999.99, ErrorMessage = "IVA cannot be negative.")]
        public decimal IVA { get; set; }

        [Required(ErrorMessage = "Total amount is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "Total must be greater than 0.")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Sale type is required.")]
        [StringLength(20, ErrorMessage = "Sale type cannot exceed 20 characters.")]
        public string SaleType { get; set; } = string.Empty;

        // Dropdown helper data
        public List<User>? Users { get; set; }
        public List<Product>? Products { get; set; }
    }

    public class SaleProductItem
    {
        [Required(ErrorMessage = "You must select a product.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100000, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }
    }
}
