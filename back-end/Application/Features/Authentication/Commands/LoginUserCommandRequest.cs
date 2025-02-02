using back_end.Application.Features.Authentication.Dtos;
using MediatR;

namespace back_end.Application.Features.Authentication.Commands
{
    public class LoginUserCommandRequest:IRequest<string>
    {
        public LoginDto LoginRequest { get; set; }
    }
}
