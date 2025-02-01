using back_end.Application.Features.Authentication.Dtos;
using back_end.Domain.Entities;

namespace back_end.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsEmailTaken(string email);
        Task<bool> AddUserAsync(RegisterDto registerDto);
        Task<bool> SetPasswordAsync(SetPasswordDto setPasswordDto);
        Task<string> LoginUserAsync(LoginDto loginDto);
    }
}
