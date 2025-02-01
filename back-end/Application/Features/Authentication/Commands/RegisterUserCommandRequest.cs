using back_end.Application.Features.Authentication.Dtos;
using MediatR;

namespace back_end.Application.Features.Authentication.Commands
{
    public class RegisterUserCommandRequest : IRequest<bool>
    {
        public RegisterDto RegisterRequest { get; set; }
    }
}
