using System.ComponentModel;
using Ardalis.Result;
using Mc2.CrudTest.Application.Customers.Create;
using Mc2.CrudTest.Application.Customers.Delete;
using Mc2.CrudTest.Application.Customers.Get;
using Mc2.CrudTest.Application.Customers.Update;
using Mc2.CrudTest.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Mc2.CrudTest.Web.Endpoints;

public class Customers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this);
        // .MapPost(CreateCustomer)
        // .MapGet(GetCustomer, "{id}")
        // .MapPut(UpdateCustomer, "{id}")
        // .MapDelete(DeleteCustomer, "{id}");
    }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="command">The customer creation command.</param>
    /// <returns>The ID of the created customer.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [EndpointSummary("Create a new customer")] 
    [EndpointDescription("Creates a new customer in the system and returns the created customer ID.")] 
    [Description]
    public async Task<Created<int>> CreateCustomer(ISender sender, CreateCustomerCommand command)
    {
        var result = await sender.Send(command);

        return TypedResults.Created($"/{nameof(Customers)}/{result.Value}", result.Value);
    }

    /// <summary>
    /// Retrieves customer details by ID.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">The customer ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The customer details or a 404 response if not found.</returns>
    [EndpointSummary("Get customer details")] 
    [EndpointDescription("Retrieves details of a customer by their unique ID. Returns 404 if not found.")]    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetCustomerQuery), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    
    public async Task<IResult> GetCustomer(ISender sender, int id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCustomerQuery(id), cancellationToken);

        if (result.Status == ResultStatus.NotFound)
            return Results.NotFound();

        return TypedResults.Ok(result.Value);
    }
    
    /// <summary>
    /// Updates an existing customer.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">The customer ID.</param>
    /// <param name="request">The updated customer information.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated customer details or a 404 response if not found.</returns>
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdateCustomerCommand), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [EndpointSummary("Update customer details")] 
    [EndpointDescription("Updates an existing customer's information. Returns 404 if the customer does not exist.")]    
    public async Task<IResult> UpdateCustomer(ISender sender, int id, UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request with { CustomerId = id }, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
            return Results.NotFound();

        var query = await sender.Send(new GetCustomerQuery(id), cancellationToken);

        if (query.Status == ResultStatus.NotFound)
            return Results.NotFound();

        return TypedResults.Ok(query.Value);
    }

    /// <summary>
    /// Deletes a customer.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">The customer ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A 204 response if successful, or a 404 response if the customer does not exist.</returns>
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [EndpointName("DeleteCustomer")]
    [EndpointSummary("Delete a customer")] 
    [EndpointDescription("Deletes a customer by ID. Returns 204 on success or 404 if the customer is not found.")]    

    public async Task<IResult> DeleteCustomer(ISender sender, int id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteCustomerCommand(id), cancellationToken);

        if (result.Status == ResultStatus.NotFound)
            return Results.NotFound();
        return TypedResults.NoContent();
    }
}