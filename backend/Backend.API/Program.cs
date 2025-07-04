using Backend.API.Extensions;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logger
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentName()
    .Enrich.WithProperty("Application", "Backend.API")
    .WriteTo.Console()
    // .WriteTo.GrafanaLoki(
    //     "GrafanaEndpoint",
    //     new List<LokiLabel>
    //     {
    //         new() { Key = "app", Value = "api" },
    //     },
    //     credentials: new LokiCredentials
    //     {
    //         Login = builder.Configuration.GetSection("LokiConfiguration:Username").Value!,
    //         Password = builder.Configuration.GetSection("LokiConfiguration:Password").Value!
    //     })
    .CreateLogger();

builder.Host.UseSerilog();

// Mapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Database
builder.Services.AddDbContext<ExampleDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetSection("DatabaseConnectionString").Value);
});

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetSection("DatabaseConnectionString").Value);

// Http
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

// OpenTelemetry
var openTelemetryBuilder = builder.Services.AddOpenTelemetry();

// Configure OpenTelemetry Resources with the application name
openTelemetryBuilder.ConfigureResource(resource => resource
    .AddService(builder.Environment.ApplicationName));

// Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
// openTelemetryBuilder.WithMetrics(metrics => metrics
//     .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName))
//     .AddMeter("Vitera.Api", "1.0")
//     .AddAspNetCoreInstrumentation()
//     .AddHttpClientInstrumentation()
//     .AddOtlpExporter(opts =>
//     {
//         opts.Endpoint = new Uri("OpenTelemetryCollectorEndpoint");
//         opts.Protocol = OtlpExportProtocol.HttpProtobuf;
//         opts.TimeoutMilliseconds = 1000;
//         opts.Headers = $"Authorization=Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"OpenTelCollectorUserName:OpenTelCollectorPassword"))}";
//     })
// );

// ### APP ###
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ExampleDbContext>();

    // Liste aller noch nicht angewendeten Migrations ermitteln
    var pending = db.Database.GetPendingMigrations();
    if (pending.Any())
    {
        Log.Information("Wende {Count} ausstehende Migration(en) an: {Migrations}", pending.Count(), pending);
        db.Database.Migrate();
    }
    else
    {
        Log.Information("Keine ausstehenden Migrationen.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseSerilogRequestLogging(options =>
{
    // Optional: Customize the message template
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
    
    // Enrich the diagnostic context with e.g. User, QueryString, etc.
    options.EnrichDiagnosticContext = (diagCtx, httpCtx) =>
    {
        diagCtx.Set("RequestHost", httpCtx.Request.Host.Value);
        diagCtx.Set("UserAgent", httpCtx.Request.Headers["User-Agent"].ToString());
        diagCtx.Set("ClientIP", httpCtx.Connection.RemoteIpAddress?.ToString());
    };
});

app.MapEndpoints();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()); // allow credentials
app.Run();