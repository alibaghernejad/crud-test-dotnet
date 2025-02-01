using Ardalis.Result;
using Ardalis.SharedKernel;
using AutoMapper;
using Mc2.CrudTest.Domain.CustomerAggregate;
using Mc2.CrudTest.Domain.Specifications;

namespace Mc2.CrudTest.Application.Customers.Get;

public record GetCustomerQuery(int CustomerId)
    : IQuery<Result<CustomerDto>>;


public class GetCustomerQueryHandler(IReadRepository<Customer> repository, IMapper mapper )
    : IQueryHandler<GetCustomerQuery, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(GetCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new CustomerByIdSpec(request.CustomerId);
        var entity = await repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (entity == null) return Result.NotFound();

        return mapper.Map<CustomerDto>(entity);
    }
}
