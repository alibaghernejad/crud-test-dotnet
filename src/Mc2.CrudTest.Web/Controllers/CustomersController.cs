using Ardalis.Result;
using Mc2.CrudTest.Application.Customers.Create;
using Mc2.CrudTest.Application.Customers.Delete;
using Mc2.CrudTest.Application.Customers.Get;
using Mc2.CrudTest.Application.Customers.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Mc2.CrudTest.Web.Controllers;

/// <summary>
/// Manages customer operations including Create, Read, Update, and Delete.
/// </summary>
[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ISender _sender;

    public CustomersController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="command">The customer creation request.</param>
    /// <returns>The ID of the created customer.</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new customer",
        Description = "Creates a new customer with the provided details and returns the created ID."
    )]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        var result = await _sender.Send(command);
        return CreatedAtAction(nameof(GetCustomer), new { id = result.Value }, result.Value);
    }

    /// <summary>
    /// Retrieves customer details by ID.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The customer details or a 404 response if not found.</returns>
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get customer by ID",
        Description = "Retrieves the details of a customer based on their unique ID."
    )]
    [ProducesResponseType(typeof(GetCustomerQuery), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomer(int id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetCustomerQuery(id), cancellationToken);
        return result.Status == ResultStatus.NotFound ? NotFound() : Ok(result.Value);
    }

    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <param name="request">The updated customer information.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated customer details or a 404 response if not found.</returns>
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update an existing customer",
        Description = "Updates a customer's details using the provided ID and request data."
    )]
    [ProducesResponseType(typeof(UpdateCustomerCommand), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(request with { CustomerId = id }, cancellationToken);
        if (result.Status == ResultStatus.NotFound) return NotFound();

        var query = await _sender.Send(new GetCustomerQuery(id), cancellationToken);
        return query.Status == ResultStatus.NotFound ? NotFound() : Ok(query.Value);
    }

    /// <summary>
    /// Deletes a customer.
    /// </summary>
    /// <param name="id">The customer ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A 204 response if successful, or a 404 response if the customer does not exist.</returns>
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a customer",
        Description = "Deletes a customer by their ID. Returns 204 No Content if successful."
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer(int id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteCustomerCommand(id), cancellationToken);
        return result.Status == ResultStatus.NotFound ? NotFound() : NoContent();
    }
}
