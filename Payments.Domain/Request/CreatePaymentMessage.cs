using Payments.Domain.Enum;

namespace Payments.Domain.Request
{
    public class CreatePaymentMessage
    {
        public string ContractNumber { get; set; }
        public int Quota { get; set; }
        public decimal Value { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
