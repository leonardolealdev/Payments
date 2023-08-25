using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Payments.Consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "meu-grupo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            // Adicionar o consumidor como serviço
            services.AddSingleton<ConsumerConfig>(consumerConfig);
            services.AddHostedService<KafkaConsumerService>();

        }

    }
}
