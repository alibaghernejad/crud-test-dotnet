using Ardalis.SharedKernel;

namespace Mc2.CrudTest.Domain.CustomerAggregate.Events;

/// <summary>
/// A domain event that is dispatched whenever a customer is created.
/// </summary>
public sealed class CustomerCreatedEvent(int customerId) : DomainEventBase
{
    public int CustomerId { get; init; } = customerId;
}