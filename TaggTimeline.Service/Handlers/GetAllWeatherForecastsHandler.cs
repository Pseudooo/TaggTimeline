using MediatR;
using TaggTimeline.ClientModel;

namespace TaggTimeline.Service.Queries;

public class GetAllWeatherForecastsHandler : IRequestHandler<GetAllWeatherForecastsQuery, List<WeatherForecast>>
{
    private readonly List<string> Summaries = new List<string>()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    public async Task<List<WeatherForecast>> Handle(GetAllWeatherForecastsQuery request, CancellationToken cancellationToken)
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Count())]
            })
            .ToList();
    }
}
