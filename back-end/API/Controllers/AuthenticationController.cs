using back_end.Application.Features.Authentication.Dtos;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using back_end.Application.Features.Authentication.Commands;

namespace back_end.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            bool result = await _mediator.Send(new RegisterUserCommandRequest { RegisterRequest = registerDto });
            if (result)
                return Ok("User registered successfully.");
            else
                return BadRequest("User registration failed.");
        }

        [HttpPost("password-setup")]
        public async Task<IActionResult> PasswordSetup(SetPasswordDto setPasswordDto)
        {
            bool result = await _mediator.Send(new PasswordSetupCommandRequest { PasswordSetupRequest = setPasswordDto });
            if (result)
                return Ok("Password set successfully.");
            else
                return BadRequest("Password setup failed.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            string token = await _mediator.Send(new LoginUserCommandRequest { LoginRequest = loginDto });
            if (token == "")
                return BadRequest("User login failed.");
            else
                return Ok(new { token });
        }
    }

}

