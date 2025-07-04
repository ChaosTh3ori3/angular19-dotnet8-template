using Backend.Models;
using Backend.Repository;
using MediatR;

namespace Backend.API.Domain;

public class CreateWeatherForecastCommandHandler(ILogger<CreateWeatherForecastCommandHandler> logger, ExampleDbContext dbContext) : IRequestHandler<CreateWeatherForecastCommand, WeatherForecastEntity>
{
    public async Task<WeatherForecastEntity> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new weather forecast entry.");
        
        var weatherForecast = await dbContext.WeatherForecastEntities.AddAsync(request.WeatherForecast);
        
        var savingResult = await dbContext.SaveChangesAsync(cancellationToken);
        
        if (savingResult <= 0)
        {
            logger.LogError("Failed to save the new weather forecast entry.");
            throw new Exception("Failed to create a new weather forecast entry.");
        }
        
        logger.LogInformation("Successfully created a new weather forecast entry with ID {Id}.", weatherForecast.Entity.Id);
        return weatherForecast.Entity;
    }
}