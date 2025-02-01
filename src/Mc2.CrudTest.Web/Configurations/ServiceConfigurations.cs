using Marten;
using Mc2.CrudTest.Application;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrudTest.Infrastructure;
using Mc2.CrudTest.Infrastructure.EventSourcing;

namespace Mc2.CrudTest.Web.Configurations;

public static class ServiceConfigurations
{
    public static IServiceCollection AddServiceConfigs(this IServiceCollection services, Microsoft.Extensions.Logging.ILogger logger, WebApplicationBuilder builder)
    {
        services.AddInfrastructureServices(builder.Configuration, logger);
        builder.AddApplicationServices();
        // Configure Event Store (Marten + PostgreSQL)
        builder.Services.AddSingleton<IDocumentStore>(sp =>
            DocumentStore.For("Host=localhost;Database=eventstore;Username=postgres;Password=admin@123"));

        builder.Services.AddScoped<IEventStore, MartenEventStore>();
        services.AddMediatrConfigs();
        
        logger.LogInformation("{Project} services registered", "Mediatr and other services.");

        return services;
    }


}