using Mc2.CrudTest.Domain.CustomerAggregate.Events;
using Mc2.CrudTest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Application.Handlers;

public class CustomerUpdatedHandler(ILogger<CustomerUpdatedHandler> logger,
    IEventStore eventStore) : INotificationHandler<CustomerUpdatedEvent>
{
    public async Task Handle(CustomerUpdatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling Customer Updated event for {customerId}", domainEvent.CustomerId);
        
        // Store the event in Event Store
        await eventStore.AppendEventAsync(Guid.NewGuid(), domainEvent);
        
        // Publish event asynchronously (e.g., via RabbitMQ, Kafka)
        // (You can implement an event bus to handle this.)
    }
}