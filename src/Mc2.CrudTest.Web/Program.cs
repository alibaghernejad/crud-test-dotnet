using Mc2.CrudTest.Web.Configurations;
using Mc2.CrudTest.Web.Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();
var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

logger.Information("Starting web host");
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var appLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

builder.Services.AddServiceConfigs(appLogger, builder);

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Crud Test with Clean Architecture, REPR, CQRS, and Event Sourcing, BDD and TDD",
        Version = "v1",
        Description = "yet another test project with CQRS + Event Sourcing via Postgres+Marten, Domain Events and more.",
        Contact = new OpenApiContact
        {
            Name = "Ali Baghernejad",
            Email = "alibaghernezhad@gmail.com",
            Url = new Uri("https://fsharp.org/learn.com")
        }
    });

    // Optional: Enable XML comments (if you generate them)
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "Crud Test Dotnet Clean Architecture API v1");
        c.RoutePrefix = string.Empty; // Set as root (localhost:5000)
    });
}
await app.UseAppMiddlewareAndSeedDatabase();
app.UseExceptionHandler(options => { });

app.UseHttpsRedirection();
app.MapControllers();
app.MapEndpoints();
app.Run();