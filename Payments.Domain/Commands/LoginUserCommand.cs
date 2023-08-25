using MediatR;

namespace Payments.Domain.Commands
{
    public class LoginUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
