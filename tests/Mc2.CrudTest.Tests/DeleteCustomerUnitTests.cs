using Ardalis.SharedKernel;
using FluentAssertions;
using Mc2.CrudTest.Application.Customers.Delete;
using Mc2.CrudTest.Application.Services;
using Mc2.CrudTest.Domain.CustomerAggregate;
using MediatR;
using Moq;

namespace Mc2.CrudTest.Tests
{
    public class DeleteCustomerUnitTests
    {
        private readonly Mock<IRepository<Customer>> _repositoryMock;
        private readonly DeleteCustomerCommandHandler _handler;
        private readonly CustomerService _customerService;
        private readonly Mock<IMediator> _mediaterMock;
        public DeleteCustomerUnitTests()
        {
            _repositoryMock = new Mock<IRepository<Customer>>();
            _mediaterMock = new Mock<IMediator>();
            _customerService = new CustomerService(_repositoryMock.Object, _mediaterMock.Object);
            _handler = new DeleteCustomerCommandHandler(_customerService);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenCustomerExists()
        {
            // Arrange
            var customerId = 1;
            var customer = new Customer
            {
                Id = customerId,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Email = "john.doe@example.com",
                PhoneNumber = "+11234567890",
                BankAccountNumber = "DE44500105175407324931"
            };

            // Mock GetByIdAsync to return the existing customer
            _repositoryMock.Setup(repo => repo.GetByIdAsync(customerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

            // Mock DeleteAsync to simulate successful deletion
            _repositoryMock.Setup(repo => repo.DeleteAsync(customer,It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var command = new DeleteCustomerCommand(customerId);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();

            _repositoryMock.Verify(repo => repo.GetByIdAsync(customerId, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(repo => repo.DeleteAsync(customer, It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCustomerNotFound()
        {
            var nonExistingCustomerId = 2889500;
            var command = new DeleteCustomerCommand(nonExistingCustomerId);

            // Mock GetByIdAsync to return null (customer does not exist)
            _repositoryMock.Setup(repo => repo.GetByIdAsync(nonExistingCustomerId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Customer?)null);

            var result = await _handler.Handle(command, CancellationToken.None);
            result.IsSuccess.Should().BeFalse();

            // Verify that DeleteAsync was never called since customer does not exist
            _repositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never);
        }

    }
}