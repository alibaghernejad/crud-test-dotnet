using Ardalis.Specification;
using Mc2.CrudTest.Domain.CustomerAggregate;

namespace Mc2.CrudTest.Domain.Specifications;

public class CustomerUpdateCheckUniqueSpec : Specification<Customer>
{
    public CustomerUpdateCheckUniqueSpec(int customerId, string firstName, string lastName, DateTime dateOfBirth) =>
        Query
            .Where(customer => customer.Id != customerId && customer.FirstName == firstName &&
                               customer.LastName == lastName && customer.DateOfBirth == dateOfBirth);
}