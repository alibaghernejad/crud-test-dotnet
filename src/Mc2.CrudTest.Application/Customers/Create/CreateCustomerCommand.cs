using Ardalis.Result;
using Ardalis.SharedKernel;
using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Domain.CustomerAggregate.Events;
using MediatR;

namespace Mc2.CrudTest.Application.Customers.Create;

public record CreateCustomerCommand(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Email,
    string BankAccountNumber)
    : ICommand<Result<int>>;


public class CreateCustomerHandler(IRepository<Customer> repository, IMediator mediator)
    : ICommandHandler<CreateCustomerCommand, Result<int>>
{
    public async Task<Result<int>> Handle(CreateCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        var newContributor = new Customer()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            BankAccountNumber = request.BankAccountNumber,
        };
        var createdItem = await repository.AddAsync(newContributor, cancellationToken);
        
        var domainEvent = new CustomerCreatedEvent(createdItem.Id);
        await mediator.Publish(domainEvent, cancellationToken);
        return createdItem.Id;
    }
}
