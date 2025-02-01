using Mc2.CrudTest.Domain.CustomerAggregate.Events;
using Mc2.CrudTest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Application.Handlers;

public class CustomerCreatedHandler(ILogger<CustomerCreatedHandler> logger,
    IEventStore eventStore) : INotificationHandler<CustomerCreatedEvent>
{
    public async Task Handle(CustomerCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling Customer Created event for {customerId}", domainEvent.CustomerId);
        
        // Store the event in Event Store
        await eventStore.AppendEventAsync(Guid.NewGuid(), domainEvent);
        
        // Publish event asynchronously (e.g., via RabbitMQ, Kafka)
        // (You can implement an event bus to handle this.)
    }
}