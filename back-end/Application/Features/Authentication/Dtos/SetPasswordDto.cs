using System.ComponentModel.DataAnnotations;

namespace back_end.Application.Features.Authentication.Dtos
{
    public class SetPasswordDto
    {
        [Required(ErrorMessage = "Token is required.")]
        public string Token { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, and one number.")]
        public string NewPassword { get; set; }
    }
}
