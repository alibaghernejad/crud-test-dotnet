using Ardalis.Result;
using Ardalis.SharedKernel;
using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Domain.CustomerAggregate.Events;
using Mc2.CrudTest.Domain.Interfaces;
using MediatR;

namespace Mc2.CrudTest.Application.Services;

/// <summary>
/// This is here mainly so there's an example of a domain service
/// and also to demonstrate how to fire domain events from a service.
/// </summary>
/// <param name="repository"></param>
public class CustomerService(IRepository<Customer> repository, IMediator mediator) : ICustomerService
{
    public async Task<Result> DeleteCustomer(int customerId)
    {
        Customer? aggregateToDelete = await repository.GetByIdAsync(customerId);
        if (aggregateToDelete == null) return Result.NotFound();

        await repository.DeleteAsync(aggregateToDelete);
        
        var domainEvent = new CustomerDeletedEvent(customerId);
        await mediator.Publish(domainEvent);

        return Result.Success();
    }
}