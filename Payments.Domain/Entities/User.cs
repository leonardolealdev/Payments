using Microsoft.AspNetCore.Identity;

namespace Payments.Domain.Entities
{
    public class User : IdentityUser<long>
    {
        public string Name { get; set; }
    }
}
