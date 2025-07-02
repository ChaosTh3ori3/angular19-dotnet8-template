using AutoMapper;
using Backend.Contracts;
using Backend.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logger
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
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

// Http
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ### APP ###
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", async (IMediator mediator, IMapper mapper) =>
    {
        var result = await mediator.Send(new Backend.API.Domain.GetAllWeatherForecastQuery());
        
        if (!result.Any()) 
            return Results.NoContent();
        
        var mappedWeatherForecasts = result.Select(mapper.Map<Backend.Models.WeatherForecastEntity>).ToList();
        
        return Results.Ok(mappedWeatherForecasts);
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapPut("/weatherforecast", async (CreateWeatherForecastDto dto, IMediator mediator, IMapper mapper) =>
    {
        try
        {
            var weatherForecastEntity = mapper.Map<Backend.Models.WeatherForecastEntity>(dto);
            
            var result = await mediator.Send(new Backend.API.Domain.CreateWeatherForecastCommand(weatherForecastEntity));
            
            return Results.Created($"/weatherforecast/{result.Id}", mapper.Map<Backend.Models.WeatherForecastEntity>(result));
        }
        catch (Exception e)
        {
            return Results.Problem(
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An error occurred while creating the weather forecast entry."
            );
        }
    })
    .WithName("CreateWeatherForecast")
    .WithOpenApi();

app.Run();