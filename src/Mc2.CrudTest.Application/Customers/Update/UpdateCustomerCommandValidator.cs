using FluentValidation;
using Mc2.CrudTest.Domain.Interfaces;
using PhoneNumbers;

namespace Mc2.CrudTest.Application.Customers.Update;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{

    public UpdateCustomerCommandValidator()
    {

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
}