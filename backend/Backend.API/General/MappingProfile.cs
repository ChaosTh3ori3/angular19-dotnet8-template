using AutoMapper;
using Backend.Contracts;
using Backend.Models;

namespace Backend.API.General;

public class MappingProfile : Profile
{
 public MappingProfile()   
 {
     // Define your mappings here
     // Example:
     // CreateMap<SourceModel, DestinationModel>();
     // CreateMap<CreateSourceDto, SourceModel>();
     // CreateMap<UpdateSourceDto, SourceModel>();
     
     // WeatherForecast
     CreateMap<WeatherForecastEntity, ReadWeatherForecastDto>();
     CreateMap<CreateWeatherForecastDto, WeatherForecastEntity>();
 }
}