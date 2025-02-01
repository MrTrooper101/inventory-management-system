using back_end.Application.Features.Authentication.Dtos;
using MediatR;

namespace back_end.Application.Features.Authentication.Commands
{
    public class PasswordSetupCommandRequest:IRequest<bool>
    {
        public SetPasswordDto PasswordSetupRequest { get; set; }
    }
}
