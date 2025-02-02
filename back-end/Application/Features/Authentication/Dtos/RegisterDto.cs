using System.ComponentModel.DataAnnotations;

namespace back_end.Application.Features.Authentication.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\+?[1-9]\d{1,10}$", ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Company name is required.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
    }
}
