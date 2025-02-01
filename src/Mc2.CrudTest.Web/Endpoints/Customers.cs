using Mc2.CrudTest.Application.Commands.Create;
using Mc2.CrudTest.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Mc2.CrudTest.Web.Endpoints;

public class Customers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateCustomer);
    }

    public async Task<Created<int>> CreateCustomer(ISender sender, CreateCustomerCommand command)
    {
        var result = await sender.Send(command);

        return TypedResults.Created($"/{nameof(Customers)}/{result.Value}", result.Value);
    }
}