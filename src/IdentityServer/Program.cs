﻿using IdentityServer;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    const string serviceName = "IdentityServer.API";

    builder.Logging.AddOpenTelemetry(options =>
    {
        options
            .SetResourceBuilder(
                ResourceBuilder.CreateDefault()
                    .AddService(serviceName))
            .AddConsoleExporter();
    });
    builder.Services.AddOpenTelemetry()
          .ConfigureResource(resource => resource.AddService(serviceName))
          .WithTracing(tracing => tracing
              .AddAspNetCoreInstrumentation()
              .AddConsoleExporter()
              .AddJaegerExporter()
              .AddSqlClientInstrumentation())
          .WithMetrics(metrics => metrics
              .AddAspNetCoreInstrumentation()
              .AddConsoleExporter());

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();
    
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}