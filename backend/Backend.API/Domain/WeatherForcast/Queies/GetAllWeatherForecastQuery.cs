using Backend.Models;
using MediatR;

namespace Backend.API.Domain;

public record GetAllWeatherForecastQuery : IRequest<List<WeatherForecastEntity>>;