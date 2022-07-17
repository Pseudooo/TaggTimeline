
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TaggController : ControllerBase
{

    private readonly IMediator _mediator;
    private readonly ILogger<TaggController> _logger;

    public TaggController(IMediator mediator, 
                          ILogger<TaggController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id:Guid?}")]
    public async Task<IActionResult> GetTagById(Guid id)
    {
        var query = new GetTaggByIdQuery()
        {
            Id = id,
        };
        var result = await _mediator.Send(query);

        if(result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewTag()
    {
        throw new NotImplementedException();
    }

}

