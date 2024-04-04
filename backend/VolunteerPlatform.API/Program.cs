using System.Reflection;
using Microsoft.AspNetCore.HttpLogging;
using Minio;
using Minio.DataModel.Args;
using Serilog;
using Serilog.Exceptions;
using VolunteerPlatform.Application;
using VolunteerPlatform.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
    .WriteTo.Debug()
    .WriteTo.Console()
    .WriteTo.Elasticsearch(
        new(new Uri(configuration["ElasticConfiguration:Uri"] ?? throw new ApplicationException()))
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

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = options.LoggingFields |
                            HttpLoggingFields.RequestBody |
                            HttpLoggingFields.ResponseBody;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddPersistense(configuration);

var app = builder.Build();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.ApplyMigrations();
}

app.MapControllers();

app.MapGet("/images/presigned", async (IMinioClient minio) =>
{
    var args = new PresignedGetObjectArgs()
        .WithBucket("bucket")
        .WithObject("image.jpg")
        .WithExpiry(86400);

    var url = await minio.PresignedGetObjectAsync(args);

    return Results.Ok(url);
});

app.Run();

namespace VolunteerPlatform.API
{
    public partial class Program
    {
    }
}