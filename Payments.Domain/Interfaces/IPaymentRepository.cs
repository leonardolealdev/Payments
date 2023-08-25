using Payments.Domain.Entities;
using Payments.Domain.Responses;

namespace Payments.Domain.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<long> UpdatePayment(Payment payment);
        Task<Payment> GetById(long id);
        Task<decimal> GetTotalOpenAmount(string contractNumer);
        Task<TotalPaymentResponse> TotalPayment();
        Task<List<StatePaymentSummary>> GetStatePaymentSummaries();
        Task<List<StatePaymentReportResponse>> GetStatePaymentReports();
        Task<List<StateGrossIncomeReportResponse>> GetStateGrossIncomeReports();
    }
}
