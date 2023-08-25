using FluentValidation;
using Payments.Domain.Commands;
using Payments.Domain.Interfaces;
using Payments.Infra.Data.Repositories;

namespace Payments.API.Configuration
{
    public static class DependencyInjection
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddScoped<IIdentityManager, IdentityManager>();

            services.AddTransient<IValidator<CreateClientCommand>, CreateClientCommandValidator>();
        }
    }
}
