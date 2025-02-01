using Mc2.CrudTest.Application;
using Mc2.CrudTest.Infrastructure;

namespace Mc2.CrudTest.Web.Configurations;

public static class ServiceConfigurations
{
    public static IServiceCollection AddServiceConfigs(this IServiceCollection services, Microsoft.Extensions.Logging.ILogger logger, WebApplicationBuilder builder)
    {
        services.AddInfrastructureServices(builder.Configuration, logger);
        builder.AddApplicationServices();
        services.AddMediatrConfigs();
        
        logger.LogInformation("{Project} services registered", "Mediatr and other services.");

        return services;
    }


}