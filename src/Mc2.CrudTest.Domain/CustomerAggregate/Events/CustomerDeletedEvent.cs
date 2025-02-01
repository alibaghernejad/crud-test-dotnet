using Ardalis.SharedKernel;

namespace Mc2.CrudTest.Domain.CustomerAggregate.Events;

/// <summary>
/// A domain event that is dispatched whenever a cusomer is deleted.
/// The DeleteContributorService is used to dispatch this event.
/// </summary>
public sealed class CustomerDeletedEvent(int customerId) : DomainEventBase
{
    public int CustomerId { get; init; } = customerId;
}