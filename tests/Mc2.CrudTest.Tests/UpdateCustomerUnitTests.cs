using Ardalis.SharedKernel;
using AutoMapper;
using Mc2.CrudTest.Application.Customers.Get;
using Mc2.CrudTest.Application.Customers.Update;
using Mc2.CrudTest.Domain.CustomerAggregate;
using Moq;

namespace Mc2.CrudTest.Tests
{
    public class UpdateCustomerUnitTests
    {
        private readonly UpdateCustomerCommandValidator _validator;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRepository<Customer>> _repositoryMock;
        private readonly UpdateCustomerCommandHandler _handler;

        public UpdateCustomerUnitTests()
        {
            _repositoryMock = new Mock<IRepository<Customer>>();

            _validator = new UpdateCustomerCommandValidator();
            _mapperMock = new Mock<IMapper>();
            _handler = new UpdateCustomerCommandHandler(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task UpdateCustomer_ShouldUpdateSuccessfully_WhenCustomerExists()
        {
            var customerId = 1;
            var command =
                new UpdateCustomerCommand(customerId, "alinew", "Baghernejadnew", DateTime.UtcNow, "123456789");
            var existingCustomer = new Customer
                { Id = customerId, FirstName = "Old", LastName = "Name", PhoneNumber = "987654321" };
            var expectedDto = new CustomerDto
                { Id = customerId, FirstName = "alinew", LastName = "Baghernejadnew", PhoneNumber = "123456789" };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(command.CustomerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingCustomer);
            _repositoryMock.Setup(repo => repo.UpdateAsync(existingCustomer, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _mapperMock.Setup(mapper => mapper.Map<CustomerDto>(existingCustomer))
                .Returns(expectedDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedDto, result.Value);
            _repositoryMock.Verify(repo => repo.UpdateAsync(existingCustomer, It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateCustomer_ShouldReturnFalse_WhenCustomerNotFound()
        {
            var command = new UpdateCustomerCommand(
                100000,
                "Ali-new",
                "Baghernejad-new",
                DateTime.UtcNow.AddYears(-30),
                "+1234567890");

            var nonExistingCustomer = new Customer
                { Id = 1000000, FirstName = "Old", LastName = "Name", PhoneNumber = "987654321" };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(command.CustomerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(Customer));

            var result = await _handler.Handle(command, CancellationToken.None);
            Assert.True(!result.IsSuccess);
        }
    }
}