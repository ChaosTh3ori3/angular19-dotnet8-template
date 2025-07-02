using Backend.Models;

namespace Backend.Repository.Extensions.WeatherForecast;

public static class ReadAllWeatherForecastExtension
{
    public static IQueryable<WeatherForecastEntity> ReadAllWeatherForecast(
        this ExampleDbContext dbContext)
    {
        return dbContext.WeatherForecastEntities
            .OrderByDescending(x => x.CreatedAt);
    }
}