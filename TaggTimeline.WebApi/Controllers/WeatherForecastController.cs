using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMediator _mediatr;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediatr)
    {
        _logger = logger;
        _mediatr = mediatr;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        var query = new GetAllWeatherForecastsQuery();
        var result = await _mediatr.Send(query);
        return Ok(result);
    }
}
