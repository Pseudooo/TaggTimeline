using MediatR;
using TaggTimeline.ClientModel;

namespace TaggTimeline.Service.Queries;

public class GetAllWeatherForecastsQuery : IRequest<List<WeatherForecast>>
{
}
