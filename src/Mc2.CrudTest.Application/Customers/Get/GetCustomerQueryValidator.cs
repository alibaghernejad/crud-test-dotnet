using FluentValidation;

namespace Mc2.CrudTest.Application.Customers.Get;

public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerQueryValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required.");

        RuleFor(x => x.CustomerId)
            .GreaterThanOrEqualTo(1).WithMessage("CustomerId at least greater than or equal to 1.");
    }
}