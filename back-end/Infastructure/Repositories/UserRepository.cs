using back_end.Application.Interfaces;
using back_end.Domain.Entities;
using back_end.Infastructure.Persistence;
using back_end.Application.Features.Authentication.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using back_end.Infastructure.Services;

namespace back_end.Infastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserRepository(ApplicationDbContext dbContext, ILogger<UserRepository> logger, EmailService emailService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
            _emailService = emailService;
            _configuration = configuration;
        }


        public async Task<bool> IsEmailTaken(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> AddUserAsync(RegisterDto registerDto)
        {
            try
            {
                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == registerDto.Email);
                var verificationToken = Guid.NewGuid().ToString();

                if (existingUser != null)
                {
                    if (existingUser.IsEmailVerified)
                    {
                        _logger.LogWarning("User with email {Email} is already verified.", registerDto.Email);
                        return false;
                    }

                    existingUser.VerificationToken = verificationToken;
                    existingUser.TokenExpiration = DateTime.UtcNow.AddHours(24);
                    _dbContext.Users.Update(existingUser);
                }
                else
                {
                    var newUser = new User
                    {
                        FirstName = registerDto.FirstName,
                        MiddleName = registerDto.MiddleName,
                        LastName = registerDto.LastName,
                        Email = registerDto.Email,
                        PhoneNumber = registerDto.PhoneNumber,
                        CompanyName = registerDto.CompanyName,
                        Address = registerDto.Address,
                        IsEmailVerified = false,
                        VerificationToken = verificationToken,
                        TokenExpiration = DateTime.UtcNow.AddHours(24)
                    };

                    _dbContext.Users.Add(newUser);
                }

                await _dbContext.SaveChangesAsync();

                var verificationLink = $"{_configuration["App:BaseUrl"]}/password-setup?token={verificationToken}";
                await _emailService.SendVerificationEmailAsync(registerDto.Email, verificationLink);

                _logger.LogInformation("User registration completed for email: {Email}", registerDto.Email);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user with email: {Email}", registerDto.Email);
                throw;
            }
        }

        public async Task<bool> SetPasswordAsync(SetPasswordDto setPasswordDto)
        {
            try
            {
                var user = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.VerificationToken == setPasswordDto.Token);

                if (user == null)
                {
                    _logger.LogWarning("No user found with the provided verification token: {Token}", setPasswordDto.Token);
                    return false;
                }

                var passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, setPasswordDto.NewPassword);
                user.IsEmailVerified = true;
                user.VerificationToken = null;

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Password successfully set for user: {Email}", user.Email);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while setting the password for token: {Token}", setPasswordDto.Token);
                throw;
            }
        }


        public async Task<string> LoginUserAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

                if (user == null)
                {
                    _logger.LogWarning("No user found with the provided email: {Email}", loginDto.Email);
                    return null;
                }

                var passwordHasher = new PasswordHasher<User>();
                var verificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);

                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    _logger.LogWarning("Invalid password provided for user: {Email}", loginDto.Email);
                    return null;
                }

                _logger.LogInformation("User login successful: {Email}", user.Email);
                var token = GenerateJwtToken(user);
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for email: {Email}", loginDto.Email);
                throw;
            }
        }

        private string GenerateJwtToken(User user)
        {
            try
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                _logger.LogInformation("JWT token successfully generated for user: {Email}", user.Email);

                return jwt;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the JWT token for user: {Email}", user.Email);
                throw;
            }
        }
    }

}
