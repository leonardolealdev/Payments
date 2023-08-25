using Payments.Domain.Entities;
using Payments.Domain.Interfaces;

namespace Payments.Infra.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(PaymentDbContext ctx) : base(ctx) { }
    }
}
