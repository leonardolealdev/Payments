using MediatR;

namespace Payments.Domain.Commands
{
    public class DeletePaymentCommand : IRequest<GenericCommand>
    {
        public long Id { get; set; }
    }
}
