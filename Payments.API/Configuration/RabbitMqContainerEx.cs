using MassTransit;
using Payments.Infra.Messaging;

namespace Payments.API.Configuration
{
    public static class RabbitMqContainerEx
    {
        private static RabbitMqConnectionConfiguration RabbitMqSettings { get; set; }

        public static IServiceCollection RegisterServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            RabbitMqSettings = new RabbitMqConnectionConfiguration();
            configuration.GetSection("RabbitMqOptions").Bind(RabbitMqSettings);

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<CreateClientConsumer>();
                configure.AddConsumer<CreatePaymentConsumer>();
                configure.UsingRabbitMq(ConfigureHostWithRawSerializer);
            });

            services.AddMassTransitHostedService();

            return services;
        }

        private static void ConfigureHostWithRawSerializer(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator cfg)
        {
            cfg.ClearMessageDeserializers();
            cfg.UseRawJsonSerializer();

            cfg.Host($"rabbitmq://{RabbitMqSettings.Host}", configure =>
            {
                configure.Username(RabbitMqSettings.UserName);
                configure.Password(RabbitMqSettings.Password);
            });

            cfg.ReceiveEndpoint("payments:createclient".ToLower(), endPoint =>
            {
                endPoint.ConcurrentMessageLimit = 1;
                endPoint.ConfigureConsumer<CreateClientConsumer>(context);
            });

            cfg.ReceiveEndpoint("payments:createpayment".ToLower(), endPoint =>
            {
                endPoint.ConcurrentMessageLimit = 1;
                endPoint.ConfigureConsumer<CreatePaymentConsumer>(context);
            });
        }
    }
}
