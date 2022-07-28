
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaggTimeline.ClientModel.Taggs;
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
    public async Task<ActionResult<TaggModel>> GetTagg(Guid id)
    {
        var query = new GetTaggByIdQuery()
        {
            Id = id,
        };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<TaggPreviewModel>>> GetAllTaggs()
    {
        var query = new GetAllTaggsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<TaggModel>> CreateTagg([FromBody] CreateTaggCommand command)
    {
        command.UserId = HttpContext.GetUserId();
        
        var result = await _mediator.Send(command);
        return Created("GetOrder", result);
    }

    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<TaggPreviewModel>>> SearchForTagg([FromBody] SearchForTaggQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("instance")]
    public async Task<ActionResult<InstanceModel>> CreateTaggInstance([FromBody] CreateInstanceCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("{taggId:Guid?}/categorise/{categoryId:Guid?}")]
    public async Task<ActionResult<TaggModel>> AddCategory(Guid taggId, Guid categoryId)
    {
        var command = new AddCategoryToTaggCommand()
        {
            TaggId = taggId,
            CategoryId = categoryId,
        };

        var result = await _mediator.Send(command);
        return Ok(result);
    }

}
