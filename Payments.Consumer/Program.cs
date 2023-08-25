using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Payments.Consumer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
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

                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}