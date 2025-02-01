using Ardalis.SharedKernel;

namespace Mc2.CrudTest.Domain.CustomerAggregate;

public class Customer: EntityBase, IAggregateRoot
{
    public string FirstName { get; set; }  = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string BankAccountNumber { get; set; } = string.Empty;
}
