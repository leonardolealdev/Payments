namespace Payments.Domain.Responses
{
    public class TotalPaymentResponse
    {
        public int TotalPayments { get; set; }
        public int TotalPaymentsLateOrDue { get; set; }
    }
}
