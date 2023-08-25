using MediatR;
using Microsoft.AspNetCore.Identity;
using Payments.Domain.Commands;
using Payments.Domain.Entities;
using Payments.Domain.Interfaces;
using Payments.Domain.Query;
using Payments.Domain.Responses;

namespace Payments.Domain.Handlers
{
    public class UserCommandHandler :
        IRequestHandler<GetUserQuery, IEnumerable<UserResponse>>,
        IRequestHandler<LoginUserCommand, bool>,
        IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IUserRepository _repository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public UserCommandHandler(
            IUserRepository repository,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _repository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _repository.GetAllAsync();

            var result = usuarios.AsQueryable().Select(p => new UserResponse
            {
                Id = p.Id,
                Name = p.Name
            });

            return result;
        }

        public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return true;
            }

            return false;
        }
    }
}
