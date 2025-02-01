using Ardalis.Result;
namespace Mc2.CrudTest.Application.Commands.Create;
using Ardalis.SharedKernel;
using Domain.CustomerAggregate;

public record CreateCustomerCommand(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Email,
    string BankAccountNumber)
    : ICommand<Result<int>>;


public class CreateCustomerHandler(IRepository<Customer> repository)
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
        return createdItem.Id;
    }
}
