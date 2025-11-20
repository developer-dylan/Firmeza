using System.ComponentModel.DataAnnotations;

namespace Firmezaa.Web.ViewModels;

public class EditProductViewModel : CreateProductViewModel
{
    [Required]
    public int Id { get; set; }
    
}
