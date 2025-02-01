using FluentAssertions;
using FluentValidation.TestHelper;
using Mc2.CrudTest.Application.Customers.Get;

namespace Mc2.CrudTest.Tests;

public class GetCustomerUnitTests
{
    private readonly GetCustomerQueryValidator _validator;
    
    public GetCustomerUnitTests()
    {
        _validator = new GetCustomerQueryValidator();
    }
    [Fact]
    public void  GetCustomerById_ShouldReturnCustomer_WhenCustomerExists()
    {
        var getCustomerQuery = new GetCustomerQuery(1); // Ali Baghernejad
        var result = _validator.TestValidate(getCustomerQuery);
        result.Should().NotBeNull();

    }

    [Fact]
    public void  GetCustomerByEmail_ShouldReturnNull_WhenCustomerDoesNotExist()
    {

        var getCustomerQuery = new GetCustomerQuery(0); 
        var result = _validator.TestValidate(getCustomerQuery);
        result.ShouldHaveValidationErrorFor(c => c.CustomerId);
    }
}