using System.Reflection;
using Serilog;
using Serilog.Exceptions;
using VolunteerPlatform.Application;
using VolunteerPlatform.Persistence;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(
        new(new Uri(configuration["ElasticConfiguration:Uri"] ?? throw new InvalidOperationException()))
        {
            IndexFormat =
                $"{Assembly.GetExecutingAssembly().GetName().Name?
                    .ToLower()
                    .Replace(".", "-")}-{builder.Environment.EnvironmentName.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
            AutoRegisterTemplate = true,
            NumberOfShards = 1,
            NumberOfReplicas = 2
        })
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddPersistense(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapGet("/get", () => { app.Logger.LogDebug("Loggind!!!!!!!!!!!!"); });

app.Run();