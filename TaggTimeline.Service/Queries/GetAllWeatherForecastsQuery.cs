using MediatR;

namespace TaggTimeline.Service.Queries;

public class GetAllWeatherForecastsQuery : IRequest<List<WeatherForecast>>
{
}
