using System.ComponentModel.DataAnnotations;

namespace Firmezaa.Web.ViewModels.Users
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be 3-100 characters.")]
        public string? FullName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Document number is too long.")]
        public string? DocumentNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        [RegularExpression(@"^.+@.+\..+$", ErrorMessage = "Email must have @ and a domain.")]
        [StringLength(100, ErrorMessage = "Email is too long.")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Phone number is not valid.")]
        [StringLength(15, ErrorMessage = "Phone number is too long.")]
        public string? Phone { get; set; }

        public DateTime RegisterDate { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Username is too long.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 120, ErrorMessage = "Age must be 18-120.")]
        public int Age { get; set; }
    }
}
