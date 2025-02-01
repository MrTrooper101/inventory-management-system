using back_end.Application.Interfaces;
using back_end.Application.Features.Authentication.Dtos;
using back_end.Application.Features.Authentication;
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
        public async Task<IActionResult> Register(RegisterUserCommandRequest command)
        {
            bool result = await _mediator.Send(command);
            if (result)
                return Ok("User registered successfully.");
            else
                return BadRequest("User registration failed.");
        }

        [HttpPost("password-setup")]
        public async Task<IActionResult> PasswordSetup(PasswordSetupCommandRequest command)
        {
            bool result = await _mediator.Send(command);
            if (result)
                return Ok("Password set successfully.");
            else
                return BadRequest("Password setup failed.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommandRequest command)
        {
            bool token = await _mediator.Send(command);
            if (token)
                return Ok(new { token });
            else
                return BadRequest("User login failed.");
        }
    }

}

