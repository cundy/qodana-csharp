using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AzureFunctionCSharpCrud.Services;
using AzureFunctionCSharpCrud.Repositories;
using AzureFunctionCSharpCrud.DbContexts;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        var credential = new DefaultAzureCredential();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddDbContext<DatabaseContext>(
            options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");
                options.UseSqlServer(connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure());
            });

        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<ITodoRepository, TodoRepository>();
    })
    .Build();

host.Run();
