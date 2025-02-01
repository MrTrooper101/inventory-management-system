using back_end.Application.Features.Authentication.Commands;
using back_end.Application.Features.Authentication.Dtos;
using back_end.Application.Interfaces;
using MediatR;

namespace back_end.Application.Features.Authentication.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, bool>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<bool> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var loginUser = new LoginDto
            {
                Email = request.LoginRequest.Email,
                Password = request.LoginRequest.Password,
            };

            await _userRepository.LoginUserAsync(loginUser);
            return true;
        }
    }
}