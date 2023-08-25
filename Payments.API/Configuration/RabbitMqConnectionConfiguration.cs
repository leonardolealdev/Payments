using System.Diagnostics.CodeAnalysis;

namespace Payments.API.Configuration
{
    [ExcludeFromCodeCoverage]
    public class RabbitMqConnectionConfiguration
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
