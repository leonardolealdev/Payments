using MediatR;
using Payments.Domain.Responses;

namespace Payments.Domain.Query
{
    public class GetUserQuery : IRequest<IEnumerable<UserResponse>>
    {
    }
}
