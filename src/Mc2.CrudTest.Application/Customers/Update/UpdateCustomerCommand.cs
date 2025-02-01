using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using FluentValidation.Results;
using Mc2.CrudTest.Application.Customers.Get;
using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Domain.Specifications;

namespace Mc2.CrudTest.Application.Customers.Update;

public record UpdateCustomerCommand(
    int CustomerId,
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    string PhoneNumber)
    : ICommand<Result<CustomerDto>>;

public class UpdateCustomerCommandHandler(IRepository<Customer> repository, IMapper mapper)
    : ICommandHandler<UpdateCustomerCommand, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var existingCustomer = await repository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (existingCustomer == null)
        {
            return Result.NotFound();
        }

        var spec = new CustomerUpdateCheckUniqueSpec(request.CustomerId, request.FirstName, request.LastName,
            request.DateOfBirth);
        var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        var isDuplicate = entity != null;
        if (isDuplicate)
            throw new Application.Common.Exceptions.ValidationException(new List<ValidationFailure>()
            {
                new("DuplicateEntryViolation",
                    "A customer with the same identity key is already exists.")
            });
        existingCustomer.FirstName = request.FirstName;
        existingCustomer.LastName = request.LastName;
        existingCustomer.DateOfBirth = request.DateOfBirth;
        existingCustomer.PhoneNumber = request.PhoneNumber;

        await repository.UpdateAsync(existingCustomer, cancellationToken);
        return mapper.Map<CustomerDto>(existingCustomer);
    }
}