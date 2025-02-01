using Ardalis.Specification;
using Mc2.CrudTest.Domain.CustomerAggregate;

namespace Mc2.CrudTest.Domain.Specifications;

public class CustomerByIdSpec : Specification<Customer>
{
    public CustomerByIdSpec(int customerId) =>
        Query
            .Where(contributor => contributor.Id == customerId);
}