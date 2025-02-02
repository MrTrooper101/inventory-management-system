using back_end.Application.Interfaces;
using back_end.Application.Features.Authentication.Commands;
using MediatR;
using back_end.Domain.Entities;
using back_end.Infastructure.Services;
using back_end.Application.Features.Authentication.Dtos;

namespace back_end.Application.Features.Authentication.Handlers
{
    public class RegisterUserCommandHandler: IRequestHandler<RegisterUserCommandRequest, bool>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<bool> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            if (await _userRepository.IsEmailTaken(request.RegisterRequest.Email))
            {
                throw new Exception("Email is already taken.");
            }

            var newUser = new RegisterDto
            {
                FirstName = request.RegisterRequest.FirstName,
                MiddleName = request.RegisterRequest.MiddleName,
                LastName = request.RegisterRequest.LastName,
                Email = request.RegisterRequest.Email,
                PhoneNumber = request.RegisterRequest.PhoneNumber,
                CompanyName = request.RegisterRequest.CompanyName,
                Address = request.RegisterRequest.Address,
            };

            await _userRepository.AddUserAsync(newUser);

            return true;
        }
    }
}
