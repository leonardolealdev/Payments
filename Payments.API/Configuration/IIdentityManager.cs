
using Payments.Domain.Responses;

namespace Payments.API.Configuration
{
    public interface IIdentityManager
    {
        Task<LoginResponse> GenerateJwt(string email);
    }
}
