using MediatR;
using Payments.Domain.Enum;

namespace Payments.Domain.Commands
{
    public class UpdatePaymentCommand : IRequest<GenericCommand>
    {
        public long Id { get; set; }
        public string ContractNumber { get; set; }
        public int Quota { get; set; }
        public decimal Value { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
