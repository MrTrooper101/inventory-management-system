using back_end.Application.Features.Authentication.Commands;
using back_end.Application.Features.Authentication.Dtos;
using back_end.Application.Interfaces;
using MediatR;

namespace back_end.Application.Features.Authentication.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, string>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }

        public async Task<string> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var loginUser = new LoginDto
            {
                Email = request.LoginRequest.Email,
                Password = request.LoginRequest.Password,
            };

            var token = await _userRepository.LoginUserAsync(loginUser);
            return token;
        }
    }
}