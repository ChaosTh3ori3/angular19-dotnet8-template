namespace Backend.Models;

public class WeatherForecastEntity : BaseEntity
{
    public DateTime DateTime { get; set; }
    public float Temperature { get; set; }
    public float Humidity { get; set; }
    public float Pressure { get; set; }
    public float WindSpeed { get; set; }
    public string? WindDirection { get; set; }
    public string Summary { get; set; } = string.Empty;
}