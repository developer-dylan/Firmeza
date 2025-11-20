namespace Firmezaa.Web.ViewModels.Sales
{
    public class SaleListItemViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal IVA { get; set; }
    }
}
