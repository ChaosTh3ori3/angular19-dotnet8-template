using Backend.Models;
using Backend.Repository;
using Backend.Repository.Extensions.WeatherForecast;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Domain;

public class GetAllWeatherForecastQueryHandler(ExampleDbContext dbContext) : IRequestHandler<GetAllWeatherForecastQuery, List<WeatherForecastEntity>>
{
    public async Task<List<WeatherForecastEntity>> Handle(GetAllWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        var weatherForecasts = dbContext.ReadAllWeatherForecast();
        
        return await weatherForecasts.ToListAsync(cancellationToken: cancellationToken);
    }
}