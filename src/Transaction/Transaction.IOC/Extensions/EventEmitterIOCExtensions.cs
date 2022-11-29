using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using MassTransit.MultiBus;
using Shared.Contract.Message;
using Microsoft.Extensions.Configuration;
using Transaction.Messaging;
using Transaction.Domain.Events;
using Transaction.MessageBroker;

namespace Transaction.IOC.Extensions;

public static class EventEmitterIOCExtensions
{
    public static IServiceCollection AddEmitter(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddTransient<IEventEmitter, EventEmitter>()
            .AddMassTransit(x =>
            {
                var options = new EventEmitterOptions();
                configuration.GetSection(options.Section).Bind(options);

                x.UsingInMemory();

                x.AddRider(rider =>
                {
                    rider.AddProducer<TransactionCreatedMessage>(options.TopicName);

                    rider.UsingKafka((c, k) =>
                    {
                        k.Host(options.Host);

                        k.TopicEndpoint<TransactionCreatedMessage>(options.TopicName, "fsfsd", e => e.CreateIfMissing());
                    });
                });
            });
}
