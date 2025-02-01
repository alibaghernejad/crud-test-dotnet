namespace Mc2.CrudTest.Domain.Interfaces;

public interface ICustomerRepository
{
    bool EmailExists(string email);
    bool CustomerExists(string firstName, string lastName, DateTime dateOfBirth);
}
