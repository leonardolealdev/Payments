using MediatR;
using Payments.Domain.Commands;

namespace Payments.Domain.Query
{
    public class StatePaymentReportQuery : IRequest<GenericCommand>
    {
    }
}
