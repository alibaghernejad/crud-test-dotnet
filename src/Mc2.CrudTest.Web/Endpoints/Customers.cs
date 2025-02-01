using Ardalis.Result;
using Mc2.CrudTest.Application.Customers.Create;
using Mc2.CrudTest.Application.Customers.Get;
using Mc2.CrudTest.Application.Customers.Update;
using Mc2.CrudTest.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Mc2.CrudTest.Web.Endpoints;

public class Customers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateCustomer)
            .MapGet(GetCustomer, "{id}")
            .MapPut(UpdateCustomer, "{id}");
    }

    public async Task<Created<int>> CreateCustomer(ISender sender, CreateCustomerCommand command)
    {
        var result = await sender.Send(command);

        return TypedResults.Created($"/{nameof(Customers)}/{result.Value}", result.Value);
    }

    public async Task<IResult> GetCustomer(ISender sender, int id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCustomerQuery(id), cancellationToken);

        if (result.Status == ResultStatus.NotFound)
            return Results.NotFound();

        return TypedResults.Ok(result.Value);
    }

    public async Task<IResult> UpdateCustomer(ISender sender, int id, UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(request with{CustomerId = id}, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
            return Results.NotFound();

        var query = await sender.Send(new GetCustomerQuery(id), cancellationToken);

        if (query.Status == ResultStatus.NotFound)
            return Results.NotFound();

        return TypedResults.Ok(query.Value);
    }
}
