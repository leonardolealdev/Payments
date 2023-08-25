using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Payments.Domain.Commands
{
    public class RegisterUserCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }
}
