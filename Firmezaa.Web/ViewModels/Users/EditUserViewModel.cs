using System.ComponentModel.DataAnnotations;

namespace Firmezaa.Web.ViewModels.Users
{
    public class EditUserViewModel : CreateUserViewModel
    {
        [Required]
        public string? Id { get; set; }

    }
}
