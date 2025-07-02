using Backend.Models;
using MediatR;

namespace Backend.API.Domain;

public record CreateWeatherForecastCommand(WeatherForecastEntity WeatherForecast) : IRequest<WeatherForecastEntity>;
