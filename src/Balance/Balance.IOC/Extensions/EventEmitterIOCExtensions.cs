using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using MassTransit.MultiBus;
using Shared.Contract.Message;
using Microsoft.Extensions.Configuration;
using Balance.MessageBroker.Options;
using Balance.MessageBroker;

namespace Transaction.IOC.Extensions;

public static class EventConsumerIOCExtensions
{
    public static IServiceCollection AddEventConsumer(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddMassTransit(x =>
            {
                var options = new EventConsumerOptions();
                configuration.GetSection(options.Section).Bind(options);

                x.UsingInMemory();

                x.AddRider(rider =>
                {
                    rider.AddConsumer<EventConsumer>();

                    rider.UsingKafka((registrationContext, kafkaConfigurator) =>
                    {
                        kafkaConfigurator.Host(options.Host);

                        kafkaConfigurator.TopicEndpoint<TransactionCreatedMessage>(
                            options.TopicName,
                            options.TopicName,
                            e =>
                            {
                                e.CreateIfMissing();
                                e.ConfigureConsumer<EventConsumer>(registrationContext);
                            });
                    });
                });
            });
}
