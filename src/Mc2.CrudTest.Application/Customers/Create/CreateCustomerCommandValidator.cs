using FluentValidation;
using Mc2.CrudTest.Domain.Interfaces;
using PhoneNumbers;

namespace Mc2.CrudTest.Application.Customers.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandValidator(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;

        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must be at most 50 characters.");

        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must be at most 50 characters.");

        RuleFor(c => c.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");

        RuleFor(c => c.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Must(BeValidMobilePhone).WithMessage("Invalid mobile phone number format. Must be in E.164 format.");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .Must(BeUniqueEmail).WithMessage("Email must be unique.")
            .MaximumLength(254);

        RuleFor(c => c.BankAccountNumber)
            .NotEmpty().WithMessage("Bank account number is required.")
            .Matches(@"^\d{10,18}$").WithMessage("Invalid bank account number format (must be 10-18 digits).");

        RuleFor(c => c)
            .Must(BeUniqueCustomer).WithMessage("Customer with the same FirstName, LastName, and DateOfBirth already exists.");
    }

    // Validate E.164 phone number format & check if it's a mobile number
    private bool BeValidMobilePhone(string phoneNumber)
    {
        try
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();
            var number = phoneUtil.Parse(phoneNumber, null);
            return phoneUtil.IsValidNumber(number);
        }
        catch
        {
            return false;
        }
    }

    // Ensure email is unique in the database
    private bool BeUniqueEmail(string email)
    {
        return !_customerRepository.EmailExists(email);
    }

    // Ensure (FirstName, LastName, DateOfBirth) is unique
    private bool BeUniqueCustomer(CreateCustomerCommand customer)
    {
        return !_customerRepository.CustomerExists(customer.FirstName, customer.LastName, customer.DateOfBirth);
    }
}