
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Service.Commands;
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
    public async Task<ActionResult<Tagg>> GetTagg(Guid id)
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
    public async Task<ActionResult<Tagg>> CreateTagg([FromBody] CreateTaggCommand command)
    {
        var result = await _mediator.Send(command);
        return Created("GetOrder", result);
    }

    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<Tagg>>> SearchForTagg([FromBody] SearchForTaggQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("{taggId:Guid?}/instance")]
    public async Task<ActionResult<Instance>> CreateTaggInstance(Guid taggId)
    {
        var command = new CreateInstanceCommand()
        {
            TaggId = taggId,
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }

}
