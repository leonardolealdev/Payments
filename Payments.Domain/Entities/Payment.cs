using Payments.Domain.Commands;
using Payments.Domain.Enum;

namespace Payments.Domain.Entities
{
    public class Payment : EntityBase
    {
        public Payment(string contractNumber, int quota, decimal value, PaymentStatus paymentStatus) 
        {
            ContractNumber = contractNumber;
            Quota = quota;
            Value = value;
            PaymentStatus = paymentStatus;
        }

        public string ContractNumber { get; private set; }
        public int Quota { get; private set; }
        public decimal Value { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }

        public void Update(UpdatePaymentCommand request)
        {
            ContractNumber= request.ContractNumber;
            Quota= request.Quota;
            Value= request.Value;
            PaymentStatus= request.PaymentStatus;
        }
    }
}
