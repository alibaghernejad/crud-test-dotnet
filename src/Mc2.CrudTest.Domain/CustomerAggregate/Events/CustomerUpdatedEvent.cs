using Ardalis.SharedKernel;

namespace Mc2.CrudTest.Domain.CustomerAggregate.Events;

/// <summary>
/// A domain event that is dispatched whenever a customer is updated..
/// </summary>
public sealed class CustomerUpdatedEvent(int customerId) : DomainEventBase
{
    public int CustomerId { get; init; } = customerId;
}