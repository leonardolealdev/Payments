using Payments.Domain.Enum;

namespace Payments.Domain.Responses
{
    public class StatePaymentReportResponse
    {
        public string State { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int TotalCount { get; set; }
    }
}
