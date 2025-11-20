using Firmezaa.Web.Models.Entities;

namespace Firmezaa.Web.ViewModels.Sales
{
    public class SaleDetailsViewModel
    {
        public Sale Sale { get; set; } = null!;
        public List<SaleDetail> Details { get; set; } = new();
    }
}
