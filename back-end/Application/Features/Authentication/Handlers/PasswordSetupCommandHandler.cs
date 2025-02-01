using back_end.Application.Features.Authentication.Commands;
using back_end.Application.Features.Authentication.Dtos;
using back_end.Application.Interfaces;
using MediatR;

namespace back_end.Application.Features.Authentication.Handlers
{
    public class PasswordSetupCommandHandler : IRequestHandler<PasswordSetupCommandRequest, bool>
    {
        private readonly IUserRepository _userRepository;

        public PasswordSetupCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(PasswordSetupCommandRequest request, CancellationToken cancellationToken)
        {
            var passwordSetup = new SetPasswordDto
            {
                Token = request.PasswordSetupRequest.Token,
                NewPassword = request.PasswordSetupRequest.NewPassword,
            };

            await _userRepository.SetPasswordAsync(passwordSetup);

            return true;
        }
    }
}