using AutoMapper;
using Backend.Contracts;
using Backend.Models;
using MediatR;

namespace Backend.API.Extensions;

public static class WebApplicationEndpointExtension
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGet("/weatherforecast", async (IMediator mediator, IMapper mapper) =>
        {
            var result = await mediator.Send(new Domain.GetAllWeatherForecastQuery());

            if (!result.Any()) 
                return Results.NoContent();

            var mappedWeatherForecasts = result.Select(mapper.Map<WeatherForecastEntity>).ToList();

            return Results.Ok(mappedWeatherForecasts);
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        app.MapPut("/weatherforecast", async (CreateWeatherForecastDto dto, IMediator mediator, IMapper mapper) =>
        {
            try
            {
                var weatherForecastEntity = mapper.Map<WeatherForecastEntity>(dto);

                var result = await mediator.Send(new Domain.CreateWeatherForecastCommand(weatherForecastEntity));

                return Results.Created($"/weatherforecast/{result.Id}", mapper.Map<WeatherForecastEntity>(result));
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
    }
}