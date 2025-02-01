using Mc2.CrudTest.Domain.Interfaces;

namespace Mc2.CrudTest.Infrastructure.Data;

public class CustomerRepository(AppDbContext context) : ICustomerRepository
{
    public bool EmailExists(string email) => context.Customers.Any(c => c.Email == email);

    public bool CustomerExists(string firstName, string lastName, DateTime dateOfBirth)
        => context.Customers.Any(c => c.FirstName == firstName && c.LastName == lastName && c.DateOfBirth == dateOfBirth);
    
}