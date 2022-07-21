
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaggTime.Service.Commands;
using TaggTime.Service.Queries;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:Guid?}")]
    public async Task<ActionResult<Category>> GetCategory(Guid id)
    {
        var query = new GetCategoryByIdQuery()
        {
            Id = id,
        };
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
    {
        var query = new GetAllCategoriesQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return Created("GetTagg", result);
    }

    [HttpPost("search")]
    public async Task<ActionResult<IEnumerable<Category>>> SearchForCategory()
    {
        throw new NotImplementedException();
    }

}
