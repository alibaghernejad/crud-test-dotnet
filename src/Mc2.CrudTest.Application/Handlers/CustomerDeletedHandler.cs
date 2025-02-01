using Mc2.CrudTest.Domain.CustomerAggregate.Events;
using Mc2.CrudTest.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Application.Handlers;

public class CustomerDeletedHandler(ILogger<CustomerDeletedHandler> logger,
    IEventStore eventStore) : INotificationHandler<CustomerDeletedEvent>
{
    public async Task Handle(CustomerDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling Customers Deleted event for {customerId}", domainEvent.CustomerId);
        
        // Store the event in Event Store
        await eventStore.AppendEventAsync(Guid.NewGuid(), domainEvent);
        
        // Publish event asynchronously (e.g., via RabbitMQ, Kafka)
        // (You can implement an event bus to handle this.)
    }
}