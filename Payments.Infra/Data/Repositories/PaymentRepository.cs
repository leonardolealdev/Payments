using Microsoft.EntityFrameworkCore;
using Payments.Domain.Entities;
using Payments.Domain.Enum;
using Payments.Domain.Interfaces;
using Payments.Domain.Responses;

namespace Payments.Infra.Data.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(PaymentDbContext dbContext) : base(dbContext) { }

        public async Task<long> UpdatePayment(Payment payment)
        {
            var entityEntry = _dbContext.Entry(payment);
            entityEntry.State = EntityState.Modified;
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Payment> GetById(long id)
        {
            return await _dbContext.Payments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<decimal> GetTotalOpenAmount(string contractNumer)
        {
            return await _dbContext.Payments.Where(x => x.ContractNumber.Equals(contractNumer) &&
                                                        (x.PaymentStatus == PaymentStatus.Late || x.PaymentStatus == PaymentStatus.Due))
                                            .SumAsync(y => y.Value);
        }

        public async Task<TotalPaymentResponse> TotalPayment()
        {
            var result = new TotalPaymentResponse();
            result.TotalPayments = await _dbContext.Payments.CountAsync();
            result.TotalPaymentsLateOrDue = await _dbContext.Payments.Where(x => x.PaymentStatus == PaymentStatus.Late || x.PaymentStatus == PaymentStatus.Due).CountAsync();
            return result;
        }

        public async Task<List<StatePaymentSummary>> GetStatePaymentSummaries()
        {
            var summaries = await _dbContext.Clients
                .GroupJoin(
                    _dbContext.Payments,
                    client => client.ContractNumber,
                    payment => payment.ContractNumber,
                    (client, payments) => new { client.State, Payments = payments }
                )
                .Select(result => new StatePaymentSummary
                {
                    State = result.State,
                    TotalPayments = result.Payments.Count()
                })
                .ToListAsync();

            return summaries;
        }

        public async Task<List<StatePaymentReportResponse>> GetStatePaymentReports()
        {
            var reports = await _dbContext.Clients
                .GroupJoin(
                    _dbContext.Payments,
                    client => client.ContractNumber,
                    payment => payment.ContractNumber,
                    (client, payments) => new { client.State, Payments = payments }
                )
                .SelectMany(result => result.Payments,
                    (clientData, payment) => new { clientData.State, payment.PaymentStatus })
                .GroupBy(
                    data => new { data.State, data.PaymentStatus },
                    (key, group) => new StatePaymentReportResponse
                    {
                        State = key.State,
                        PaymentStatus = key.PaymentStatus,
                        TotalCount = group.Count()
                    })
                .ToListAsync();

            return reports;
        }

        public async Task<List<StateGrossIncomeReportResponse>> GetStateGrossIncomeReports()
        {
            var reports = _dbContext.Payments
            .GroupJoin(
                _dbContext.Clients,
                payment => payment.ContractNumber,
                client => client.ContractNumber,
                (payment, clients) => new { payment.PaymentStatus, Clients = clients }
            )
            .SelectMany(result => result.Clients,
                (paymentData, client) => new { paymentData.PaymentStatus, client.GrossIncome })
            .GroupBy(
                data => data.PaymentStatus,
                (key, group) => new StateGrossIncomeReportResponse
                {
                    PaymentStatus = key,
                    AverageGrossIncome = group.Average(data => (double)data.GrossIncome)
                })
            .ToList();

            return reports;
        }
    }
}
