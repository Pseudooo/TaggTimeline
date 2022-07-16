using MediatR;

namespace TaggTimeline.WebApi.Queries;

public class GetAllWeatherForecastsQuery : IRequest<List<WeatherForecast>>
{
}
