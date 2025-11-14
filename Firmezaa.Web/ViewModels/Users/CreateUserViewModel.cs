using System.ComponentModel.DataAnnotations;

namespace Firmezaa.Web.ViewModels.Users
{
    public class CreateUserViewModel 
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be 3-100 characters.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Document number is required.")]
        [StringLength(20, ErrorMessage = "Document number is too long.")]
        public string DocumentNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        [RegularExpression(@"^.+@.+\..+$", ErrorMessage = "Email must have @ and a domain.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Phone number is not valid.")]
        [StringLength(20, ErrorMessage = "Phone number is too long.")]
        public string Phone { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, ErrorMessage = "Username is too long.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 120, ErrorMessage = "Age must be 18-120.")]
        public int Age { get; set; }
    }
}
