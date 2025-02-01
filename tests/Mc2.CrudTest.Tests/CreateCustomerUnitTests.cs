using FluentValidation.TestHelper;
using Mc2.CrudTest.Application.Customers.Create;
using Mc2.CrudTest.Domain.Interfaces;
using Moq;

namespace Mc2.CrudTest.Tests;

public class CreateCustomerUnitTests
{
    private readonly CreateCustomerCommandValidator _validator;
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    
    public CreateCustomerUnitTests()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();

        // Set up repository mocks to return default values
        _customerRepositoryMock.Setup(repo => repo.EmailExists(It.IsAny<string>())).Returns(false);
        _customerRepositoryMock.Setup(repo => repo.CustomerExists(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(false);

        _validator = new CreateCustomerCommandValidator(_customerRepositoryMock.Object);
    }
    
    [Fact]
    public void Should_Have_Error_When_FirstName_Is_Empty()
    {
        var createCustomerCommand = new CreateCustomerCommand( "", "Baghernejad",DateTime.UtcNow.AddYears(-20), "+1234567890", "alibaghernezhad@example.com", "DE44500105175407324931");
        var result = _validator.TestValidate(createCustomerCommand);
        result.ShouldHaveValidationErrorFor(c => c.FirstName);
    }

    [Fact]
    public void Should_Have_Error_When_PhoneNumber_Is_Invalid()
    {
        var createCustomerCommand = new CreateCustomerCommand( "Ali", "Baghernejad",DateTime.UtcNow.AddYears(-20), "invalid-number", "alibaghernezhad@example.com", "DE44500105175407324931");
        var result = _validator.TestValidate(createCustomerCommand);
        result.ShouldHaveValidationErrorFor(c => c.PhoneNumber);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var createCustomerCommand = new CreateCustomerCommand( "Ali", "Baghernejad",DateTime.UtcNow.AddYears(-20), "+1234567890", "invalid-email", "DE44500105175407324931");
        var result = _validator.TestValidate(createCustomerCommand);
        result.ShouldHaveValidationErrorFor(c => c.Email);
    }

    [Fact]
    public void Should_Have_Error_When_BankAccountNumber_Is_Invalid()
    {
        var createCustomerCommand = new CreateCustomerCommand( "Ali", "Baghernejad",DateTime.UtcNow.AddYears(-20), "+1234567890", "invalid-email", "invalid-iban");
        var result = _validator.TestValidate(createCustomerCommand);
        result.ShouldHaveValidationErrorFor(c => c.BankAccountNumber);
    }
}