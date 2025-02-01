using Ardalis.Result;
using Ardalis.SharedKernel;
using Mc2.CrudTest.Domain.Interfaces;

namespace Mc2.CrudTest.Application.Customers.Delete;

public record DeleteCustomerCommand(int CustomerId)
    : ICommand<Result>;


public class DeleteCustomerCommandHandler(ICustomerService customerService)
    : ICommandHandler<DeleteCustomerCommand, Result>
{
    public async Task<Result> Handle(DeleteCustomerCommand request,
        CancellationToken cancellationToken)
    {
        return await customerService.DeleteCustomer(request.CustomerId);
    }
}
