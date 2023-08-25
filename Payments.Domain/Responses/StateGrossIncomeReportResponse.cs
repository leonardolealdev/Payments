using Payments.Domain.Enum;

namespace Payments.Domain.Responses
{
    public class StateGrossIncomeReportResponse
    {
        public PaymentStatus PaymentStatus { get; set; }
        public double AverageGrossIncome { get; set; }
    }
}
