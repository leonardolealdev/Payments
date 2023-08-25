using Payments.API.Messaging;
using Payments.Domain.Interfaces.Messaging;
using System.Diagnostics.CodeAnalysis;

namespace Payments.API.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class QueueExtension
    {
        public static IServiceCollection RegisterQueues(this IServiceCollection services)
        {
            services.AddScoped<IBaseQueue, BaseQueue>();
            services.AddScoped<ICreateClientQueue, CreateClientQueue>();
            services.AddScoped<ICreatePaymentQueue, CreatePaymentQueue>();

            return services;
        }
    }
}
